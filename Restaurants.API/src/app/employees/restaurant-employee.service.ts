import { Injector, Injectable } from '@angular/core';
import { Headers, Http, RequestOptions, URLSearchParams } from '@angular/http';

import { BaseService } from './../common/base.service';
import { EmployeeType, AssignedEmployeeTypes, Employees } from './../common/model';

@Injectable()
export class RestaurantEmployeeService extends BaseService<Employees, Employees, Employees, Employees, Boolean> {

    constructor(
        injector: Injector
    ) {
        const url = 'api/restaurantEmployees';
        const headers = new Headers({
            'Content-Type': 'application/json;charset=utf-8'
        });
        super(url, headers, injector);
    }

    getEnumTypeList(): Promise<EmployeeType[]> {
        let url = `${this.baseUrl}/tasks/list`;

        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as EmployeeType[]
            ).catch(this.handleError);
    }

    getPagedList(
        restaurantId: number,
        pageNumber: number,
        pageSize: number
    ): Promise<Employees[]> {
        let params = new URLSearchParams();
        params.append("restaurantId", restaurantId.toString());
        params.append("pageNumber", pageNumber.toString());
        params.append("pageSize", pageSize.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(this.baseUrl, options)
            .toPromise()
            .then(response => response.json() as Employees[]
            ).catch(this.handleError);
    }

    getEmployeeTask(
        employeeId: number
    ): Promise<AssignedEmployeeTypes[]> {
        let url = `${this.baseUrl}/tasks`;

        let params = new URLSearchParams();
        params.append("employeeId", employeeId.toString());

        let options = new RequestOptions({ search: params });

        return this.http
            .get(url, options)
            .toPromise()
            .then(response => response.json() as AssignedEmployeeTypes[]
            ).catch(this.handleError);
    }

    assignEmployeeTasks(
        item: AssignedEmployeeTypes
    ): Promise<AssignedEmployeeTypes[]> {
        let url = `${this.baseUrl}/tasks`;
        let options = new RequestOptions({ headers: this.headers });

        let json = JSON.stringify(item);
        Object.keys(item).filter(key => key[0] === "_").forEach(key => {
            json = json.replace(key, key.substring(1));
        });

        return this.http
            .post(url, json, options)
            .toPromise()
            .then(response => response.json() as AssignedEmployeeTypes[])
            .catch(this.handleError);
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
