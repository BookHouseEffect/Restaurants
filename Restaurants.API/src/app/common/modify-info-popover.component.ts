import { Component, Input } from '@angular/core';
import { BaseModel } from "./model";

@Component({
    selector: 'modify-info-popover',
    templateUrl: './modify-info-popover.component.html'
})

export class ModifyInfoPopoverComponent {
    @Input() entity: BaseModel;
}