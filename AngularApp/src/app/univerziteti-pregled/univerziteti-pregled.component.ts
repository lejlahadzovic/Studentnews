import {MatPaginator, PageEvent} from '@angular/material/paginator';
import { Component, OnInit, ViewChild } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {Observable} from "rxjs";
import {Router} from "@angular/router";



@Component({
  selector: 'app-univerziteti-pregled',
  templateUrl: './univerziteti-pregled.component.html',
  styleUrls: ['./univerziteti-pregled.component.css']
})
export class UniverzitetiPregledComponent implements OnInit {

  univerzitetiPodaci: any;
  filtrirano: any;
  dialogtitle: any;
  gradoviPodaci: any;
  filter_grad: any;
  pageNumber: number=1;
  pageSize: number=5;


  constructor(private httpKlijent: HttpClient, private dialog: MatDialog, private router:Router) {
  }

  ngOnInit(): void {
    this.getUniverziteti();
    this.getGradovi()
  }

  openDialog(templateRef: any) {
    this.dialog.open(templateRef, {
      width: '60%'
    });
  }

  restart() {
    this.filter_grad = null;
  }

  getpodaci() {
    if (this.univerzitetiPodaci == null)
      return [];
    return this.univerzitetiPodaci.dataItems.filter((x: any) => (
      (this.filter_grad != null ? x.gradid == this.filter_grad : true))
    );

  }

  getUniverziteti() {
    const params=new HttpParams()
      .set('pageNumber', this.pageNumber.toString())
      .set('pageSize', this.pageSize.toString());

    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll",{params}).subscribe(((x: any) => {
      this.univerzitetiPodaci = x;
    }));
  }

  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }
  pregledDetalja(univerzitet: any) {
    this.router.navigate(["univerzitet-detalji",univerzitet.id]);
  }

  handlePageEvent($event: PageEvent) {
    this.pageNumber=$event.pageIndex+1;
    this.pageSize=$event.pageSize;
    this.getUniverziteti();
  }
}
