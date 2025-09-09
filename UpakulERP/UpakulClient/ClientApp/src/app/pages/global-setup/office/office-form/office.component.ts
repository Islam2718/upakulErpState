import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormControl, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CommonGlobalServiceService } from '../../../../services/generic/common-global-service.service';
import { Button } from '../../../../shared/enums/button.enum';
import { Message } from '../../../../shared/enums/message.enum';
import { OfficeService } from '../../../../services/Global/office/office.service'; // Adjust path if needed
import { Office } from '../../../../models/Global/office/office';
import { ToastrService } from 'ngx-toastr';
import flatpickr from 'flatpickr';
import { BtnService } from '../../../../services/btn-service/btn-service';

interface OfficeType {
  text: string;
  value: string;
}
interface PrincipalType {
  text: string;
  value: string;
}
interface ZonalType {
  text: string;
  value: string;
}
interface RegionalType {
  text: string;
  value: string;
}
interface AreaType {
  text: string;
  value: string;
}

@Component({
  selector: 'app-office',
  standalone: true, // Allow the component to be used without a module
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './office.component.html',
  styleUrls: ['./office.component.css'],
  // flatpickrOptions: any = {
  //   dateFormat: 'd-M-Y', // dd-mm-yyyy
  //   disableMobile: true,
  //   allowInput: true
  // }
})
export class OfficeComponent implements OnInit {
  qry: string | null = null;
  officeForm!: FormGroup;
  isSubmitting = false;
  officeTypes: OfficeType[] = [];
  principalTypes: PrincipalType[] = [];
  zonalTypes: ZonalType[] = [];
  regionalTypes: RegionalType[] = [];
  areaTypes: AreaType[] = [];
  showPrincipal = false;
  showZonal = false;
  showRegional = false;
  showArea = false;
  showAreaFields = true;
  isEditMode = false;
  button = Button; // Access Button Text Enum
  message = '';

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
    private fb: FormBuilder, private route: ActivatedRoute,
    private officeService: OfficeService,
    private toastr: ToastrService,
    private commonGlobalService: CommonGlobalServiceService,
    public router: Router,
    private activeatedRoute: ActivatedRoute
  ) {
    this.officeForm = this.fb.group({
      OfficeType: ['', Validators.required],

      PrincipalOfficeId:   ['', Validators.required],
      RegonalOfficeId:   ['', Validators.required],
      ZonalOfficeId:   ['', Validators.required],
      AreaOfficeId:   ['', Validators.required],



      // principalOfficeId: ['', Validators.required],
      // OfficeZonalId: ['', Validators.required],
      // OfficeRegionalId: ['', Validators.required],
      // OfficeAreaId: ['', Validators.required],
      OfficeCode: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],
      OfficeShortCode: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],      
      OfficeName: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],
      OfficeAddress: ['', Validators.required],
      OperationStartDate: ['', Validators.required],
      OfficeEmail: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],
      OfficePhoneNo: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],
      Latitude: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],
      Longitude: ['', Validators.required, this.commonGlobalService.noLeadingSpaceValidator()],
      ParentId: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.officeForm = this.fb.group({ 
      OfficeId: [null],
      OfficeType: [null],

      PrincipalOfficeId:  [null],
      RegonalOfficeId:  [null],
      ZonalOfficeId:  [null],
      AreaOfficeId:  [null],
      // OfficePrincipalId: [null],
      // OfficeZonalId: [null],
      // OfficeRegionalId: [null],
      // OfficeAreaId: [null],
      OfficeCode: [''],
      OfficeShortCode: [''],
      OfficeName: [''],
      OfficeAddress: [''],
      OperationStartDate: [''],
      OfficeEmail: [''],
      OfficePhoneNo: [''],
      Latitude: [''],
      Longitude: [''],
      ParentId: [''],
      principal: [null],
      zonal: [null],
      regional: [null],
      area: [null],
    });
    
    this.loadOfficeTypes();
    this.loadPrincipalTypes();

    const id = history.state.editId;
    if (id) {
      //console.log(id);
      this.isEditMode = true;
      this.officeService.getOfficeById(id).subscribe(res => {
        const restData = res ?? res; // handle both wrapped and raw objects
       // console.log('in isEditMode:', restData);
        if (restData) {
         // console.log('_response:', restData);
          // type, principal, zonal 
          // this.officeForm.get('officeType')?.setValue(restData.officeType);  // will select Banana  
         // this.officeForm.controls['OfficeType'].setValue(restData.officeType);

          this.officeForm.controls['OfficeId'].setValue(restData.officeId);
          this.officeForm.controls['ParentId'].setValue(restData.parentId);
          this.officeForm.controls['OfficeShortCode'].setValue(restData.officeShortCode);
          this.officeForm.controls['OfficeCode'].setValue(restData.officeCode);
          this.officeForm.controls['OfficeName'].setValue(restData.officeName);
          this.officeForm.controls['OfficeAddress'].setValue(restData.officeAddress);
          this.officeForm.controls['OperationStartDate'].setValue(restData.operationStartDate ? this.commonGlobalService.formatDate(restData.operationStartDate) : "");
          this.officeForm.controls['OfficeEmail'].setValue(restData.officeEmail);
          this.officeForm.controls['OfficePhoneNo'].setValue(restData.officePhoneNo);
          this.officeForm.controls['Latitude'].setValue(restData.latitude);
          this.officeForm.controls['Longitude'].setValue(restData.longitude);
          this.officeForm.controls['OfficeType'].setValue(restData.officeType);
         // this.officeForm.controls['principal'].setValue(restData.principalOfficeId);
          
         
          
          this.officeForm.get('OfficeType')?.disable();



          //this.officeForm.controls['OfficeType'].setValue(6);

          //this.officeForm.controls['principal'].setValue(1058); 
          // if(restData.officeType){
          //   this.loadOfficeTypes();
          //   console.log("OfficeType", restData.officeType);
          //   const event = { target: { value: restData.officeType } };
          //    //this.dataForm.controls['ParentId'].setValue(singleData.geoLocationDivisionId);
          //   // Call your method
          //   //this.onPrincipalOfficeChange(event);
          //   this.officeForm.controls['OfficeType'].setValue(restData.officeType);
          //  // this.showPrincipal=true;
          // }
                       
           if(restData.principalOfficeId){
            this.showPrincipal=true;

            //console.log("OfficePrincipalId", restData.officePrincipalId);
            //this.loadZonalTypes(restData.officePrincipalId);
            this.officeForm.controls['PrincipalOfficeId'].setValue(restData.principalOfficeId);
            
            this.officeForm.get('PrincipalOfficeId')?.disable();
            this.officeForm.get('PrincipalOfficeId')?.setValidators([Validators.required]);
            this.officeForm.get('PrincipalOfficeId')?.updateValueAndValidity();


          //   this.loadPrincipalTypes(); 
            // console.log("principalOffice", restData.principalOfficeId);
             //const event = { target: { value: restData.principalOfficeId } };
            // Call your method
            //this.loadZonalTypes(restData.principalOfficeId);
            // this.officeForm.get('principal')?.setValue(restData.principalOfficeId);

            //this.officeForm.controls['principal'].setValue(restData.principalOfficeId);  //restData.principalOfficeId
          //   console.log('_printcipal_offic_id_', restData.principalOfficeId);
          //   this.officeForm.patchValue({ principal: restData.principalOfficeId });
             
          }

          if(restData.zonalOfficeId){
            this.showZonal=true;
          //  console.log("zonalOffice", restData.zonalOfficeId);
            //this.loadRegionalTypes(restData.officeZonalId);
            this.loadZonalTypes(restData.principalOfficeId);

           // const event = { target: { value: restData.zonalOfficeId } };
            // Call your method
           // this.loadZonalTypes(event.target.value);
            this.officeForm.controls['ZonalOfficeId'].setValue(restData.zonalOfficeId);
            this.officeForm.get('ZonalOfficeId')?.disable();


            this.officeForm.get('ZonalOfficeId')?.setValidators([Validators.required]);
            this.officeForm.get('ZonalOfficeId')?.updateValueAndValidity();
           // this.officeForm.patchValue({ zonal: restData.zonalOfficeId });            
          }

           
          if(restData.regonalOfficeId){
            this.showRegional=true;
           // this.loadAreaTypes(restData.officeRegionalId);
            this.loadRegionalTypes(restData.zonalOfficeId);
          //  console.log("regionalOffice", restData.regonalOfficeId);
           // const event = { target: { value: restData.regionalOfficeId } };
            // Call your method
           // this.loadRegionalTypes(event.target.value);
            this.officeForm.controls['RegonalOfficeId'].setValue(restData.regonalOfficeId);
            this.officeForm.get('RegonalOfficeId')?.disable();


            this.officeForm.get('RegonalOfficeId')?.setValidators([Validators.required]);
            this.officeForm.get('RegonalOfficeId')?.updateValueAndValidity();
          }

           if(restData.areaOfficeId){
            this.showArea=true;
            //console.log("areaOffice", restData.areaOfficeId);
            this.loadAreaTypes(restData.regonalOfficeId);
            // const event = { target: { value: restData.areaOfficeId } };
            // Call your method
            // this.loadRegionalTypes(event.target.value);
            this.officeForm.controls['AreaOfficeId'].setValue(restData.areaOfficeId);
            this.officeForm.get('AreaOfficeId')?.disable();


            this.officeForm.get('AreaOfficeId')?.setValidators([Validators.required]);
            this.officeForm.get('AreaOfficeId')?.updateValueAndValidity();
          }




          // if(restData.officeType == 6){
          //   this.showArea = false;
          // }

          if(restData.officeType == 5){
            this.showArea = false;
          }

          if(restData.officeType == 4){
            this.showRegional = false;
          }

          if(restData.officeType == 3){
            this.showZonal = false;
          }



        }
      });
    }
  }


  loadOfficeTypes() {
    this.officeService.getOfficeTypes().subscribe({
      next: (data) => {
        this.officeTypes = data;
        //  console.log(this.officeTypes)
      },
      error: (err) => {
        console.error(this.message + ' Office Types:', err);
      }
    });
  }

  loadPrincipalTypes() {
    this.officeService.getOfficeByParentId().subscribe({
      next: (data) => {
        this.principalTypes = data;
        //  console.log(this.principalTypes)
      },
      error: (err) => {
       // console.error(this.message.FetchErr+' Principal Types:', err);
      }
    });
  }

  loadZonalTypes(principalId: number) {
    console.log("principalId", principalId);
    this.officeService.getOfficeByParentId(principalId).subscribe({
      next: (data) => {
        this.zonalTypes = data;
        //console.log("this.zonalTypes", this.zonalTypes);
      },
      error: (err) => {
       // console.error(this.message.FetchErr+' Zonal Types:', err);
      }
    });
  }

  loadRegionalTypes(zonalId: number) {
    console.log("zonalId", zonalId);
    this.officeService.getOfficeByParentId(zonalId).subscribe({
      next: (data) => {
        this.regionalTypes = data;
        //  console.log(this.regionalTypes)
      },
      error: (err) => {
        console.error(this.message + ' Regional Types:', err);
      }
    });
  }

  loadAreaTypes(regionalId: number) {
    this.officeService.getOfficeByParentId(regionalId).subscribe({
      next: (data) => {
        this.areaTypes = data;
       // console.log(this.areaTypes)
      },
      error: (err) => {
      console.error(this.message + ' Area Types:', err);
      }
    });
  }

  onOfficeTypeChange(event: any) {
    // Reset all dropdown fields when OfficeType changes
    this.officeForm.patchValue({
      PrincipalOfficeId: [null],
      ZonalOfficeId: [null],
      RegonalOfficeId: [null],
      AreaOfficeId: [null],
      OperationStartDate: this.commonGlobalService.getCurrentDate()     //new Date().toISOString().split('T')[0], // Defaults to today's date
    });

    this.areaTypes = [];
    this.regionalTypes = [];
    this.zonalTypes = [];
    const selectedOfficeType = event.target.value; // Get selected value correctly

    this.showPrincipal = selectedOfficeType === '3' || selectedOfficeType === '4' || selectedOfficeType === '5' || selectedOfficeType === '6';
    this.showZonal = selectedOfficeType === '4' || selectedOfficeType === '5' || selectedOfficeType === '6';
    this.showRegional = selectedOfficeType === '5' || selectedOfficeType === '6';
    this.showArea = selectedOfficeType === '6';
    // this.showAreaFields = selectedOfficeType === 'area' || selectedOfficeType === 'branch';
    //console.log("Selected Office Type: ", selectedOfficeType); // Debugging log
    const parentId =
      selectedOfficeType === '3' ? 3 :
        selectedOfficeType === '4' ? 4 :
          selectedOfficeType === '5' ? 5 :
            selectedOfficeType === '6' ? 6 : null;

    this.officeForm.controls['ParentId'].setValue(parentId); // ✅ Update form control

  }

  onPrincipalOfficeChange(event: any) {
    const selectedType = event.target.value;
    console.log("selectedType", selectedType);
    this.loadZonalTypes(selectedType);
    this.regionalTypes = [];
    this.areaTypes = [];
    this.officeForm.controls['ParentId'].setValue(selectedType);
  }

  onZonalOfficeChange(event: any) {
    const selectedType = event.target.value;
    console.log("selectedType", selectedType);
    this.loadRegionalTypes(selectedType);
    this.areaTypes = [];
    this.officeForm.controls['ParentId'].setValue(selectedType);
  }

  onRegionalOfficeChange(event: any) {
    const selectedType = event.target.value;
    this.loadAreaTypes(selectedType);
    this.officeForm.controls['ParentId'].setValue(selectedType);
  }

  onAreaOfficeChange(event: any) {
    const selectedType = event.target.value;
    this.officeForm.controls['ParentId'].setValue(selectedType);
  }

