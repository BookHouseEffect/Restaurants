import { Component, OnInit, Injector, Input,  ViewChild } from '@angular/core';

import { PageComponent } from './../common/page.component'; 
import { Restaurant, MenuCurrencies } from './../common/model';
import { MenuCurrencyService } from './menu-currency.service';
import { ModalDialogComponent } from "../common/modal-dialog.component";

@Component({
    selector: 'menu-currency',
    templateUrl: './menu-currency.component.html'
})
export class  MenuCurrencyComponent extends PageComponent implements OnInit
{
    constructor(
        private currencyService: MenuCurrencyService,
        injector: Injector
    ) { 
        super("", injector);
    }

    @Input() restaurant: Restaurant;
    currencyList: MenuCurrencies[];
    hasEditing: boolean = false;
    editedCurrency: MenuCurrencies;

    ngOnInit(): void {
        this.getCurrencyList(this.restaurant.id);
    }

    getCurrencyList(restaurantId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        this.currencyService.getPagedList(restaurantId, pageNumber, pageSize)
            .then(response => {
                this.currencyList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;

    newClicked(value: any) {
        this.hasEditing = false;
        this.editedCurrency = new MenuCurrencies();
    }

    onResultReturn(returnedResult: MenuCurrencies): void {
        this.getCurrencyList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    onEdit(returnedResult: MenuCurrencies) {
        this.getCurrencyList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    editClicked(item: MenuCurrencies) {
        this.modal.performClickBtn.nativeElement.click();

        this.hasEditing = true;
        this.editedCurrency = item;
    }

    remove(item: MenuCurrencies) {
        this.currencyService.delete(item.id, this.restaurant.id)
            .then(res => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getCurrencyList(this.restaurant.id, this.pageNumber, this.pageSize);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }

    getPreviousPage() {
        this.getCurrencyList(this.restaurant.id, this.pageNumber - 1, this.pageSize);
    }

    getNextPage() {
        this.getCurrencyList(this.restaurant.id, this.pageNumber + 1, this.pageSize);
    }
}