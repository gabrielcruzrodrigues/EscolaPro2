import { AbstractControl, ValidationErrors } from '@angular/forms';

export function cnpjValidator(control: AbstractControl): ValidationErrors | null {
  const cnpj = (control.value || '').replace(/\D/g, '');

  if (cnpj.length !== 14) return { cnpjInvalid: true };

  const invalidCnpjs = [
    '00000000000000', '11111111111111', '22222222222222', '33333333333333',
    '44444444444444', '55555555555555', '66666666666666', '77777777777777',
    '88888888888888', '99999999999999'
  ];

  if (invalidCnpjs.includes(cnpj)) return { cnpjInvalid: true };

  const validateDigits = (cnpj: string, weights: number[]) => {
    let sum = 0;
    for (let i = 0; i < weights.length; i++) {
      sum += parseInt(cnpj[i]) * weights[i];
    }
    const remainder = sum % 11;
    return remainder < 2 ? 0 : 11 - remainder;
  };

  const firstDigit = validateDigits(cnpj, [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
  const secondDigit = validateDigits(cnpj, [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);

  if (parseInt(cnpj[12]) !== firstDigit || parseInt(cnpj[13]) !== secondDigit) {
    return { cnpjInvalid: true };
  }

  return null;
}
