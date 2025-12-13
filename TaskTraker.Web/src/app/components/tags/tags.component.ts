import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tags',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tags.component.html',
  styleUrl: './tags.component.scss',
})
export class TagsComponent {
  @Input() type: 'status' | 'priority' = 'status';
  @Input() value: number | string = '';

  // Map the numeric enums to readable strings
  statusMap: Record<number, string> = {
    0: 'New',
    1: 'InProgress',
    2: 'Done',
  };

  priorityMap: Record<number, string> = {
    0: 'Low',
    1: 'Medium',
    2: 'High',
  };

  get displayValue(): string {
    if (this.type === 'status' && typeof this.value === 'number') {
      return this.statusMap[this.value];
    }

    if (this.type === 'priority' && typeof this.value === 'number') {
      return this.priorityMap[this.value];
    }

    return String(this.value);
  }

  get cssClass() {
    return `${this.type}-${this.displayValue}`;
  }
}
