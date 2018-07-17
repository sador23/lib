import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthServiceService } from '_services/auth-service.service';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { BooksComponent } from './books/books.component';
import { UserGuard } from './_guards/UserGuard';
import { UserListComponent } from './user-list/user-list.component';
import { UserpageComponent } from './userpage/userpage.component';
import { BookpageComponent } from './bookpage/bookpage.component';
import { AdminGuard } from './_guards/AdminGuard';
import { CommunicationService } from './_services/communication.service';
import { AlertifyService } from './_services/alertify.service';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    BooksComponent,
    UserListComponent,
    UserpageComponent,
    BookpageComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      {path:'login', component:LoginComponent},
      { path: 'register', component: RegisterComponent },
      { path: 'bookList', component: BooksComponent, canActivate: [UserGuard] },
      { path: 'getBook/:id', component: BooksComponent, canActivate: [UserGuard] },
      { path: 'userList', component: UserListComponent, canActivate: [AdminGuard] },
      { path: 'getUser/:id', component: UserpageComponent, canActivate: [AdminGuard] },
    ])
  ],
  providers: [AuthServiceService, CommunicationService, UserGuard, AdminGuard, AlertifyService],
  bootstrap: [AppComponent, NavbarComponent]
})
export class AppModule { }
