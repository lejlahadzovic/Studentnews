import { Component } from '@angular/core';
import {MojConfig} from "./MojConfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent{
  title: string="Upravljanje podacima";

  ngOnInit(): void {

  }
  constructor() {

  }
}
