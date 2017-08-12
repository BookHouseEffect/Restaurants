import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Restaurant } from './restaurant';
import { RestaurantService } from "./restaurant.service";
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'restaurant',
    templateUrl: './restaurant.component.html'
})

export class RestaurantComponent implements OnInit, OnDestroy {
        
    constructor(
        private restaurantService: RestaurantService,
        private route: ActivatedRoute,
        private router: Router,
    ) { }

    private sub: Subscription;

    error: string = "";
    restaurants: Restaurant[];
    pageNumber: number;
    pageSize: number;
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

    getPreviousPage() {
        this.router.navigate(['/restaurants'], { queryParams: { page: this.pageNumber - 1 } });
    }

    getNextPage() {
        this.router.navigate(['/restaurants'], { queryParams: { page: this.pageNumber + 1 } });
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

    @ViewChild('closeBtn') closeBtn: ElementRef;

    onResultReturn(returnedRestaurnat : Restaurant): void {
        this.closeBtn.nativeElement.click();
        this.onClick(returnedRestaurnat);
    }

    clearError() { this.error = "" }

}