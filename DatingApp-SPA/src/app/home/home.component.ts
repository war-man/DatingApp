import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../globals';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  globals: Globals;

  values: any;
  constructor(private http: HttpClient,globals: Globals) {
    this.globals = globals;
   }

  ngOnInit() {
    this.getValues();
  }

  registerToogle() {
    this.registerMode = true;
  }
  getValues(){
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
    this.values = response;
  }, error => {
      console.log(error);
    });
  }
  cancelRegisterMode(registerMode: boolean){
    this.registerMode = registerMode;
  }
}
