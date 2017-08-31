import { Component, OnInit, Injector, Input,  ViewChild } from '@angular/core';

import { PageComponent } from './../common/page.component'; 
import { Restaurant, MenuLanguages, MenuCategories } from './../common/model';
import { MenuCategoryService } from './menu-category.service';
import { MenuLanguageService } from './../menu-languages/menu-language.service';
import { ModalDialogComponent } from "../common/modal-dialog.component";

@Component({
    selector: 'menu-category',
    templateUrl: './menu-category.component.html'
})
export class  MenuCategoryComponent extends PageComponent implements OnInit
{
    constructor(
        private categoryService: MenuCategoryService,
        private languageService: MenuLanguageService,
        injector: Injector
    ) { 
        super("", injector);
    }

    @Input() restaurant: Restaurant;
    hasEditing: boolean = false;

    categoryList: MenuCategories[];
    editedCategory: MenuCategories;

    ngOnInit(): void {
        this.getCategoryList(this.restaurant.id);
    }

    getCategoryList(restaurantId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        this.categoryService.getPagedList(restaurantId, pageNumber, pageSize)
            .then(response => {
                this.categoryList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;

    newClicked(value: any) {
        this.hasEditing = false;
        this.editedCategory = new MenuCategories();
    }

    remove(returnedResult: MenuCategories): void {
        this.getCategoryList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    onEdit(returnedResult: MenuCategories) {
        this.getCategoryList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    editClicked(item: MenuCategories) {
        this.modal.performClickBtn.nativeElement.click();

        this.hasEditing = true;
        this.editedCategory = item;
    }

    removeClicked(item: MenuCategories) {
        this.categoryService.delete(item.id, this.restaurant.id)
            .then(res => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getCategoryList(this.restaurant.id, this.pageNumber, this.pageSize);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }

    getPreviousPage() {
        this.getCategoryList(this.restaurant.id, this.pageNumber - 1, this.pageSize);
    }

    getNextPage() {
        this.getCategoryList(this.restaurant.id, this.pageNumber + 1, this.pageSize);
    }
}