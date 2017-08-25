import { Component, Input, Output, OnInit, EventEmitter, PipeTransform, Pipe } from '@angular/core';
import { Restaurant, Worktime, DayOfWeek } from "../common/model";
import { ErrorComponent } from "../common/error.component";
import { ScheduleService } from "./schedule.service";

@Component({
    selector: 'schedule-editor',
    templateUrl: './schedule-editor.component.html',
})

export class ScheduleEditorComponent extends ErrorComponent implements OnInit
{
    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Input() currentSchedule: Worktime;
    @Output() notify: EventEmitter<Worktime> = new EventEmitter<Worktime>();

    constructor(
        private service: ScheduleService
    ) {
        super();
    }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.currentSchedule = new Worktime();
        }
    }

    save(): void {
        if (this.isNew) {
            this.currentSchedule.restaurantId = this.restaurant.id;
            this.service.create(this.currentSchedule)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentSchedule[key] = result[key]);
                    this.notify.emit(this.currentSchedule);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.service.update(this.currentSchedule.id, this.currentSchedule)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    Object.keys(result).forEach(key => this.currentSchedule[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                    this.getOriginal();
                });
        }
    }

    getOriginal() {
        this.service.getSingle(this.currentSchedule.id)
            .then(result => {
                Object.keys(result).forEach(key => this.currentSchedule[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}

@Pipe({ name: 'iteratorNumber' })
export class IteratorNumberPipe implements PipeTransform {
    transform(value, args: string[]): any {
        let res = [];
        for (let i = 0; i < value; i++) {
            res.push(i);
        }
        return res;
    }
}

@Pipe({ name: 'getDayString' })
export class DayStringPipe implements PipeTransform {
    transform(value, args: string[]): any {
        return DayOfWeek[value].toString();
    }
}