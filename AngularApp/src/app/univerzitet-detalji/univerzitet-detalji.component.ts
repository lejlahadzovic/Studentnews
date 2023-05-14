import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient, HttpParams} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";

declare var google: any;

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
  map:any;
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
      this.univerzitet=x;
      this.ucitajMapu();
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

  private ucitajMapu() {
    this.map = new google.maps.Map(document.getElementById('map'), {
      center: {lat: 43.85, lng: 18.41},
      zoom: 10
    });

    if (this.univerzitet.lokacija != null) {
      this.map.setCenter({lat: this.univerzitet.lokacija.lat, lng: this.univerzitet.lokacija.lng});
      new google.maps.Marker({
        position: {lat: this.univerzitet.lokacija.lat, lng: this.univerzitet.lokacija.lng},
        map: this.map,
      });
    }
  }
}
