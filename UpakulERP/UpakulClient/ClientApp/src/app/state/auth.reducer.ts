// state/auth.reducer.ts
import { createReducer, on } from '@ngrx/store';
import { AuthState, initialState } from './auth.state';
import { setLoginData, updateStatePersonal } from './auth.actions';

export const authReducer = createReducer(
  initialState,
  on(setLoginData, (state, { stateToken, statePersonal, stateModules, stateNotification, stateMenu }) => ({
    ...state,
    stateToken,
    statePersonal,
    stateModules,
    stateNotification,
    stateMenu
  })),
  on(updateStatePersonal, (state, { statePersonal }) => ({
    ...state,
    statePersonal
  }))
);
