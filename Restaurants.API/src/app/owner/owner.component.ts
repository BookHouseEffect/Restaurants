import { Component, OnInit, Injector, Input } from '@angular/core';

import { BaseComponent } from './../common/base.component'; 
import { OwnerService } from './owner.service';
import { Restaurant, Employers, EmployersRestaurants } from "../common/model";

@Component({
    selector: 'owner',
    templateUrl: './owner.component.html'
})

export class OwnerComponent extends BaseComponent implements OnInit {

    constructor(
        private ownerService: OwnerService,
        injector: Injector
    ) { 
        super("/restaurant/owners", injector);
    }

    @Input() restaurant: Restaurant;
    employers: EmployersRestaurants[];

    ngOnInit(): void {
        this.getOwnersList(this.restaurant.id);
    }

    getOwnersList(restaurantId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        this.ownerService.getPagedList(restaurantId, pageNumber, pageSize)
            .then(owners => {
                this.employers = owners
            })
            .catch(err => this.error = err as string[]);
    }

    getPreviousPage() {
        this.getOwnersList(this.restaurant.id, this.pageNumber - 1, this.pageSize);
    }

    getNextPage() {
        this.getOwnersList(this.restaurant.id, this.pageNumber + 1, this.pageSize);
    }

    remove(empl: Employers) {
        this.ownerService.delete(this.restaurant.id, empl.id)
            .then(res => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getOwnersList(this.restaurant.id, this.pageNumber, this.pageSize);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }

    onResultReturn(returnedResult: EmployersRestaurants): void {
        if (this.employers.length < this.pageSize) {
            this.employers.push(returnedResult);
        } else {
            this.getOwnersList(this.restaurant.id, this.pageNumber, this.pageSize);
        }
    }
}