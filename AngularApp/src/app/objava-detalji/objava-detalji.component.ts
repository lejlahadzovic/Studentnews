import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-objava-detalji',
  templateUrl: './objava-detalji.component.html',
  styleUrls: ['./objava-detalji.component.css']
})
export class ObjavaDetaljiComponent implements OnInit {
  objavaId:number=0;
  objava: any;
  slikaPutanja=MojConfig.SlikePutanja;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.objavaId= +params['id'];
      this.getObjavaDetalji();
    })
  }

  private getObjavaDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Objava/GetById?id="+this.objavaId).subscribe((x:any)=>{
      this.objava=x
    });
  }
}
