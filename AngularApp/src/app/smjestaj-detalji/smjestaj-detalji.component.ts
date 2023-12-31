import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AppComponent} from "../app.component";

@Component({
  selector: 'app-smjestaj-detalji',
  templateUrl: './smjestaj-detalji.component.html',
  styleUrls: ['./smjestaj-detalji.component.css']
})
export class SmjestajDetaljiComponent implements OnInit {
  smjestajId:number=0;
  smjestaj: any;
  slikaPutanja=MojConfig.SlikePutanja;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private appComponent:AppComponent) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.smjestajId= +params['id'];
      this.getSmjestajDetalji();
    })
  }

  private getSmjestajDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Smjestaj/GetById?id="+this.smjestajId).subscribe((x:any)=>{
      this.smjestaj=x
    });
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  snimiOcjenu(data: any) {
    const newData= {
      ...data,
      smjestajID: this.smjestajId
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Smjestaj/OcijeniSmjestaj",newData).subscribe((s:any)=>{
      location.reload();
    })
  }
}
