import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../_services/communication.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: any;

  constructor(private service : CommunicationService, private alertify : AlertifyService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.service.getUsers().subscribe(data => {
      
      this.users = data;
      this.alertify.success("List loaded");
    });
  }

  deleteUser(id) {
    this.service.deleteUser(id).subscribe(data => {
      this.users = this.users.filter(item => item.id != id);
      this.alertify.success("Deleted User");
    });
    console.log(id);
  }


}
