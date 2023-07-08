import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {HttpClient, HttpParams} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {FileUpload} from "../models/file-upload.model";
import {FileUploadService} from "../services/file-upload.service";

@Component({
  selector: 'app-profil',
  templateUrl: './profil.component.html',
  styleUrls: ['./profil.component.css']
})
export class ProfilComponent implements OnInit {
  korisnik:any='';
  slika:any;
  slikaURL:string="";
  fakultetiPodaci: any;
  disabled=true;
  korisnikInfo:any;
  univerzitetiPodaci: any;
  firmePodaci: any;
  stipenditoriPodaci: any;
  selectedFiles?: FileList;
  currentFileUpload?: FileUpload;

  constructor(private httpKlijent: HttpClient, private uploadService: FileUploadService) { }

  ngOnInit(): void {
    this.korisnikInfo=this.loginInfo().autentifikacijaToken?.korisnik;
    this.getKorisnika();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  private getKorisnika() {
    if(this.korisnikInfo.isStudent) {
      this.getFakulteti();
      this.getStudent();
    }
    if(this.korisnikInfo.isIzdavacSmjestaja) {
      this.getIzdavac();
    }
    if(this.korisnikInfo.isReferentFakulteta) {
      this.getRefFakulteta();
      this.getFakulteti();
    }
    if(this.korisnikInfo.isReferentUniverziteta) {
      this.getRefUniverziteta();
      this.getUniverziteti();
    }
    if(this.korisnikInfo.isUposlenikFirme) {
      this.getUposlenikFirme();
      this.getFirme();
    }
    if(this.korisnikInfo.isUposlenikStipenditora) {
      this.getUposlenikStipenditora();
      this.getStipenditori();
    }
    if(this.korisnikInfo.isAdmin) {
      this.getAdmin();
    }
  }

  getFakulteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.fakultetiPodaci = x;
    }));
  }

  private getStudent() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Student/GetById?id=" + this.korisnikInfo.id).subscribe((x: any) => {
      this.korisnik=x;
      this.setSlika();
      });
  }
  private getIzdavac() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/IzdavacSmjestaja/GetById?id=" + this.korisnikInfo.id).subscribe(((x: any) => {
      this.korisnik = x;
      this.setSlika();
    }));
  }

  private setSlika() {
    this.uploadService.getUserImageURL(this.korisnikInfo.id).subscribe(url => {
      if (url) {
        this.slikaURL = url;
      } else if (this.korisnik.slika) {
        this.slikaURL = MojConfig.SlikePutanja + this.korisnik.slika;
      } else {
        this.slikaURL = "assets/Images/User%20icon.png";
      }
    });
  }

  snimi(){
    if(this.korisnikInfo.isStudent) {
      this.snimiStudenta();
    }
    if(this.korisnikInfo.isIzdavacSmjestaja) {
      this.snimiIzdavaca();
    }
    if(this.korisnikInfo.isReferentUniverziteta) {
      this.snimiRefUni();
    }
    if(this.korisnikInfo.isReferentFakulteta) {
      this.snimiRefFakulteta();
    }
    if(this.korisnikInfo.isUposlenikStipenditora) {
      this.snimiUposlenikS();
    }
    if(this.korisnikInfo.isUposlenikFirme) {
      this.snimiUposlenikF();
    }
    if(this.korisnikInfo.isAdmin) {
      this.snimiAdmin();
    }
  }
  snimiStudenta() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('email', this.korisnik.email);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('broj_indeksa', this.korisnik.broj_indeksa);
    formData.append('godinaStudija', this.korisnik.godinaStudija);
    formData.append('nacin_studiranja', this.korisnik.nacin_studiranja);
    formData.append('fakultetID', this.korisnik.fakultetID);
    formData.append('slika',this.slika);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/Student/Snimi",formData).subscribe((s:any)=>{
      this.getStudent();
    })
  }

  selectFile(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      const fileReader = new FileReader();
      fileReader.onload = () => {
        this.slikaURL = fileReader.result as string;
      };
      fileReader.readAsDataURL(file);
    }
    this.selectedFiles = event.target.files;
  }

  uploadSlike(): void {
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
      this.selectedFiles = undefined;

      if (file) {
        this.currentFileUpload = new FileUpload(file);
        this.uploadService.pushFileToStorage(this.currentFileUpload).subscribe(
          error => {
            console.log(error);
          }
        );
      }
    }
  }

  edit() {
    this.disabled = !this.disabled;
  }

  private snimiIzdavaca() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('slika', this.korisnik.slika);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('email', this.korisnik.email);
    formData.append('broj_telefona', this.korisnik.broj_telefona);
    formData.append('slika',this.slika);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/IzdavacSmjestaja/Snimi",formData).subscribe((x:any)=>{
    })
  }

  private getRefFakulteta() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/ReferentFakulteta/GetById?id=" + this.korisnikInfo.id).subscribe(((x: any) => {
      this.korisnik = x;
      this.setSlika();
    }));
  }

  private getRefUniverziteta() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/ReferentUniverziteta/GetById?id=" + this.korisnikInfo.id).subscribe(((x: any) => {
      this.korisnik = x;
      this.setSlika();
    }));
  }

  private snimiRefUni() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('email', this.korisnik.email);
    formData.append('univerzitetID', this.korisnik.univerzitetID);
    formData.append('slika',this.slika);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/ReferentUniverziteta/Snimi",formData).subscribe((x:any)=>{
    })
  }
  private snimiRefFakulteta() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('email', this.korisnik.email);
    formData.append('fakultetID', this.korisnik.fakultetID);
    formData.append('slika',this.slika);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/ReferentFakulteta/Snimi",formData).subscribe((x:any)=>{
    })
  }

  private getUniverziteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.univerzitetiPodaci = x;
    }));
  }

  private getUposlenikStipenditora() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikStipenditora/GetById?id=" + this.korisnikInfo.id).subscribe(((x: any) => {
      this.korisnik = x;
      this.setSlika();
    }));
  }

  private getUposlenikFirme() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikFirme/GetById?id=" + this.korisnikInfo.id).subscribe(((x: any) => {
      this.korisnik = x;
      this.setSlika();
    }));
  }

  private getFirme() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Firma/GetAll").subscribe(((x: any) => {
      this.firmePodaci = x;
    }));
  }

  private getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipenditor/GetAll").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
    }));
  }

  private snimiUposlenikS() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('slika', this.slika);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('email', this.korisnik.email);
    formData.append('stipenditorID', this.korisnik.stipenditorID);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikStipenditora/Snimi",formData).subscribe((s:any)=>{
    })
  }

  private snimiUposlenikF() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('email', this.korisnik.email);
    formData.append('firmaID', this.korisnik.firmaID);
    formData.append('pozicija', this.korisnik.pozicija);
    formData.append('slika',this.slika);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikFirme/Snimi",formData).subscribe((s:any)=>{
    })
  }

  private snimiAdmin() {
    const formData = new FormData();
    formData.append('id', this.korisnik.id);
    formData.append('password', this.korisnik.password);
    formData.append('username', this.korisnik.username);
    formData.append('ime', this.korisnik.ime);
    formData.append('prezime', this.korisnik.prezime);
    formData.append('email', this.korisnik.email);
    formData.append('slika',this.slika);
    this.uploadSlike();
    this.httpKlijent.post(MojConfig.adresa_servera+"/Administrator/Snimi",formData).subscribe((s:any)=>{
    })
  }

  private getAdmin() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Administrator/GetById?id=" + this.korisnikInfo.id).subscribe(((x: any) => {
      this.korisnik = x;
      this.setSlika();
    }));
  }
}
