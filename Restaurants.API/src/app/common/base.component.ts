import { Router } from "@angular/router";
import { Injector } from "@angular/core";

export abstract class BaseComponent {

    protected router: Router;

    constructor(
        protected currentUrl: string,
        injector: Injector
    ) {
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

    success: Array<string> = new Array<string>(0);
    error: Array<string> = new Array<string>(0);

    clearSuccess() {
        while (this.success.length > 0)
            this.success.pop();
    }
    clearError() {
        while (this.error.length > 0)
            this.error.pop();
    }
}