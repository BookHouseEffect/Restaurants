import { Component, OnInit, Injector, Input,  ViewChild } from '@angular/core';

import { PageComponent } from './../common/page.component'; 
import { Restaurant, MenuLanguages } from './../common/model';
import { MenuLanguageService } from './menu-language.service';
import { ModalDialogComponent } from "../common/modal-dialog.component";

@Component({
    selector: 'menu-language',
    templateUrl: './menu-language.component.html'
})
export class  MenuLanguageComponent extends PageComponent implements OnInit
{
    constructor(
        private languageService: MenuLanguageService,
        injector: Injector
    ) { 
        super("", injector);
    }

    @Input() restaurant: Restaurant;
    languageList: MenuLanguages[];
    hasEditing: boolean = false;
    editedlanguage: MenuLanguages;

    ngOnInit(): void {
        this.getLanguageList(this.restaurant.id);
    }

    getLanguageList(restaurantId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        this.languageService.getPagedList(restaurantId, pageNumber, pageSize)
            .then(response => {
                this.languageList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;

    newClicked(value: any) {
        this.hasEditing = false;
        this.editedlanguage = new MenuLanguages();
    }

    onResultReturn(returnedResult: MenuLanguages): void {
        this.getLanguageList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    onEdit(returnedResult: MenuLanguages) {
        var item = this.languageList.find(x => x.id === returnedResult.id);
        Object.keys(returnedResult).forEach(key => item[key] = returnedResult[key]);
    }

    editClicked(item: MenuLanguages) {
        this.modal.performClickBtn.nativeElement.click();

        this.hasEditing = true;
        this.editedlanguage = item
    }

    remove(item: MenuLanguages) {
        this.languageService.delete(item.id, this.restaurant.id)
            .then(res => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getLanguageList(this.restaurant.id, this.pageNumber, this.pageSize);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }

    getPreviousPage() {
        this.getLanguageList(this.restaurant.id, this.pageNumber - 1, this.pageSize);
    }

    getNextPage() {
        this.getLanguageList(this.restaurant.id, this.pageNumber + 1, this.pageSize);
    }
}