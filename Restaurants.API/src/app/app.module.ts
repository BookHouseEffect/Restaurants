import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';

import { AppComponent } from './app.component';

import { ModifyInfoPopoverComponent } from './common/modify-info-popover.component';
import { AlertDispayComponent } from './common/alert-display.component';
import { TableViewComponent } from './common/table-view.component';
import { ModalDialogComponent } from './common/modal-dialog.component';

import { RestaurantComponent } from './restaurant/restaurant.component';
import { RestaurantEditorComponent } from './restaurant/restaurant-editor.component';
import { RestaurantDetailComponent } from './restaurant/restaurant-details.component';
import { RestaurantService } from './restaurant/restaurant.service';

import { OwnerComponent } from './owner/owner.component';
import { OwnerSearchComponent } from './owner/owner-search.component';
import { OwnerService } from './owner/owner.service';

import { PhoneComponent } from './phone/phone.component';
import { PhoneEditorComponent } from './phone/phone-editor.component'
import { PhoneService } from './phone/phone.service';

import { AddressComponent } from './address/address.component';
import { AddressService } from './address/address.service';

import { SearchService } from './common/search.service';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpModule } from '@angular/http';
import { PopoverModule } from "ngx-popover";

@NgModule({
    declarations: [
        AppComponent, 
        ModifyInfoPopoverComponent, 
        AlertDispayComponent,
        TableViewComponent,
        ModalDialogComponent,

        RestaurantComponent,
        RestaurantEditorComponent,
        RestaurantDetailComponent,

        OwnerComponent,
        OwnerSearchComponent,

        PhoneComponent,
        PhoneEditorComponent,

        AddressComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
        PopoverModule,
        AgmCoreModule.forRoot({
            libraries: ['places'],
            apiKey: 'AIzaSyAuIyUStWIS6KC7kHQiLCuTxARS1SKYlpY'
        }),
        ReactiveFormsModule
    ],
    providers: [
        SearchService, 
        RestaurantService,
        OwnerService,
        PhoneService,
        AddressService
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }