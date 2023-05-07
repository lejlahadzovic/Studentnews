import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-fakulteti-pregled',
  templateUrl: './fakulteti-pregled.component.html',
  styleUrls: ['./fakulteti-pregled.component.css']
})
export class FakultetiPregledComponent implements OnInit {

  podaciFakulteti: any;
  dialogtitle: any;
  pageNumber: number=1;
  pageSize: number=5;
  constructor(private httpKlijent:HttpClient,private dialog: MatDialog, private router:Router) { }

  ngOnInit(): void {
    this.getFakulteti();
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'60%'
    });
  }


  getFakulteti() {
    const params=new HttpParams()
      .set('pageNumber', this.pageNumber.toString())
      .set('pageSize', this.pageSize.toString());
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll",{params}).subscribe(((x: any) => {
      this.podaciFakulteti = x;
    }));
  }


  pregledDetalja(fakultet: any) {
    this.router.navigate(["fakultet-detalji",fakultet.id]);
  }

  handlePageEvent($event: PageEvent) {
    this.pageNumber=$event.pageIndex+1;
    this.pageSize=$event.pageSize;
    this.getFakulteti();
  }
}
