import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ErrorComponent } from "../common/error.component";
import { ScheduleService } from "./schedule.service";
import { Restaurant, Worktime } from "../common/model";
import { ModalDialogComponent } from "../common/modal-dialog.component";
import { ScheduleEditorComponent } from "./schedule-editor.component";

@Component({
    selector: 'schedule',
    templateUrl: './schedule.component.html'
})

export class ScheduleComponent extends ErrorComponent implements OnInit
{
    @Input() restaurant: Restaurant;
    scheduleList: Worktime[];
    hasEditing: Boolean = false;
    editingItem: Worktime = new Worktime();

    constructor(
        private scheduleService: ScheduleService
    ) {
        super();
    }

    ngOnInit(): void {
        this.scheduleList = [] as Worktime[];
        this.getSchedules();
    }

    getSchedules() {
        this.scheduleService.getList(this.restaurant.id)
            .then(response => {
                this.clearError();
                this.scheduleList = [] as Worktime[];
                for (var res of response) {
                    var item: Worktime = new Worktime();
                    Object.keys(res).forEach(key => item[key] = res[key]);
                    this.scheduleList.push(item);
                }
            }).catch(error => {
                this.clearSuccess();
                this.error = error as string[];
            });
    }

    @ViewChild(ModalDialogComponent) modal: ModalDialogComponent;
    @ViewChild(ScheduleEditorComponent) editor: ScheduleEditorComponent;

    newClicked() {
        this.hasEditing = false;
        this.editingItem = new Worktime();
    }

    editClicked(value: Worktime) {
        this.modal.performClickBtn.nativeElement.click();

        this.hasEditing = true;
        this.editingItem = value;

    }

    onResultReturn(value: Worktime) {
        this.getSchedules();
    }

    onEdit(value: Worktime) {
        this.getSchedules();
    }

    remove(value: Worktime) {
        this.scheduleService.delete(value.id, this.restaurant.id)
            .then(response => {
                this.success = ["Successfully removed."];
                this.clearError();
                this.getSchedules();
            }).catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            })
    }
}