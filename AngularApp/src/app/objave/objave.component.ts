import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-objave',
  templateUrl: './objave.component.html',
  styleUrls: ['./objave.component.css']
})
export class ObjaveComponent implements OnInit {
  dialogtitle: any;
  objava:any;
  displayedColumns: string[] = ['naslov', 'sadrzaj', 'kategorija', 'datumVrijeme','akcije'];
  slika:any;
  slikaURL:string="assets/Images/no-image.jpg";
  objavePodaci:any;
  kategorijePodaci: any;
  filter_naziv= '';
  filter_kategorija: any;

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getObjave();
    this.getKategorije();
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'50%'
    });
  }
  getObjave() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Objava/GetAll").subscribe(((x: any) => {
      this.objavePodaci = x;
    }));
  }
  getKategorije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetKategorije").subscribe(((x: any) => {
      this.kategorijePodaci = x;
    }));
  }
  dodajObjavu() {
    this.dialogtitle='Dodaj objavu';
    this.slikaURL="assets/Images/no-image.jpg";
    this.slika=null;
    this.objava={
      id:0,
      naslov:'',
      sadrzaj:'',
      kategorijaID: 1
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
    return this.objava.naslov==''||
      this.objava.sadrzaj==''||
      this.slikaURL=='assets/Images/no-image.jpg'
  }


  snimi() {
    const formData=new FormData();
    formData.append('id',this.objava.id);
    formData.append('naslov',this.objava.naslov);
    formData.append('sadrzaj',this.objava.sadrzaj);
    formData.append('slika',this.slika);
    formData.append('kategorijaID',this.objava.kategorijaID);
    this.httpKlijent.post(MojConfig.adresa_servera+"/Objava/Snimi",formData).subscribe((s:any)=>{
      this.getObjave();
    })
  }

  getpodaci(){
    if(this.objavePodaci==null)
      return [];
    return this.objavePodaci.filter((x:any)=>
      ((x.naslov.toLowerCase().startsWith(this.filter_naziv.toLowerCase()))
        && (this.filter_kategorija!=null?x.kategorijaID==this.filter_kategorija:true)
      )
    );
  }

  editObjave(x:any) {
    this.dialogtitle='Edit objave';
    this.objava=x;
    this.slikaURL=this.objava.slika;
  }

  obrisiObjavu(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Objava/Obrisi",x.id).subscribe((s:any)=>{
      this.getObjave();
    })
  }

  restart() {
    this.filter_naziv='';
    this.filter_kategorija=null;
  }
}
