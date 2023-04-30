import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})
export class StarRatingComponent implements OnInit {
  rating: number = 0;
  totalStars: number = 5;
  showForm = false;
  @Input() naziv:any;
  @Input() prosjecnaOcjena:any;
  @Output() ocjenaPodaci = new EventEmitter<any>();
  komentar: any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  showRatingForm() {
  this.showForm=true;
  }

  hideForm() {
    this.showForm=false;
  }

  setRating(rating: number) {
    this.rating = rating;
  }

  submitRating(rating: number) {
    console.log(`Ocjena: ${rating}`);
  }

  snimi() {
    const data = {
      studentId: this.loginInfo().autentifikacijaToken?.korisnik.id,
      ocjena: this.rating,
      komentar: this.komentar
    };
    this.ocjenaPodaci.emit(data);
    this.showForm=false;
  }
}
