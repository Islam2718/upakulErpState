export interface EmployeeRegister {
    message: string;
    id:number;
    employeeId:number;
    groupId:number;
    releaseDate:string | null;
    releaseNote?:string | null;
    assignedGroupList: string[];

    employeeName: string;
    employeeCode: string;
    groupName : string;
    groupCode : string;
    joiningDate: string | null;



}

