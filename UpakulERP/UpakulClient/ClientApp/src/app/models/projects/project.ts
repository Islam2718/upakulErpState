export interface Project {
  value: string;
  message:string;
  projectId: number;
  projectTitle: string;
  projectType: string;
  objective: string;
  chipEmployeeId: number;
  totalStaff: number;
  monitoringPeriod: number;
  target: string;
  totalTarget: string;
  startMonth?: Date;
  expireDate?: Date;
  projectShortName: string;
  targetType: string;
  monthlyQuarterly: string;
  financialTarget: string;
}