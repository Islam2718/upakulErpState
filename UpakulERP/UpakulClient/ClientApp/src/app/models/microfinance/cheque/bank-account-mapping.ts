export interface BankAccountMapping {
    message: string;
    bankAccountMappingId : number;
    bankId ?: number;
    officeId ?: number;
    bankName ?: string;
    isRefData?: boolean;

    branchName ?: string;
    routingNo ?: string;
    branchAddress ?: string;
    bankAccountName ?: string;
    bankAccountNumber ?: string;
    accountId ?: string;
}
