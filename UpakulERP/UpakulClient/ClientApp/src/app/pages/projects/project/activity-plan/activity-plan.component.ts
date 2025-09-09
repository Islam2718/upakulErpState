import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { BtnService } from '../../../../services/btn-service/btn-service';
import { ProjectService } from '../../../../services/Projects/project.service';
import flatpickr from 'flatpickr';
import { fromEvent } from 'rxjs';

interface ActivityPlan {
  activityName: string;
  activityFrom: string;
  activityTo: string;
  reportingDate: string;
  targetType: string;
  monthlyTarget: number | null;
  totalTarget: number | null;
  programParticipantsTarget: number | null;
  beneficiaryType: string;
  programParticipants_U_18_Boys: number | null;
  programParticipants_U_18_Girls: number | null;
  programParticipants_18_59_Male: number | null;
  programParticipants_18_59_Female: number | null;
  programParticipants_Up_59_Male: number | null;
  programParticipants_Up_59_Female: number | null;
  programParticipants_Disable_Male: number | null;
  programParticipants_Disable_Female: number | null;
  programParticipantsEthnicityandMarginalized: number | null;
  womenHeadedProgramParticipants: number | null;
  transgenderProgramParticipants: number | null;
}
interface ProjectDropDown {
  text: string;
  value: string;
  selected: boolean;
}

@Component({
  selector: 'app-activity-plan',
  standalone: true, // mark standalone
  imports: [FormsModule, CommonModule], //include FormsModule for ngModel
  templateUrl: './activity-plan.component.html',
  styleUrls: ['./activity-plan.component.css'],
})
export class ActivityPlanComponent {
  projectList: ProjectDropDown[] = [];
  activityPlanList: ActivityPlan[] = [];
  targetTypeList: ProjectDropDown[] = [];
  // ProjectService: any;

  constructor(
    public Button: BtnService,
    private projectService: ProjectService
  ) {
    this.searchProject();
    this.loadTargetTypes();
  }

  ngAfterViewInit() {
    flatpickr('.dtpickr', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }

  searchProject() {
    this.projectService.getProjectDropdown().subscribe({
      next: (data: ProjectDropDown[]) => {
        console.log('API Response:', data);
        this.projectList = data;
      },
      error: (err: any) => {
        console.error('Failed to load project dropdown:', err);
      },
    });
  }  

  loadTargetTypes() {
    this.projectService.getProjectTargetTypes().subscribe({
      next: (data: ProjectDropDown[]) => {
        console.log('Target types:', data);
        this.targetTypeList = data;
      },
      error: (err: any) => {
        console.error('Failed to load target types:', err);
      },
    });
  }

  getActivityListFunc(event: any) {
    // this.activityPlanList = [];
    // console.log('_this_is_event:', event);
    this.projectService
      .getProjectActivityByProjectId(event.target.value)
      .subscribe({
        next: (data: ProjectDropDown[]) => {
          console.log('Project activities:', data);
          // this.projectList = data;
        },
        error: (err: any) => {
          console.error('Failed to load project activities:', err);
        },
      });
  }

  addRow() {
    // const today = new Date();
    this.activityPlanList.push({
      activityName: '',
      activityFrom: '', //formatDate(fromEvent., 'dd-MMM-yyyy', 'en-US'),
      activityTo: '',
      reportingDate: '',
      targetType: '',
      monthlyTarget: 0,
      totalTarget: 0,
      programParticipantsTarget: 0,
      beneficiaryType: '',
      programParticipants_U_18_Boys: 0,
      programParticipants_U_18_Girls: 0,
      programParticipants_18_59_Male: 0,
      programParticipants_18_59_Female: 0,
      programParticipants_Up_59_Male: 0,
      programParticipants_Up_59_Female: 0,
      programParticipants_Disable_Male: 0,
      programParticipants_Disable_Female: 0,
      programParticipantsEthnicityandMarginalized: 0,
      womenHeadedProgramParticipants: 0,
      transgenderProgramParticipants: 0,
    });
  }

  removeRow(index: number) {
    this.activityPlanList.splice(index, 1);
  }
  // clean all vars
  clean() {
    this.projectList = [];
    this.activityPlanList = [];
    this.searchProject();
  }
}
