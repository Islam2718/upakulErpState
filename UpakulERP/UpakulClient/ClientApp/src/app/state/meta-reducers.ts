import { ActionReducer, INIT, UPDATE } from '@ngrx/store';

export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return (state, action) => {
    // On app init or state update, load from localStorage
    if (action.type === INIT || action.type === UPDATE) {
      const storedState = localStorage.getItem('authState');
      if (storedState) {
        try {
          return { ...state, auth: JSON.parse(storedState) };
        } catch {
          return state;
        }
      }
    }

    const nextState = reducer(state, action);

    // Persist the auth slice into localStorage
    localStorage.setItem('authState', JSON.stringify(nextState.auth));

    return nextState;
  };
}
