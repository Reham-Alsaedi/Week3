import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideHttpClient, withFetch } from '@angular/common/http'; // ✅ Import `withFetch`
import { provideRouter } from '@angular/router';

import { appRoutes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideHttpClient(withFetch()), // ✅ Enable Fetch API
  
    provideRouter(appRoutes),
    provideClientHydration(withEventReplay())
  ]
};



