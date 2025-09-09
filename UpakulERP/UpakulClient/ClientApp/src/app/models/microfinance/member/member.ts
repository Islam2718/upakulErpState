export interface Member {
  [key: string]: any; 
  message: string;
  status: string;
  isOtpVerified: boolean;
  isApproved: any;
  officeId: any;
  memberId: number;
  groupId: number; //dropdown  
  groupCode: string;
  groupName: string;
  admissionDate: string;
  memberName: string;
  memberCode: string;
  occupationId: string;
  occupationLabel: string;
  fatherName: string;
  motherName: string;
  maritalStatus: string;
  maritalStatusLabel: string;
  spouseName: string;
  gender: string;
  genderLabel: string;
  dateOfBirth: string;
  birthYear: number | null;
  age: number | null;
  nationalId: string;
  smartCard: string;
  nidVerified:boolean;
  birthCertificate: string;
  birthCertificateVerified: boolean;
  tin: string;
  otherIDType: string;
  otherIdType: string;
  otherIdTypeLabel: string;
  otherIdNumber: string;
  contactNoOwn: string;
  mobileNumber: string | null;
  academicQualification: number | null; //dropdown
  academicQualificationId: number | null; //dropdown
  memberRemarks: number | null; //drops 
  memberRemarksLabel: string; //
  // remarksId: number | null;
  noOfDependents: string;

  memberImgUrl: string;
  memberImg: string;
  signatureImg: string;
  signatureImgUrl: string;
  nidBackImg: string;
  nigBackImgUrl: string;
  nidFrontImg: string;
  nidFrontImgUrl: string;

  statusDate: string | null;
  // authorizedPersonId: number | null;
  authorizedEmployeeId: number | null;
  authorizedEmployeeName: string;
  maximumMember: string;
  currentMember: string;
  admissionFee: number;
  passbookFee: number;
  applicationNo: string;
  passbookNo: string;
  verificationNote: string;
  // uploadDocument: string;

  presentCountryId: number | null;
  presentCountryName:string; //
  presentDivisionId: number | null;
  presentDivisionName: string;
  presentDistrictId: number | null;
  presentDistrictName: string;
  presentUpazilaId: number | null;
  presentUpazilaName: string;
  presentUnionId: number | null;
  presentUnionName: string;
  presentVillageId: number | null;
  presentVillageName: string;
  presentAddress: string;  
  
  permanentCountryId: number | null;
  permanentCountryName: string;
  permanentDivisionId: number | null;
  permanentDivisionName: string;
  permanentDistrictId: number | null;
  permanentDistrictName: string;
  permanentUpazilaId: number | null;
  permanentUpazilaName: string;
  permanentUnionId: number | null;
  permanentUnionName: string;
  permanentVillageId: number | null;
  permanentVillageName: string;
  permanentAddress: string;

  referenceMemberId: number | null;
  identifierName: string;
  relationWithIdentifier: string;
  totalFamilyMember: number | null;
  totalChildren: number | null;
  totalIncome: number | null;
  incomeType: string;
  incomeAmt: number | null;
  residentialHouseArea: string;
  arableLandArea: string;
  previouslyLoanReceiver: boolean;
  relatedOtherProgram: boolean;
  memberOfOtherOrganization: boolean;  
  isCheckedInContactNo:boolean;
  isMigrated:Boolean;
  migratedNote:String;
  latitude: string;
  longitude: string;
}
