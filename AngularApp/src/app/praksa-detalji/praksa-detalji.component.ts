import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-praksa-detalji',
  templateUrl: './praksa-detalji.component.html',
  styleUrls: ['./praksa-detalji.component.css']
})
export class PraksaDetaljiComponent implements OnInit {
  praksaId:number=0;
  praksa: any;
  slikaPutanja=MojConfig.SlikePutanja;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.praksaId= +params['id'];
      this.getPraksaDetalji();
    })
  }

  private getPraksaDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Praksa/GetById?id="+this.praksaId).subscribe((x:any)=>{
      this.praksa=x
    });
  }
}
