import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../_services/communication.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  models: any = [];


  constructor(private service: CommunicationService) { }

  ngOnInit() {
    console.log("here");
    this.getBooks();
    console.log(this.models);
  }

  getBooks() {
    this.service.getBooks().subscribe(data => {
      this.models = data;
    });
  }

}
