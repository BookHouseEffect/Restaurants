import { Component, OnInit, Injector, Input,  ViewChild } from '@angular/core';

import { BaseComponent } from './../common/base.component'; 
import { Restaurant, Phone } from './../common/model';
import { PhoneService } from './phone.service';
import { ModalDialogComponent } from "../common/modal-dialog.component";
import { PhoneEditorComponent } from "./phone-editor.component";

@Component({
    selector: 'phone',
    templateUrl: './phone.component.html'
})
export class PhoneComponent extends BaseComponent implements OnInit
{
    constructor(
        private phoneService: PhoneService,
        injector: Injector
    ) { 
        super("/restaurant/phone", injector);
    }

    @Input() restaurant: Restaurant;
    phoneList: Phone[];
    hasEditing: boolean = false;

    ngOnInit(): void {
        this.getPhoneList(this.restaurant.id);
    }

    getPhoneList(restaurantId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        this.phoneService.getPagedList(restaurantId, pageNumber, pageSize)
            .then(response => {
                this.phoneList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;
    @ViewChild(PhoneEditorComponent) editor: PhoneEditorComponent;

    newClicked(value: any) {
        this.hasEditing = false;

        this.editor.isNew = true;
        this.editor.restaurant = this.restaurant;
        this.editor.currentPhone = null;
    }

    onResultReturn(returnedResult: Phone): void {
        if (this.phoneList.length < this.pageSize) {
            this.phoneList.push(returnedResult);
        } else {
            this.getPhoneList(this.restaurant.id, this.pageNumber, this.pageSize);
        }
    }

    onEdit(returnedResult: Phone) {
        var item = this.phoneList.find(x => x.id === returnedResult.id);
        Object.keys(returnedResult).forEach(key => item[key] = returnedResult[key]);
    }

    editClicked(item: Phone) {
        this.hasEditing = true;

        this.editor.isNew = false;
        this.editor.restaurant = this.restaurant;
        this.editor.currentPhone = item;

        this.modal.performClickBtn.nativeElement.click();
    }

    remove(item: Phone) {
        this.phoneService.delete(item.id, this.restaurant.id)
            .then(res => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getPhoneList(this.restaurant.id, this.pageNumber, this.pageSize);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }

    getPreviousPage() {
        this.getPhoneList(this.restaurant.id, this.pageNumber - 1, this.pageSize);
    }

    getNextPage() {
        this.getPhoneList(this.restaurant.id, this.pageNumber + 1, this.pageSize);
    }
}