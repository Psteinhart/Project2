import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AlertService, AuthenticationService } from '../_services/index';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from '../_models/index';

@Component({
    moduleId: module.id.toString(),
    templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
    model: any = {};
    loading = false;
    returnUrl: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService,
        private http: HttpClient) { }// added httpclient

    ngOnInit() {
        // reset login status
        this.authenticationService.logout();

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    }


    login() {
        //http://localhost:61053/api/User, 52066 is phil
        (this.http.get<UserInfo>('http://spotbotpotdataservice.azurewebsites.net/api/User/' + this.model.username).subscribe(resp => {
           // console.log(resp.email);
            //console.log(resp.password);
            localStorage.setItem('currentUser', JSON.stringify(resp.email));
            if (this.model.username === resp.email && this.model.password === resp.password) {
                this.router.navigate(['/Home']);
            } else {
                this.alertService.error("Not registered");
            this.loading = false;
            }
        },
        error => {
            this.alertService.error("Username or Password is incorrect");
            this.loading = false;
        }
    ));
     
    }

}
