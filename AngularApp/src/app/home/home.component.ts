import {Component, ElementRef, HostListener, OnInit, ViewChild} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  praksePodaci:any;
  podaciFakulteti:any;
  smjestajiPodaci:any;
  stipendijePodaci:any;
  podaciUniverziteti:any;
  praksePageNumber: number=1;
  stipendijePageNumber: number=1;
  smjestajiPageNumber: number=1;
  pageSize: number=3;

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
    this.getPrakse();
    this.getSmjestaji();
    this.getStipendije();
  }

  getPrakse() {
    const params=new HttpParams()
      .set('pageNumber', this.praksePageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Praksa/GetAll", {params}).subscribe(((x: any) => {
      this.praksePodaci = x;
    }));
  }

  getFakultete() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.podaciFakulteti = x;
    }));
  }

  private getSmjestaji() {
    const params=new HttpParams()
      .set('pageNumber', this.smjestajiPageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Smjestaj/Get",{params}).subscribe(((x: any) => {
      this.smjestajiPodaci = x;
    }));
  }

  private getStipendije() {
    const params=new HttpParams()
      .set('pageNumber', this.stipendijePageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipendija/GetAll", {params}).subscribe(((x: any) => {
      this.stipendijePodaci = x;
    }));
  }

  getUniverziteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.podaciUniverziteti = x;
    }));
  }

  praksaDetalji(praksa: any) {
    this.router.navigate(["praksa-detalji",praksa.id]);
  }

  stipendijaDetlaji(stipendija: any) {
    this.router.navigate(["stipendija-detalji",stipendija.id]);
  }

  smjestajDetlaji(smjestaj: any) {
    this.router.navigate(["smjestaj-detalji",smjestaj.id]);
  }

  prevPagePrakse() {
    if(this.praksePageNumber>1){
      this.praksePageNumber--;
      this.getPrakse();
    }
  }

  nextPagePrakse() {
    if(this.praksePageNumber!=this.praksePodaci.totalPages) {
      this.praksePageNumber++;
      this.getPrakse();
    }
  }
  prevPageSmjestaji() {
    if(this.smjestajiPageNumber>1){
      this.smjestajiPageNumber--;
      this.getSmjestaji();
    }
  }

  nextPageSmjestaji() {
    if(this.smjestajiPageNumber!=this.smjestajiPodaci.totalPages) {
      this.smjestajiPageNumber++;
      this.getSmjestaji();
    }
  }
  prevPageStipendije() {
    if(this.stipendijePageNumber>1){
      this.stipendijePageNumber--;
      this.getStipendije();
    }
  }

  nextPageStipendije() {
    if(this.stipendijePageNumber!=this.stipendijePodaci.totalPages) {
      this.stipendijePageNumber++;
      this.getStipendije();
    }
  }

}
