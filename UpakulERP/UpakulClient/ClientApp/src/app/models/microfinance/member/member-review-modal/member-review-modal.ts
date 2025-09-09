export interface MemberReviewModal {
  message: string;
  status: string;
  isOtpVerified: boolean;
  isApproved: any;
  officeId: any;
  memberId: number;
  groupId: number; //dropdown
  groupCode: string;
  groupName: string;
  admissionDate: Date;
  memberName: string;
  memberCode: string;
  occupationId: string;
  fatherName: string;
  motherName: string;
  maritalStatus: string;
  spouseName: string;
  gender: string;
  dateOfBirth: string | null;
  birthYear: number | null;
  age: number | null;
  nationalId: string;
  smartCard: string;
  birthCertificate: string;
  birthCertificateVerified: boolean;
  tin: string;
  otherIDType: string;
  otherIdType: string;
  otherIdNumber: string;
  contactNoOwn: string;
  mobileNumber: string | null;
  academicQualification: number | null; //dropdown
  academicQualificationId: number | null; //dropdown
  memberRemarks: number | null; //drops 
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
  maximumMember: string;
  currentMember: string;
  admissionFee: number;
  passbookFee: number;
  applicationNo: string;
  passbookNo: string;
  verificationNote: string;
  // uploadDocument: string;

  presentAddress: string;
  presentCountryId: number | null;
  presentDivisionId: number | null;
  presentDistrictId: number | null;
  presentUpazilaId: number | null;
  presentUnionId: number | null;
  presentVillageId: number | null;
  
  permanentAddress: string;
  permanentCountryId: number | null;
  permanentDivisionId: number | null;
  permanentDistrictId: number | null;
  permanentUpazillaId: number | null;
  permanentUnionId: number | null;
  permanentVillageId: number | null;

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
