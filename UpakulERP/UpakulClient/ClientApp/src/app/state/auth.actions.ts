// state/auth.actions.ts
import { createAction, props } from '@ngrx/store';

export const setLoginData = createAction(
  '[Auth] Set Login Data',
  props<{
    stateToken: string;
    statePersonal: string; // বা object
    stateModules: string[];
    stateNotification: any[];
    stateMenu: any[];
  }>()
);

export const updateNotifications = createAction(
  '[Auth] Update Notifications',
  props<{ stateNotification: any[] }>()
);

export const updateStatePersonal = createAction(
  '[Auth] Update State Personal',
  props<{ statePersonal: any }>()
);

export const handleModuleStateFunc = createAction(
  '[Auth] Handle Module Click',
  props<{
    moduleName: string;
    roleId: number;
    moduleId: number;
    menus?: any[];
    modules?: any[];
    notifications?: any[];
  }>()
);