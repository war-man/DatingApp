import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';

declare var require: any;
@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
@Input() user: User;
private USERPIC  = require('./../../../assets/images/userDefaultImg.jpg');
  constructor() { }

  ngOnInit() {
  }

}
