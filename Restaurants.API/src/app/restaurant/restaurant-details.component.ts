import { OnInit, Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import { Restaurant } from './restaurant';
import { RestaurantService } from "./restaurant.service";

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'restaurant-details',
    templateUrl: './restaurant-details.component.html'
})

export class RestaurantDetailComponent implements OnInit {

    constructor(
        private restaurantService: RestaurantService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    currentRestaurant: Restaurant;
    success: string;
    error: string;

    clearSuccesss() {
        this.success = "";
    }

    clearError() {
        this.error = "";
    }

    ngOnInit(): void {
        this.route.paramMap.switchMap((params: ParamMap) =>
            this.restaurantService.getSingle(+params.get('id')))
            .subscribe(restaurant => this.currentRestaurant = restaurant);
    }

    setFragment(value: any) {
        console.log(value);
    }

    closeRestaurant(): void {
        this.restaurantService.delete(this.currentRestaurant.id)
            .then(value => {
                  this.clearError();
                  this.success = "Restaurant successfully removed!";
                  this.router.navigate(['/restaurants']);
            })
            .catch(err => {
                this.clearSuccesss();
                this.error = err;
            });
    }
}