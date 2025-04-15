import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-spinning',
  imports: [
    CommonModule
  ],
  templateUrl: './spinning.component.html',
  styleUrl: './spinning.component.sass'
})
export class SpinningComponent {
  @Input() isLoading: boolean = false;
}
