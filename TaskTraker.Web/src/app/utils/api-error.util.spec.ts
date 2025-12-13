import { mapApiError } from './api-error.util';

describe('mapApiError', () => {
  it('should join validation errors into a single string', () => {
    const err = {
      error: {
        errors: ['Title is required', 'Priority is invalid'],
      },
    };

    const result = mapApiError(err);

    expect(result).toBe('Title is required<br>Priority is invalid');
  });

  it('should return API message when no errors array exists', () => {
    const err = {
      error: {
        message: 'Unauthorized',
      },
    };

    const result = mapApiError(err);

    expect(result).toBe('Unauthorized');
  });

  it('should return fallback message when error is unknown', () => {
    const result = mapApiError(null);

    expect(result).toBe('Something went wrong.');
  });
});
