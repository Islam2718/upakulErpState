import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { RouterModule, provideRouter, Routes } from '@angular/router';
import { AuthInterceptor } from './app/interceptors/auth.interceptor';
import { LoginComponent } from './app/pages/login/login.component';
import { DashboardComponent } from './app/pages/dashboard/dashboard.component';
import { GlobalDashboardComponent } from './app/pages/global-setup/dashboard.component';
import { MicrofinanceDashboardComponent } from './app/pages/microfinance/dashboard.component';
import { ProjectsComponent } from './app/pages/projects/dashboard.component';
import { AccountDashboardsComponent } from './app/pages/accounts/dashboard.component';
import { provideAnimations } from '@angular/platform-browser/animations';
import { ToastrModule, provideToastr } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { MenuComponent } from './app/pages/administration/menu/menu-form/menu.component';

import { EducationComponent } from './app/pages/hrm/education/education/education.component';
import { EducationListComponent } from './app/pages/hrm/education/education-list/education-list.component';
import { BoardUniversityComponent } from './app/pages/hrm/university/board-university/board-university.component';
import { BoardUniversitylistComponent } from './app/pages/hrm/university/board-universitylist/board-universitylist.component';

import { EmployeeStatusComponent } from './app/pages/hrm/employee-status/employee-status.component';
import { EmployeeTypeComponent } from './app/pages/hrm/employee-type/employee-type.component';

// Project
import { DonerComponent } from './app/pages/projects/doner/doner/doner.component';
import { DonerListComponent } from './app/pages/projects/doner/doner-list/doner-list.component';
import { ProjectComponent } from './app/pages/projects/project/project/project.component';
import { ProjectListComponent } from './app/pages/projects/project/project-list/project-list.component';
//End Project

import { AdministrationComponent } from './app/pages/administration/dashboard.component';
import { RoleComponent } from './app/pages/administration/role/role/role.component';
import { RoleListComponent } from './app/pages/administration/role/role-list/role-list.component';
import { MenuPermissionComponent } from './app/pages/administration/menu-permission/menu-permission.component';
import { HrDashboardComponent } from './app/pages/hrm/dashboard.component';

//Global
import { CountryComponent } from './app/pages/global-setup/country/country/country.component';
import { CountryListComponent } from './app/pages/global-setup/country/country-list/country-list.component';
import { GeolocationComponent } from './app/pages/global-setup/geolocation/geolocation/geolocation.component';
import { GeolistComponent } from './app/pages/global-setup/geolocation/geolist/geolist.component';
import { OfficeComponent } from './app/pages/global-setup/office/office-form/office.component';
import { BankComponent } from './app/pages/global-setup/bank/bank-form/bank.component';
import { OfficeListComponent } from './app/pages/global-setup/office/office-list/office-list.component';
import { BankListComponent } from './app/pages/global-setup/bank/bank-list/bank-list.component';
import { BankbranchFormComponent } from './app/pages/global-setup/bank-branch/bankbranch-form/bankbranch-form.component';
import { BankbranchListComponent } from './app/pages/global-setup/bank-branch/bankbranch-list/bankbranch-list.component';
//END Global

import { ConfigService } from './app/core/config.service';
import { APP_INITIALIZER, importProvidersFrom } from '@angular/core';

//HRM
import { DepartmentFormComponent } from './app/pages/hrm/department/dept-form/dept-form.component';
import { DepartmentListComponent } from './app/pages/hrm/department/dept-list/dept-list.component';
import { DesignationFormComponent } from './app/pages/hrm/designation/designation-form/designation-form.component';
import { DesignationListComponent } from './app/pages/hrm/designation/designation-list/designation-list.component';
import { EmployeeProfileComponent } from './app/pages/hrm/employee/employee-profile/employee-profile.component';
import { HolidayFormComponent } from './app/pages/hrm/holiday/holiday-form/holiday-form.component';
import { HolidayListComponent } from './app/pages/hrm/holiday/holiday-list/holiday-list.component';
import { LeaveSetupListComponent } from './app/pages/hrm/leavesetup/leaveSetup-list/leave-setup-list.component';
import { LeaveSetupFormComponent } from './app/pages/hrm/leavesetup/leaveSetup-form/leave-setup-form.component';
//END HRM
import { ComponentSetupFormComponent } from './app/pages/accounts/component_setup/component-setup-form/component-setup-form.component';
import { ComponentSetupListComponent } from './app/pages/accounts/component_setup/component-setup-list/component-setup-list.component';
import { EmployeeRegistrationComponent } from './app/pages/administration/user/employee-registration/employee-registration.component';
import { UserListComponent } from './app/pages/administration/user/user-list/user-list.component';

