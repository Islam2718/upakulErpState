import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, FormsModule} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, CommonModule } from '@angular/common';
import { Button } from '../../../shared/enums/button.enum';
import { RoleConfigService } from '../../../services/administration/menu/menu-permission.service';
import { ToastrService } from 'ngx-toastr';
import { Message } from '../../../shared/enums/message.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { MenuItem } from '../../../models/administration/menu-item';

interface DropdownValues {
  text: string;
  value: string;
}

@Component({
  selector: 'app-menu-permission',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule],
  templateUrl: './menu-permission.component.html',
  styleUrl: './menu-permission.component.css'
})
export class MenuPermissionComponent implements OnInit{

  form!: FormGroup;

  isSubmitting = false;
  successMessage = '';
  message = Message;  
  ModuleValues: DropdownValues[] = [];
  RoleValues: DropdownValues[] = [];
  ModId: number = 0; 
  permissionsLoaded = false;
  menuItems: MenuItem[] = []; // Add this at the top of the class J
  // modules = ['Office', 'HR', 'Finance'];
  // roles = ['Admin', 'User', 'Viewer'];

  // sectionConfig = [
  //   { name: 'Settings', modules: ['Office', 'Roles'] },
  //   { name: 'Global', modules: ['HR', 'Finance'] },
  //   { name: 'MF', modules: ['MutualFunds', 'Investments'] }
  // ];

  sectionStates: { collapsed: boolean; allSelected: boolean }[] = [];

