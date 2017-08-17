import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { EmployersRestaurants, Employers } from './../common/model';

@Injectable()
export class OwnerService extends BaseService<
EmployersRestaurants, EmployersRestaurants, EmployersRestaurants, Boolean, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant/owners';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getSingle(
        id: number
    ): Promise<EmployersRestaurants> {
        throw new Error("Not supported operation!");
    }

    getList(restaurantId: number): Promise<EmployersRestaurants[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());

        let options = new RequestOptions({ search: params });

        return this.http.get(this.baseUrl, options).toPromise()
            .then(response => response.json() as EmployersRestaurants[]
            ).catch(this.handleError);
    }

    getPagedList(
        restaurantId: number,
        pageNumber: number,
        pageSize: number
    ): Promise<EmployersRestaurants[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as EmployersRestaurants[]
            ).catch(this.handleError);
    }

    delete(
        id: number,
        coownerId: number
    ): Promise<Boolean> {
        const url = `${this.baseUrl}/${id}`;

        let params = new URLSearchParams();
        params.append("coownerId", coownerId.toString());

        let options = new RequestOptions({ headers: this.headers, search: params });

        return this.http.delete(url, options)
            .toPromise()
            .then(response => response.json() as Boolean)
            .catch(this.handleError);
    }
}