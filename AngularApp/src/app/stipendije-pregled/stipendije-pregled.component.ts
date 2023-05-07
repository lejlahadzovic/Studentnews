import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";

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
  pageNumber: number=1;
  pageSize: number=5;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog, private router:Router) { }

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
    return this.stipendijePodaci.dataItems.filter((x:any)=>(
      (this.filter_stipenditor!=null?x.stipenditorid==this.filter_stipenditor:true))
    );
  }
  private getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipenditor/GetAll").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
    }));
  }

  private getStipendije() {
    const params=new HttpParams()
      .set('pageNumber', this.pageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipendija/GetAll", {params}).subscribe(((x: any) => {
      this.stipendijePodaci = x;
    }));
  }

  pregledDetalja(stipendija: any) {
    this.router.navigate(["stipendija-detalji",stipendija.id]);
  }

  handlePageEvent($event: PageEvent) {
    this.pageNumber=$event.pageIndex+1;
    this.pageSize=$event.pageSize;
    this.getStipendije();
  }
}
