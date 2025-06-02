import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-view-document-button',
  imports: [CommonModule],
  templateUrl: './view-document-button.component.html',
  styleUrl: './view-document-button.component.sass'
})
export class ViewDocumentButtonComponent {
  @Input() filePath: string = '';

  openFile(): void {
    console.log(this.filePath)
    if (this.filePath != '') {
      let link = document.createElement('a');
      link.href = this.filePath;
      link.download = '';
      link.target = '_blank';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  }
}
