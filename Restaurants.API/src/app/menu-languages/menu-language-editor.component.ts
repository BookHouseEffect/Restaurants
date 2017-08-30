import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Restaurant, MenuLanguages, Languages } from './../common/model';
import { MenuLanguageService } from './menu-language.service';
import { ErrorComponent } from "../common/error.component";

@Component({
    selector: 'menu-language-editor',
    templateUrl: './menu-language-editor.component.html'
})
export class MenuLanguageEditorComponent extends ErrorComponent implements OnInit {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Input() currentLanguage: MenuLanguages;
    languageList: Languages[];
    @Output() notify: EventEmitter<MenuLanguages> = new EventEmitter<MenuLanguages>();

    constructor(
        private service: MenuLanguageService,
    ) {
        super();
    }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        this.service.getEnumLanguageList()
            .then(response => this.languageList = response)
            .catch(error => {
                this.clearSuccess();
                this.error = error;
            })

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.currentLanguage = {} as MenuLanguages;
        }
    }

    save(): void {
        this.currentLanguage.restaurantId = this.restaurant.id;
        if (this.isNew) {
            this.service.create(this.currentLanguage)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentLanguage[key] = result[key]);
                    this.notify.emit(this.currentLanguage);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.service.update(this.currentLanguage.id, this.currentLanguage)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    Object.keys(result).forEach(key => this.currentLanguage[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                    this.getOriginal();
                });
        }
    }

    getOriginal() {
        this.service.getSingle(this.currentLanguage.id)
            .then(result => {
                Object.keys(result).forEach(key => this.currentLanguage[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}