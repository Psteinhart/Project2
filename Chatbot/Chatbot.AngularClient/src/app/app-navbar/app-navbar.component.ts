import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/index';

@Component({
  selector: 'app-navbar',
  templateUrl: './app-navbar.component.html',
  styleUrls: ['./app-navbar.component.css']
})
export class AppNavbarComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService ) { }

  ngOnInit() {
   
  }

  ngOnClick(){
    this.authenticationService.logout();
  }

 

}
