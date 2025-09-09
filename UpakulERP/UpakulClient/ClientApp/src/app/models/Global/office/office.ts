
export interface Office {
    message:string;
    officeId: number;
    officeType: number;
    officeCode: string;
    officeShortCode?: string;
    officeName: string;
    officeAddress: string;
    operationStartDate?: Date; // Store as string for Angular form
    operationEndDate?: Date;
    officeEmail?: string;
    officePhoneNo?: string;
    longitude?: string;
    latitude?: string;
    parentId?: number;

    branchOfficeId: number;


    //officePrincipalId: number;
    //officeZonalId: number;
    // officeRegionalId: number;
    // areaOfficeId: number;

//    regonalOfficeId: number;

    principalOfficeId: number;
    regonalOfficeId: number;
    zonalOfficeId: number;
    areaOfficeId: number;

    // principalOfficeId: number;
    // regonalOfficeId: number;
    // zonalOfficeId: number;

    principalOfficeCode:string;
    principalOfficeName:string;
    zonalOfficeCode:string;
    zonalOfficeName:string;
    regonalOfficeCode:string;
    regonalOfficeName:string;
    areaOfficeCode:string;
    areaOfficeName:string;
    branchOfficeCode:string;
    branchOfficeName:string;
  }
