import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { CodegeneratorService } from '../../../../services/microfinance/configuration/codegenerator/codegenerator.service';
import { Codegenerator } from '../../../../models/microfinance/configuration/codegenerator/codegenerator.model';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BtnService } from '../../../../services/btn-service/btn-service';

@Component({
  standalone: true,
  selector: 'app-codegenerator',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  templateUrl: './codegenerator.component.html',
  styleUrls: ['./codegenerator.component.css']
})
export class CodegeneratorComponent implements OnInit {
  qry: string | null = null;
  dataForm: FormGroup;
  rows: Codegenerator[] = [];
  isSubmitting = false;
  isEditMode = true;
  draggedValue: string = '';

  constructor(
     public Button: BtnService,
            // private route: ActivatedRoute
    private fb: FormBuilder,
    private apiService: CodegeneratorService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.dataForm = this.fb.group({
      Id: [null]
    });
  }

  ngOnInit(): void {
    this.loadDataFromDatabase();
  }

  loadDataFromDatabase(): void {
    this.apiService.getAll().subscribe({
      next: (response) => {
        this.rows = response.map(row => ({
          ...row,
          id: row.id,
          typeName: row.typeName,
          description: row.description,
          CodeLength: row.CodeLength ?? 0,
          startNo: row.startNo ?? 0,
          endNo: row.endNo ?? 0,
          mainJoinCode: row.mainJoinCode ?? '',
          virtualJoinCode: row.virtualJoinCode ?? ''
        }));
        if (this.rows.length > 0) {
          this.dataForm.patchValue({ Id: this.rows[0].id });
        }
      },
      error: () => this.toastr.error('Failed to load data')
    });
  }

onSubmit(): void {
  if (this.rows.length === 0) {
    this.toastr.warning('No data to update.');
    return;
  }

  this.isSubmitting = true;


  console.log("this.rows", this.rows);
  this.apiService.updateCodegeneratorList(this.rows).subscribe({
    next: (updatedRows) => {
      this.rows = updatedRows; // ✅ Set updated list from API
      this.toastr.success('All rows updated successfully');
    },
    error: (err) => {
      console.error('Update error:', err);
      this.toastr.error('Update failed');
    },
    complete: () => this.isSubmitting = false
  });
}


  onDragStart(event: DragEvent, value: string): void {
    this.draggedValue = value;
    event.dataTransfer?.setData('text/plain', value);
  }

  allowDrop(event: DragEvent): void {
    event.preventDefault();
  }

  onDrop(event: DragEvent, row: Codegenerator, field: 'mainJoinCode' | 'virtualJoinCode'): void {
    event.preventDefault();
    const dragged = this.draggedValue;
    if (!dragged) return;

    row[field] = (row[field] ?? '') + `{${dragged}}`;
    this.draggedValue = '';
  }

  onReset(): void {
    this.loadDataFromDatabase();
    this.dataForm.reset();
    this.draggedValue = '';
    this.toastr.info('Reset done');
  }
  
}
