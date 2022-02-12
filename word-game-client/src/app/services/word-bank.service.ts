import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject } from 'rxjs';
import { Word } from '../model/word';
import { PointsService } from './points.service';

@Injectable({
  providedIn: 'root'
})
export class WordBankService {

  private currentWordBankSource = new BehaviorSubject<string[]>([]);
  wordBank$ = this.currentWordBankSource.asObservable();

  constructor(private pointService: PointsService) { }

  reset() {
    this.currentWordBankSource.next([]);
  }

  addWord(wordToAdd: Word) {
    if(!this.checkIfWordExist(wordToAdd.word))
    {
      let wordBank = this.currentWordBankSource.value;
      wordBank.push(wordToAdd.word);
      this.currentWordBankSource.next(wordBank);

      // update points
      this.pointService.addPoints(wordToAdd.points);
    }
  }

  checkIfWordExist(word: string): boolean {

    const words = this.currentWordBankSource.value;
    return words.indexOf(word) > -1;
  }
}
