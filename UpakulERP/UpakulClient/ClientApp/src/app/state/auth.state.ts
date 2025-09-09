// state/auth.state.ts
export interface AuthState {
  stateToken: string | null;
  statePersonal: string | null; // বা object, যা তুমি চাইছো
  stateModules: string[];
  stateNotification: any[];
  stateMenu: any[];
}

export const initialState: AuthState = {
  stateToken: null,
  statePersonal: null,
  stateModules: [],
  stateNotification: [],
  stateMenu: []
};
