import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {MatDialog} from "@angular/material/dialog";

declare var google: any;

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
  map:any;
  marker: any;

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
      univerzitetID:1
    }
  }


  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'60%'
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

    });
  }
  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.podaciFakulteti = x;
    }));
  }

  private getUniverziteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.univerzitetPodaci = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();
    this.getUniverziteti();
  }
  ucitajMapu() {

    this.map = new google.maps.Map(document.getElementById('map'), {
      center: this.fakultet.lokacija!=null ? {lat: this.fakultet.lokacija.lat, lng: this.fakultet.lokacija.lng}
        :{ lat: 43.85, lng: 18.41 },
      zoom: 8
    });

    this.marker= new google.maps.Marker({
      position: this.fakultet.lokacija!=null ? {lat: this.fakultet.lokacija.lat, lng: this.fakultet.lokacija.lng}
        :{lat: 0, lng: 0},
      map: this.map,
      visible:this.fakultet.lokacija!=null
    });

    this.map.addListener('click',(event:any)=>{
      this.onMapClick(event);
    });
  }

  private onMapClick(event: any) {
    this.marker.setPosition(event.latLng);
    this.marker.setVisible(true);
    this.fakultet.lat=event.latLng.lat();
    this.fakultet.lng=event.latLng.lng();
  }
}
