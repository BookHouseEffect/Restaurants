import { Component, OnInit, Input, Output, EventEmitter, Injector } from '@angular/core';
import { Router } from '@angular/router';

import { Restaurant } from './../common/model';
import { RestaurantService } from "./restaurant.service";
import { BaseComponent } from "../common/base.component";


@Component({
    selector: 'restaurant-form',
    templateUrl: './restaurant-editor.component.html'
})

export class RestaurantEditorComponent extends BaseComponent implements OnInit {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Output() notify: EventEmitter<Restaurant> = new EventEmitter<Restaurant>();

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.restaurant = {} as Restaurant;
        }
    }

    constructor(
        private service: RestaurantService,
        injector: Injector
    ) { 
        super("", injector);
    }

    save(): void {
        if (this.isNew) {
            this.service.create(this.restaurant)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result.item1).forEach(key => this.restaurant[key] = result.item1[key]);
                    this.notify.emit(this.restaurant);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.service.update(this.restaurant.id, this.restaurant)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    Object.keys(result).forEach(key => this.restaurant[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
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
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}