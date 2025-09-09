export interface User {
  id: number;
  employeeId: number;
  firstName: string;
  lastName: string;
  fullName: string;
  userName: string;
  employeeCode: string;
  officeName: string;
  email: string;
  isActive: boolean;
  createdBy: number | null;
  createdOn: string | null;
}