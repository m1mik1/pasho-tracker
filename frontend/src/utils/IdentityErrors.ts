export function mapIdentityError(code: string): string {
    switch (code) {
      case 'DuplicateUserName':
        return '❗ This username is already taken. Try another.';
      case 'DuplicateEmail':
        return '❗ This email is already in use.';
      case 'PasswordTooShort':
        return '❗ Password is too short.';
      case 'PasswordRequiresNonAlphanumeric':
        return '❗ Password must include a symbol.';
      default:
        return '❗ Something went wrong. Please try again.';
    }
  }
  