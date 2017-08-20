import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Restaurant } from './../common/model';
import { RestaurantService } from "./restaurant.service";

import 'rxjs/add/operator/switchMap';
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'restaurant-details',
    templateUrl: './restaurant-details.component.html'
})

export class RestaurantDetailComponent implements OnInit, OnDestroy {

    constructor(
        private restaurantService: RestaurantService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    private sub: Subscription;

    currentRestaurant: Restaurant;
    navigation: string[];
    currentUrl: string;
    currentFragment: string;
    success: string[] = [];
    error: string[] = [];

    ngOnInit(): void {

        this.navigation = ['about', 'owners', 'phone', 'location', 'schedule',
            'language', 'currency', 'category', 'content', 'transfer', 'closerestaurant'];

        this.currentFragment = "";
        this.currentUrl = this.router.url.split('#')[0];

        this.route.paramMap.switchMap((params: ParamMap) =>
            this.restaurantService.getSingle(+params.get('id')))
            .subscribe(restaurant => this.currentRestaurant = restaurant);

        this.sub = this.route.fragment.subscribe((fragment: string) => {
            if (this.navigation.includes(fragment)) {
                this.currentFragment = fragment;
            } else {
               this.fragmentNavigate(this.navigation[0]);
            }
            
        });
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }

    clearSuccess() {
        this.success = [];
    }

    clearError() {
        this.error = [];
    }

    fragmentNavigate(fragment: string) {
        this.router.navigate([this.currentUrl], { fragment: fragment });
    }

    closeRestaurant(): void {
        this.restaurantService.delete(this.currentRestaurant.id, null)
            .then(value => {
                this.clearError();
                this.success = ["Restaurant successfully removed!"];
                this.router.navigate(['/restaurants']);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }

    onResultReturn(x: any) {
        this.router.navigate(['/restaurants']);
    }
}