export function mapApiError(err: any): string {
  if (err?.error?.errors?.length > 0) {
    return err.error.errors.join('<br>');
  }

  if (err?.error?.message) {
    return err.error.message;
  }

  return 'Something went wrong.';
}
