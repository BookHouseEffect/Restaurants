import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Restaurant, MenuCurrencies, Currencies } from './../common/model';
import { MenuCurrencyService } from './menu-currency.service';
import { ErrorComponent } from "../common/error.component";

@Component({
    selector: 'menu-currency-editor',
    templateUrl: './menu-currency-editor.component.html'
})
export class MenuCurrencyEditorComponent extends ErrorComponent implements OnInit {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Input() currentCurrency: MenuCurrencies;
    currencyList: Currencies[];
    @Output() notify: EventEmitter<MenuCurrencies> = new EventEmitter<MenuCurrencies>();

    constructor(
        private service: MenuCurrencyService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        this.service.getEnumCurrencyList()
            .then(response => this.currencyList = response)
            .catch(error => {
                this.clearSuccess();
                this.error = error;
            })

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.currentCurrency = {} as MenuCurrencies;
        }
    }

    save(): void {
        this.currentCurrency.restaurantId = this.restaurant.id;
        if (this.isNew) {
            this.service.create(this.currentCurrency)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentCurrency[key] = result[key]);
                    this.notify.emit(this.currentCurrency);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.service.update(this.currentCurrency.id, this.currentCurrency)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentCurrency[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                    this.getOriginal();
                });
        }
    }

    getOriginal() {
        this.service.getSingle(this.currentCurrency.id)
            .then(result => {
                Object.keys(result).forEach(key => this.currentCurrency[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}