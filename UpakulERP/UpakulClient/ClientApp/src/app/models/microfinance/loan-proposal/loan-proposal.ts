export interface  LoanProposal {    
    loanApplicationId:number, //get all uniq id
    memberName:string, //get all uniq id
    groupName: string,

    proposedBy: number,
    groupId : number, 
    memberId: number,
    applicationDate: Date,
    componentId: number,
    purposeId : number,
    proposedAmount : number,
    // LoneeGroupImg : number,
    loneeGroupImgUrl:string,
    
    emp_SelfFullTimeMale : number,
    emp_SelfFullTimeFemale : number,
    emp_SelfPartTimeMale : number,
    emp_SelfPartTimeFemale : number,
    emp_WageFullTimeMale : number,
    emp_WageFullTimeFemale : number,
    emp_WagePartTimeMale : number,
    emp_WagePartTimeFemale : number,

    firstGuarantorMemberId : number,
    firstGuarantorName : string,
    firstGuarantorContactNo : string,
    firstGuarantorRelation : string,
    firstGuarantorRemark : string,
    secondGuarantorMemberId : number,
    // secendGuarantorName : string,
    secondGuarantorName : string,
    secondGuarantorContactNo : string,
    secondGuarantorRelation : string,
    secondGuarantorRemark : string,
}