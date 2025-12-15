# AI-Powered Feedback Analysis Platform

## 1. Backend (ASP.NET Core 8)

### Design

- The backend exposes a RESTful API for managing tickets.

- TicketDTO is used as the API contract to decouple persistence from transport.

- Enums (EStatus, EPriority) are numeric and shared conceptually with the frontend to ensure consistency and prevent invalid values.

### Validation

High-Level Flow

- FluentValidation enforces business rules server-side.

- The backend is treated as the source of truth, ensuring data integrity regardless of client behavior.

### Error Handling

- Validation and domain errors are returned in a structured JSON format.

- Field-level errors allow the frontend to display meaningful messages to users.

### Trade-offs

- Numeric enums require FE/BE alignment.

- Slightly more coordination, but far safer and more efficient than free-form strings.

## 2. Frontend (Angular 18)

### Architecture

- Uses standalone components (modern Angular best practice).

- Routing is simple and explicit.

- No global state management due to the small scope.

### Forms & UX

- Reactive Forms handle create/edit flows.

- Required validation is enforced in the UI to improve user experience.

- Status and priority are selected using enum-backed dropdowns with user-friendly labels.

### API Integration

- The frontend sends numeric enum values (status, priority) to match backend expectations.

- Date values are normalized to yyyy-MM-dd to align with HTML date inputs and avoid timezone issues.

### Error Handling

- Backend validation errors are mapped to readable UI messages.

- Multiple errors are displayed clearly without over-engineering.

### Trade-offs

- Sorting logic is page-specific and not extracted into shared utilities.

- This is intentional to avoid unnecessary abstraction in a small application.
