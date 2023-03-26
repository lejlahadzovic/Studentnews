import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-referenti-fakulteta',
  templateUrl: './referenti-fakulteta.component.html',
  styleUrls: ['./referenti-fakulteta.component.css']
})
export class ReferentiFakultetaComponent implements OnInit {
  referentiPodaci: any;
  referent:any;
  displayedColumns: string[] = ['ime', 'prezime','email', 'ustanova','akcije'];
  dialogtitle: any;
  filter_imePrezime= '';
  fakultetiPodaci: any;
  slika:any;
  slikaURL:string="assets/Images/User%20icon.png";

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getReferenti();
    this.getFakulteti();
  }

  getReferenti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/ReferentFakulteta/GetAll").subscribe(((x: any) => {
      this.referentiPodaci = x
    }));
  }

  getFakulteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.fakultetiPodaci = x;
    }));
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }

  getpodaci(){
    if(this.referentiPodaci==null)
      return [];
    return this.referentiPodaci.filter((x:any)=>
      (x.ime+' '+x.prezime).startsWith(this.filter_imePrezime)||
      (x.prezime+' '+x.ime).startsWith(this.filter_imePrezime)
    );
  }

  dodajReferenta() {
    this.dialogtitle='Dodaj referenta';
    this.slikaURL="assets/Images/User%20icon.png";
    this.referent={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      email:'',
      fakultet_id:1
    }
  }

  snimiRefFakultet() {
    const formData = new FormData();
    formData.append('id', this.referent.id);
    formData.append('password', this.referent.password);
    formData.append('username', this.referent.username);
    formData.append('ime', this.referent.ime);
    formData.append('prezime', this.referent.prezime);
    formData.append('email', this.referent.email);
    formData.append('fakultetID', this.referent.fakultetID);
    formData.append('slika',this.slika);
    this.httpKlijent.post(MojConfig.adresa_servera+"/ReferentFakulteta/Snimi",formData).subscribe((x:any)=>{
      this.getReferenti();
    })
  }

  editReferenta(x:any) {
    this.dialogtitle='Edit referenta';
    this.referent=x;
    if(this.referent.slika!=null) {
      this.slikaURL = MojConfig.SlikePutanja + this.referent.slika;
    }
    else{
      this.slikaURL="assets/Images/User%20icon.png";
    }
  }
  obrisiReferenta(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/ReferentFakulteta/Obrisi",x.id).subscribe((x:any)=>{
      this.getReferenti();
    })
  }

  validacija() {
    return this.referent.password==''||
      this.referent.username==''||
      this.referent.ime==''||
      this.referent.prezime==''||
      this.referent.email=='';
  }

  snimi() {
    this.snimiRefFakultet();
  }

  chooseFile(files: any) {
    this.slika = files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.slikaURL = reader.result as string;
    }
    reader.readAsDataURL(this.slika)
  }
}
