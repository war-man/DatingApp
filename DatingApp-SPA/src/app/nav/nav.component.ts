import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import {AlertifyService} from '../_services/alertify.service';
import {Router} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Globals } from '../globals';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  textDir: string = 'rtl';
  @Output() globalTextDir: EventEmitter<string> = new EventEmitter();

  constructor(public authService: AuthService, private alertifyService: AlertifyService,
              private route: Router,public translate: TranslateService,private globals: Globals
              ) {
                translate.addLangs(['en', 'ar']);
                translate.setDefaultLang('ar');
               }

  ngOnInit() {
    this.switchLang('ar');
  }
  switchLang(lang: string) {
    this.translate.use(lang);
    if(lang == 'ar')
    {
      this.textDir = 'rtl';
    } 
    else
    {
      this.textDir = 'ltr';
    }
    this.globals.textDir= this.textDir;
  }

  login() {
  this.authService.login(this.model).subscribe(next => {
    this.alertifyService.success('Logged in Successfully') ;
   }, error => {
    this.alertifyService.error(error) ;
   }, () => {
     this.route.navigate(['/members']);
   }
   );
  }
  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem('token');
    this.alertifyService.message('Logged out') ;
    this.route.navigate(['/home']);
  }
}
