import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GameComponent } from './game/game.component';
import { TileComponent } from './tile/tile.component';
import { FormsModule } from '@angular/forms';
import { WordBankComponent } from './word-bank/word-bank.component';

@NgModule({
  declarations: [
    AppComponent,
    GameComponent,
    TileComponent,
    WordBankComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
