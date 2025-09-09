import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BtnService {  
  createBtn = '<i class="fa-solid fa-plus"></i>';
  viewBtn = '<i class="fa-solid fa-eye"></i>';
  editBtn = '<i class="fa-solid fa-pencil"></i>';
  deleteBtn= '<i class="fa-regular fa-trash-can"></i>';

  searchBtn = '<i class="fa-solid fa-magnifying-glass"></i>';
  assignBtn = '<i class="fa-solid fa-link"></i>';
  printBtn = '<i class="fa-solid fa-print"></i>';
  cleanBtn = '<i class="fa-solid fa-eraser"></i>';

  migrateBtn = '<i class="fa-solid fa-arrows-alt-h"></i>';
  mobilenumberVerifyBtn = '<i class="fa-solid fa-key"></i>';
  approvedBtn = '<i class="fa-solid fa-user-check"></i>';

  collapseBtn = '<i class="fa-solid fa-compress"></i>';
  expandBtn = '<i class="fa-solid fa-expand"></i>';
}