removeLeadingSpace(form: FormGroup, controlName: string, event: Event) {
  const input = event.target as HTMLInputElement;
  if (input.value.startsWith(' ')) {
    const trimmed = input.value.trimStart();
    input.value = trimmed;
    form.get(controlName)?.setValue(trimmed, { emitEvent: false });
  }
}



  onSubmit() {
    if (this.officeForm.valid) {
     // console.log("-----",this.officeForm.value);
     this.isSubmitting = true;

    //  let OperationStartDate =  this.commonGlobalService.formatDateUS(this.officeForm.value.OperationStartDate);
    //  this.officeForm.patchValue({ OperationStartDate });
    //  console.log("OperationStartDate", OperationStartDate);

     //var officeFormatFateUS = this.commonGlobalService.formatDateUS(this.officeForm.value)

      if (this.isEditMode) {
        //console.log("in isEditMode", this.officeForm.value);
        var formVal = this.officeForm.getRawValue();

        this.officeService.Update(formVal).subscribe({
          next: (response) => {
                 this.message = response.message;
                 this.toastr.success(this.message, 'Success');  
                 this.officeForm.reset();
                 this.router.navigate(['gs/office/office-list']);            
          },
          error: (error) => {
            // this.message = error.message;
            // this.toastr.error(this.message);

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
            // this.isSubmitting = false;
            // this.zonalTypes = [];
            // this.regionalTypes = [];
            // this.areaTypes = [];
            // this.showPrincipal = false;
            // this.showZonal = false;
            // this.showRegional = false;
            // this.showArea = false;
            this.isSubmitting = false;
          }
        });
        
        
        
        // (() => {
          
        //   this.toastr.success(this.message, 'Success');
        //   this.router.navigate(['gs/office/office-list']);
        // });
      } else {
       //console.log("in AddMode");

       var formVal = this.officeForm.getRawValue();
        //console.log('Before Add');                
        this.officeService.addOffice(this.officeForm.value).subscribe({
          next: (response) => {
                 this.message = response.message;
                 this.toastr.success(this.message, 'Success');         
                 this.officeForm.reset();
          },
          error: (error) => {
            this.message = error.message;
            this.toastr.error(this.message);
          },
          complete: () => {
            this.isSubmitting = false;
            this.zonalTypes = [];
            this.regionalTypes = [];
            this.areaTypes = [];
            this.showPrincipal = false;
            this.showZonal = false;
            this.showRegional = false;
            this.showArea = false;
          }
        });

      }

    }
  }

  navigateToList() {
    this.router.navigate(['gs/global/office-list']);
  }

  onReset(): void {
    this.officeForm.reset();
  }

}
