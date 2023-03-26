import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-prakse',
  templateUrl: './prakse.component.html',
  styleUrls: ['./prakse.component.css']
})
export class PrakseComponent implements OnInit {
  dialogtitle: any;
  praksePodaci:any;
  praksa:any;
  slika:any;
  slikaURL:string="assets/Images/no-image.jpg";
  displayedColumns: string[] = ['naslov', 'rokPrijave', 'trajanjePrakse', 'placena','firma','akcije'];
  uposleniciPodaci: any;
  firmePodaci: any;
  filter_firme: any;
  filter_placena: any;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUposlenici();
    this.getPrakse();
    this.getFirme();
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'60%'
    });
  }

  getPrakse() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Praksa/GetAll").subscribe(((x: any) => {
      this.praksePodaci = x;
    }));
  }

  getFirme() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Firma/GetAll").subscribe(((x: any) => {
      this.firmePodaci = x;
    }));
  }

  getUposlenici() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikFirme/GetAll").subscribe(((x: any) => {
      this.uposleniciPodaci = x;
    }));
  }

  dodajOglas() {
    this.dialogtitle='Dodaj oglas';
    this.dialogtitle='Dodaj objavu';
    this.slikaURL="assets/Images/no-image.jpg";
    this.praksa={
      id:0,
      naslov:'',
      rokPrijave:'',
      opis:'',
      pocetakPrakse:'',
      krajPrakse:'',
      kvalifikacije:'',
      benefiti:'',
      placena:false,
      uposlenikID:1
    }
  }

  chooseFile(files: any) {
    this.slika = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.slikaURL = reader.result as string;
    }
    reader.readAsDataURL(this.slika)
  }

  validacija() {
    return this.praksa.rokPrijave==''||
      this.praksa.opis==''||
      this.praksa.pocetakPrakse==''||
      this.praksa.krajPrakse==''||
      this.praksa.kvalifikacije==''||
      this.praksa.benefiti==''||
      this.slikaURL=='assets/Images/no-image.jpg'||
      this.praksa.naslov=='';
  }

  snimi() {
    const formData=new FormData();
    formData.append('id',this.praksa.id);
    formData.append('opis',this.praksa.opis);
    formData.append('rokPrijave',formatDate(this.praksa.rokPrijave,'shortDate','en-US'));
    formData.append('pocetakPrakse',formatDate(this.praksa.pocetakPrakse,'shortDate','en-US'));
    formData.append('slika',this.slika);
    formData.append('krajPrakse',formatDate(this.praksa.krajPrakse,'shortDate','en-US'));
    formData.append('kvalifikacije',this.praksa.kvalifikacije);
    formData.append('benefiti',this.praksa.benefiti);
    formData.append('placena',this.praksa.placena);
    formData.append('uposlenikID',this.praksa.uposlenikID);
    formData.append('naslov',this.praksa.naslov);
    this.httpKlijent.post(MojConfig.adresa_servera+"/Praksa/Snimi",formData).subscribe((s:any)=>{
      this.getPrakse();
    })
  }

  getpodaci(){
    if(this.praksePodaci==null)
      return [];
    return this.praksePodaci.filter((x:any)=>(
      (this.filter_placena!=null?x.placena==this.filter_placena:true)&&
      (this.filter_firme!=null?x.firmaid==this.filter_firme:true))
    );
  }

  obrisiOglas(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Praksa/Obrisi",x.id).subscribe((s:any)=>{
      this.getPrakse();
    })
  }

  editOglas(x:any) {
    this.dialogtitle='Edit oglasa';
    this.praksa=x;
    this.slikaURL=this.praksa.slika;
  }

  restart() {
    this.filter_placena=null;
    this.filter_firme=null;
  }
}
