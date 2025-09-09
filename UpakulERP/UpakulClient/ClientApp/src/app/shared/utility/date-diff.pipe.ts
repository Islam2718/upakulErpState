import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateDiff',
  standalone: true // ✅ Angular 14+ standalone pipe support
})
export class DateDiffPipe implements PipeTransform {
  transform(startDate: string | Date, endDate: string | Date): number {
    const start = new Date(startDate);
    const end = new Date(endDate);

    if (isNaN(start.getTime()) || isNaN(end.getTime())) {
      return 0;
    }

    const diffTime = end.getTime() - start.getTime();
    return Math.floor(diffTime / (1000 * 60 * 60 * 24)) + 1; // +1 to include both start and end
  }
}
