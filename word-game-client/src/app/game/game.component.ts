import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/game.service';
import { WordBankService } from '../services/word-bank.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {

  constructor(private gameService: GameService, private wordBankService: WordBankService) { }
  letters: any = [];
  wordToCheck: string = "";
  points: number = 0;

  ngOnInit(): void {
    this.beginGame();
  }

  beginGame() {
    this.gameService.getStartingLetters().subscribe(response => {
       this.letters = response;
    });
  }

  addLetter(letter: string) {
    this.wordToCheck += letter;
  }

  submitWord() {
    this.gameService.checkWord(this.wordToCheck).subscribe((response) =>
    {
      this.wordBankService.addWord(response);
    }, err => {

    });

    this.wordToCheck = "";
  }
}
