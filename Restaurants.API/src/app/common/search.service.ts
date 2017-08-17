import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import 'rxjs/add/operator/toPromise';
import { Employers } from './model';

@Injectable()
export class SearchService {

    private headers: Headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

    constructor(
        private http: Http
    ) { }

    getOwnersNotAssosiatedWithGivenRestaurant(
        searchForRestaurantId: number,
        searchTerm: string = "",
        pageNumber: number = 1,
        pageSize: number = 10
    ): Promise<Employers[]> {
        let url: string = 'api/search/owner';

        let params = new URLSearchParams();
        params.append("searchForRestaurantId", searchForRestaurantId.toString());
        params.append("searchTerm", searchTerm);
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ headers: this.headers, search: params });

        return this.http
            .get(url, options)
            .toPromise()
            .then(res => res.json() as Employers[])
            .catch(this.handleError);
    }

    protected handleError(error: Response): Promise<any> {
        return Promise.reject(error.json() || error);
    }
}