import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Restaurant } from "./model";

@Component({
    selector: 'table-view',
    templateUrl: './table-view.component.html'
})

export class TableViewComponent implements OnInit {

    @Input() columnNames: Array<string> = new Array<string>(0);
    @Input() properties: Array<string> = new Array<string>(0);
    @Input() hasInfo: Boolean = true;
    @Input() hasEdit: Boolean = false;
    @Input() hasRemove: Boolean = false;
    @Input() hasPaging: Boolean = true;
    @Input() data: Array<any> = new Array<any>(0);

    @Input() pageNumber: number = 1;
    @Input() pageSize: number = 10;

    @Output() editClick: EventEmitter<any> = new EventEmitter<any>();
    @Output() removeClick: EventEmitter<any> = new EventEmitter<any>();
    @Output() previousClick: EventEmitter<void> = new EventEmitter<void>();
    @Output() nextClick: EventEmitter<void> = new EventEmitter<void>();

    constructor() { }

    ngOnInit(): void {
    }

    getValueByProperty(item: any, propertyName: string): string {
        var splitedName = propertyName.split('.');
        var result = Object.assign({}, item);
        for (let name of splitedName) {
            result = result[name];
        }
        return result
    }

    notifyEdit(item: any) {
        this.editClick.emit(item);
    }

    notifyForRemove(item: any) {
        this.removeClick.emit(item);
    }

    notifyForPrevious() {
        this.previousClick.emit();
    }

    notifyForNext() {
        this.nextClick.emit()
    }
}