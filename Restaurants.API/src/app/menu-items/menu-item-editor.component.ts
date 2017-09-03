import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

import {
    Restaurant, Tuple, MenuItems, MenuCategories, MenuItemContents,
    MenuLanguages, MenuItemValues, MenuCurrencies
} from './../common/model';
import { MenuItemService } from './menu-item.service';
import { MenuCategoryService } from './../menu-categories/menu-category.service';
import { MenuLanguageService } from './../menu-languages/menu-language.service';
import { MenuCurrencyService } from './../menu-currencies/menu-currency.service';
import { ErrorComponent } from "../common/error.component";

@Component({
    selector: 'menu-item-editor',
    templateUrl: './menu-item-editor.component.html',
    styleUrls: ['./menu-item-editor.component.css']
})
export class MenuItemEditorComponent extends ErrorComponent implements OnInit, OnChanges {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Input() currentItem: MenuItems;
    @Output() notify: EventEmitter<MenuItems> = new EventEmitter<MenuItems>();

    categoryList: MenuCategories[];
    languageList: MenuLanguages[];
    currencyList: MenuCurrencies[];

    constructor(
        private menuItemService: MenuItemService,
        private categoryService: MenuCategoryService,
        private languageService: MenuLanguageService,
        private currencyService: MenuCurrencyService
    ) {
        super();
    }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        this.getCategoryList(this.restaurant.id);
        this.getLanguageList(this.restaurant.id);
        this.getCurrencyList(this.restaurant.id);

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.currentItem = new MenuItems();
        }
    }

    getCategoryList(restaurantId: number) {
        this.categoryService.getPagedList(restaurantId, 1, (Math.pow(2, 31) - 1))
            .then(response => {
                this.categoryList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    getLanguageList(restaurantId: number) {
        this.languageService.getPagedList(restaurantId, 1, (Math.pow(2, 31) - 1))
            .then(response => {
                this.languageList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    getCurrencyList(restaurantId: number) {
        this.currencyService.getPagedList(restaurantId, 1, (Math.pow(2, 31) - 1))
            .then(response => {
                this.currencyList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    ngOnChanges(changes: SimpleChanges): void {

        if (!this.currentItem)
            this.currentItem = new MenuItems();

        if (!this.currentItem.theContent)
            this.currentItem.theContent = new Array<MenuItemContents>(0);

        if (this.languageList) {
            for (var content of this.languageList) {
                var cat = this.currentItem.theContent.find(x => x.menuLanguageId == content.id);

                if (!cat) {
                    cat = new MenuItemContents();
                    cat.menuLanguageId = content.id;
                    cat.theMenuLanguage = content;
                    this.currentItem.theContent.push(cat);
                }

            }
        }

        if (!this.currentItem.theValue)
            this.currentItem.theValue = new Array<MenuItemValues>(0);

        if (this.currencyList) {
            for (var value of this.currencyList) {
                var currency = this.currentItem.theValue.find(x => x.menuCurrencyId == value.id);

                if (!currency) {
                    currency = new MenuItemValues();
                    currency.menuCurrencyId = value.id;
                    currency.theMenuCurrency = value;
                    this.currentItem.theValue.push(currency);
                }

            }
        }

    }

    save(): void {
        this.currentItem.restaurantId = this.restaurant.id;
        this.prepareTupples();

        if (this.isNew) {
            this.menuItemService.create(this.currentItem)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentItem[key] = result[key]);
                    this.notify.emit(this.currentItem);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.menuItemService.update(this.currentItem.id, this.currentItem)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentItem[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                    this.getOriginal();
                });
        }
    }

    prepareTupples(): void {
        
        this.currentItem.itemName = new Array<Tuple<number, string>>(0);
        this.currentItem.itemDescription = new Array<Tuple<number, string>>(0);
        this.currentItem.itemWarnings = new Array<Tuple<number, string>>(0);

        for (var content of this.currentItem.theContent) {
            var name: Tuple<number, string> = new Tuple<number, string>();
            name.item1 = content.menuLanguageId;
            name.item2 = content.itemName;
            this.currentItem.itemName.push(name);

            var description: Tuple<number, string> = new Tuple<number, string>();
            description.item1 = content.menuLanguageId;
            description.item2 = content.itemDescription;
            this.currentItem.itemDescription.push(description);

            var warning: Tuple<number, string> = new Tuple<number, string>();
            warning.item1 = content.menuLanguageId;
            warning.item2 = content.itemWarnings;
            this.currentItem.itemWarnings.push(warning);
        }

        this.currentItem.itemPrice = new Array<Tuple<number, number>>(0);
        for (var value of this.currentItem.theValue) {
            var price: Tuple<number, number> = new Tuple<number, number>();
            price.item1 = value.menuCurrencyId;
            price.item2 = value.price;
            this.currentItem.itemPrice.push(price);
        }
    };

    getOriginal() {
        this.menuItemService.getSingle(this.currentItem.id)
            .then(result => {
                Object.keys(result).forEach(key => this.currentItem[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}
