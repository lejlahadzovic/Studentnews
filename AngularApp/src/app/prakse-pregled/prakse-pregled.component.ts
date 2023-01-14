import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
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
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getPrakse();
    this.getFirme()
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'60%'
    });
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
}
