import {Component, ElementRef, HostListener, OnInit, ViewChild} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  praksePodaci:any;
  podaciFakulteti:any;
  smjestajiPodaci:any;
  stipendijePodaci:any;
  podaciUniveziteti:any;
  arr: number[] = [1, 2, 3, 4, 5, 6, 7, 8];
  totalCards: number = this.arr.length;
  currentPage: number = 1;
  pagePosition: string = "0%";
  cardsPerPage: any;
 totalPages: any;
  overflowWidth: any;
  cardWidth:any;
  containerWidth: any;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.getPrakse();
    this.cardsPerPage = this.getCardsPerPage();
    this.initializeSlider();
  }

  getPrakse() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Praksa/GetAll").subscribe(((x: any) => {
      this.praksePodaci = x;
    }));
  }

  getFakultete() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Fakultet/GetAll").subscribe(((x: any) => {
      this.podaciFakulteti = x;
    }));
  }

  private getSmjestaji() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Smjestaj/GetAll").subscribe(((x: any) => {
      this.smjestajiPodaci = x;
    }));
  }

  private getStipendije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipendija/GetAll").subscribe(((x: any) => {
      this.stipendijePodaci = x;
    }));
  }

  getUniverziteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.podaciUniveziteti = x;
    }));
  }

  @ViewChild("container", { static: true, read: ElementRef })
  container: any;
  @HostListener("window:resize") windowResize() {
    let newCardsPerPage = this.getCardsPerPage();
    if (newCardsPerPage != this.cardsPerPage) {
      this.cardsPerPage = newCardsPerPage;
      this.initializeSlider();
      if (this.currentPage > this.totalPages) {
        this.currentPage = this.totalPages;
        this.populatePagePosition();
      }
    }
  }


  initializeSlider() {
    this.totalPages = Math.ceil(this.totalCards / this.cardsPerPage);
    this.overflowWidth = `calc(${this.totalPages * 100}% + ${this.totalPages *
    10}px)`;
    this.cardWidth = `calc((${100 / this.totalPages}% - ${this.cardsPerPage}px) / ${this.cardsPerPage})`;
  }

  getCardsPerPage() {
    return Math.floor(this.container.nativeElement.offsetWidth / 200);
  }

  changePage(incrementor: number) {
    this.currentPage += incrementor;
    this.populatePagePosition();
  }

  populatePagePosition() {
    this.pagePosition = `calc(${-100 * (this.currentPage - 1)}% - ${10 *
    (this.currentPage - 1)}px)`;
  }
}
