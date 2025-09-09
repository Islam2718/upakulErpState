export interface BankAccountChequeIDetails {
    message: string;
    bankAccountMappingId : number;
    bankId ?: number;
    bankBranchId ?: number;
    officeId ?: number;
    accountNumber ?: string;
    bankName ?: string;
    bankBranchName ?: string;
    chequeNumber ?: string;
    status ?: string;
    remarks ?: string;
}