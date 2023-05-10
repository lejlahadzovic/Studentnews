import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-two-f-otkljucaj',
  templateUrl: './two-f-otkljucaj.component.html',
  styleUrls: ['./two-f-otkljucaj.component.css']
})
export class TwoFOtkljucajComponent implements OnInit {


public code: string='';
  constructor(private httpKlijent: HttpClient, private router: Router ) { }

  ngOnInit(): void {

  }

  otkljucaj() {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Autentifikacija/Otkljucaj/"+this.code, MojConfig.http_opcije()).subscribe(x=>{
      this.router.navigateByUrl("/");
    });
  }


}
