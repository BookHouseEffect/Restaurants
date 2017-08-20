import { Component, OnInit, Input, Output, EventEmitter, Injector } from '@angular/core';

import { Restaurant, Phone } from './../common/model';
import { PhoneService } from './phone.service';
import { BaseComponent } from "../common/base.component";

@Component({
    selector: 'phone-editor',
    templateUrl: './phone-editor.component.html'
})
export class PhoneEditorComponent extends BaseComponent implements OnInit {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Input() currentPhone: Phone;
    @Output() notify: EventEmitter<Phone> = new EventEmitter<Phone>();

    constructor(
        private service: PhoneService,
        injector: Injector
    ) {
        super("", injector);
    }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.currentPhone = {} as Phone;
        }
    }

    save(): void {
        if (this.isNew) {
            this.currentPhone.restaurantId = this.restaurant.id;
            this.service.create(this.currentPhone)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentPhone[key] = result[key]);
                    this.notify.emit(this.currentPhone);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.service.update(this.currentPhone.id, this.currentPhone)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    Object.keys(result).forEach(key => this.currentPhone[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                    this.getOriginal();
                });
        }
    }

    getOriginal() {
        this.service.getSingle(this.currentPhone.id)
            .then(result => {
                Object.keys(result).forEach(key => this.currentPhone[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}