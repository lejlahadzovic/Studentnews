import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-fakultet-detalji',
  templateUrl: './fakultet-detalji.component.html',
  styleUrls: ['./fakultet-detalji.component.css']
})
export class FakultetDetaljiComponent implements OnInit {
  fakultetID:number=0;
  fakultet: any;
  slikaPutanja=MojConfig.SlikePutanja;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,  private router:Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.fakultetID= +params['id'];
      this.getFakultetDetalji();
    })
  }

  private getFakultetDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Fakultet/GetById?id="+this.fakultetID).subscribe((x:any)=>{
      this.fakultet=x
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
}
