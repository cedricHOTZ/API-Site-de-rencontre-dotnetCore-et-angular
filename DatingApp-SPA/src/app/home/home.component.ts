import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

   // initialiser
  registerMode = false;
  values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }
   // bascule sur le formulaire d'inscription
   registerToggle(){
  this.registerMode = !this.registerMode;
   }

   // récupère les valeurs pour les afficher sur la page home
   getValues(){
    this.http.get('http://localhost:5000/api/values').subscribe(response =>
    {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }
}
