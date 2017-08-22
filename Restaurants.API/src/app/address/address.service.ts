import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { Address } from './../common/model';

@Injectable()
export class AddressService extends BaseService<Address, Address, Address, Address, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant/location';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getByRestaurantId(
        restaurantId: number
    ): Promise<Address> {
        const url = `${this.baseUrl}`;

        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());

        let options = new RequestOptions({ headers: this.headers, search: params });

        return this.http.get(url, options)
            .toPromise()
            .then(response => response.json() as Address)
            .catch(this.handleError);
    }

    getList(args: any): Promise<any[]> {
        throw new Error("Invalid operation.")
    }

    getPagedList(
        args: any,
        pageNumber: number,
        pageSize: number
    ): Promise<any[]> {
        throw new Error("Invalid operation.")
    }

    delete(
        id: number,
        restaurantId: number
    ): Promise<Boolean> {
        const url = `${this.baseUrl}/${id}`;

        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());

        let options = new RequestOptions({ headers: this.headers, search: params });

        return this.http.delete(url, options)
            .toPromise()
            .then(response => response.json() as Boolean)
            .catch(this.handleError);
    }
}