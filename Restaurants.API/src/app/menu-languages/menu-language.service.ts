import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { MenuLanguages, Languages } from './../common/model';

@Injectable()
export class MenuLanguageService extends BaseService<MenuLanguages, MenuLanguages, MenuLanguages, MenuLanguages, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant/menuLanguages';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getEnumLanguageList() : Promise<Languages[]> {
        let url = `${this.baseUrl}/list`;

        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Languages[]
            ).catch(this.handleError);
    }

    getPagedList(
        restaurantId: number,
        pageNumber: number,
        pageSize: number
    ): Promise<MenuLanguages[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as MenuLanguages[]
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