import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, map, of } from 'rxjs';

export const masterGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.loggedVerify()) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};