  constructor(private fb: FormBuilder,
    private apiService: RoleConfigService,
    private toastr: ToastrService, 
    public router: Router,
    private activeatedRoute: ActivatedRoute // ✅ This one, not Router,  
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      selectedModule: [''],
      selectedRole: [''],
      //sections: this.fb.array([])
      //sections: this.fb.array([] as FormGroup[]) 
      sections: this.fb.array<FormGroup>([])
    });

    ///On Static
    // this.sectionConfig.forEach(section => {
    //   this.sectionForms.push(this.createSection(section.name, section.modules));
    //   this.sectionStates.push({ collapsed: true, allSelected: false });
    // });

    this.loadDropDownModule();
  }


  loadDropDownModule() {
    this.apiService.getModuleDropdown().subscribe({
      next: (data) => {
        this.ModuleValues = data;
      },
      error: (err) => {
        //console.error('Error fetching dropdown data:', err);
      }
    });
  }

  onDropDownModuleChange(event: any) {
    const selectedValue = event.target.value;
    if(selectedValue>0)
      this.getRolesByModuleIdDropdown(selectedValue);
    else
      this.RoleValues = [];

    this.clearPermissionForm();
    this.ModId = selectedValue;
  }

  getRolesByModuleIdDropdown(moduleId: number) {
    this.apiService.getRolesByModuleIdDropdown(moduleId).subscribe({
      next: (data) => {
        this.RoleValues = data;
      },
      error: (err) => {
       // console.error('Error fetching dropdown data:', err);
      }
    });
  }  

  onDropDownRoleChange(event: any) {
   // console.log("onDropDownRoleChange");
    const selectedValue = event.target.value;
    this.clearPermissionForm();
  }

  get sectionForms(): FormArray {
    return this.form.get('sections') as FormArray;
  }

  getModules(sectionIndex: number): FormArray {
    //console.log("getModules");
    return this.sectionForms.at(sectionIndex).get('modules') as FormArray;
  }

  // createSection(name: string, modules: string[]): FormGroup {
  //   return this.fb.group({
  //     name,
  //     modules: this.fb.array(
  //       modules.map(mod =>
  //         this.fb.group({
  //           name: mod,
  //           checked: false,
  //           add: false,
  //           edit: false,
  //           delete: false,
  //           view: false
  //         })
  //       )
  //     )
  //   });
  // }

  toggleCollapse(index: number): void {
    this.sectionStates[index].collapsed = !this.sectionStates[index].collapsed;
  }

  
  toggleSectionPermissions(sectionIndex: number): void {
    //console.log("toggleSectionPermissions"+sectionIndex);
    const checked = !this.sectionStates[sectionIndex].allSelected;
    const modules = this.getModules(sectionIndex).controls;
  
    modules.forEach(control => {
      control.get('checked')?.setValue(checked);
    });
  
    this.sectionStates[sectionIndex].allSelected = checked;
  }

  onSearch(): void {
    //console.log(this.form.value);
    const moduleId = this.form.get('selectedModule')?.value;
    const roleId = this.form.get('selectedRole')?.value;
    //console.log("IN On Search");

    if (!moduleId || !roleId) {
      this.toastr.warning("Please select both module and role");
      return;
    }

    this.apiService.getMenuPermission(moduleId, roleId).subscribe({
      next: (data: { parent: any; child: MenuItem[] }[]) => {
       // console.log(data);
        // Flatten the child items from each group into one array
        const allMenuItems = data.flatMap(group => group.child);
        this.menuItems = allMenuItems.concat(data.map(d => d.parent).filter(Boolean)); // Cache all items
        
        if (allMenuItems.length === 0) {
          this.clearPermissionForm(); 
          this.toastr.info("No permissions found for selected module and role");
          return;
        }

        this.buildPermissionForm(allMenuItems);
        this.permissionsLoaded = true;
      },
      error: (err) => {
        //console.error('Failed to fetch menu permissions', err);
        this.permissionsLoaded = false;
        this.clearPermissionForm();
        //this.toastr.error('Failed to load menu permissions');
      }
    });
  }

  clearPermissionForm(): void {
    this.permissionsLoaded = false;
    this.sectionForms.clear(); // clear the FormArray
    this.sectionStates = [];   // reset section state flags
   // this.ModuleValues = [];
    //this.RoleValues = [];
  }

  buildPermissionForm(data: MenuItem[]) {
    //console.log("buildPermissionForm", data);
    // debugger
    const parents = data.filter(item => item.parentId === null);
   // console.log("parents"+parents);
    const sectionsArray = this.fb.array<FormGroup>([]);
    this.sectionStates = [];
  
    parents.forEach((parent, sectionIndex) => {
      const childrenOne = data.filter(item => item.parentId === parent.menuId );
      
      const childrenTwo = data.filter(item =>
        childrenOne.some(child => child.menuId === item.parentId)
      );

      const children = [...childrenOne, ...childrenTwo];


      //console.log("allChildren", children);

      //const subParents = data.filter(item => item.menuId === );
      // children.forEach((subparent, sectionIndex) => {
      // const subchildren = data.filter(item => item.menuId === subparent.parentId);
      // console.log("subchildrenparents.forEach", subchildren);
      // });

     
  
      const childControls = children.map(child => {
        const isModuleChecked =
          child.isPermitted || child.isAdd || child.isEdit || child.isDelete || child.isView;
  
        const moduleGroup = this.fb.group({
          menuId: child.menuId,
          name: child.menuText,
          checked: isModuleChecked,
          add: [{ value: child.isAdd, disabled: !isModuleChecked }],
          edit: [{ value: child.isEdit, disabled: !isModuleChecked }],
          delete: [{ value: child.isDelete, disabled: !isModuleChecked }],
          view: [{ value: child.isView, disabled: !isModuleChecked }]
        });
  
        // Subscribe to module checkbox changes
        moduleGroup.get('checked')?.valueChanges.subscribe((isChecked: boolean|null) => {
          const perms = ['add', 'edit', 'delete', 'view'];
          perms.forEach(perm => {
            const control = moduleGroup.get(perm);
            if (isChecked) {
              control?.enable({ emitEvent: false });
            } else {
              control?.disable({ emitEvent: false });
              control?.setValue(false, { emitEvent: false });
            }
          });
  
          this.updateSectionAllSelected(sectionIndex);
        });
  
        // Subscribe to permission checkbox changes to auto-check module
        ['add', 'edit', 'delete', 'view'].forEach(perm => {
          moduleGroup.get(perm)?.valueChanges.subscribe(() => {
            const hasPermission = ['add', 'edit', 'delete', 'view']
              .some(p => moduleGroup.get(p)?.value === true);
            if (hasPermission && !moduleGroup.get('checked')?.value) {
              moduleGroup.get('checked')?.setValue(true);
            }
          });
        });
  
        return moduleGroup;
      });
  
      const sectionGroup = this.fb.group({
        name: parent.menuText,
        modules: this.fb.array(childControls)
      });
  
      sectionsArray.push(sectionGroup);
  
      // Initialize section header state
      const anyModuleChecked = childControls.some(ctrl => ctrl.get('checked')?.value === true);
      this.sectionStates.push({ collapsed: true, allSelected: anyModuleChecked });
    });
  
   // console.log("sectionsArray "+sectionsArray);
    this.form.setControl('sections', sectionsArray);
  }

  updateSectionAllSelected(sectionIndex: number): void {
    const modules = this.getModules(sectionIndex).controls;
    const anyChecked = modules.some(m => m.get('checked')?.value === true);
    this.sectionStates[sectionIndex].allSelected = anyChecked;
  }

  ///J
  private findMenuIdBySectionName(name: string): number | null {
    const parentItem = this.menuItems.find(item => item.menuText === name && item.parentId === null);
    return parentItem ? parentItem.menuId : null;
  }


  onSave(): void {
   // debugger;
    const moduleId = +this.form.get('selectedModule')?.value;
    const roleId = +this.form.get('selectedRole')?.value;
  
    if (!moduleId || !roleId) {
      this.toastr.warning("Please select both module and role");
      return;
    }
  
    const permissions: any[] = [];
  
    this.sectionForms.controls.forEach(section => {
      const sectionName = section.get('name')?.value; //J
      const modules = section.get('modules') as FormArray;

      
      // Push section header with all permissions false
      const sectionHeader = this.ModuleValues.find(m => m.text === sectionName);
      const headerMenuId = this.findMenuIdBySectionName(sectionName);
      if (headerMenuId) {
        permissions.push({
          ModuleId: moduleId,
          RoleId: roleId,
          MenuId: headerMenuId,
          IsAdd: false,
          IsEdit: false,
          IsDelete: false,
          IsView: false
        });
      }


      modules.controls.forEach(module => {
        if (!module.get('checked')?.value) return; // skip if not selected
  
        const item: any = {
          ModuleId: moduleId,
          RoleId: roleId,
          MenuId: module.get('menuId')?.value
        };
  
        // Add permission fields only if true
        ['IsAdd', 'IsEdit', 'IsDelete', 'IsView'].forEach(field => {
          const controlName = field.replace('Is', '').toLowerCase(); // add, edit, etc.
          const value = module.get(controlName)?.value;
          if (value) item[field] = true;
        });
        //console.log("InsertItem:"+item);
        permissions.push(item);
      });
    });
  
    if (permissions.length === 0) {
      this.toastr.warning("No permissions selected");
      return;
    }
  
    this.apiService.saveMenuPermissions(permissions).subscribe({
      next: () => {
        this.toastr.success("Permissions saved successfully")
        //this.clearPermissionForm();
      },
      error: err => {
        //console.error("Failed to save permissions:", err);
        this.toastr.error("Error saving permissions");
      }
    });
  }


}











  

  

