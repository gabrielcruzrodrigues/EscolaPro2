import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function rgValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const rg = control.value;

    if (rg === null || rg === undefined || rg === '') return null;

    const rgStr = rg.toString();

    // RG válido tem de 7 a 9 dígitos
    const isValid = /^\d{7,9}$/.test(rgStr);

    return isValid ? null : { invalidRg: true };
  };
}
