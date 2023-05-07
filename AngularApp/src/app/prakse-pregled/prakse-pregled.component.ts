import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient, HttpParams} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";
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
  pageNumber: number=1;
  pageSize: number=5;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog, private router:Router) { }

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
    return this.praksePodaci.dataItems.filter((x:any)=>(
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
    const params=new HttpParams()
      .set('pageNumber', this.pageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Praksa/GetAll", {params}).subscribe(((x: any) => {
      this.praksePodaci = x;
    }));
  }


  pregledDetalja(praksa: any) {
    this.router.navigate(["praksa-detalji",praksa.id]);
  }

  handlePageEvent($event: PageEvent) {
    this.pageNumber=$event.pageIndex+1;
    this.pageSize=$event.pageSize;
    this.getPrakse();
  }
}
