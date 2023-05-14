import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient, HttpParams} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";

declare var google: any;

@Component({
  selector: 'app-fakultet-detalji',
  templateUrl: './fakultet-detalji.component.html',
  styleUrls: ['./fakultet-detalji.component.css']
})
export class FakultetDetaljiComponent implements OnInit {
  objavePageNumber: number=1;
  fakultetID:number=0;
  fakultet: any;
  slikaPutanja=MojConfig.SlikePutanja;
  map:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,  private router:Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.fakultetID= +params['id'];
      this.getFakultetDetalji();
    })
  }

  private getFakultetDetalji() {
    const params=new HttpParams()
      .set('id', this.fakultetID.toString())
      .set('objavePageNumber', this.objavePageNumber.toString());

    this.httpKlijent.get(MojConfig.adresa_servera+"/Fakultet/GetById",{params}).subscribe((x:any)=>{
      this.fakultet=x;
      this.ucitajMapu();
    });
  }
  objavaDetalji(objava: any) {
    this.router.navigate(["objava",objava.id]);
  }

  snimiOcjenu(data: any) {
    const newData= {
      ...data,
      fakultetID: this.fakultetID
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Fakultet/OcijeniFakultet",newData).subscribe((s:any)=>{
      location.reload();
    })
  }

  objavePageChanged($event: PageEvent) {
    this.objavePageNumber=$event.pageIndex+1;
    this.getFakultetDetalji();
  }

  private ucitajMapu() {
    this.map = new google.maps.Map(document.getElementById('map'), {
      center: { lat: 43.85, lng: 18.41 },
      zoom: 10
    });

    if(this.fakultet.lokacija!=null) {
      this.map.setCenter({lat: this.fakultet.lokacija.lat, lng: this.fakultet.lokacija.lng});
      new google.maps.Marker({
        position: {lat: this.fakultet.lokacija.lat, lng: this.fakultet.lokacija.lng},
        map: this.map,
        title: 'Hello World!',
      });
    }
  }
}
