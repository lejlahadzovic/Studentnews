import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-smjestaj-pregled',
  templateUrl: './smjestaj-pregled.component.html',
  styleUrls: ['./smjestaj-pregled.component.css']
})
export class SmjestajPregledComponent implements OnInit {

  smjestajiPodaci: any;
  dialogtitle: any;
  izdavaciPodaci:any;
  filter_izdavaci:any
  filter_gradovi:any;
  gradoviPodaci:any;
   smjestaj: any;
   korisnik:any;
  pageNumber: number=1;
  pageSize: number=5;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog, private router:Router) { }

  ngOnInit(): void {
    this.korisnik=this.loginInfo().autentifikacijaToken?.korisnik;
    this.getSmjestaji();
    this.getGradovi();
    this.getIzdavaci();
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'60%'
    });
  }

  getPodaci() {
    if(this.smjestajiPodaci==null)
      return [];
    return this.smjestajiPodaci.dataItems.filter((x:any)=>(
      (this.filter_gradovi!=null?x.gradID==this.filter_gradovi:true)&&
      (this.filter_izdavaci!=null?x.izdavacID==this.filter_izdavaci:true)
    ));
  }

  restart() {
    this.filter_izdavaci=null;
    this.filter_gradovi=null;
  }

  private getSmjestaji() {
    const params=new HttpParams()
      .set('pageNumber', this.pageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Smjestaj/Get",{params}).subscribe(((x: any) => {
      this.smjestajiPodaci = x;
    }));
  }
  private getIzdavaci() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/IzdavacSmjestaja/GetAll").subscribe(((x: any) => {
      this.izdavaciPodaci = x;
    }));
  }
  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }


  dodajSmjestaj(id:number) {
    this.dialogtitle='Dodaj smjestaj';
    this.smjestaj={
      id:0,
      smjestajID:id,
      studentID:this.korisnik.isStudent? this.korisnik.id : 39,
      datumPrijave:new Date(),
      datumOdjave:new Date(),
      brojOsoba:0,

    }
  }

  snimi_dugme() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/RezervacijaSmjestaja/Snimi", this.smjestaj).subscribe(((x: any) => {
     this.dialog.closeAll();
    }));
  }
  pregledDetalja(smjestaj: any) {
    this.router.navigate(["smjestaj-detalji",smjestaj.id]);
  }
  handlePageEvent($event: PageEvent) {
    this.pageNumber=$event.pageIndex+1;
    this.pageSize=$event.pageSize;
    this.getSmjestaji();
  }
}
