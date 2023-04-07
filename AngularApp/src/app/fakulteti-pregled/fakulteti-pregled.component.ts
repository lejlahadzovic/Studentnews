import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";
import {Router} from "@angular/router";

@Component({
  selector: 'app-fakulteti-pregled',
  templateUrl: './fakulteti-pregled.component.html',
  styleUrls: ['./fakulteti-pregled.component.css']
})
export class FakultetiPregledComponent implements OnInit {

  podaciFakulteti: any;
  dialogtitle: any;

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
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.podaciFakulteti = x;
    }));
  }


  pregledDetalja(fakultet: any) {
    this.router.navigate(["fakultet-detalji",fakultet.id]);
  }
}
