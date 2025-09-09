export interface MenuItem {
  menuId: number;
  menuText: string;
  parentId: number | null;
  isPermitted: boolean;
  isAdd: boolean;
  isEdit: boolean;
  isDelete: boolean;
  isView: boolean;
  [key: string]: any;
  }