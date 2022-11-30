import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-univerziteti',
  templateUrl: './univerziteti.component.html',
  styleUrls: ['./univerziteti.component.css']
})
export class UniverzitetiComponent implements OnInit {

  podaci: any;
  title: string="AppComponent" ;
  displayedColumns: string[] = ['naziv', 'email', 'telefon', 'veza','grad'];


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
