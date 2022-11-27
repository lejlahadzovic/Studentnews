import { Component } from '@angular/core';
import {MojConfig} from "./MojConfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  podaci: any;
  title: string="AppComponent" ;


  getPodaci() {
    if (this.podaci == null)
      return [];
    return this.podaci;
  }

  constructor(private httpKlijent: HttpClient) {

  }

  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.podaci = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();
  }


}
