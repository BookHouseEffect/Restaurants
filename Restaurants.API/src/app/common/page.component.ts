import { Router } from "@angular/router";
import { Injector } from "@angular/core";

import { ErrorComponent } from "./error.component";

export abstract class PageComponent extends ErrorComponent {

    protected router: Router;

    constructor(
        protected currentUrl: string,
        injector: Injector
    ) {
        super();
        this.router = injector.get(Router);
    }

    pageNumber: number;
    pageSize: number;

    getPreviousPage() {
        this.router.navigate([this.currentUrl], {
            queryParams: {
                page: this.pageNumber - 1,
                size: this.pageSize
            }
        });
    }

    getNextPage() {
        this.router.navigate([this.currentUrl], {
            queryParams: {
                page: this.pageNumber + 1,
                size: this.pageSize
            }
        });
    }
}