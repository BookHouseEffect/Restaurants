import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { ModifyInfoPopoverComponent } from './common/modify-info-popover.component';
import { AlertDispayComponent } from './common/alert-display.component'

import { RestaurantComponent } from './restaurant/restaurant.component';
import { RestaurantEditorComponent } from './restaurant/restaurant-editor.component';
import { RestaurantDetailComponent } from './restaurant/restaurant-details.component';
import { RestaurantService } from './restaurant/restaurant.service';

import { SearchService } from './common/search.service';

import { OwnerComponent } from './owner/owner.component';
import { OwnerSearchComponent } from './owner/owner-search.component';
import { OwnerService } from './owner/owner.service';

import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpModule } from '@angular/http';
import { PopoverModule } from "ngx-popover";

@NgModule({
    declarations: [
        AppComponent, 
        ModifyInfoPopoverComponent, 
        AlertDispayComponent,

        RestaurantComponent,
        RestaurantEditorComponent,
        RestaurantDetailComponent,

        OwnerComponent,
        OwnerSearchComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
        PopoverModule
    ],
    providers: [
        SearchService, 
        RestaurantService,
        OwnerService
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }