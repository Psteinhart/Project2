// // Importing the libraries
// import { Injectable } from '@angular/core';
// import { Http, Response, Headers } from '@angular/http';
// import 'rxjs/add/operator/map';
// import 'rxjs/add/operator/do'; // debug
// import { Observable } from 'rxjs/Observable';
// import { BehaviorSubject } from 'rxjs/BehaviorSubject';

// // To inject the dependancies in the service
// @Injectable()
// // tslint:disable-next-line:class-name
// export class userInfoService {
// public employeeList: Observable<Employee[]>;
// private _employeeList: BehaviorSubject<Employee[]>;
// private baseUrl: string;
// private dataStore: {
// employeeList: Employee[];
// };
 
// //// Constructor to set the values
// constructor(private http: Http) {
// // Base URL for the API
// this.baseUrl = '/api/';
// this.dataStore = { employeeList: [] };
// this._employeeList = <BehaviorSubject<Employee[]>>new BehaviorSubject([]);
// this.employeeList = this._employeeList.asObservable();
// }
 
// // Method to get all employees by calling /api/GetAllEmployees
// getAll() {
// this.http.get(`${this.baseUrl}User`)
// .map(response => response.json())
// .subscribe(data => {
// this.dataStore.employeeList = data;
// // Adding newly added Employee in the list
// this._employeeList.next(Object.assign({}, this.dataStore).employeeList);
// }, error => console.log('Could not load employee.'));
// }
// }