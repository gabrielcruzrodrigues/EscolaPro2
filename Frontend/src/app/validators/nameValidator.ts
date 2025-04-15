import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function nameValidators(): ValidatorFn {
     return (control: AbstractControl): ValidationErrors | null => {
          const username = control.value;

          // Verifica se contém pelo menos uma letra
          const hasLetter = /[a-zA-Z]/.test(username);
          if (!hasLetter) {
               return { missingLetter: true };
          }

          return null;
     }
}