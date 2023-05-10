import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
@Component({
  selector: 'app-prakse-pregled',
  templateUrl: './prakse-pregled.component.html',
  styleUrls: ['./prakse-pregled.component.css']
})
export class PraksePregledComponent implements OnInit {
  praksePodaci: any;
  dialogtitle: any;
  filter_placena: any;
  filter_firme:any;
  firmePodaci:any;
  korisnik:any;
  CV:any;
  cvURL:string="assets/CV/";
  imeFileCV:string='';
  imeFileCertifikati:string='';
  imeFilePropratnoPismo:string='';
  propratno_pismoURL:string="assets/PropratnoPismo/";
  propratnoPismo:any;
  certifikatiURL:string="assets/Certifikati/";
  Certifikati:any;
   prijavaprakse: any;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.korisnik=this.loginInfo().autentifikacijaToken?.korisnik;
    this.getPrakse();
    this.getFirme()
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  restart() {
    this.filter_placena=null;
    this.filter_firme=null;
  }
  getpodaci(){
    if(this.praksePodaci==null)
      return [];
    return this.praksePodaci.filter((x:any)=>(
      (this.filter_placena!=null?x.placena==this.filter_placena:true)&&
      (this.filter_firme!=null?x.firmaid==this.filter_firme:true))
    );
  }


  getFirme() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Firma/GetAll").subscribe(((x: any) => {
      this.firmePodaci = x;
    }));
  }

  getPrakse() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Praksa/GetAll").subscribe(((x: any) => {
      this.praksePodaci = x;
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
  chooseFilePismo(files: any) {
    this.propratnoPismo = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this. propratno_pismoURL = reader.result as string;
    }
    reader.readAsDataURL( this.propratnoPismo)
    this.imeFilePropratnoPismo=this.propratnoPismo.name;
  }
  chooseFileCert(files: any) {
    this.Certifikati = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.certifikatiURL = reader.result as string;
    }
    reader.readAsDataURL( this.Certifikati)
    this.imeFileCertifikati=this.Certifikati.name;
  }
  dodajPrijavu(id: number) {
    this.prijavaprakse={
      praksaID: id,
      propratnoPismo: '',
      cv: '',
      certifikati: '',
      studentIme:this.korisnik.isStudent? this.korisnik.ime: 'Sqdzsc',
      studentID:this.korisnik.isStudent? this.korisnik.id : 39}
  }
  snimiDugme() {
    const formData=new FormData();
    formData.append('praksaID',this.prijavaprakse.praksaID);
    formData.append('studentID',this.prijavaprakse.studentID);
    formData.append('studentIme',this.prijavaprakse.studentIme);
    formData.append('propratnoPismo',this.propratnoPismo);
    formData.append('cv',this.CV);
    formData.append('certifikati',this.Certifikati);
    this.httpKlijent.post(MojConfig.adresa_servera+"/PrijavaPraksa/Snimi",formData).subscribe((s:any)=>{
      this.dialog.closeAll();
    })
  }
}
