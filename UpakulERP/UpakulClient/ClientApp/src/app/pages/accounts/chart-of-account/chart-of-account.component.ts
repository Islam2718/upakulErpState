import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BtnService } from '../../../services/btn-service/btn-service';
import { ConfirmModalComponent } from '../../../shared/confirm-modal/confirm-modal.component';
import {
  AccountHead,
  ChartOfAccountService as apiService,
} from '../../../services/accounts/chart-of-account/chart-of-account.service';
import { ToastrService } from 'ngx-toastr';
import { CoaAssignModalComponent } from './components/coa-assign-modal/coa-assign-modal.component';

@Component({
  selector: 'app-chart-of-account',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    ConfirmModalComponent,
    CoaAssignModalComponent,
  ],
  templateUrl: './chart-of-account.component.html',
  styleUrls: ['./chart-of-account.component.css'],
})
export class ChartOfAccountComponent {
  treeData: AccountHead[] = [];
  activeNodeId: number | null = null; // track which node is editing
  @ViewChild('confirmModal') confirmModal!: ConfirmModalComponent;
  // @ViewChild('assignModal') assignModal!: CoaAssignModalComponent;
  @ViewChild('assignModal', { static: false })
  assignModal!: CoaAssignModalComponent;
  selectedNode: any;
  showAssignModal: boolean | undefined;
  isCollapsed: boolean = true; // default mode = Collapse
  accountType: string = 'L';    // default account type
  
  constructor(
    public Button: BtnService,
    private apiService: apiService,
    private toastr: ToastrService
  ) {}

  openAssignModal(node: any) {
    this.selectedNode = node;
    this.showAssignModal = true;

    // Wait a tick to ensure the component exists
    setTimeout(() => this.assignModal.open(), 0);
  }

  ngOnInit(): void {
    this.getAccountHead();
  }

  toggleCollapseExpand(collapse: boolean) {
    this.isCollapsed = collapse;
    this.accountType = collapse ? 'L' : 'E';

    // Reload account heads with the selected type
    this.getAccountHead(undefined, this.accountType);
  }  

  getAccountHead(parentId?: number, requestType: string = 'L'): void {
    this.apiService.getAccountHeads(parentId, requestType).subscribe({
      next: (data: AccountHead[]) => {
        if (!parentId) {
          this.treeData = data;
        } else {
          this.addChildrenToParent(this.treeData, parentId, data);
        }
      },
      error: (err: any) => console.error('API error:', err),
    });
  }

  addChildrenToParent(
    tree: AccountHead[],
    parentId: number,
    child: AccountHead[]
  ): void {
    for (let node of tree) {
      if (node.accountId === parentId) {
        if (!node.child) node.child = [];
        child.forEach((ch) => {
          const exists = node.child!.some((c) => c.accountId === ch.accountId);
          if (!exists) node.child!.push(ch);
        });
        return;
      }
      if (node.child && node.child.length > 0) {
        this.addChildrenToParent(node.child, parentId, child);
      }
    }
  }

  startEdit(node: any) {
    this.activeNodeId = node.accountId; // mark this node as editing
    node.isEditing = true;
  }

  stopEdit(node: any) {
    node.isEditing = false;
    this.activeNodeId = null;
  }

  addChild(node: any) {
    this.activeNodeId = node.accountId;
    if (!node.child) node.child = [];
    const tempChild = {
      accountId: Date.now(),
      headName: '',
      isTransactable: false,
      parentId: node.accountId,
      isEditing: true,
      isTemp: true, // mark as temp
    };

    // node.child.push(tempChild);
    node.child.unshift(tempChild);
  }

  confirmChild(node: any, parentId?: number) {
    if (node.isTemp) {
      node.accountId = null;
      // Add new item
      this.apiService.addAccountHead(node).subscribe({
        next: (response: any) => {
          const newNode = { ...response.data, isEditing: false, isTemp: false };

          // Insert the new node into the tree, preserving siblings and existing children
          this.insertNode(this.treeData, newNode);

          // Refresh for Angular if using OnPush
          this.treeData = [...this.treeData];
          this.activeNodeId = null;
        },
        error: (err: any) => console.error('Add API error:', err),
      });
    } else {
      // Update existing item
      this.apiService.updateAccountHead(node).subscribe({
        next: (updatedNode: AccountHead) => {
          Object.assign(node, updatedNode, { isEditing: false });
          this.activeNodeId = null;
          this.treeData = [...this.treeData];
        },
        error: (err: any) => console.error('Update API error:', err),
      });
    }
  }
  private insertNode(tree: any[], newNode: any): boolean {
    // If the node is top-level, just add it
    if (!newNode.parentId) {
      tree.unshift(newNode);
      return true;
    }

    // Otherwise, recursively search for parent
    for (const node of tree) {
      if (node.accountId === newNode.parentId) {
        if (!Array.isArray(node.child)) node.child = [];
        node.child.unshift(newNode); // insert as first child
        return true;
      }

      if (Array.isArray(node.child) && node.child.length) {
        const inserted = this.insertNode(node.child, newNode);
        if (inserted) return true;
      }
    }

    return false; // parent not found
  }

  cancelChild(parentList: any[], index: number, node: any) {
    if (node.isTemp) {
      parentList.splice(index, 1);
    } else {
      node.isEditing = false;
    }
    this.activeNodeId = null;
  }

  // delete vars
  deleteIdToConfirm: any;
  // func
  delete(node: any) {
    this.deleteIdToConfirm = Number(node.accountId);
    this.confirmModal.show();
  }
  onDeleteConfirmed() {
    if (!this.deleteIdToConfirm) return;
    // console.log('_a_id_:', this.deleteIdToConfirm);
    const acID = this.deleteIdToConfirm;

    this.apiService.deleteAccountHead(this.deleteIdToConfirm).subscribe({
      next: (response) => {
        if (response.type === 'warning')
          this.toastr.warning(response.message, 'Warning');
        else if (response.type === 'strongerror')
          this.toastr.error(response.message, 'Error');
        else this.toastr.success(response.message, 'Success');

        console.log('_acID:', this.deleteIdToConfirm);

        const removed = this.removeFromTreeRecursive(this.treeData, acID);
        console.log('_resultOfRemove:', removed);

        if (removed) {
          console.log('_rItems:', removed);
          this.treeData = [...this.treeData];
        }
      },
      error: () => this.toastr.error('Delete failed'),
    });
    this.deleteIdToConfirm = null;
  }
  removeFromTreeRecursive(list: any[], accountId: number | string): boolean {
    const targetId = Number(accountId);
    const index = list.findIndex((x) => Number(x.accountId) === targetId);
    if (index !== -1) {
      list.splice(index, 1);
      return true;
    }

    // search in children
    for (const item of list) {
      if (Array.isArray(item.child) && item.child.length > 0) {
        const removed = this.removeFromTreeRecursive(item.child, targetId);
        if (removed) return true;
      }
    }
    return false;
  }
}
