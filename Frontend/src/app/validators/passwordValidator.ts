import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordValidator(): ValidatorFn {
     return (control: AbstractControl): ValidationErrors | null => {
          const password = control.value;

          if (!password) {
               return null;
          }

          // Verifica se a senha tem pelo menos 8 caracteres
          if (password.length < 8) {
               return { passwordTooShort: true };
          }

          // Verifica se contém pelo menos uma letra
          const hasLetter = /[a-zA-Z]/.test(password);
          if (!hasLetter) {
               return { missingLetter: true };
          }

          // Verifica se contém pelo menos um número
          const hasNumber = /\d/.test(password);
          if (!hasNumber) {
               return { missingNumber: true };
          }

          return null;
     };
}