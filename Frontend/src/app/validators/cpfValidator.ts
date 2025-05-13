import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function cpfValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const cpf = control.value;

    if (cpf === null || cpf === undefined) return null;

    const cpfStr = cpf.toString().padStart(11, '0'); // Garante 11 dígitos

    // Verifica se tem 11 dígitos numéricos
    if (!/^\d{11}$/.test(cpfStr)) {
      return { invalidCpf: true };
    }

    // Verifica se todos os dígitos são iguais (inválido)
    if (/^(\d)\1{10}$/.test(cpfStr)) {
      return { invalidCpf: true };
    }

    // Validação dos dígitos verificadores
    const calcCheckDigit = (cpfArray: number[], length: number): number => {
      const sum = cpfArray
        .slice(0, length)
        .reduce((acc, digit, index) => acc + digit * (length + 1 - index), 0);

      const remainder = (sum * 10) % 11;
      return remainder === 10 ? 0 : remainder;
    };

    const cpfDigits = cpfStr.split('').map(Number);
    const digit1 = calcCheckDigit(cpfDigits, 9);
    const digit2 = calcCheckDigit(cpfDigits, 10);

    if (digit1 !== cpfDigits[9] || digit2 !== cpfDigits[10]) {
      return { invalidCpf: true };
    }

    return null;
  };
}
