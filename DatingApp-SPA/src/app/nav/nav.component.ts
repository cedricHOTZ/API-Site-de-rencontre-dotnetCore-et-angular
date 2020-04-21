import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
 model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  
  //connection
  login(){
   this.authService.login(this.model).subscribe(next => {
     console.log('connecter');
   }, error => {
     console.log('erreur de connection');
   });
  }
  // connecté
  loggedIn(){
    const token = localStorage.getItem('token');
    return !! token;
  }
  //déconnection
  logout(){
    localStorage.removeItem('token');
    console.log('logged out');
  }
}
