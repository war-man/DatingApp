import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Globals } from 'src/app/globals';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  globals: Globals;

  constructor(private userService: UserService, private alertifyService: AlertifyService
    ,private route: ActivatedRoute,globals: Globals) {
      this.globals = globals;
     }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.users;
    });
    //this.loadUsers();
  }
// loadUsers() {
//   this.userService.getUsers().subscribe((users: User[]) => {
//     this.users = users;
//   }, error => {
//     this.alertifyService.error(error);
//   });
// }

}
