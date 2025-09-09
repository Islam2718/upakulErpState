export interface  LoanProposalPut {
    loanApplicationId:number, //get all uniq id
    proposedAmount : number,
    // LoneeGroupImg : number,
    actionType: string,
    note: string,

    bankId:number,
    chequeNo:string,
    referenceNo:string,
    paymentType:string
}