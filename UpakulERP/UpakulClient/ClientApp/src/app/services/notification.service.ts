// notification.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface NotificationItem {
  notificationType: string;
  loanType: string;
  id: number;
  title: string;
  office: string;
  group: string;
  graceFrom: string | null;
  graceTo: string | null;
  applicationDate: string;
  proposedAmount: number;
  orderBy: string;
}

export interface NotificationData {
  count: number;
  summary: NotificationItem[];
}

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private storageKey = 'Notification';

  private notificationsSubject = new BehaviorSubject<NotificationData>(
    this.loadFromStorage()
  );
  notifications$ = this.notificationsSubject.asObservable();

  constructor() {}

  private loadFromStorage(): NotificationData {
    const data = localStorage.getItem(this.storageKey);
    return data ? JSON.parse(data) : { count: 0, summary: [] };
  }

  private saveToStorage(data: NotificationData) {
    localStorage.setItem(this.storageKey, JSON.stringify(data));
  }

  updateNotifications(newData: NotificationData) {
    this.saveToStorage(newData);
    this.notificationsSubject.next(newData);
  }

  removeNotification(id: number) {
    const current = this.notificationsSubject.value;
    const updatedSummary = current.summary.filter((item) => item.id !== id);
    const newData = {
      count: updatedSummary.length,
      summary: updatedSummary,
    };
    this.updateNotifications(newData);
  }

  // 🔹 Add a new notification
  addNotification(newItem: NotificationItem) {
    const current = this.notificationsSubject.value;
    const updatedSummary = [...current.summary, newItem];
    const newData = {
      count: updatedSummary.length,
      summary: updatedSummary,
    };
    this.updateNotifications(newData);
  }

  // 🔹 Clear all
  clearAll() {
    const emptyData: NotificationData = { count: 0, summary: [] };
    this.updateNotifications(emptyData);
  }
}
