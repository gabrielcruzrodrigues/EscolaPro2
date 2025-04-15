import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal-confirm',
  imports: [],
  templateUrl: './modal-confirm.component.html',
  styleUrl: './modal-confirm.component.sass'
})
export class ModalConfirmComponent {
  @Input() message: string = 'Tem certeza de que deseja desativar?';
  @Output() confirm = new EventEmitter<boolean>();

  closeModal(result: boolean): void {
    this.confirm.emit(result);
  }
}