import { BudgetComponent } from './app/pages/accounts/budget/budget/budget.component';
import { BudgetListComponent } from './app/pages/accounts/budget/budget-list/budget-list.component';
import { BudgetEntryComponent } from './app/pages/accounts/budget/budget-entry/budget-entry.component';
import { EmployeeListComponent } from './app/pages/hrm/employee/employee-list/employee-list.component';

import { AuthGuard } from './app/guards/auth.guard';
import path from 'path';
//import { LoanconfigureComponent } from './app/pages/microfinance/configuration/loanconfigure/loanconfigure.component';
// Microfinance
import { GroupComponent } from './app/pages/microfinance/group/group/group.component';
import { GroupListComponent } from './app/pages/microfinance/group/group-list/group-list.component';

import { LoanApprovalComponent } from './app/pages/microfinance/loan/loan-approval-setting/loan-approval.component';
import { LoanProposalsComponent } from './app/pages/microfinance/loan/loan-application/loan-proposals/loan-proposals.component';
import { LoanProposalListComponent } from './app/pages/microfinance/loan/loan-application/loan-proposal-list/loan-proposal-list.component';
// import { LoanWorkflowComponent } from './app/pages/microfinance/loan/workflow/loan-workflow/loan-workflow.component';
// import { LoanDetailsComponent } from './app/pages/microfinance/loan/workflow/loan-details/loan-details.component';
import { LeaveMappingFormComponent } from './app/pages/hrm/leavemapping/leavemappingfrom/leave-mapping-form.component';

import { CreateMemberComponent } from './app/pages/microfinance/member/member-form/member.component';
import { MemberListComponent } from './app/pages/microfinance/member/member-list/member-list.component';

import { PurposeListComponent } from './app/pages/microfinance/purpose/purpose-list/purpose-list.component';
import { PurposeFormComponent } from './app/pages/microfinance/purpose/purpose-form/purpose-form.component';
import { OccupationComponent } from './app/pages/microfinance/occupation/occupation/occupation.component';
import { OccupationListComponent } from './app/pages/microfinance/occupation/occupation-list/occupation-list.component';

import { DailyProcessComponent } from './app/pages/microfinance/Process/daily-process/daily-process.component';

import { MasterComponentFormComponent } from './app/pages/microfinance/components/masterComponent/master-component-form/master-component-form.component';
import { MasterComponentListComponent } from './app/pages/microfinance/components/masterComponent/master-component-list/master-component-list.component';
import { ComponentMFFormComponent } from './app/pages/microfinance/components/mfComponent/mfComponent-form/mfComponent-form.component';
import { ComponentMFListComponent } from './app/pages/microfinance/components/mfComponent/mfComponent-list/mfComponent-list.component';
import { CodegeneratorComponent } from './app/pages/microfinance/configuration/codegenerator/codegenerator.component';
import { BankAccountMappingComponent } from './app/pages/microfinance/cheque/bank-account-mapping/bank-account-mapping.component';

import { OfficeComponentMappingComponent } from './app/pages/microfinance/office-component-mapping/office-component-mapping.component';

import { EmployeeRegisterFormComponent } from './app/pages/microfinance/employee-register/employee-register-form/employee-register-form.component';
import { EmployeeRegisterListComponent } from './app/pages/microfinance/employee-register/employee-register-list/employee-register-list.component';
import { GraceScheduleFormComponent } from './app/pages/microfinance/GraceSchedule/graceschedule-form/graceschedule-form.component';
import { GraceScheduleListComponent } from './app/pages/microfinance/GraceSchedule/graceschedule-list/graceschedule-list.component';

