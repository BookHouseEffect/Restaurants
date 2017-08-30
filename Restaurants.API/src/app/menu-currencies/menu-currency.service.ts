import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { MenuCurrencies, Currencies } from './../common/model';

@Injectable()
export class MenuCurrencyService extends BaseService<MenuCurrencies, MenuCurrencies, MenuCurrencies, MenuCurrencies, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant/menuCurrencies';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getEnumCurrencyList(): Promise<Currencies[]> {
        let url = `${this.baseUrl}/list`;

        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Currencies[]
            ).catch(this.handleError);
    }

    getPagedList(
        restaurantId: number,
        pageNumber: number,
        pageSize: number
    ): Promise<MenuCurrencies[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as MenuCurrencies[]
            ).catch(this.handleError);
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