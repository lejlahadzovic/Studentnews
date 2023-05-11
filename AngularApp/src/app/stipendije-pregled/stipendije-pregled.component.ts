import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {formatDate} from "@angular/common";
import {Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-stipendije-pregled',
  templateUrl: './stipendije-pregled.component.html',
  styleUrls: ['./stipendije-pregled.component.css']
})
export class StipendijePregledComponent implements OnInit {

  stipendijePodaci: any;
  dialogtitle: any;
  cvURL:string="assets/CV/";
  CV:any;
  imeFileCV:string='';
  imeFileDokumentacija:string='';
  imeFileProsjek:string='';
  dokumentacijaURL:string="assets/Dokumentacija/";
  Dokumentacija:any;
  prosjekURL:string="assets/ProsjekOcjena/";
  ProsjekOcjena:any;
  filter_stipenditor:any;
  stipenditoriPodaci:any;
  odabranastipendija: any;
  prijavastipendije: any;
  korisnik:any;
  pageNumber: number=1;
  pageSize: number=5;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog, private router:Router) { }

  ngOnInit(): void {
    this.korisnik=this.loginInfo().autentifikacijaToken?.korisnik;
    this.getStipendije();
    this.getStipenditori()
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%',

    });
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  restart() {
    this.filter_stipenditor=null;
  }
  getpodaci(){
    if(this.stipendijePodaci==null)
      return [];
    return this.stipendijePodaci.dataItems.filter((x:any)=>(
      (this.filter_stipenditor!=null?x.stipenditorid==this.filter_stipenditor:true))
    );
  }
  private getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipenditor/GetAll").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
    }));
  }
  chooseFileCV(files: any) {
    this.CV = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.cvURL = reader.result as string;
    }
    reader.readAsDataURL( this.CV)
    this.imeFileCV=this.CV.name;
  }
  chooseFileDok(files: any) {
    this.Dokumentacija = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.dokumentacijaURL = reader.result as string;
    }
    reader.readAsDataURL( this.Dokumentacija)
    this.imeFileDokumentacija=this.Dokumentacija.name;
  }
  chooseFileProsjek(files: any) {
    this.ProsjekOcjena = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.prosjekURL = reader.result as string;
    }
    reader.readAsDataURL( this.ProsjekOcjena)
this.imeFileProsjek=this.ProsjekOcjena.name;
  }
  private getStipendije() {
    const params=new HttpParams()
      .set('pageNumber', this.pageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipendija/GetAll", {params}).subscribe(((x: any) => {
      this.stipendijePodaci = x;
    }));
  }
  dodajPrijavu(id: number) {
    this.prijavastipendije={
      stipendijaId: id,
      dokumentacija: '',
      cv: '',
      prosjekOcjena: '',
      studentIme:this.korisnik.isStudent? this.korisnik.ime: 'Sqdzsc',
      studentID:this.korisnik.isStudent? this.korisnik.id : 39}
  }
  snimiDugme() {
    const formData=new FormData();
    formData.append('stipendijaId',this.prijavastipendije.stipendijaId);
    formData.append('studentID',this.prijavastipendije.studentID);
    formData.append('studentIme',this.prijavastipendije.studentIme);
    formData.append('dokumentacija',this.Dokumentacija);
    formData.append('cv',this.CV);
    formData.append('prosjekOcjena',this.ProsjekOcjena);
    this.httpKlijent.post(MojConfig.adresa_servera+"/PrijavaStipendija/Snimi",formData).subscribe((s:any)=>{
      this.dialog.closeAll();
    })
  }
  pregledDetalja(stipendija: any) {
    this.router.navigate(["stipendija-detalji",stipendija.id]);
  }

  handlePageEvent($event: PageEvent) {
    this.pageNumber=$event.pageIndex+1;
    this.pageSize=$event.pageSize;
    this.getStipendije();
  }
}
