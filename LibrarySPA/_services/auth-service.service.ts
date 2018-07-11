import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { pipe } from 'rxjs';
import { map } from 'rxjs/operators';
//import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  url = "https://localhost:44360/api/auth";
  userToken: any;

  constructor(private http: Http) { }

  isLoggedIn() {
   // const helper = new JwtHelperService();
    var token = localStorage.getItem("token");
    if (token) {
      return true;
    }
    return false;
  }

  logout() {
    this.userToken = "";
    localStorage.removeItem('token');
    localStorage.removeItem('role');
  }

  login(model: any) {
    const headers = new Headers({ "Content-type": "application/json" });
    const options = new RequestOptions({ headers: headers });
    return this.http.post(this.url + "/login", model, options).pipe(map(response => {
      const data = response.json();
      if (data) {
        localStorage.setItem("token", data.token);
        localStorage.setItem("role", data.role);
        this.userToken = data.token;
      }
    }));
  }
}
