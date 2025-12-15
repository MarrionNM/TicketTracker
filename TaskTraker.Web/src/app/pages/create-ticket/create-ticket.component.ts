import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EPriority, EStatus } from '../../models/Ticket';

@Component({
  selector: 'app-create-ticket',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.scss'],
})
export class CreateTicketComponent implements OnInit {
  isEdit = false;
  ticketId: string | null = null;

  loading = false;
  submitting = false;
  apiError: string | null = null;

  minDate = new Date().toISOString().split('T')[0];

  statusOptions = [
    { id: EStatus.New, label: 'New' },
    { id: EStatus.InProgress, label: 'In Progress' },
    { id: EStatus.Done, label: 'Done' },
  ];

  priorityOptions = [
    { id: EPriority.Low, label: 'Low' },
    { id: EPriority.Medium, label: 'Medium' },
    { id: EPriority.High, label: 'High' },
  ];

  form = new FormGroup({
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    status: new FormControl<EStatus | null>(null, Validators.required),
    priority: new FormControl<EPriority | null>(null, Validators.required),
    dueDate: new FormControl<string | null>(null),
  });

  constructor(
    private api: ApiService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.ticketId = this.route.snapshot.paramMap.get('id');

    if (this.ticketId) {
      this.isEdit = true;
      this.loadTicket();
    }
  }

  loadTicket() {
    this.loading = true;

    this.api.getTicket(Number(this.ticketId)).subscribe({
      next: (res) => {
        const ticket = res.data;

        this.form.patchValue({
          title: ticket.title,
          description: ticket.description,
          status: ticket.status,
          priority: ticket.priority,
          dueDate: ticket.dueDate ? ticket.dueDate.split('T')[0] : this.minDate,
        });

        this.loading = false;
      },
      error: () => {
        this.apiError = 'Failed to load ticket.';
        this.loading = false;
      },
    });
  }

  submit() {
    this.apiError = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;

    const task = {
      title: this.form.value.title,
      description: this.form.value.description,
      status: Number(this.form.value.status),
      priority: Number(this.form.value.priority),
      dueDate: this.form.value.dueDate || null,
    };

    const request$ = this.isEdit
      ? this.api.updateTicket(this.ticketId!, task)
      : this.api.createTicket(task);

    request$.subscribe({
      next: () => {
        this.submitting = false;
        this.router.navigate(
          this.isEdit ? ['/ticket', this.ticketId] : ['/tickets']
        );
      },
      error: (err) => {
        this.apiError =
          err?.error?.errors?.length > 0
            ? err.error.errors.join('<br>')
            : err?.error?.message || 'Something went wrong.';

        this.submitting = false;
      },
    });
  }

  cancel() {
    this.router.navigate(
      this.isEdit ? ['/ticket', this.ticketId] : ['/tickets']
    );
  }
}
