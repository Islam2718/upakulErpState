export interface MemberViewModal {
    isApproved?: boolean;
    admissionDate: string; // ISO date string
    memberId:number;
    memberCode: number;
    groupName: string;
    memberName?: string ;
    officeName: string;
    officeCode:string;
    gender:string ;
    occupationName:string;
    fatherName: string;
    motherName: string;
    maritalStatus: string;
    spouseName?: string ;
    dateOfBirth: Date; // ISO date string
    nationalId?: string ;
    birthCertificate?: string ;
    contactNoOwn: string;
    mobileNumber?: string ;
    permanentDivision?: number;
    permanentDistrict?: number ;
    permanentUpazila?: number ;
    permanentUnion?: number ;
    permanentVillage?: number ;
    permanentAddress?: string ;
    presentDivision: number;
    presentDistrict: number;
    presentUpazila: number;
    presentUnion: number;
    presentVillage: number;
    presentAddress?: string ;
    ref_MemberName?: number ;
    ref_MemberCode?: string ;
    ref_MemberSignatureUrl?: string ;
    chairmanName?: string ;
    chairman_SignatureUrl?: string ;
    signatureImgUrl?: string ;
    memberImgUrl?:string;

}

