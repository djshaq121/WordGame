import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { StartingBoard } from '../model/startingLetters';
import { Word } from '../model/word';
import { WordBankService } from './word-bank.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  baseUrl: string = environment.apiUrl;

  private currentLettersSource = new BehaviorSubject<string[]>([]);
  letters$ = this.currentLettersSource.asObservable();

  constructor(private http: HttpClient, private wordBankService: WordBankService, private toastr: ToastrService) { }

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
    return this.http.get(this.baseUrl + 'Word/start-game');
  }

  checkWord(wordToCheck: string) {
    if(this.wordBankService.checkIfWordExist(wordToCheck))
    {
      this.toastr.error(`${wordToCheck} is found already`);
      return;
    }


    this.verifyWord(wordToCheck).subscribe((response) =>
    {
      this.wordBankService.addWord(response);
    }, err => {
      this.toastr.error("Not a valid word");
    });
  }

  verifyWord(wordToCheck: string) {
    return this.http.post<Word>(this.baseUrl + 'Word/submit-word', { word: wordToCheck });
  }
}
