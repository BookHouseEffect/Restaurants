import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { Restaurant } from './restaurant';
import { RestaurantService } from "./restaurant.service";


@Component({
    selector: 'restaurant-form',
    templateUrl: './restaurant-editor.component.html'
})

export class RestaurantEditorComponent implements OnInit {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Output() notify: EventEmitter<Restaurant>
    = new EventEmitter<Restaurant>();
    success: string;
    error: string;

    ngOnInit(): void {
        this.clearError();
        this.clearSuccesss();

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.restaurant = {} as Restaurant;
        }
    }

    constructor(
        private service: RestaurantService,
        private router: Router
    ) { }

    save(): void {
        if (this.isNew) {
            this.service.create(this.restaurant)
                .then(result => {
                    this.clearError();
                    this.success = "Changes have been saved!";
                    this.isNew = false;
                    Object.keys(result.item1).forEach(key => this.restaurant[key] = result.item1[key]);
                    this.notify.emit(this.restaurant);
                })
                .catch(err => {
                    this.clearSuccesss();
                    this.error = err;
                });
        } else {
            this.service.update(this.restaurant.id, this.restaurant)
                .then(result => {
                    this.clearError();
                    this.success = "Changes have been saved!";
                    Object.keys(result).forEach(key => this.restaurant[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccesss();
                    this.error = err;
                    this.getOriginal();
                });
        }
    }

    getOriginal() {
        this.service.getSingle(this.restaurant.id)
            .then(result => {
                Object.keys(result).forEach(key => this.restaurant[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccesss();
                this.error = err
            });
    }

    clearSuccesss() {
        this.success = "";
    }

    clearError() {
        this.error = "";
    }
}