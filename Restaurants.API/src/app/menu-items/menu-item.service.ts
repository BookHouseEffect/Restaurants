import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { MenuItems, Tuple } from './../common/model';

@Injectable()
export class MenuItemService
    extends BaseService<MenuItems, MenuItems, MenuItems, MenuItems, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant/menuItems';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getPagedList(
        restaurantIdAndMenuCategoryId: Tuple<number,number>,
        pageNumber: number,
        pageSize: number
    ): Promise<MenuItems[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantIdAndMenuCategoryId.item1.toString());
        params.append("menuCategoryId", restaurantIdAndMenuCategoryId.item2.toString());
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as MenuItems[]
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
