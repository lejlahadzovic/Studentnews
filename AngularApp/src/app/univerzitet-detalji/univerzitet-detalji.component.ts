import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient, HttpParams} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-univerzitet-detalji',
  templateUrl: './univerzitet-detalji.component.html',
  styleUrls: ['./univerzitet-detalji.component.css']
})
export class UniverzitetDetaljiComponent implements OnInit {
  objavePageNumber: number=1;
  fakultetiPageNumber: number=1;
  univerzitetID:number=0;
  univerzitet: any;
  slikaPutanja=MojConfig.SlikePutanja;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,  private router:Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.univerzitetID= +params['id'];
      this.getUniverzitetDetalji();
    })
  }

  private getUniverzitetDetalji() {
    const params=new HttpParams()
      .set('id', this.univerzitetID.toString())
      .set('objavePageNumber', this.objavePageNumber.toString())
      .set('fakultetiPageNumber', this.fakultetiPageNumber.toString());

    this.httpKlijent.get(MojConfig.adresa_servera+"/Univerzitet/GetById",{params}).subscribe((x:any)=>{
      this.univerzitet=x
    });
  }

  fakultetDetalji(fakultet: any) {
    this.router.navigate(["fakultet-detalji",fakultet.id]);
  }

  objavaDetalji(objava: any) {
    this.router.navigate(["objava",objava.id]);
  }


  snimiOcjenu(data: any) {
    const newData= {
      ...data,
      univerzitetId: this.univerzitetID
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Univerzitet/OcijeniUniverzitet",newData).subscribe((s:any)=>{
      location.reload();
    })
  }

  objavePageChanged($event: PageEvent) {
    this.objavePageNumber=$event.pageIndex+1;
    this.getUniverzitetDetalji();
  }

  fakultetiPageChanged($event: PageEvent) {
    this.fakultetiPageNumber=$event.pageIndex+1;
    this.getUniverzitetDetalji();
  }
}
