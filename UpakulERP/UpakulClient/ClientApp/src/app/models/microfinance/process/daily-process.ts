export interface DailyProcess {
    message: string;
    OfficeId: number;
    TransactionDate: Date;
    InitialDate: Date;
    EndDate?: Date;
    IsDayClose: Boolean;
    ReOpenDate?: Date;
}