import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  values: any;
  registerMode = false;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {}

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegistration(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
