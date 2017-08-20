import { Component, OnInit, Input, ElementRef, ViewChild, EventEmitter } from '@angular/core';

@Component({
    selector: 'modal-dialog',
    templateUrl: './modal-dialog.component.html'
})

export class ModalDialogComponent implements OnInit
{
    @Input() buttonTitle: string = ""
    @Input() title: string = "";
    id: string;
    notify: EventEmitter<any> = new EventEmitter<any>();

    constructor() { }
   
    ngOnInit(): void {
        this.id = new Date().getTime().toString();
    }

    @ViewChild('performClickBtn') performClickBtn: ElementRef;
    @ViewChild('closeBtn') closeBtn: ElementRef;

    buttonClickNotify(): void {
        this.notify.emit(null);
    }
}