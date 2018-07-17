import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { pipe } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {

  url = "https://localhost:44360/api/";
  constructor(private http: Http) { }

  getBooks() {
    const options = this.getOptions();
    return this.http.get(this.url + "/books", options).pipe(map(response => {
      return response.json();
    }));
  }

  getUsers() {
    const options = this.getOptions();
    return this.http.get(this.url + "admin/users", options).pipe(map(response => {
      return response.json();
    }));
  }

  deleteUser(id) {
    const options = this.getOptions();
    return this.http.delete(this.url + "admin/user/delete/" + id, options).pipe(map(response => {
      return response.json();
    }));
  }

  getUser(id) {
    const options = this.getOptions();
    return this.http.get(this.url + "admin/user/get/" + id, options).pipe(map(response => {
      console.log(response.json());
      return response.json();
    }));
  }

  getOptions() {
    let token = localStorage.getItem('token');
    const headers = new Headers({ "Content-type": "application/json", "Authorization": "Bearer " + token });
    const options = new RequestOptions({ headers: headers });
    return options;
  }
}
