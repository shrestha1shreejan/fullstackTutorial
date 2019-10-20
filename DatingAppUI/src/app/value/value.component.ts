import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {

  values: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    this.httpClient.get('https://localhost:44379/api/test').subscribe(
      response => {
        this.values = response;
      }, error => {
        console.log(error);
      });
  }

}
