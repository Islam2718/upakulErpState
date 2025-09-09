interface DropdownValue {
    text: string;
    value: string;
    selected: boolean
}

export interface EmployeeAllDropdown {
    data: EmployeeDropdownList
}

export interface EmployeeDropdownList {
    department: DropdownValue[];
    designation: DropdownValue[];
    country: DropdownValue[];
    office: DropdownValue[];
    circular: DropdownValue[];
    bank: DropdownValue[];
    division: DropdownValue[];
    occupation: DropdownValue[];

    // Middleware dropdown
    employeeType: DropdownValue[];
    employeeStatus: DropdownValue[];
    gender: DropdownValue[];
    religion: DropdownValue[];
    bloodGroup: DropdownValue[];
    maritalStatus: DropdownValue[];
}