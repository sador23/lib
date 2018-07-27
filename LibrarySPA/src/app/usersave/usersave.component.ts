import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../_services/communication.service';
import { Router } from '@angular/router';
import { Promise } from 'q';

@Component({
  selector: 'app-usersave',
  templateUrl: './usersave.component.html',
  styleUrls: ['./usersave.component.css']
})
export class UsersaveComponent implements OnInit {

  model: any = {};

  constructor(private service: CommunicationService, private router : Router) { }

  ngOnInit() {
  }

  saveUser() {
    this.service.saveUser(this.model).subscribe(data => {
      if (data.ok) {
        this.router.navigate(["/userList"]);
      }
    });
    
  }

}
