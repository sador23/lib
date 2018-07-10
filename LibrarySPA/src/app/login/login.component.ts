import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '_services/auth-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private authService : AuthServiceService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(data => {
      console.log("Login ok");
    }, error => {
      console.log("Login not ok");
    });
  }

}
