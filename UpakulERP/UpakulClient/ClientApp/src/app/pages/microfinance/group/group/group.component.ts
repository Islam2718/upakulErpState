import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormsModule, FormGroup, Validators, ValidationErrors, ValidatorFn, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Button } from '../../../../shared/enums/button.enum';
import { Message } from '../../../../shared/enums/message.enum';
import { NgFor, CommonModule } from '@angular/common';
import { GroupService } from '../../../../services/microfinance/group/group.service'; // Adjust path if needed
import { CommonGlobalServiceService } from '../../../../services/generic/common-global-service.service';
import { GroupModel } from '../../../../models/microfinance/group/groupModel';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';


interface DropdownValues {
  text: string;
  value: string;
  selected: boolean;
}


@Component({
  selector: 'app-group',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './group.component.html',
  styleUrl: './group.component.css'
})


export class GroupComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  GroupId = '';
  isEditMode = false;
  successMessage = '';
  message = '';
  isPermitted: boolean = false;
  
  dropdownOfficeValues: DropdownValues[] = [];
  dropdownLedarMemberOptions: DropdownValues[] = [];
  dropdownGroupTypeValues: DropdownValues[] = [];
  dropdownDaysValues: DropdownValues[] = [];
  dropdownDivisionOptions: any[] = [];
  dropdownDistrictOptions: any[] = [];
  dropdownUpazillaOptions: any[] = [];
  dropdownUnionOptions: any[] = [];
  dropdownVillageOptions: any[] = [];

  dropdownOptions: any[] = [];
  selectedValue: any;
  button = Button;
  transactionDate: string | null = '';
  officeTypeId: number | null = null;
  officeType: string | null = null;

   ScheduleTypeDropdown = [
    { value: 'W', text: 'Weekly' },
    { value: 'M', text: 'Monthly' }
  ];

  // fromating date 
  ngAfterViewInit() {
    flatpickr('#datepickerValCalender', {
      dateFormat: 'd-M-Y',
      disableMobile: true,
      allowInput: true
    });
  }

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute

    private fb: FormBuilder,
    private apiService: GroupService,
    //private apiGeoLocationService: GeolocationService,
    private commonGlobalService: CommonGlobalServiceService,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      OfficeId: ['', Validators.required],
    //  GroupCode: ['-', Validators.required],
      GroupName: ['', Validators.required],
      GroupType: ['', Validators.required],
      ScheduleType: ['', Validators.required],
      //GroupLeaderMemberId: ['', Validators.required],   //['0', Validators.pattern(/^\d+$/)],
      OpeninigDate: ['', Validators.required],
      StartDate: ['', Validators.required],
      MeetingDay: [null],
      MeetingPlace: [''],
      DivisionId: ['', Validators.required],
      DistrictId: ['', Validators.required],
      UpazilaId: ['', Validators.required],
      UnionId: ['', Validators.required],
      VillageId: ['', Validators.required],
      Address: [''],
      Latitude: [''],
      Longitude: [''],
    });


  }

  ngOnInit() {
      const personalData = localStorage.getItem('personal');
    if (personalData) {
      const parsed = JSON.parse(personalData);
      this.isPermitted = parsed.office_type_id === 6;
    }
    const transDate = localStorage.getItem('transactionDate');
    if (transDate) {
      this.transactionDate = transDate;
    }


    // const personalData = localStorage.getItem('personal');
    // if (personalData) {
    //   const parsed = JSON.parse(personalData);
    //    this.isPermitted = parsed.office_type_id === 6;
    //   // this.officeTypeId = parsedData.office_type_id;
    //   // this.officeType = parsedData.office_type;
    // }

    this.dataForm = this.fb.group({
      GroupId: [null],
      ParentId: [null],
      OfficeId: [null],
     // GroupCode: ['-'],
      GroupName: [''],
      GroupType: [''],
      ScheduleType: [''],
      OpeninigDate: [''],
      StartDate: [''],
      //ClosingDate: [],
      MeetingDay: [0],
      MeetingPlace: [''],
      //GroupLeaderMemberId: [null],
      DivisionId: [null],
      DistrictId: [null],
      UpazilaId: [null],
      UnionId: [null],
      VillageId: [null],
      Address: [''],
      Latitude: [''],
      Longitude: ['']
    });

    //this.loadLederMemberDropDown();
    this.loadDivisionDropDown();
    this.loadGroupTypeDropDown();
    this.loadDaysDropDown();
    this.loadOfficeDropDown();

    const id = history.state.editId;
    if (id) {
      //console.log("EditId", id);
      this.isEditMode = true;
      this.apiService.getDataById(id).subscribe((res: any) => {
        const Group = res ?? res;

        StartDate: Group.startDate ? this.formatDate(Group.startDate) : ''

        if (Group) {
          this.dataForm.patchValue({
            GroupId: Group.groupId,
            ParentId:[null],
            OfficeId: Group.officeId,
            //GroupCode: Group.groupCode,
            GroupName: Group.groupName,
            GroupType: Group.groupType,
            ScheduleType: Group.scheduleType,
           // GroupLeaderMemberId: Group.GroupLeaderMemberId, //?Group.GroupLeaderMemberId:0,
            OpeninigDate: this.commonGlobalService.formatDate(Group.openinigDate ?? Group.openinigDate),  //this.formatDate(Group.openinigDate),
            // StartDate: this.commonGlobalService.formatDate(Group.startDate ?? Group.startDate), // this.formatDate(Group.startDate),
            StartDate: Group.startDate,
            MeetingDay: Group.meetingDay,
            MeetingPlace: Group.meetingPlace ? Group.meetingPlace : '',
            DivisionId: Group.divisionId,
            DistrictId: Group.districtId,
            UpazilaId: Group.upazilaId,
            UnionId: Group.unionId,
            VillageId: Group.villageId,
            Address: Group.address ? Group.address : '',
            Latitude: Group.latitude ? Group.latitude : '',
            Longitude: Group.longitude ? Group.longitude : ''
          });
          if (Group.divisionId != null) {
            this.loadDropdownDistrict(Group.divisionId);
          }
          if (Group.districtId != null) {
            this.loadDropdownUpazilla(Group.districtId);
          }
          if (Group.upazilaId != null) {
            this.loadDropdownUnion(Group.upazilaId);
          }
          if (Group.unionId != null) {
            this.loadDropdownVillage(Group.unionId);
          }

          this.dataForm.patchValue({ DistrictId: Group.districtId });
          this.dataForm.patchValue({ UpazilaId: Group.upazilaId });
          this.dataForm.patchValue({ UnionId: Group.unionId });
          this.dataForm.patchValue({ VillageId: Group.villageId });
        }
      });

    }

    this.dataForm.get('MeetingDay')?.disable();
    this.dataForm.get('ScheduleType')?.setValue('W');
  }

  formatDate(date: any): string {
    if (!date) return '';
    const d = new Date(date);
    return `${d.getFullYear()}-${(d.getMonth() + 1).toString().padStart(2, '0')}-${d.getDate().toString().padStart(2, '0')}`
  }

  loadOfficeDropDown() {
    //console.log('In loadOfficeDropDown');
    this.apiService.getOfficeDropDownData().subscribe({
      next: (data: DropdownValues[]) => {

        this.dropdownOfficeValues = data;
        // Set default to the item where selected == true
        const selectedOption = this.dropdownOfficeValues.find(opt => opt.selected);
        const defaultValue = selectedOption?.value ?? this.dropdownOfficeValues[0]?.value;
        this.dataForm.get('OfficeId')?.setValue(defaultValue);
      },
      error: (err: any) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }

  loadGroupTypeDropDown() {
    this.apiService.getGroupTypeDropdown().subscribe({
      next: (data: DropdownValues[]) => {
        this.dropdownGroupTypeValues = data;
        const selectedOption = this.dropdownGroupTypeValues.find(opt => opt.value == 'F');
        const defaultValue = selectedOption?.value ?? this.dropdownOfficeValues[0]?.value;
        this.dataForm.get('GroupType')?.setValue(defaultValue);
      },
      error: (err: any) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }

  loadDaysDropDown() {
    this.apiService.getDaysDropdown().subscribe({
      next: (data: DropdownValues[]) => {
        this.dropdownDaysValues = data;
      },
      error: (err: any) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  loadDivisionDropDown() {
    this.apiService.getGeoLocationDropDownData().subscribe({
      next: (data: any[]) => {
        this.dropdownDivisionOptions = data;
      },
      error: (err: any) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  loadLederMemberDropDown() {
    this.apiService.getLederMemberDropDownData().subscribe({
      next: (data: DropdownValues[]) => {
        this.dropdownLedarMemberOptions = data;
      },
      error: (err: any) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }


  DivisionOnChange(event: any) {
    const dropdownValue = event.target.value;
    this.loadDropdownDistrict(dropdownValue);

    this.dataForm.patchValue({ UpazilaId: '' });
    this.dataForm.patchValue({ UnionId: '' });
    this.dataForm.patchValue({ VillageId: '' });
    this.dataForm.controls['ParentId'].setValue(dropdownValue);
  }

  DistrictOnChange(event: any) {
    const dropdownValue = event.target.value;
    this.loadDropdownUpazilla(dropdownValue);
    this.dataForm.controls['ParentId'].setValue(dropdownValue);
  }

  UpazillaOnChange(event: any) {
    const dropdownValue = event.target.value;
    this.loadDropdownUnion(dropdownValue);
    this.dataForm.controls['ParentId'].setValue(dropdownValue);
  }

  UnionOnChange(event: any) {
    const dropdownValue = event.target.value;
    this.loadDropdownVillage(dropdownValue);
    this.dataForm.controls['ParentId'].setValue(dropdownValue);
  }

  VillageOnChange(event: any) {
    const dropdownValue = event.target.value;
    this.dataForm.controls['ParentId'].setValue(dropdownValue);
  }

  loadDropdownDivision(id: number | undefined) {
    this.apiService.getGeoLocationDropDownData().subscribe({
      next: (data: any[]) => {
        this.dropdownDivisionOptions = data;
        //     console.log(this.dropdownOptions);
      },
      error: (error: any) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        //    console.log('Dropdown data fetching completed.');
      }
    });
  }

  //District Dropdown
  loadDropdownDistrict(id: number) {
    this.apiService.getGeoLocationDropDownSubData(id).subscribe({
      next: (data: any[]) => {
        this.dropdownDistrictOptions = data;
        //console.log("----",this.dropdownOptions);
      },
      error: (error: any) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        //   console.log('Dropdown data fetching completed.');
      }
    });
  }

  //Dropdown Upazilla
  loadDropdownUpazilla(id: number) {
    this.apiService.getGeoLocationDropDownSubData(id).subscribe({
      next: (data: any[]) => {
        this.dropdownUpazillaOptions = data;
        //     console.log(this.dropdownOptions);
      },
      error: (error: any) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        // console.log('Dropdown data fetching completed.');
      }
    });
  }

  //District Union
  loadDropdownUnion(id: number) {
    this.apiService.getGeoLocationDropDownSubData(id).subscribe({
      next: (data: any[]) => {
        this.dropdownUnionOptions = data;
        //  console.log(this.dropdownOptions);
      },
      error: (error: any) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        //  console.log('Dropdown data fetching completed.');
      }
    });
  }

  //Dropdown Village
  loadDropdownVillage(id: number) {
    this.apiService.getGeoLocationDropDownSubData(id).subscribe({
      next: (data: any[]) => {
        this.dropdownVillageOptions = data;
        //  console.log(this.dropdownOptions);
      },
      error: (error: any) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        // console.log('Dropdown data fetching completed.');
      }
    });
  }

  ScheduleTypeOnChange(event: any): void {
    const selectedScheduleType = event.target.value; 
    var startDateValue = this.dataForm.get('StartDate')?.value;
    this.onDateChange(startDateValue, selectedScheduleType);
  }

  onDateChange(startDateParam?:any, scheduleTypeParam?:any){
        // console.log("in onDateChange");

        // var getSchType = this.dataForm.get('ScheduleType')?.value;
        // console.log("getSchType", getSchType);
        // const startDateValueVar = this.dataForm.get('StartDate')?.value;
        // console.log("startDateValueVar", startDateValueVar);
        var startDateValue = this.dataForm.get('StartDate')?.value;
        if(startDateParam !='')
        {
            startDateValue = startDateParam;
        }

        //ScheduleType
        var scheduleTypeValue = this.dataForm.get('ScheduleType')?.value;
        if(scheduleTypeParam !='')
        {
            scheduleTypeValue = scheduleTypeParam;
        }
        

    if (scheduleTypeValue === 'W') {
      
      console.log("startDateValue",startDateValue);
      if (startDateValue) {
        const selectedDate = new Date(startDateValue);

        let dayNumber = selectedDate.getDay();
        console.log("dayNumber", dayNumber);
       if (dayNumber === 5 || dayNumber === 6) 
        { //⚠️ 
            this.toastr.warning("Selected day is non-working day (Friday or Saturday)", 'Warning');
            this.dataForm.patchValue({
            StartDate: '',
            MeetingDay: ''
          });
        }else{
          const dayName = selectedDate.toLocaleDateString('en-US', { weekday: 'long' });
          console.log(dayName);
          const matchedDay = this.dropdownDaysValues.find(d => d.text === dayName);
          if (matchedDay) {
            this.dataForm.get('MeetingDay')?.setValue(matchedDay.value);
          }
        }

        
      }
    } else {
      const startDateValue = this.dataForm.get('StartDate')?.value;
      this.dataForm.get('MeetingDay')?.setValue('');
    }
  }


  onDateChangeByStartDate(event: any) {
  const selectedDate = event.target.value;   // raw string date: '2025-08-18'
  console.log("Selected Date:", selectedDate);
  var scheduleTypeValue = this.dataForm.get('ScheduleType')?.value;
  this.onDateChange(selectedDate, scheduleTypeValue);

  // If you want it as a Date object
  //const formattedDate = new Date(selectedDate);
  //console.log("Formatted Date:", formattedDate);
}





  removeLeadingSpace(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.value.startsWith(' ')) {
      input.value = input.value.trimStart();
      this.dataForm.get('geoLocationName')?.setValue(input.value, { emitEvent: false });
    }
  }

  noLeadingSpaceValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as string;
      if (value && value.startsWith(' ')) {
        return { leadingSpace: true };
      }
      return null;
    };
  }




  onSubmit() {
    //console.log(this.dataForm.value);
    
    // const startDateRaw = this.dataForm.get('StartDate')?.value;
    // if (startDateRaw) {
    //   const isoDate = new Date(startDateRaw).toISOString(); // "2025-07-08T00:00:00.000Z"
    //   this.dataForm.get('StartDate')?.setValue(isoDate);
    // }


    // const openDateRaw = this.dataForm.get('OpeninigDate')?.value;
    // if (openDateRaw) {
    //   const isoDate = new Date(openDateRaw).toISOString(); // "2025-07-08T00:00:00.000Z"
    //   this.dataForm.get('OpeninigDate')?.setValue(isoDate);
    // }
    
    
    const formData = this.dataForm.getRawValue();
    //console.log("--formData--", formData);

    if (formData.ScheduleType == 'M') {
        formData.MeetingDay = null;
    }

    if (!formData.MeetingPlace) {
        formData.MeetingPlace = null;
      }
      if (!formData.Address) {
        formData.Address = null;
      }
      if (!formData.Latitude) {
        formData.Latitude = null;
      }
      if (!formData.Longitude) {
        formData.Longitude = null;
      }
      if (!formData.ScheduleType) {
        formData.MeetingDay = null;
      }



    if (this.dataForm.valid) {
      this.isSubmitting = true;

      if (this.isEditMode) {
        console.log("UpdateformData", formData);

        this.apiService.UpdateData(formData).subscribe({
          next: (response: { message: string | undefined; }) => {
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/microfinance/Group/Group-list']);
          },
          error: (error: { type: string; message: string | undefined; }) => {
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      } else {
        console.log("AddformData", formData);
        this.apiService.addData(formData).subscribe({
          next: (response: { message: string | undefined; }) => {
            this.toastr.success(response.message, 'Success');
            this.dataForm.reset();
            console.log(response);
          },

          error: (err) => {
            //: { type: string; message: string | undefined; }
            // console.log("err", err);
            // console.log("ErrorArray",err.errors[""]);
            // Check if errors exist in this format
            // console.log(" err.error.errors",  err.error.errors);



            //     console.log("Full HttpErrorResponse:", err);

                // if (err.error && err.error.errors) {
                //   const errorObj = err.error.errors;

                //   // loop through each key ("" or field names if provided)
                //   for (const key in errorObj) {
                //     if (errorObj.hasOwnProperty(key)) {
                //       const messages: string[] = errorObj[key];
                //       messages.forEach(msg => {
                //         this.toastr.error(msg, "Validation Error"); // toast
                //         // Or: alert(msg);
                //         // Or: console.error(msg);
                //       });
                //     }
                //   }
                // } else {
                //   this.toastr.error("Something went wrong!", "Error");
                // }
        




                  // if (err.error && err.error.errors[""]) {
                  //   const errors: string[] = err.errors[""];
                  //   errors.forEach(msg => {
                  //     this.toastr.error(msg, "Validation Error");
                  //   });
                  // } else if (err.error && err.error.message) {
                  //   // fallback for single message responses
                  //   this.toastr.error(err.error.message, "Error");
                  // } else {
                  //   this.toastr.error("Something went wrong", "Error");
                  // }



            // console.log(error);
            if (err.type === 'warning') {
              this.toastr.warning(err.message, 'Warning');
            } else if (err.type === 'strongerror') {
              this.toastr.error(err.message, 'Error');
            } else {
              this.toastr.error(err.message);
            }
            this.isSubmitting = false;
          },
          complete: () => {
            this.isSubmitting = false;
          }
        });
      }
    }

  }

  filterToDigits(event: Event, controlName: string) {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
    this.dataForm.get(controlName)?.setValue(input.value);
  }

  navigateToList() {
    this.router.navigate(['mf/group/group-list']);
  }

  onReset(): void {
    this.dataForm.reset();
  }

}
