// shared/confirm-modal.component.ts
import { Component, EventEmitter, Input, Output, ViewChild, ElementRef  } from '@angular/core';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
})
export class ConfirmModalComponent {
  @Input() title?: string;
  @Input() message?: string;
  @Output() onConfirm = new EventEmitter<void>();

  @ViewChild('confirmModal') modalRef!: ElementRef;
  private modalInstance!: Modal;

  ngAfterViewInit(): void {
    // Initialize modal ONCE after the view is ready
    this.modalInstance = new Modal(this.modalRef.nativeElement, {
      backdrop: 'static', // optional: backdrop won't close modal
      keyboard: false,    // optional: Esc won't close modal
    });
  }

  show(): void {
    // this.modalInstance = new Modal(this.modalRef.nativeElement);
    this.modalInstance.show();    
  }

  confirm(): void {
    this.onConfirm.emit();    
    this.modalInstance.hide();
  }
}
