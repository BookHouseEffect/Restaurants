import { Component, OnInit, Input, ViewChild, ElementRef, NgZone } from '@angular/core';
import { FormControl } from "@angular/forms";
import { MapsAPILoader } from "@agm/core";
import {  } from '@types/googlemaps';

import { Restaurant, Address, Coordinates } from './../common/model';
import { AddressService } from './address.service';

@Component({
    selector: 'address',
    templateUrl: './address.component.html',
    styleUrls: ['./address.component.css']
})
export class AddressComponent implements OnInit
{
    @Input() restaurant: Restaurant;
    restaurantAddress: Address;
    error: string[];
    success: string[];

    searchControl: FormControl;
    zoom: number;

    @ViewChild("search")
    public searchElementRef: ElementRef;

    constructor(
        private addressService: AddressService,
        private mapsAPILoader: MapsAPILoader,
        private ngZone: NgZone
    ) { }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();
        this.initAndClearAddress();

        this.getRestaurantAddress();

        this.zoom = 4;

        this.searchControl = new FormControl();

        this.mapsAPILoader.load().then(() => {
            let autocomplete = new google.maps.places.Autocomplete(
                this.searchElementRef.nativeElement, { types: ["address"] });
            autocomplete.addListener("place_changed", () => {
                this.ngZone.run(() => {
                    let place: google.maps.places.PlaceResult = autocomplete.getPlace();

                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }

                    this.setData(place);
                })
            })
        })
    }

    initAndClearAddress(clear: Boolean = false) {
        if (clear) {
            let obj: Address = {} as Address;
            Object.keys(obj).forEach(key => this.restaurantAddress[key] = obj[key]);

            let coor: Coordinates = {} as Coordinates;
            Object.keys(coor).forEach(key => this.restaurantAddress.theLocationPoint[key] = coor[key]);
        }

        if (!this.restaurantAddress)
            this.restaurantAddress = {} as Address;

        if (!this.restaurantAddress.theLocationPoint)
            this.restaurantAddress.theLocationPoint = {} as Coordinates;
    }

    setData(place: google.maps.places.PlaceResult) {
        this.initAndClearAddress();
        this.setAddressComponents(place.address_components);
        this.setPlaceGeometrComponents(place.geometry);

        this.restaurantAddress.googleLink = place.url;

        this.zoom = 16;
    }

    setAddressComponents(value: google.maps.GeocoderAddressComponent[]) {
        this.restaurantAddress.floor = null;
        this.restaurantAddress.streetNumber = "";

        for (var comp of value) {
            for (var types of comp.types) {
                switch (types) {
                    case 'route': { this.restaurantAddress.route = comp.long_name || comp.short_name || ''; break; }
                    case 'locality': { this.restaurantAddress.locality = comp.long_name; break; }
                    case 'administrative_area_level_1': { this.restaurantAddress.administrativeAreaLevel1 = comp.long_name || comp.short_name || ''; break; }
                    case 'administrative_area_level_2': { this.restaurantAddress.administrativeAreaLevel2 = comp.long_name || comp.short_name || ''; break; }
                    case 'country': { this.restaurantAddress.country = comp.long_name || comp.short_name || ''; break; }
                    case 'postal_code': { this.restaurantAddress.zipCode = +comp.long_name || +comp.short_name || 0; break; }
                }
            }
        }
    }

    setPlaceGeometrComponents(value: google.maps.places.PlaceGeometry) {
        this.initAndClearAddress();

        this.restaurantAddress.theLocationPoint.latitude = value.location.lat();
        this.restaurantAddress.theLocationPoint.longitude = value.location.lng();
    }

    getRestaurantAddress() {
        this.addressService.getByRestaurantId(this.restaurant.id)
            .then(response => {
                this.clearError();
                if (response)
                    Object.keys(response).forEach(key => this.restaurantAddress[key] = response[key]);
                else
                    this.initAndClearAddress(true);
            }).catch(error => {
                this.clearSuccess();
                this.error = error;
            })
    }

    clearError() {
        this.error = [];
    }

    clearSuccess() {
        this.success = [];
    }

    placeMarker(value: any) {
        let geocoder = new google.maps.Geocoder();
        let latlng = new google.maps.LatLng(value.coords.lat, value.coords.lng);
        let request = { latLng: latlng };   

        var test: google.maps.GeocoderResult;
        geocoder.geocode(request, (results, status) => {      

            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0] != null) {
                    this.setAddressComponents(results[0].address_components);
                    this.restaurantAddress.theLocationPoint.latitude = value.coords.lat;
                    this.restaurantAddress.theLocationPoint.longitude = value.coords.lng;
                    this.zoom = 16;
                    this.restaurantAddress.googleLink =
                        'https://www.google.com/maps/preview/@'
                          + value.coords.lat.toString() + ',' + value.coords.lng.toString() + ',16z';
                } else {
                    this.error = ['No address available on the chosen location'];
                }
            }
        });
    }

    save() {
        if (!this.restaurantAddress.id || this.restaurantAddress.id <= 0) {
            this.restaurantAddress.restaurantId = this.restaurant.id;
            this.addressService.create(this.restaurantAddress)
                .then(response => {
                    Object.keys(response).forEach(key =>
                        this.restaurantAddress[key] = response[key]);
                    this.clearError();
                    this.success = ['Changes saved successfully'];
                }).catch(error => {
                    this.clearSuccess();
                    this.error = error as string[];
                })
        } else {
            this.addressService.update(this.restaurantAddress.id, this.restaurantAddress)
                .then(response => {
                    Object.keys(response).forEach(key =>
                        this.restaurantAddress[key] = response[key]);
                    this.clearError();
                    this.success = ['Changes saved successfully'];
                }).catch(error => {
                    this.clearSuccess();
                    this.error = error as string[];
                });
        }
    }

    remove() {
        this.addressService.delete(this.restaurantAddress.id, this.restaurant.id)
            .then(response => {
                this.clearError();
                this.success = ['Address removed successfully successfully'];
                this.getRestaurantAddress();
            }).catch(error => {
                this.clearSuccess();
                this.error = error as string[];
            })
    }
}