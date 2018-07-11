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
    let token = localStorage.getItem('token');
    const headers = new Headers({ "Content-type": "application/json", "Authorization":"Bearer " + token});
    const options = new RequestOptions({ headers: headers });

    return this.http.get(this.url + "/books", options).pipe(map(response => {
      
      return response.json();
    }));

  }
}
