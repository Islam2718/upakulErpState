export interface componentMF {
    message:string;
    
    id:number;
    masterComponentId:number;
    componentName:string;
    componentCode:string;
    componentType:string;
    loanType:string;
    savingMap:boolean;
    paymentFrequency:string;
    interestRate:string;
    durationInMonth?:number;
    noOfInstalment?: number;
    gracePeriodInDay?:number;
    minimumLimit:number;
    maximumLimit:number;
    calculationMethod:string;
    latefeeperchantage:number;
}
