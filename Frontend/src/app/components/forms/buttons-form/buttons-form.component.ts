import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-buttons-form',
  imports: [CommonModule],
  templateUrl: './buttons-form.component.html',
  styleUrl: './buttons-form.component.sass'
})
export class ButtonsFormComponent implements OnChanges {
  @Input() step: string = 'etapa familiar : 1';
  @Output() stepButton = new EventEmitter<string>();
  @Input() lastStep: boolean = false;
  @Input() firstStep: boolean = true;

  constructor(private cdr: ChangeDetectorRef) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['lastEtep']) {
      this.cdr.detectChanges();
    }
  }

  emitBack(): void {
    this.stepButton.emit('back');
  }

  emitNext(): void {
    this.stepButton.emit('next');
  }

  finish(): void {
    this.stepButton.emit('finish');
  }
}
