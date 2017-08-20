import { Component, OnInit, OnDestroy, ViewChild, Injector } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Restaurant } from './../common/model';
import { RestaurantService } from "./restaurant.service";
import { Subscription } from "rxjs/Subscription";

import { BaseComponent } from './../common/base.component';
import { ModalDialogComponent } from "../common/modal-dialog.component";

@Component({
    selector: 'restaurant',
    templateUrl: './restaurant.component.html'
})

export class RestaurantComponent extends BaseComponent implements OnInit, OnDestroy {
        
    constructor(
        private restaurantService: RestaurantService,
        private route: ActivatedRoute,
        injector: Injector
    ) {
        super('/restaurants', injector);
    }

    private sub: Subscription;
    restaurants: Restaurant[];
    userMock: number;

    ngOnInit(): void {
        this.userMock = 11;
        this.sub = this.route
            .queryParams
            .subscribe(params => {
                this.pageNumber = +params['page'] || 1;
                this.pageSize = +params['size'] || 10;
                this.getRestaurantList(this.userMock, this.pageNumber, this.pageSize);
            })

        this.restaurants = new Array<Restaurant>(0);
        this.userMock = 11;
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }

    onClick(restaurant: Restaurant) {
        this.router.navigate(['/restaurants', restaurant.id]);
    }

    getRestaurantList(ownerId: number, pageNumber: number = 1, pageSize: number = 10) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;

        this.restaurantService.getPagedList(ownerId, pageNumber, pageSize)
            .then(restaurants => this.restaurants = restaurants)
            .catch(err => this.error = err);
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;

    onResultReturn(returnedRestaurnat: Restaurant): void {
        this.modal.closeBtn.nativeElement.click();
        this.onClick(returnedRestaurnat);
    }
}