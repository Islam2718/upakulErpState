export interface LoanProposalReport {
  applicationDate: Date;
  applicationNo: string;
  disburseDate?: Date;
  officeCode: string;
  officeName: string;
  groupCode?: string;
  groupName?: string;
  admissionDate?: Date;
  memberCode: string;
  memberName: string;
  contactNoOwn?: string;
  nationalId?: string;
  spouseName?: string;
  mobileNumber?: string;
  fatherName?: string;
  motherName?: string;

  totalIncome?: number;

  emp_SelfFullTimeMale?: number;
  emp_SelfFullTimeFemale?: number;
  emp_SelfPartTimeMale?: number;
  emp_SelfPartTimeFemale?: number;
  emp_WageFullTimeMale?: number;
  emp_WageFullTimeFemale?: number;
  emp_WagePartTimeMale?: number;
  emp_WagePartTimeFemale?: number;

  phaseNumber?: number;
  presentDivision?: string;
  purposeName?: string;
  component?: string;
  principleAmount: number;
  proposedAmount: number;
  applicationStatus?: string;

  presentDistrict?: string;
  presentUpazila?: string;
  presentUnion?: string;
  presentVillage?: string;
  presentAddress?: string;

  permanentDivision?: string;
  permanentDistrict?: string;
  permanentUpazila?: string;
  permanentUnion?: string;
  permanentVillage?: string;
  permanentAddress?: string;


// database e nay
  memberOccoupation:string;
  headofhouseholdoccupation:string;
  childoccupation:string;
  monthlyincome:number;
  yearlyexpense:number;

    // 4. Member Security
  savingsDeposit: number;
  savingsWithdrawable: number;
  specialSavings: number;
  emergencyFund: number;
  groupFund: number;
  insuranceDeposit: number;
  total: number;
    // 5. Guarantor Info
  guarantorName: string;
  guarantorRelation: string;
  guarantorNid: string;
  guarantorAddress: string;
  guarantorMobile: string;
  clauseNo:number;
  sector:string;
  meetingDate:Date;
  outstandingLoan:number;
  membersPresent:string;
  presidentSign:string;
  presidentname:string;
  membershipCode:string;
  editorSign:string;
  editorName:string;
  editormobile:number;

  paysOnTime?: string;
  homeInspected?: string;
  notDefaulted?: string;
  attendsRegularly?: string;
  savesRegularly?: string;
  unemployedCount?: number;
  monthlyIncome?: number;
  recommendedLoanAmount?: number;
  amountWord?: string;
  observerSignature?: string;
  observerDate?: Date;
  observerSeal?: string;

  // Accountant observations and comments
  lastLoanRepaymentDate?: Date;
  savingsAsPerProposal?: string;
  oneTimeInstallmentsPaid?: number;
  supportLoanStatus?: string;
  oneTimePaymentAmount?: number;
  nidVerified?: string;
  previousDefaultersCount?: number;
  possibleLoanDisbursementDate?: Date;
  recommendedLoanAmountAccountant?: number;
  amountWordAccountant?: string;
  accountsObserverSignature?: string;
  accountsObserverDate?: Date;
  accountsObserverSeal?: string;

   memberType?: string;

  totalAccumulatedSavings?: number | string;
  latestLoanAmount?: number | string;

  securitySavings?: number | string;
  securityWeeklyDeposit?: number | string;

  openStorage?: number | string;
  openStorageWeeklyDeposit?: number | string;

  termSavings?: number | string;
  termMonthlyDeposit?: number | string;

  dateOfAcceptance?: string | Date;


  paymentDate?: string | Date;
  numberofonetimeinstallment:number;
  quantity?: number | string;
}