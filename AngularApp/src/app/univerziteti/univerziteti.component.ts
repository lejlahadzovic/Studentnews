import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {MatDialog} from "@angular/material/dialog";
import {formatDate} from "@angular/common";

declare var google: any;

@Component({
  selector: 'app-univerziteti',
  templateUrl: './univerziteti.component.html',
  styleUrls: ['./univerziteti.component.css']
})
export class UniverzitetiComponent implements OnInit {

  podaciUniveziteti: any;
  title: string="AppComponent" ;
  displayedColumns: string[] = ['naziv', 'email', 'telefon', 'veza','grad','akcije'];
  dialogtitle: any;
  univerzitet:any;
  gradoviPodaci:any;
  map:any;
  marker: any;
  constructor(private httpKlijent: HttpClient,private dialog: MatDialog) {

  }
  dodajUniverzitet() {
    this.dialogtitle='Dodaj univerzitet';

    this.univerzitet={
      id:0,
      naziv:'',
      email:'',
      telefon:'',
      veza:'',
      gradID:1
    }
  }


  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
     width:'60%'
    });
  }
  obrisiUniverzitet(s: any) {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Univerzitet/Obrisi/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
    });
  }
  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }


  snimi_dugme() {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Univerzitet/Snimi`, this.univerzitet, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
    });
  }
  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.podaciUniveziteti = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();
    this.getGradovi();
  }


  ucitajMapu() {

      this.map = new google.maps.Map(document.getElementById('map'), {
        center: this.univerzitet.lokacija!=null ? {lat: this.univerzitet.lokacija.lat, lng: this.univerzitet.lokacija.lng}
        :{ lat: 43.85, lng: 18.41 },
        zoom: 8
      });

      this.marker= new google.maps.Marker({
        position: this.univerzitet.lokacija!=null ? {lat: this.univerzitet.lokacija.lat, lng: this.univerzitet.lokacija.lng}
        :{lat: 0, lng: 0},
        map: this.map,
        visible:this.univerzitet.lokacija!=null
      });

    this.map.addListener('click',(event:any)=>{
      this.onMapClick(event);
    });
  }

  private onMapClick(event: any) {
    this.marker.setPosition(event.latLng);
    this.marker.setVisible(true);
    this.univerzitet.lat=event.latLng.lat();
    this.univerzitet.lng=event.latLng.lng();
  }
}
