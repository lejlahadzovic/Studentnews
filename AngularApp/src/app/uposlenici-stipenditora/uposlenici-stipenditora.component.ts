import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-uposlenici-stipenditora',
  templateUrl: './uposlenici-stipenditora.component.html',
  styleUrls: ['./uposlenici-stipenditora.component.css']
})
export class UposleniciStipenditoraComponent implements OnInit {
  uposleniciPodaci: any;
  uposlenik:any;
  displayedColumns: string[] = ['ime', 'prezime', 'email','stipenditor','akcije'];
  dialogtitle: any;
  filter_imePrezime= '';
  stipenditoriPodaci: any;
  slika:any;
  slikaURL:string="assets/Images/User%20icon.png";

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUposlenici();
    this.getStipenditori();
  }

  getUposlenici() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikStipenditora/GetAll").subscribe(((x: any) => {
      this.uposleniciPodaci = x;
    }));
  }
  getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipenditor/GetAll").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
    }));
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }
  getpodaci(){
    if(this.uposleniciPodaci==null)
      return [];
    return this.uposleniciPodaci.filter((s:any)=>
      (s.ime+' '+s.prezime).startsWith(this.filter_imePrezime)||
      (s.prezime+' '+s.ime).startsWith(this.filter_imePrezime)
    );
  }

  dodajUposlenika() {
    this.dialogtitle='Dodaj uposlenika';
    this.slikaURL="assets/Images/User%20icon.png";
    this.slika=null;
    this.uposlenik={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      email:'',
      stipenditorID: 1
    }
  }

  snimi() {
    const formData = new FormData();
    formData.append('id', this.uposlenik.id);
    formData.append('slika', this.slika);
    formData.append('password', this.uposlenik.password);
    formData.append('username', this.uposlenik.username);
    formData.append('ime', this.uposlenik.ime);
    formData.append('prezime', this.uposlenik.prezime);
    formData.append('email', this.uposlenik.email);
    formData.append('stipenditorID', this.uposlenik.stipenditorID);
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikStipenditora/Snimi",formData).subscribe((s:any)=>{
      this.getUposlenici();
    })
  }

  validacija() {
    return this.uposlenik.password==''||
      this.uposlenik.username==''||
      this.uposlenik.ime==''||
      this.uposlenik.prezime==''||
      this.uposlenik.email=='';
  }

  editUposlenika(x:any) {
    this.dialogtitle='Edit uposlenika';
    this.uposlenik=x;
    if(this.uposlenik.slika!=null && this.uposlenik.slika!='') {
      this.slikaURL = MojConfig.SlikePutanja + this.uposlenik.slika;
    }
    else{
      this.slikaURL="assets/Images/User%20icon.png";
    }
  }

  obrisiUposlenika(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikStipenditora/Obrisi",x.id).subscribe((s:any)=>{
      this.getUposlenici();
    })
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
