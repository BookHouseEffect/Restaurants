import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { Worktime } from './../common/model';

@Injectable()
export class ScheduleService extends BaseService<Worktime, Worktime, Worktime, Worktime, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurant/schedule';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getList(
        restaurantId: number
    ): Promise<Worktime[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as Worktime[]
            ).catch(this.handleError);
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