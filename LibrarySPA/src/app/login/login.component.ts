import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '_services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private authService : AuthServiceService, private router : Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(data => {
      if (localStorage.getItem('role') === "Administrator") {
        this.router.navigate(['/userList']);
      } else if (localStorage.getItem('role') === "User") {
        this.router.navigate(['/bookList']);
      } else {
        console.log("error");
      }
    }, error => {
      console.log("Login not ok");
    });
  }

}
