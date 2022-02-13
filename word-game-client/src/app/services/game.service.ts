import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { StartingBoard } from '../model/startingLetters';
import { Word } from '../model/word';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  baseUrl: string = "https://localhost:5001/api/Word/";

  private currentLettersSource = new BehaviorSubject<string[]>([]);
  letters$ = this.currentLettersSource.asObservable();

  constructor(private http: HttpClient) { }

  startGame() {
    this.getStartingLetters().subscribe((response: any) => {
      this.currentLettersSource.next(response.startingLetters);
    }, err => {
      console.log(err);
    })
  }

  endGame() {
    // Maybe Have states of the game, that broadcast when something the game ends 
  }


  getStartingLetters() {
    return this.http.get(this.baseUrl + 'start-game');
  }

  checkWord(wordToCheck: string) {
    return this.http.post<Word>(this.baseUrl + 'submit-word', { word: wordToCheck });
  }
}
