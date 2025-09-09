export interface Leavesetup {
  //LeaveSetupId?: number;
  leaveTypeId: number;
  leaveCategoryId: string;
  leaveTypeName: string;
  employeeTypeId: string;
  yearlyMaxLeave: number;
  yearlyMaxAvailDays: number;
  leaveGender: string;
  effectiveStartDate: Date;
  effectiveEndDate: Date;
  message: string;
}

