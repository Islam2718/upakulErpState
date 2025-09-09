export interface  LoanApprovalModel {
    message: string;
    level: number;
    approvalType:string;
    designationId: number;
    method?: string;
    startingValueAmount: number;
}