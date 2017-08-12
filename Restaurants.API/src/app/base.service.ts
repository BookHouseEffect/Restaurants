import { Injector } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

export abstract class BaseService<TEntity, TCreateReturn, TReadReturn, TUpdateReturn, TDeleteReturn> {

    protected baseUrl: string = null;
    protected headers: Headers = null;
    protected http: Http;

    constructor(
        url: string,
        headers: Headers,
        injector: Injector
    ) { 
        this.baseUrl = url;
        this.headers = headers;
        this.http = injector.get(Http);
    }

    getSingle(
        id: number
    ): Promise<TReadReturn> {
        const url = `${this.baseUrl}/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as TReadReturn)
            .catch(this.handleError);
    }

    getList(args: any): Promise<TReadReturn[]> {
        return this.http.get(this.baseUrl)
            .toPromise()
            .then(response => response.json() as TReadReturn[]
            ).catch(this.handleError);
    }

    getPagedList(
        args: any,
        pageNumber: number,
        pageSize: number
    ): Promise<TReadReturn[]> {
        let url = `${this.baseUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`;
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as TReadReturn[]
            ).catch(this.handleError);
    }

    create(
        item: TEntity
    ): Promise<TCreateReturn> {
        return this.http
            .post(this.baseUrl,
            JSON.stringify(item),
            { headers: this.headers }
            ).toPromise()
            .then(response => response.json() as TCreateReturn)
            .catch(this.handleError);
    }

    update(
        id: number,
        item: TEntity
    ): Promise<TUpdateReturn> {
        const url = `${this.baseUrl}/${id}`;
        return this.http
            .put(url,
            JSON.stringify(item),
            { headers: this.headers })
            .toPromise()
            .then(response => response.json() as TUpdateReturn)
            .catch(this.handleError);
    }

    delete(
        id: number
    ): Promise<TDeleteReturn> {
        const url = `${this.baseUrl}/${id}`;
        return this.http.delete(url,
            { headers: this.headers })
            .toPromise()
            .then(response => response.json() as TDeleteReturn)
            .catch(this.handleError);
    }

    protected handleError(error: any): Promise<any> {
        // TODO: for demo purposes only
        console.error('An error occurred', error);

        return Promise.reject(error.message || error);
    }
}