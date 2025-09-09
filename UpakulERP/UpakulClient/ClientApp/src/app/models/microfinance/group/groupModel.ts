export interface GroupModel {
    message: string;
    groupId: number;
    officeId: number;
    groupCode: string;
    groupName: string;
    groupType: string;
    scheduleType: string;
    meetingDay?: number;
    openinigDate: Date;
    startDate: Date;
    closingDate?: Date;
    meetingPlace?: string;
    //samityLeaderMemberId?: number; // | null;
    divisionId: number;
    districtId: number;
    upazilaId: number;
    unionId: number;
    villageId: number;
    address?: string;
    latitude?: string;
    longitude?: string;
    //documentURL?: string;
    //
    office?: string;
    samityTypeName?: string;
    //samityLeaderMember?: string;
    meetingDayName?: string;

    division?: string;
    district?: string;
    upazila?: string;
    union?: string;
    village?: string;
}