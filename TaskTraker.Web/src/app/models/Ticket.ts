export enum EStatus {
  New = 0,
  InProgress = 1,
  Done = 2,
}

export enum EPriority {
  Low = 0,
  Medium = 1,
  High = 2,
}

export interface Ticket {
  id?: number;
  title: string;
  description: string;
  status: EStatus;
  priority: EPriority;
  dueDate?: string | null;
  createdAt?: string;
}
