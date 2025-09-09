import { Injectable } from '@angular/core';
import { FormGroup, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';


@Injectable({
  providedIn: 'root'
})
export class CommonGlobalServiceService {

  constructor() { }


  //DateFormat
    formatDateUS(date: any): string {
        if (!date) return '';
        const d = new Date(date);
        return `${d.getFullYear()}-${(d.getMonth() + 1).toString().padStart(2, '0')}-${d.getDate().toString().padStart(2, '0')}`
   }

   formatDate(date: any): string {
              if (!date) return '';
              const d = new Date(date);

              const day = d.getDate().toString().padStart(2, '0');
              const month = d.toLocaleString('en-US', { month: 'short' }); // e.g., Jan, Feb, Mar
              const year = d.getFullYear();

              return `${day}-${month}-${year}`;
      }


   getCurrentDate(): string {
        const today = new Date();
        const day = today.getDate().toString().padStart(2, '0');
        const month = today.toLocaleString('en-US', { month: 'short' });
        const year = today.getFullYear();
        return `${day}-${month}-${year}`;
      }


removeLeadingSpace(form: FormGroup, controlName: string, event: Event) {
  const input = event.target as HTMLInputElement;
  if (input.value.startsWith(' ')) {
    const trimmed = input.value.trimStart();
    input.value = trimmed;
    form.get(controlName)?.setValue(trimmed, { emitEvent: false });
  }
}

noLeadingSpaceValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = (control.value || '') as string;
    return value.startsWith(' ') ? { leadingSpace: true } : null;
  };
}


}
