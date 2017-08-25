import { Component, OnInit, Input, Output, EventEmitter, Injector } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

// Observable class extensions
import 'rxjs/add/observable/of';

// Observable operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { SearchService } from './../common/search.service';
import { OwnerService } from './owner.service';
import { Restaurant, Employers, EmployersRestaurants } from "./../common/model";
import { PageComponent } from './../common/page.component';

@Component({
    selector: 'owner-search',
    templateUrl: './owner-search.component.html',
    styleUrls: ['./owner-search.component.css']
})

export class OwnerSearchComponent extends PageComponent implements OnInit {

    @Input() restaurant: Restaurant;
    @Input() isTransfer: Boolean = false;
    employers: Observable<Employers[]>;
    private searchTerms = new Subject<string>();
    @Output() notify: EventEmitter<EmployersRestaurants> = new EventEmitter<EmployersRestaurants>();

    constructor(
        private searchService: SearchService,
        private ownerService: OwnerService,
        injector: Injector
    ) { super("api/search", injector); }

    search(term: string): void {
        this.searchTerms.next(term);
    }

    ngOnInit(): void {
        this.initSearch();
    }

    initSearch() {
        this.employers = this.searchTerms
            .debounceTime(200)        // wait 300ms after each keystroke before considering the term
            .distinctUntilChanged()   // ignore if next search term is same as previous
            .switchMap(term => term   // switch to new observable each time the term changes
                // return the http search observable
                ? this.searchService.getOwnersNotAssosiatedWithGivenRestaurant(
                    this.restaurant.id, term, this.pageNumber, this.pageSize)
                // or the observable of empty heroes if there was no search term
                : Observable.of<Employers[]>([]))
            .catch(error => {
                // TODO: add real error handling
                return Observable.of<Employers[]>([]);
            });
    }

    onAddClick(empl: Employers) {
        var emplRest: EmployersRestaurants =
            { employerId: empl.id, restaurantId: this.restaurant.id } as EmployersRestaurants;

        this.ownerService.create(emplRest)
            .then(res => {
                this.clearError();
                this.success = ["Successfully added!"];

                this.initSearch();

                this.notify.emit(res as EmployersRestaurants);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }

    onTransferClick(empl: Employers) {
        var emplRest: EmployersRestaurants =
            { employerId: empl.id } as EmployersRestaurants;

        this.ownerService.update(this.restaurant.id, emplRest)
            .then(res => {
                this.notify.emit({} as EmployersRestaurants);
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }

}