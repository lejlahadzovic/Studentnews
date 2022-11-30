import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import { UniverzitetiComponent } from './univerziteti/univerziteti.component';
import {RouterModule} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonToggleModule} from "@angular/material/button-toggle";
import { FakultetiComponent } from './fakulteti/fakulteti.component';

@NgModule({
  declarations: [
    AppComponent,
    UniverzitetiComponent,
    FakultetiComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
    RouterModule.forRoot([
      {path: 'putanja-univerziteti', component: UniverzitetiComponent},
      {path: 'putanja-fakulteti', component: FakultetiComponent}
    ]),
    FormsModule,
    MatToolbarModule,
    MatButtonToggleModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
