import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';

import { BaseService } from './../base.service'
import { Restaurant, EmployersRestaurants, Tupple } from './restaurant';

@Injectable()
export class RestaurantService extends BaseService<
  Restaurant, Tupple<Restaurant, EmployersRestaurants>, Restaurant, Restaurant, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant';
        const headers = new Headers({
            'Content-Type': 'application/json'
        });
        super(url, headers, injector);
    }

    getList(ownerId: number): Promise<Restaurant[]> {
        let url = `${this.baseUrl}?ownerId=${ownerId}`;
        return this.http.get(url).toPromise()
            .then(response => response.json() as Restaurant[]
            ).catch(this.handleError);
    }

    getPagedList(
        ownerId: number,
        pageNumber: number,
        pageSize: number
    ): Promise<Restaurant[]> {
        let url = `${this.baseUrl}?ownerId=${ownerId}&pageNumber=${pageNumber}&pageSize=${pageSize}`;

        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Restaurant[]
            ).catch(this.handleError);
    }
}