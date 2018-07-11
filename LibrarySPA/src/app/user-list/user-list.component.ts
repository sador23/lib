import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../_services/communication.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: any;

  constructor(private service : CommunicationService) { }

  ngOnInit() {
  }

  getUsers() {
    this.service.getUsers().subscribe(data => {
      this.users = data;
    });
  }

}
