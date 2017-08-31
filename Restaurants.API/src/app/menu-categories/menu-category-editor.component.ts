import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

import { Restaurant, MenuCategories, Categories, MenuLanguages, Tuple } from './../common/model';
import { MenuCategoryService } from './menu-category.service';
import { MenuLanguageService } from './../menu-languages/menu-language.service';
import { ErrorComponent } from "../common/error.component";

@Component({
    selector: 'menu-category-editor',
    templateUrl: './menu-category-editor.component.html',
    styleUrls: ['./menu-category-editor.component.css']
})
export class MenuCategoryEditorComponent extends ErrorComponent implements OnInit, OnChanges {

    @Input() isNew: boolean;
    @Input() restaurant: Restaurant;
    @Input() currentCategory: MenuCategories;
    @Output() notify: EventEmitter<MenuCategories> = new EventEmitter<MenuCategories>();
    menuLanguageList: MenuLanguages[];

    constructor(
        private service: MenuCategoryService,
        private languageService: MenuLanguageService
    ) {
        super();
    }

    ngOnInit(): void {
        this.clearError();
        this.clearSuccess();

        this.languageService.getPagedList(this.restaurant.id, 1, (Math.pow(2, 31) - 1))
            .then(result => {
                this.menuLanguageList = result;

                if (!this.currentCategory.theCategories)
                    this.currentCategory.theCategories = new Array<Categories>(0);

                for (var item of this.menuLanguageList) {
                    var cat = new Categories();
                    cat.menuLanguageId = item.id;
                    cat.theMenuLanguage = item;
                    this.currentCategory.theCategories.push(cat);
                }
            })
            .catch(error => { this.clearSuccess(); this.error = error as string[]; });

        if (this.isNew === undefined || this.isNew === true) {
            this.isNew = true;
            this.currentCategory = {} as MenuCategories;
        }
    }

    ngOnChanges(changes: SimpleChanges): void {

        if (!this.currentCategory)
            this.currentCategory = new MenuCategories();

        if (!this.currentCategory.theCategories)
            this.currentCategory.theCategories = new Array<Categories>(0);

        if (this.menuLanguageList) {
            for (var item of this.menuLanguageList) {
                var cat = this.currentCategory.theCategories.find(x => x.menuLanguageId == item.id);

                if (!cat) {
                    cat = new Categories();
                    cat.menuLanguageId = item.id;
                    cat.theMenuLanguage = item;
                    this.currentCategory.theCategories.push(cat);
                }

            }
        }

    }

    save(): void {
        this.currentCategory.restaurantId = this.restaurant.id;
        this.prepareTupples();

        if (this.isNew) {
            this.service.create(this.currentCategory)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentCategory[key] = result[key]);
                    this.notify.emit(this.currentCategory);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                });
        } else {
            this.service.update(this.currentCategory.id, this.currentCategory)
                .then(result => {
                    this.clearError();
                    this.success = ["Changes have been saved!"];
                    this.isNew = false;
                    Object.keys(result).forEach(key => this.currentCategory[key] = result[key]);
                })
                .catch(err => {
                    this.clearSuccess();
                    this.error = err as string[];
                    this.getOriginal();
                });
        }
    }

    prepareTupples(): void {
        
        this.currentCategory.categoryName = new Array<Tuple<number, string>>(0);
        this.currentCategory.categoryDescription = new Array<Tuple<number, string>>(0);

        for (var item of this.currentCategory.theCategories) {
            var name: Tuple<number, string> = new Tuple<number, string>();
            name.item1 = item.menuLanguageId;
            name.item2 = item.categoryName;
            this.currentCategory.categoryName.push(name);

            var description: Tuple<number, string> = new Tuple<number, string>();
            description.item1 = item.menuLanguageId;
            description.item2 = item.categoryDescription;
            this.currentCategory.categoryDescription.push(description);
        }
    };

    getOriginal() {
        this.service.getSingle(this.currentCategory.id)
            .then(result => {
                Object.keys(result).forEach(key => this.currentCategory[key] = result[key]);
            })
            .catch(err => {
                this.clearSuccess();
                this.error = err as string[];
            });
    }
}