import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-stipendije-pregled',
  templateUrl: './stipendije-pregled.component.html',
  styleUrls: ['./stipendije-pregled.component.css']
})
export class StipendijePregledComponent implements OnInit {

  stipendijePodaci: any;
  dialogtitle: any;

  filter_stipenditor:any;
  stipenditoriPodaci:any;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getStipendije();
    this.getStipenditori()
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'60%'
    });
  }

  restart() {
    this.filter_stipenditor=null;
  }
  getpodaci(){
    if(this.stipendijePodaci==null)
      return [];
    return this.stipendijePodaci.filter((x:any)=>(
      (this.filter_stipenditor!=null?x.stipenditorid==this.filter_stipenditor:true))
    );
  }
  private getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetStipenditori").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
    }));
  }

  private getStipendije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipendija/GetAll").subscribe(((x: any) => {
      this.stipendijePodaci = x;
    }));
  }

}