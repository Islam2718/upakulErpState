export interface Menu {
    menuId: number;
    menuText: string;
    url: string;
    parentId: number;
    children?: Menu[];
    iconCss: string;
  }
  export interface Module {
    module_id: number;
    role_id: number;
    module_name: string;
    secend_div_class: string;
    icon_class: string;
    title: string;
    url: string;
    display_order: number;
  }
  
  export interface MenuResponse {
    notification: any;
    token: string;
    transactionDate:string;
    menus: Menu[];
    modules: Module[];
  }