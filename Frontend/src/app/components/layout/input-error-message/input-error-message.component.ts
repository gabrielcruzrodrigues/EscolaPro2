import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-input-error-message',
  imports: [CommonModule],
  templateUrl: './input-error-message.component.html',
  styleUrl: './input-error-message.component.sass'
})
export class InputErrorMessageComponent {
  @Input() errors: string[] = [];
}
