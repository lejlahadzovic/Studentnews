import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-stipendije',
  templateUrl: './stipendije.component.html',
  styleUrls: ['./stipendije.component.css']
})
export class StipendijeComponent implements OnInit {

  displayedColumns: string[] = ['naslov', 'rokPrijave', 'iznos', 'brojStipendisata','naziv_stipenditora','vrijemeObjave','akcije'];
  stipendijePodaci: any;
  dialogtitle: any;
  stipendija:any;
  uposleniciPodaci: any;
  slika:any;
  slikaURL:string="";
  filter_stipenditori:any;
  stipenditoriPodaci:any;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getStipendije();
    this.getUposlenici();
    this.getStipenditori();
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'50%'
    });
  }

  private getStipendije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipendija/GetAll").subscribe(((x: any) => {
      this.stipendijePodaci = x;
    }));
  }

  private getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetStipenditori").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
    }));
  }

  chooseFile(files: any) {
    this.slika = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.slikaURL = reader.result as string;
    }
    reader.readAsDataURL(this.slika)
  }
  getpodaci(){
    if(this.stipendijePodaci==null)
      return [];
    return this.stipendijePodaci.filter((x:any)=>(
      (this.filter_stipenditori!=null?x.stipenditorid==this.filter_stipenditori:true)
    ));
  }
  dodajOglas() {
    this.dialogtitle='Dodaj oglas';
    this.stipendija={
      id:0,
      naslov:'',
      rokPrijave:'',
      opis:'',
      uslovi:'',
      iznos:'',
      kriterij:'',
      potrebnaDokumentacija:'',
      izvor:'',
      nivoObrazovanja:'',
      brojStipendisata:'',
      uposlenikID:0
    }
  }

  validacija() {
    return this.stipendija.rokPrijave==''||
      this.stipendija.opis==''||
      this.stipendija.uslovi==''||
      this.stipendija.iznos==''||
      this.stipendija.kriterij==''||
      this.stipendija.potrebnaDokumentacija==''||
      this.stipendija.izvor==''||
      this.stipendija.nivoObrazovanja==''||
      this.stipendija.brojStipendisata==''||
      this.slikaURL==''||
      this.stipendija.naslov==''||
      this.stipendija.uposlenikID==0;
  }

  snimi() {
    const formData=new FormData();
    formData.append('id',this.stipendija.id);
    formData.append('opis',this.stipendija.opis);
    formData.append('rokPrijave',formatDate(this.stipendija.rokPrijave,'shortDate','en-US'));
    formData.append('slika',this.slika);
    formData.append('uslovi',this.stipendija.uslovi);
    formData.append('iznos',this.stipendija.iznos);
    formData.append('kriterij',this.stipendija.kriterij);
    formData.append('potrebnaDokumentacija',this.stipendija.potrebnaDokumentacija);
    formData.append('izvor',this.stipendija.izvor);
    formData.append('nivoObrazovanja',this.stipendija.nivoObrazovanja);
    formData.append('brojStipendisata',this.stipendija.brojStipendisata);
    formData.append('naslov',this.stipendija.naslov);
    formData.append('uposlenikID',this.stipendija.uposlenikID);
    this.httpKlijent.post(MojConfig.adresa_servera+"/Stipendija/Snimi",formData).subscribe((s:any)=>{
      this.getStipendije();
    })
  }

  editOglas(x:any) {
    this.dialogtitle='Edit oglasa';
    this.stipendija=x;
    this.slikaURL=this.stipendija.slika;
  }

  obrisiOglas(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Stipendija/Obrisi",x.id).subscribe((s:any)=>{
      this.getStipendije();
    })
  }

  private getUposlenici() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikStipenditora/GetAll").subscribe(((x: any) => {
      this.uposleniciPodaci = x;
    }));
  }

  restart() {
    this.filter_stipenditori=null;
  }
}
