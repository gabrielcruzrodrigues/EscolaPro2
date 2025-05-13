import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function phoneValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const phone = control.value;

    // Verifica se é um número e tem 10 ou 11 dígitos
    const isValid = /^\d{10,11}$/.test(phone);

    if (!isValid) {
      return { invalidPhone: true };
    }

    return null;
  };
}
