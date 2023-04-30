import {Component, OnInit, ViewChild} from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AppComponent} from "../app.component";

@Component({
  selector: 'app-stipendija-detalji',
  templateUrl: './stipendija-detalji.component.html',
  styleUrls: ['./stipendija-detalji.component.css'],
})
export class StipendijaDetaljiComponent implements OnInit {
  stipendijaId:number=0;
  stipendija: any;
  slikaPutanja=MojConfig.SlikePutanja;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.stipendijaId= +params['id'];
      this.getStipenijaDetalji();
    })
  }
  private getStipenijaDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Stipendija/GetById?id="+this.stipendijaId).subscribe((x:any)=>{
      this.stipendija=x
    });
  }

}
