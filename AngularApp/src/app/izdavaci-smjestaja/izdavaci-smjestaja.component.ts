import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-izdavaci-smjestaja',
  templateUrl: './izdavaci-smjestaja.component.html',
  styleUrls: ['./izdavaci-smjestaja.component.css']
})
export class IzdavaciSmjestajaComponent implements OnInit {
  izdavaciPodaci: any;
  filter_imePrezime= '';
  displayedColumns: string[] = ['ime', 'prezime', 'email', 'broj_telefona','akcije'];
  dialogtitle: any;
  izdavac:any;
  slika:any;
  slikaURL:string="assets/Images/User%20icon.png";

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getIzdavaci();
  }
  getIzdavaci() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/IzdavacSmjestaja/GetAll").subscribe(((x: any) => {
      this.izdavaciPodaci = x;
    }));
  }

  getpodaci(){
    if(this.izdavaciPodaci==null)
      return [];
    return this.izdavaciPodaci.filter((s:any)=>
      (s.ime+' '+s.prezime).startsWith(this.filter_imePrezime)||
        (s.prezime+' '+s.ime).startsWith(this.filter_imePrezime)
    );
  }

  snimi() {
    const formData = new FormData();
    formData.append('id', this.izdavac.id);
    formData.append('slika', this.izdavac.slika);
    formData.append('password', this.izdavac.password);
    formData.append('username', this.izdavac.username);
    formData.append('ime', this.izdavac.ime);
    formData.append('prezime', this.izdavac.prezime);
    formData.append('email', this.izdavac.email);
    formData.append('broj_telefona', this.izdavac.broj_telefona);
    formData.append('slika',this.slika);
    this.httpKlijent.post(MojConfig.adresa_servera+"/IzdavacSmjestaja/Snimi",formData).subscribe((x:any)=>{
      this.getIzdavaci();
    })
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }

  dodaj() {
    this.dialogtitle='Dodaj izdavača';
    this.slikaURL="assets/Images/User%20icon.png";
    this.slika=null;
    this.izdavac={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      email:'',
      broj_telefona:''
    }
  }

  editIzdavac(x:any) {
    this.dialogtitle='Edit izdavača';
    this.izdavac=x;
    if(this.izdavac.slika!=null) {
      this.slikaURL = MojConfig.SlikePutanja + this.izdavac.slika;
    }
    else{
      this.slikaURL="assets/Images/User%20icon.png";
    }
  }

  validacija() {
    return this.izdavac.password==''||
      this.izdavac.username==''||
      this.izdavac.ime==''||
      this.izdavac.prezime==''||
      this.izdavac.email==''||
      this.izdavac.broj_telefona=='';
  }
  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/IzdavacSmjestaja/Obrisi",x.id).subscribe((x:any)=>{
      this.getIzdavaci();
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
