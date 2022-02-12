import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Word } from '../model/word';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  baseUrl: string = "https://localhost:5001/api/Word/";
  constructor(private http: HttpClient) { }

  getStartingLetters() {
    return this.http.get<string[]>(this.baseUrl + 'start-game');
  }

  checkWord(wordToCheck: string) {
    return this.http.post<Word>(this.baseUrl + 'submit-word', { word: wordToCheck });
  }
}