import { DailyCollectionFormComponent } from './app/pages/microfinance/weekly_collection/daily-collection-form/daily-collection-form.component';
import { DailyCollectionListComponent } from './app/pages/microfinance/weekly_collection/daily-collection-list/daily-collection-list.component';
import { ChartOfAccountComponent } from './app/pages/accounts/chart-of-account/chart-of-account.component';
import { types } from 'util';
import { ActivityPlanComponent } from './app/pages/projects/project/activity-plan/activity-plan.component';
// state manage 
import { provideStore, META_REDUCERS } from '@ngrx/store';
import { authReducer } from './app/state/auth.reducer';
import { localStorageSyncReducer } from './app/state/meta-reducers';


const configService = new ConfigService(new HttpClient({} as any));

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' }, // Default route
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'mf/:moduleId', component: MicrofinanceDashboardComponent, canActivate: [AuthGuard] },
  { path: 'pr/:moduleId', component: ProjectsComponent, canActivate: [AuthGuard] },
  { path: 'ac/:moduleId', component: AccountDashboardsComponent, canActivate: [AuthGuard] },
  { path: 'gs/:moduleId', component: GlobalDashboardComponent, canActivate: [AuthGuard] },
  { path: 'adm/:moduleId', component: AdministrationComponent, canActivate: [AuthGuard] },
  { path: 'hr/:moduleId', component: HrDashboardComponent, canActivate: [AuthGuard] },

  { path: 'bank/edit', component: BankComponent, canActivate: [AuthGuard] },
  { path: 'occupation/edit', component: OccupationComponent, canActivate: [AuthGuard] },
  { path: 'bankbranchform/edit', component: BankbranchFormComponent, canActivate: [AuthGuard] },
  { path: 'group/edit', component: GroupComponent, canActivate: [AuthGuard] },
  { path: 'education/edit', component: EducationComponent, canActivate: [AuthGuard] }, 
  { path: 'loan-proposal/edit', component: LoanProposalsComponent, canActivate: [AuthGuard] }, 
  { path: 'board-university/edit', component: BoardUniversityComponent, canActivate: [AuthGuard] },
  { path: 'dept-form/edit', component: DepartmentFormComponent, canActivate: [AuthGuard] },
  { path: 'emp-profile/edit', component: EmployeeProfileComponent, canActivate: [AuthGuard] },
  { path: 'designation-form/edit', component: DesignationFormComponent, canActivate: [AuthGuard] },
  { path: 'holiday-form/edit', component: HolidayFormComponent, canActivate: [AuthGuard] },
  { path: 'doner/edit', component: DonerComponent, canActivate: [AuthGuard] },
  { path: 'project/edit', component: ProjectComponent, canActivate: [AuthGuard] },
  { path: 'geolocation/edit', component: GeolocationComponent, canActivate: [AuthGuard] },
  { path: 'purpose/edit', component: PurposeFormComponent, canActivate: [AuthGuard] },
  { path: 'role/edit', component: RoleComponent, canActivate: [AuthGuard] },
  { path: 'budget/edit', component: BudgetComponent, canActivate: [AuthGuard] },
  { path: 'office/edit', component: OfficeComponent, canActivate: [AuthGuard] },
  { path: 'country/edit', component: CountryComponent, canActivate: [AuthGuard] },
  { path: 'member-setup/edit', component: CreateMemberComponent, canActivate: [AuthGuard] },
  { path: 'ComponentSetupFormComponent/edit', component: ComponentSetupFormComponent, canActivate: [AuthGuard] },
  { path: 'master-component-form/edit', component: MasterComponentFormComponent, canActivate: [AuthGuard] },
  { path: 'component-mf-form/edit', component: ComponentMFFormComponent, canActivate: [AuthGuard] },
  { path: 'leave-setup-form/edit', component: LeaveSetupFormComponent, canActivate: [AuthGuard] },
  { path: 'bank-account-mapping/edit', component: BankAccountMappingComponent, canActivate: [AuthGuard] },
  { path: 'employee-register-form/edit', component: EmployeeRegisterFormComponent, canActivate: [AuthGuard] },
  { path: 'graceschedule-form/edit', component:GraceScheduleFormComponent, canActivate: [AuthGuard] },
  { path: 'dailycollection-form/edit', component: DailyCollectionFormComponent, canActivate: [AuthGuard] },

  //MF
  {
    path: 'mf/:moduleId',
    children: [
      { path: '', component: MicrofinanceDashboardComponent, canActivate: [AuthGuard] },
      { path: 'group', component: GroupComponent, canActivate: [AuthGuard] },
      { path: 'group-list', component: GroupListComponent, canActivate: [AuthGuard] },

      { path: 'occupation', component: OccupationComponent, canActivate: [AuthGuard] },
      { path: 'occupation-list', component: OccupationListComponent, canActivate: [AuthGuard] },
     
      { path: 'purpose-form', component: PurposeFormComponent, canActivate: [AuthGuard] },
      { path: 'purpose-list', component: PurposeListComponent, canActivate: [AuthGuard] },

      { path: 'member-setup', component: CreateMemberComponent, canActivate: [AuthGuard] },
      { path: 'member-list', component: MemberListComponent, canActivate: [AuthGuard] },
      
      { path: 'daily-process', component: DailyProcessComponent, canActivate: [AuthGuard] },
      { path: 'component-mf-form', component: ComponentMFFormComponent, canActivate: [AuthGuard] },
      { path: 'component-mf-list', component: ComponentMFListComponent, canActivate: [AuthGuard] },
      { path: 'master-component-form', component: MasterComponentFormComponent, canActivate: [AuthGuard] },
      { path: 'master-component-list', component: MasterComponentListComponent, canActivate: [AuthGuard] },
      { path: 'codegenerator', component: CodegeneratorComponent, canActivate: [AuthGuard] },
     // { path: 'loanconfigure', component: LoanconfigureComponent, canActivate: [AuthGuard] },

      { path: 'loan-approval', component: LoanApprovalComponent, canActivate:[AuthGuard]},
      { path: 'loan-proposal', component: LoanProposalsComponent, canActivate:[AuthGuard]},
      { path: 'loan-proposal-list', component: LoanProposalListComponent, canActivate: [AuthGuard] },
      // { path: 'loan-proposal-workflow', component: LoanWorkflowComponent, canActivate: [AuthGuard] },
      // { path: 'loan-details', component: LoanDetailsComponent, canActivate: [AuthGuard] },
      
      { path: 'bank-account-mapping', component: BankAccountMappingComponent, canActivate: [AuthGuard] },
      { path: 'office-component-mapping', component: OfficeComponentMappingComponent, canActivate: [AuthGuard] },
      
      { path: 'employee-register-form', component: EmployeeRegisterFormComponent, canActivate:[AuthGuard]},
      { path: 'employee-register-list', component: EmployeeRegisterListComponent, canActivate: [AuthGuard] },
      { path: 'graceschedule-form', component: GraceScheduleFormComponent, canActivate: [AuthGuard] },
      { path: 'graceschedule-list', component: GraceScheduleListComponent, canActivate: [AuthGuard] },
      { path: 'dailycollection-form', component: DailyCollectionFormComponent, canActivate: [AuthGuard] },
      { path: 'dailycollection-list', component: DailyCollectionListComponent, canActivate: [AuthGuard] },
    ]
  },

  //Administration
  {
    path: 'adm/:moduleId',
    children: [
      { path: '', component: AdministrationComponent, canActivate: [AuthGuard] },
      { path: 'role', component: RoleComponent, canActivate: [AuthGuard] },
      { path: 'role-list', component: RoleListComponent, canActivate: [AuthGuard] },
      { path: 'menu-permission', component: MenuPermissionComponent, canActivate: [AuthGuard] },
      { path: 'menu-form', component: MenuComponent, canActivate: [AuthGuard] },
      { path: 'employee-registration', component: EmployeeRegistrationComponent, canActivate: [AuthGuard] },
      { path: 'user-list', component: UserListComponent, canActivate: [AuthGuard] },
    ]
  },

  //Global
  {
    path: 'gs/:moduleId',
    children: [
      { path: '', component: GlobalDashboardComponent, canActivate: [AuthGuard] },
      { path: 'office', component: OfficeComponent, canActivate: [AuthGuard] },
      { path: 'office-list', component: OfficeListComponent, canActivate: [AuthGuard] },
      { path: 'bank', component: BankComponent, canActivate: [AuthGuard] },
      { path: 'bank-list', component: BankListComponent, canActivate: [AuthGuard] },
      { path: 'bankbranchform', component: BankbranchFormComponent, canActivate: [AuthGuard] },
      { path: 'bankbranch-list', component: BankbranchListComponent, canActivate: [AuthGuard] },
      { path: 'geolocation', component: GeolocationComponent, canActivate: [AuthGuard] },
      { path: 'geolist', component: GeolistComponent, canActivate: [AuthGuard] },
      { path: 'country', component: CountryComponent, canActivate: [AuthGuard] },
      { path: 'country-list', component: CountryListComponent, canActivate: [AuthGuard] }
      // Add more child routes here if needed
    ]
  },
  {
    path: 'hr/:moduleId',
    children: [
      { path: '', component: HrDashboardComponent, canActivate: [AuthGuard] },
      { path: 'education', component: EducationComponent, canActivate: [AuthGuard] },
      { path: 'education-list', component: EducationListComponent, canActivate: [AuthGuard] },
      { path: 'bu', component: BoardUniversityComponent, canActivate: [AuthGuard] },
      { path: 'board-universitylist', component: BoardUniversitylistComponent, canActivate: [AuthGuard] },
      { path: 'employee_status', component: EmployeeStatusComponent, canActivate: [AuthGuard] },
      { path: 'employee_type', component: EmployeeTypeComponent, canActivate: [AuthGuard] },
      { path: 'dept-form', component: DepartmentFormComponent, canActivate: [AuthGuard] },
      { path: 'dept-list', component: DepartmentListComponent, canActivate: [AuthGuard] },
      { path: 'designation-form', component: DesignationFormComponent, canActivate: [AuthGuard] },
      { path: 'designation-list', component: DesignationListComponent, canActivate: [AuthGuard] },
      { path: 'emp-profile', component: EmployeeProfileComponent, canActivate: [AuthGuard] },
      { path: 'emp-list', component: EmployeeListComponent, canActivate: [AuthGuard] },
      { path: 'holiday-form', component: HolidayFormComponent, canActivate: [AuthGuard] },
      { path: 'holiday-list', component: HolidayListComponent, canActivate: [AuthGuard] },
      { path: 'leave-setup-form', component: LeaveSetupFormComponent, canActivate: [AuthGuard] },
      { path: 'leave-setup-list', component: LeaveSetupListComponent, canActivate: [AuthGuard] },
      { path: 'leave-mapping-form',component:LeaveMappingFormComponent, canActivate: [AuthGuard]},
    ]
  },

  //Projects  
  {
    path: 'pr/:moduleId',
    children: [
      { path: '', component: ProjectsComponent, canActivate: [AuthGuard] },
      { path: 'doner', component: DonerComponent, canActivate: [AuthGuard] },
      { path: 'doner-list', component: DonerListComponent, canActivate: [AuthGuard] },
      { path: 'project', component: ProjectComponent, canActivate: [AuthGuard] },
      { path: 'project-list', component: ProjectListComponent, canActivate: [AuthGuard] },
      { path: 'target-activity', component: ActivityPlanComponent, canActivate: [AuthGuard]}
      // Add more child routes here if needed
    ]
  },
  {
    path: 'ac/:moduleId',
    children: [
      { path: '', component: AccountDashboardsComponent, canActivate: [AuthGuard] },
      { path: 'ComponentSetupFormComponent', component: ComponentSetupFormComponent, canActivate: [AuthGuard] },
      { path: 'ComponentSetupListComponent', component: ComponentSetupListComponent, canActivate: [AuthGuard] },

      { path: 'budget', component: BudgetComponent, canActivate: [AuthGuard] },
      { path: 'budget-list', component: BudgetListComponent, canActivate: [AuthGuard] },
      { path: 'chart-of-account', component: ChartOfAccountComponent, canActivate: [AuthGuard] },
    ]
  }
];

bootstrapApplication(AppComponent, {
  providers: [
    provideStore(
      { auth: authReducer },
      { metaReducers: [localStorageSyncReducer] },
      // {[
      //   { provide: META_REDUCERS, useValue: [localStorageSyncReducer], multi: true }
      // ]}
    ),
    provideRouter(routes),
    provideHttpClient(withInterceptors([AuthInterceptor])),
    provideAnimations(), // Required for Toastr animations
    provideToastr(), // Provides Toastr globally
    ConfigService,
    {
        provide: APP_INITIALIZER,
        useFactory: (config: ConfigService) => () => config.loadConfig(),
        deps: [ConfigService],
        multi: true
    },
    importProvidersFrom(BsDatepickerModule.forRoot()),
    // provideStore()
]
}).catch(err => console.error(err));
