// src/app/state/document.reducer.ts
import { createReducer, on } from '@ngrx/store';
import { loadDocuments, loadDocumentsSuccess, loadDocumentsFailure } from './document.actions';
import { DocumentState, initialState } from './document.state';

export const documentReducer = createReducer(
  initialState,
  on(loadDocuments, (state) => ({
    ...state,
    loading: true,
  })),
  on(loadDocumentsSuccess, (state, { documents }) => ({
    ...state,
    documents,
    loading: false,
  })),
  on(loadDocumentsFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
  }))
);
