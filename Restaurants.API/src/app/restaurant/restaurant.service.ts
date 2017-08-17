import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { Restaurant, EmployersRestaurants, Tupple } from './../common/model';

@Injectable()
export class RestaurantService extends BaseService<
  Restaurant, Tupple<Restaurant, EmployersRestaurants>, Restaurant, Restaurant, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant';
        const headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        super(url, headers, injector);
    }

    getList(ownerId: number): Promise<Restaurant[]> {
        let params = new URLSearchParams();
        params.append("ownerId", ownerId.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as Restaurant[])
            .catch(this.handleError);
    }

    getPagedList(
        ownerId: number,
        pageNumber: number,
        pageSize: number
    ): Promise<Restaurant[]> {
        let params = new URLSearchParams();
        params.append("ownerId", ownerId.toString());
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as Restaurant[]
            ).catch(this.handleError);
    }
}