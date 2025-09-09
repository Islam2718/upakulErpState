export interface Menu {
    id: number;          // Make sure the 'id' property is defined
    ModuleId: string;
    ParentId: string;
    MenuText: string;
    ParentUrl: string;
    ParentComponent: string;
    ChildUrl: string;
    ChildComponent: string;
    IconCss: string;
    displayOrder: number;
    menuPosition: number;
  }
  