import {MatPaginator, PageEvent} from '@angular/material/paginator';
import { Component, OnInit, ViewChild } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {Observable} from "rxjs";


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



  constructor(private httpKlijent: HttpClient, private dialog: MatDialog) {
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
    return this.univerzitetiPodaci.filter((x: any) => (
      (this.filter_grad != null ? x.gradid == this.filter_grad : true))
    );

    return this.filtrirano;
  }

  getUniverziteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.univerzitetiPodaci = x;
    }));
  }

  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }


}
