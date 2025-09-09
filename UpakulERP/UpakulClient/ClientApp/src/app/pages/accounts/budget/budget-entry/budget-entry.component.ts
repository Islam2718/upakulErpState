import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../../shared/enums/button.enum';
import { BudgetEntryService } from '../../../../services/accounts/budget/budget-entry.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { ComponentMFService } from '../../../../services/microfinance/components/componentMF/componentMF.service';

interface Dropdown {
  text: string;
  value: string;
}

export interface BudgetComponent {
  id: number;
  parentId: number;
  componentName: string;
  isMedical: boolean;
  isDesignation: boolean;
  labelShow: string;
}
@Component({
  selector: 'app-budget-entry',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule
  ],
  templateUrl: './budget-entry.component.html',
  styleUrl: './budget-entry.component.css'
})
export class BudgetEntryComponent implements OnInit{

  //tableData: TableRow[] = [];
  tableData: any[] = [];


  dataForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  isEditMode = false;

  fiscalYears: string[] = [];
  cachedFiscalYears: Dropdown[] = [];
  selectedFiscalYear = '';  

  offices: Dropdown[] = [];
  cachedOffices: Dropdown[] = [];
  selectedOffice: string = '';

  components: Dropdown[] = [];
  cachedComponents: Dropdown[] = [];
  selectedComponent: string = '';

componentsList: BudgetComponent[] = [];

selectedComponentId: number =0 ;
selectedOfficeId: number= 0 ;
selectedFinancialYear: string ="";
fiscal_startYear: string ="";
fiscal_endYear: string ="";

tableHeader: number =0;

constructor(
    private fb: FormBuilder, 
    private apiService: BudgetEntryService,
    private apiComponentService:ComponentMFService,
    private toastr: ToastrService, 
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {
    this.dataForm = this.fb.group({
        
    });
  }

  ngOnInit(): void {
    const today = new Date();
    const fiscalStartMonth = 3; // April (0-indexed, Jan = 0)
    const fiscalBaseYear = today.getMonth() >= fiscalStartMonth ? today.getFullYear() + 1 : today.getFullYear() + 1 ;
    
    
    console.log("fiscalBaseYear", fiscalBaseYear);
    
    const numberOfYears = 3;

    
    this.fiscalYears = Array.from({ length: numberOfYears }, (_, i) => {
      const startYear = fiscalBaseYear - i;
      console.log("startYear", startYear);
      const endYear = startYear - 1;
      return `${endYear}-${startYear}`;
    });


    this.LoadOfficeDropdown();
    this.LoadComponentsDropdown();

    console.log(this.offices);
    console.log(this.components);
    console.log(this.tableData);

    // this.CachedDropdown();
    
  }
 
  // CachedDropdown()
  // {
  //   this.cachedOffices = [...this.offices];
  //   this.cachedComponents = [...this.components]; 
  // }


  LoadComponentsDropdown(){
    //  this.apiService.getComponentDropdown().subscribe({
    //   next: (data) => this.components = data,
    //   error: (err) => console.error('Error loading components', err)
    // });

     this.apiComponentService.getLoanComponentDropdown().subscribe({
      next: (data) => {
        this.components = data;
        this.cachedComponents = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });


  }

  LoadOfficeDropdown() {
    this.apiService.getOfficeDropdown().subscribe({
      next: (data) => {
        this.offices = data;
        this.cachedOffices = data;
      },
      error: (err) => {
        console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownFiscalYearChange(event: any) {
    const selectedValue = event.target.value;
    this.selectedFinancialYear = selectedValue;  
    this.onResetFasicalYear();
    //this.offices = [...this.cachedOffices];
    //this.components = [...this.cachedComponents];
    console.log("onDropDownFiscalYearChange", this.selectedFinancialYear);
  }

  onDropDownOfficeChange(event: any) {
    const selectedValue = event.target.value;
    this.selectedOfficeId = selectedValue ;
    this.onResetOffice();
    console.log("onDropDownOfficeChange", this.selectedOfficeId);
  }

  onDropDownComponentChange(event: any) {
    const selectedValue = event.target.value;
    this.selectedComponentId = selectedValue ;
    this.onResetComponent();
    console.log("onDropDownComponentChange", this.selectedComponentId);
  }

  calculateTotal(row: any): number {
  return (row.noOfStaff || 0) + (row.noOfPiece || 0) + (row.noOfDay || 0) + 
         (row.perAmount || 0) + (row.totalAmount || 0) + (row.noOfGratuity || 0) +
         (row.basic || 0) + (row.other || 0) + (row.bonus || 0) +
         (row.pf || 0) + (row.gratuity || 0) + (row.medical || 0);
  }

calculateTotal2(row: any): number {
  return (
    (row.noOfStaff || 0) +
    (row.noOfPiece || 0) +
    (row.noOfDay || 0) +
    (row.perAmount || 0) +
    (row.totalAmount || 0) +
    (row.noOfGratuity || 0) +
    (row.basic || 0) +
    (row.other || 0) +
    (row.bonus || 0) +
    (row.pf || 0) +
    (row.gratuity || 0) +
    (row.medical || 0)
  );
}


  onSearch()
  {

    console.log("In Search");
    console.log("Table Data",this.tableData);
    // const fiscalYear = "2025-2026";
    const [startYear, endYear] = this.selectedFinancialYear.split('-');

    this.fiscal_startYear = startYear;
    this.fiscal_endYear = endYear;

    console.log("ID", this.selectedComponentId);
    
    this.tableHeader = this.selectedComponentId;
  //  this.apiService.getComponentListByParams(this.selectedFinancialYear, this.selectedOfficeId, this.selectedComponentId).subscribe(response => {
  //   this.tableData = response.data.map((item: any) => ({
  //               id: item.componentId,
  //               name: item.componentName,
  //               parentId: item.parentId,
  //               noOfStaff: item.noOfStaff ?? 0,
  //               noOfPiece: item.noOfPiece ?? 0,
  //               noOfDay: item.componentNoOfDay ?? 0,
  //               perAmount: item.componentPerAmount ?? 0,
  //               totalAmount: item.componentTotalAmount ?? 0,
  //               noOfGratuity: item.noOfGratuity ?? 0,
  //               basic: item.basic1 ?? 0,
  //               other: item.other1 ?? 0,
  //               bonus: item.bonus ?? 0,
  //               pf: item.pf ?? 0,
  //               gratuity: item.gratuity ?? 0,
  //               medical: item.medicalAllowance ?? 0
  //             }));
            
  //         });



        this.apiService.getComponentListByParams(
          this.selectedFinancialYear,
          this.selectedOfficeId,
          this.selectedComponentId
        ).subscribe(response => {
          this.tableData = response.data.map((item: any) => ({
            id: item.componentId,
            name: item.componentName,
            parentId: item.parentId,
            noOfStaff: item.noOfStaff ?? 0,
            noOfPiece: item.noOfPiece ?? 0,
            noOfDay: item.componentNoOfDay ?? 0,
            perAmount: item.componentPerAmount ?? 0,
            totalAmount: item.componentTotalAmount ?? 0,
            noOfGratuity: item.noOfGratuity ?? 0,
            basic: item.basic1 ?? 0,
            other: item.other1 ?? 0,
            bonus: item.bonus ?? 0,
            pf: item.pf ?? 0,
            gratuity: item.gratuity ?? 0,
            medical: item.medicalAllowance ?? 0,
            isDesignation: item.isDesignation ?? false,
            isMedical: item.isMedical ?? false,
            labelShow: item.labelShow
          }));
        });

        console.log("Table Data here", this.tableData);

  } 


  onReset()
  {
   // this.cachedOffices = [...this.offices];
  }

onResetFasicalYear(){
    //this.LoadOfficeDropdown();
    // this.LoadComponentsDropdown();
    //this.cachedOffices = [...this.offices];
    this.offices = [];
    this.components = [];

    //this.dataForm.get('officeId')?.reset();  
    //this.dataForm.get('componentId')?.reset();  
    
    //this.dataForm.get('OfficeDropdown')?.reset();
    //this.dataForm.get('ComponnetDropdown')?.reset();

    this.selectedOfficeId = 0;
    this.selectedComponentId = 0;

    this.offices = [...this.cachedOffices];
    this.components = [...this.cachedComponents];


    //this.offices = [];
   
    console.log(this.components);
    console.log(this.tableData);
    //this.components = [];
    this.tableData = [];
    this.selectedComponentId =0 ;
    // this.selectedOfficeId = 0 ;
    // this.selectedFinancialYear  ="";
    // this.fiscal_startYear  ="";
    // this.fiscal_endYear ="";

    this.tableHeader = 0;
}

onResetComponent(){
   //this.LoadOfficeDropdown();
    this.tableData = [];
   // this.selectedOfficeId = 0 ;
    this.tableHeader =0;
}

onResetOffice(){
    this.tableData = [];
    this.tableHeader =0;
}

  saveData() {
    
  }

  navigateToList() {
    this.router.navigate(['ac/budget/budget-list']);
  }

}
