import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'alert-dispay',
    templateUrl: './alert-display.component.html'
})

export class AlertDispayComponent {
    @Input() messages: Array<string> = new Array<string>(0);
    @Input() isError: Boolean = false;
    @Output() notify: EventEmitter<void> = new EventEmitter<void>();

    clearMessages() {
        this.notify.emit();
    }
}