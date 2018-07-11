import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { CommunicationService } from '../_services/communication.service';

@Component({
  selector: 'app-bookpage',
  templateUrl: './bookpage.component.html',
  styleUrls: ['./bookpage.component.css']
})
export class BookpageComponent implements OnInit {

  models: any = [];


  constructor(private service : CommunicationService) { }

  ngOnInit() {
  }

  getBooks() {
    this.service.getBooks().subscribe(data => {
      this.models.push(data);
    });
  }

}
