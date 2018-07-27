import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs/internal/Observable";
import { AuthServiceService } from "_services/auth-service.service";
import { Injectable } from "@angular/core";

@Injectable()
export class AdminGuard implements CanActivate {

  constructor(private authserice: AuthServiceService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    if (this.authserice.isLoggedIn() && localStorage.getItem('role') === "Administrator") {
      return true;
    }
    return false;
  }
}
