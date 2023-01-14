import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-fakulteti',
  templateUrl: './fakulteti.component.html',
  styleUrls: ['./fakulteti.component.css']
})
export class FakultetiComponent implements OnInit {



  title: string="FakultetComponent" ;
  fakultet:any;
  dialogtitle:any;
  gradoviPodaci:any;
  univerzitetPodaci:any;
  podaciFakulteti:any;
  displayedColumns: string[] = ['naziv', 'adresa', 'email', 'telefon', 'veza','univerzitet','akcije'];

  constructor(private httpKlijent: HttpClient,private dialog: MatDialog) {

  }
  dodajFakultet() {
    this.dialogtitle='Dodaj fakultet';

    this.fakultet={
      id:0,
      naziv:'',
      adresa:'',
      email:'',
      telefon:'',
      veza:'',
      univerzitetID:0,


    }
  }


  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'20%'
    });
  }
  obrisiFakultet(s: any) {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Fakultet/Obrisi/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
    });
  }



  snimi_dugme() {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Fakultet/Snimi`, this.fakultet, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
      this.fakultet=null;
    });
  }
  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.podaciFakulteti = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();

  }

}
