export interface  DailyCollection {
        groupXMemberComponentDetailsId : number;
        groupId : number;
        memberCode : string;
        memberName : string;

        loanNo?: number;
        insNo?: number;
        component?: number;
        disburseDate?: Date;
        disburseAmt?: number;
        openingOutst?: number;
        openingOD?: number;
        openingAdv?: number;
        openingSaving?: number;
        insAmt?: number;
        att?: number;
        loanCollection?: number;
        loanRebate?: number;
        loanAdjust?: number;
        savingsCollectionCom?: boolean;
        savingsCollectionVol?: boolean;
        savingsCollectionOth?: boolean;
        savingsRefundRefund?: number;
        savingsRefundCom?: number;
        savingsRefundVol?: number;
        savingsRefundOth?: number;
        ledger?: string;

}
