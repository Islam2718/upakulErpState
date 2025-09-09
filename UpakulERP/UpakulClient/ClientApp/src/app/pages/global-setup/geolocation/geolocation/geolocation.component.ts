import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormBuilder, FormGroup, FormsModule, Validators, ValidationErrors, ValidatorFn, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Button } from '../../../../shared/enums/button.enum';
import { NgFor, CommonModule } from '@angular/common';
import { GeolocationService } from '../../../../services/Global/geolocation/geolocation.service'; // Adjust path if needed
// import { Geotype } from '../../../../models/Global/geolist/geotype';
// import { GeotypeData } from '../../../../models/Global/geolist/geotype-data';
import { ToastrService } from 'ngx-toastr';
// import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { BtnService } from '../../../../services/btn-service/btn-service';
// import { single } from 'rxjs';

interface DropdownValues {
  text: string;
  value: string;
}

@Component({
  selector: 'app-geolocation',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './geolocation.component.html',
  styleUrl: './geolocation.component.css'
})


export class GeolocationComponent implements OnInit {
    qry: string | null = null;
  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  isEditMode = false;
  message = '';

  GeoTypeValues: DropdownValues[] = [];
  dropdownDivisionOptions: any[] = [];
  dropdownDistrictOptions: any[] = [];
  dropdownUpazillaOptions: any[] = [];
  dropdownUnionOptions: any[] = [];

  dropdownOptions: any[] = [];
  selectedValue: any;
  showDivision = false;
  showDistrict = false;
  showUpazilla = false;
  showUnion = false;
  // showCode = false;
  // showName = false;
  //showVillage = false;

  button = Button; // Access Button Text Enum

