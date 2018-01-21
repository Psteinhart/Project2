import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AlertService, UserService } from '../_services/index';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from '../_models/index';
import { Body } from '@angular/http/src/body';
import { HttpHeaders } from '@angular/common/http/src/headers';

@Component({
    moduleId: module.id.toString(),
    templateUrl: 'register.component.html'
})

export class RegisterComponent {
    model: any = {};
    loading = false;

    constructor(
        private router: Router,
        private userService: UserService,
        private http: HttpClient,
        private alertService: AlertService) { }

    register() {
        var us = new UserInfo();
        us.email = this.model.username;
        us.firstname = this.model.firstName;
        us.lastname = this.model.lastName;
        us.password = this.model.password;

       // console.log(this.model.username+" "+ this.model.password+" "+this.model.firstName+" "+this.model.lastName);

        this.loading = true;
        this.http.post('http://spotbotpotdataservice.azurewebsites.net/api/User/', us).subscribe(
                           data => {
                    this.alertService.success('Registration successful', true);
                    this.router.navigate(['/login']);
                },
                error => {
                    this.alertService.error("Unsuccessful");
                    this.loading = false;
                });

    }
}
