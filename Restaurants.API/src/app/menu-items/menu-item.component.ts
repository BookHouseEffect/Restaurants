import { Component, OnInit, Injector, Input,  ViewChild } from '@angular/core';

import { PageComponent } from './../common/page.component'; 
import { Restaurant, Tuple, MenuItems, MenuCategories } from './../common/model';
import { MenuItemService } from './menu-item.service';
import { MenuCategoryService } from './../menu-categories/menu-category.service';
import { ModalDialogComponent } from "../common/modal-dialog.component";

@Component({
    selector: 'menu-item',
    templateUrl: './menu-item.component.html'
})
export class  MenuItemComponent extends PageComponent implements OnInit
{
    constructor(
        private menuItemService: MenuItemService,
        private categoryService: MenuCategoryService,
        injector: Injector
    ) { 
        super("", injector);
    }

    @Input() restaurant: Restaurant;
    hasEditing: boolean = false;

    itemList: MenuItems[];
    categoryList: MenuCategories[];

    selectedCategoryId: number = 0;
    editedItem: MenuItems;

    ngOnInit(): void {
        this.getCategoryList(this.restaurant.id);
    }

    getItemsOnCategoryChange() {
        this.getItemList(this.restaurant.id, 1, 10);
    }

    getItemList(restaurantId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        var tuple = new Tuple<number, number>();
        tuple.item1 = restaurantId;
        tuple.item2 = this.selectedCategoryId;

        this.menuItemService.getPagedList(tuple, pageNumber, pageSize)
            .then(response => {
                this.itemList = response;
            })
            .catch(err => this.error = err as string[]);
    }

    getCategoryList(restaurantId: number) {
        this.categoryService.getPagedList(restaurantId, 1, (Math.pow(2, 31) - 1))
            .then(response => {
                this.categoryList = response;
                if (this.categoryList && this.categoryList.length != 0) {
                    this.selectedCategoryId = this.categoryList[0].id;
                    this.getItemList(this.restaurant.id);
                }
            })
            .catch(err => this.error = err as string[]);
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;

    newClicked(value: any) {
        this.hasEditing = false;
        this.editedItem = new MenuItems();
    }

    remove(returnedResult: MenuItems): void {
        this.getItemList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    onEdit(returnedResult: MenuItems) {
        this.getItemList(this.restaurant.id, this.pageNumber, this.pageSize);
    }

    editClicked(item: MenuItems) {
        this.modal.performClickBtn.nativeElement.click();

        this.hasEditing = true;
        this.editedItem = item;
    }

    removeClicked(item: MenuItems) {
        this.menuItemService.delete(item.id, this.restaurant.id)
            .then(res => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getItemList(this.restaurant.id, this.pageNumber, this.pageSize);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }

    getPreviousPage() {
        this.getItemList(this.restaurant.id, this.pageNumber - 1, this.pageSize);
    }

    getNextPage() {
        this.getItemList(this.restaurant.id, this.pageNumber + 1, this.pageSize);
    }
}
