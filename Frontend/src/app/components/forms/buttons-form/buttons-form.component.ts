import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-buttons-form',
  imports: [],
  templateUrl: './buttons-form.component.html',
  styleUrl: './buttons-form.component.sass'
})
export class ButtonsFormComponent {
  @Input() step: string = 'etapa familiar : 1'

  emitBack(): void {

  }

  emitNext(): void {

  }
}
