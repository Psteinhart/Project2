import { Component, OnInit } from '@angular/core';
import { AuthenticationService, UserService } from '../_services/index';
import { User } from '../_models/index';

@Component({
  selector: 'app-navbar',
  templateUrl: './app-navbar.component.html',
  styleUrls: ['./app-navbar.component.css']
})
export class AppNavbarComponent implements OnInit {
  authenticationService: any;
    currentUser: User;
 
    constructor(private userService: UserService) {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }
 // constructor(private authenticationService: AuthenticationService ) { }
  ngOnInit() {   
  }

  ngOnClick(){
    this.authenticationService.logout();
  }
}
