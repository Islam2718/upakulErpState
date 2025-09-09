// state/auth.selectors.ts
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from './auth.state';

export const selectAuthState = createFeatureSelector<AuthState>('auth');

export const selectStateToken = createSelector(selectAuthState, (state) => state.stateToken);
export const selectStatePersonal = createSelector(selectAuthState, (state) => state.statePersonal);
export const selectStateModules = createSelector(selectAuthState, (state) => state.stateModules);
export const selectStateNotification = createSelector(selectAuthState, (state) => state.stateNotification);
export const selectStateMenu = createSelector(selectAuthState, (state) => state.stateMenu);
