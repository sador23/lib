import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { pipe } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  
  url = "https://localhost:44360/api/auth";
  userToken: any;

  constructor(private http: Http) { }

  login(model: any) {
    const headers = new Headers({ "Content-type": "application/json" });
    const options = new RequestOptions({ headers: headers });
    return this.http.post(this.url + "/login", model, options).pipe(map(response => {
      const data = response.json();
      if (data) {
        localStorage.setItem("token", data.token);
        this.userToken = data.token;
      }
    }));
  }
}
