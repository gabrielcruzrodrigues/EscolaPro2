import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-buttons-form',
  imports: [],
  templateUrl: './buttons-form.component.html',
  styleUrl: './buttons-form.component.sass'
})
export class ButtonsFormComponent {
  @Input() step: string = 'etapa familiar : 1';
  @Output() stepButton = new EventEmitter<string>();

  emitBack(): void {
    this.stepButton.emit('back');
  }

  emitNext(): void {
    this.stepButton.emit('next');
  }
}