  constructor(
      public Button: BtnService,   
    private fb: FormBuilder,
    private apiService: GeolocationService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
      geoLocationType: ['', Validators.required],
      parentId: ['', Validators.required],
      geoLocationId: ['', Validators.required],
      geoLocationCode: ['', Validators.required, this.noLeadingSpaceValidator()],
      geoLocationName: ['', Validators.required, this.noLeadingSpaceValidator()]
    });
  }

  ngOnInit() {
    this.dataForm = this.fb.group({
      geoLocationId: [null],
      geoLocationType: [1], //default division 
      geoLocationCode: [null],
      geoLocationName: [''],
      parentId: [null],

      geoLocationDivisionId: [null],
      geoLocationDistrictId: [null],
      geoLocationThanaId: [null],
      geoLocationUnionId: [null],
    });
    this.loadDropDown();
    
    const id = history.state.editId;

    if (id) {
      this.isEditMode = true;
      this.apiService.getSingleData(id).subscribe(res => {
        const singleData = res.data ?? res; // handle both wrapped and raw objects
       
        this.dataForm.controls['parentId'].setValue(singleData.parentId);
        console.log("Edit singleData", singleData);

        if (singleData) {
          this.dataForm.controls['geoLocationId'].setValue(singleData.geoLocationId);
          this.dataForm.controls['geoLocationType'].setValue(singleData.geoLocationType);
          this.dataForm.controls['geoLocationCode'].setValue(singleData.geoLocationCode);
          this.dataForm.controls['geoLocationName'].setValue(singleData.geoLocationName);
          this.dataForm.get('geoLocationType')?.disable();
         // this.dataForm.controls['parentId'].setValue(singleData.parentId);


          if (singleData.geoLocationDivisionId) {
            this.loadDropdownDivision();
            this.dataForm.controls['geoLocationDivisionId'].setValue(singleData.geoLocationDivisionId);
            this.showDivision = true;
            this.dataForm.get('geoLocationDivisionId')?.disable();
          }

          if (singleData.geoLocationDistrictId) {
            this.selectedOnChangeFunc(singleData.geoLocationDivisionId, 'district');
            this.dataForm.controls['geoLocationDistrictId'].setValue(singleData.geoLocationDistrictId);
            const event = { target: { value: singleData.geoLocationDistrictId } };
            this.showDistrict = true;
            this.dataForm.get('geoLocationDistrictId')?.disable();
          }

          if (singleData.geoLocationThanaId) {
            this.selectedOnChangeFunc(singleData.geoLocationDistrictId, 'thana');
            this.dataForm.controls['geoLocationThanaId'].setValue(singleData.geoLocationThanaId);
            this.showUpazilla = true;
            this.dataForm.get('geoLocationThanaId')?.disable();
          }

          if (singleData.geoLocationUnionId) {
            this.selectedOnChangeFunc(singleData.geoLocationThanaId, 'union');
            this.dataForm.controls['geoLocationUnionId'].setValue(singleData.geoLocationUnionId);
            this.showUnion = true;
            this.dataForm.get('geoLocationUnionId')?.disable();
          }
        }

        ////Hide Dropdown
        if(singleData.geoLocationType==1){
          this.showDivision = false;
          this.dataForm.get('geoLocationDistrictId')?.clearValidators();
          this.dataForm.get('geoLocationDistrictId')?.updateValueAndValidity();
          
          //this.dataForm.get('geoLocationDivisionId')?.disable();
        }

        if(singleData.geoLocationType==2){
          this.showDistrict = false;
          this.dataForm.get('geoLocationThanaId')?.clearValidators();
          this.dataForm.get('geoLocationThanaId')?.updateValueAndValidity();
          //this.dataForm.get('geoLocationDistrictId')?.disable();
        }

        if(singleData.geoLocationType==3){
          this.showUpazilla = false;
          this.dataForm.get('geoLocationUnionId')?.clearValidators();
          this.dataForm.get('geoLocationUnionId')?.updateValueAndValidity();
          //this.dataForm.get('geoLocationThanaId')?.disable();
        }

        if(singleData.geoLocationType==4){
          this.showUnion = false;
         // this.dataForm.get('geoLocationUnionId')?.disable();
        }
      });
    }
  }

  loadDropDown() {
    this.apiService.getGeoLocationTypeDropdown().subscribe({
      next: (data) => {
        this.GeoTypeValues = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  // type Droped action 
  onDropDownGeoTypeChange(event: any) {
    this.resetDropdown();
    
    this.dataForm.controls['geoLocationDivisionId'].setValue('');
    this.dataForm.controls['geoLocationDistrictId'].setValue('');
    this.dataForm.controls['geoLocationThanaId'].setValue('');
    this.dataForm.controls['geoLocationUnionId'].setValue('');

    const selectedGeoType = event.target.value;
    this.loadDropdownDivision();

    // this.showCode = selectedGeoType === '1' || selectedGeoType === '2' || selectedGeoType === '3' || selectedGeoType === '4' || selectedGeoType === '5';
    // this.showName = selectedGeoType === '1' || selectedGeoType === '2' || selectedGeoType === '3' || selectedGeoType === '4' || selectedGeoType === '5';
    this.showDivision = selectedGeoType === '2' || selectedGeoType === '3' || selectedGeoType === '4' || selectedGeoType === '5';
    this.showDistrict = selectedGeoType === '3' || selectedGeoType === '4' || selectedGeoType === '5';
    this.showUpazilla = selectedGeoType === '4' || selectedGeoType === '5';  //|| selectedGeoType === '4'
    this.showUnion = selectedGeoType === '5';

    console.log("Selected Geo Type: ", selectedGeoType); // Debugging log

    const parentId =
      selectedGeoType === '3' ? 1 :
        selectedGeoType === '4' ? 3 :
          selectedGeoType === '5' ? 4 :
            selectedGeoType === '6' ? 5 : null;

   // console.log("in onDropDownGeoTypeChange parentId", parentId);
   // this.dataForm.controls['parentId'].setValue(parentId); // ✅ Update form control
  }

  // Master dropdown selected event function
  selectedOnChangeFunc(eventOrId: any, hookType: string) {
    //this.clearDistrictDropdown();
    


    console.log("in selectedOnChangeFunc", eventOrId, hookType);
    let id: any;

    if (typeof eventOrId === 'object' && eventOrId.target) {
      id = eventOrId.target.value; // HTML change event
      console.log(id);
      this.dataForm.controls['parentId'].setValue(id); // ✅ Update form control
    } else {
      id = eventOrId; // ID passed from code (edit mode)
    }

    if (!id) return;

    this.apiService.getDropDownSubData(id).subscribe({
      next: (data) => {
        // console.log('_setData_', data);
        if (hookType === 'district') {  
          this.dropdownDistrictOptions = [];
          this.dropdownDistrictOptions = data; 
        }
        if (hookType === 'thana') { 
          this.dropdownUpazillaOptions = [];
          this.dropdownUpazillaOptions = data; 
        }
        if (hookType === 'union') { 
          this.dropdownUnionOptions = [];
          this.dropdownUnionOptions = data; 
        }        
      },
      error: (error) => { console.error('Error fetching dropdown data:', error); }
    });
  }

  loadDropdownDivision() {
    this.apiService.getDropDownData().subscribe({
      next: (data) => {
        this.dropdownDivisionOptions = data;
      },
      error: (error) => {
        console.error('Error fetching dropdown data:', error);
      },
      complete: () => {
        // console.log('Dropdown data fetching completed.');
      }
    });
  }

  clearDistrictDropdown() {
    this.dropdownDistrictOptions = []; // clear options if needed
    this.dataForm.get('geoLocationDistrictId')?.setValue('');
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
    console.log("onSubmit this.dataForm.getRawValue()", this.dataForm.getRawValue());

    if (this.dataForm.valid) {
      this.isSubmitting = true;
      if (this.isEditMode) {
        // console.log("Form Value", this.dataForm.value);
        // const selectedId = this.dataForm.getRawValue().geoLocationType;
        // console.log("LocationType", selectedId);
        console.log("this.dataForm.getRawValue()", this.dataForm.getRawValue());
        this.apiService.UpdateData(this.dataForm.getRawValue()).subscribe({
          next: (response) => {
            console.log('_response', response);
            this.toastr.success(response.message, 'Success');
            this.router.navigate(['/gs/global/geolist']);
          },
          error: (error) => {            
            //console.log('__error', error);
            if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message);
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
        this.apiService.addGeoLocation(this.dataForm.value).subscribe({
          next: (response) => {
            this.toastr.success(response.message, 'Success');
           // this.dataForm.reset();

            this.dataForm.get('geoLocationCode')?.setValue('');
            this.dataForm.get('geoLocationName')?.setValue('');
            this.isSubmitting = true;
            console.log("in addGeoLocation");
          },
          error: (error) => {
             if (error.type === 'warning') {
              this.toastr.warning(error.message, 'Warning');
            } else if (error.type === 'strongerror') {
              this.toastr.error(error.message, 'Error');
            } else {
              this.toastr.error(error.message);
            }
            this.isSubmitting = false;
            // debugger
            // if (error.type === 'warning') {
            //   this.toastr.warning(error.error.message);
            // } else if (error.type === 'strongerror') {
            //   this.toastr.error(error.message, 'Error');
            // } else {
            //   this.toastr.error(error.message);
            // }
            // this.isSubmitting = false;
          },
          complete: () => { this.isSubmitting = false; }
        });
      }
    }
  }

  navigateToList() {
    this.router.navigate(['gs/global/geolist']);
  }

  resetDropdown(): void{
    this.dropdownDivisionOptions = [];
    this.dropdownDistrictOptions = [];
    this.dropdownUpazillaOptions = [];
    this.dropdownUnionOptions = [];
  }

  onReset(): void {
    this.resetDropdown();
    // this.dropdownDivisionOptions = [];
    // this.dropdownDistrictOptions = [];
    // this.dropdownUpazillaOptions = [];
    // this.dropdownUnionOptions = [];
    this.dataForm.reset();
    this.dataForm.controls['geoLocationType'].setValue(1);
  }
}