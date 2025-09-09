export interface NotificationDetails {
  id: number;
  title: string;
  applicationDate: string;
  graceFrom: string | null;
  graceTo: string | null;
  group: string;
  loanType: string;
  notificationType: string;
  office: string;
  orderBy: string | null;
  proposedAmount: number;
}