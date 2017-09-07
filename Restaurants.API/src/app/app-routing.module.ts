import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RestaurantComponent } from './restaurant/restaurant.component';
import { RestaurantDetailComponent } from './restaurant/restaurant-details.component';
import { RestaurantEditorComponent } from './restaurant/restaurant-editor.component';
import { OwnerComponent } from './owner/owner.component';
import { PhoneComponent } from './phone/phone.component';
import { AddressComponent } from './address/address.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { MenuLanguageComponent } from './menu-languages/menu-language.component';
import { MenuCurrencyComponent } from './menu-currencies/menu-currency.component';
import { MenuCategoryComponent } from './menu-categories/menu-category.component';
import { MenuItemComponent } from './menu-items/menu-item.component';

import { RestaurantEmployeeComponent } from './employees/restaurant-employee.component';

const routes: Routes = [
    { path: '', redirectTo: '/restaurants', pathMatch: 'full' },
    { path: 'restaurants', component: RestaurantComponent },
    {
        path: 'restaurants/:id', component: RestaurantDetailComponent,
        children: [
            { path: 'about', component: RestaurantEditorComponent },
            { path: 'owners', component: OwnerComponent },
            { path: 'phone', component: PhoneComponent },
            { path: 'location', component: AddressComponent },
            { path: 'schedule', component: ScheduleComponent},
            { path: 'language', component: MenuLanguageComponent},
            { path: 'currency', component: MenuCurrencyComponent},
            { path: 'category', component: MenuCategoryComponent},
            { path: 'content', component: MenuItemComponent},
            { path: 'transfer', component: null},
            { path: 'closerestaurant', component: null}
        ]
    },
    { path: 'employees', component: RestaurantEmployeeComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
