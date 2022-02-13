import { Component, OnInit, ViewChild } from '@angular/core';
import { CountdownComponent, CountdownConfig, CountdownEvent } from 'ngx-countdown';
import { GameService } from '../services/game.service';
import { PointsService } from '../services/points.service';
import { WordBankService } from '../services/word-bank.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {

  constructor(public gameService: GameService, private wordBankService: WordBankService, public pointService: PointsService) { }
  wordToCheck: string = "";
  points: number = 0;
  config: CountdownConfig = { leftTime: 10 };
 
  ngOnInit(): void {

    this.beginGame();
  }

  beginGame() {
    this.gameService.startGame()
  }

  addLetter(letter: string) {
    this.wordToCheck += letter;
  }

  submitWord() {
    this.gameService.checkWord(this.wordToCheck).subscribe((response) =>
    {
      this.wordBankService.addWord(response);
    }, err => {
        console.log(err);
    });

    this.wordToCheck = "";
  }

  clearWord() {
    this.wordToCheck = "";
  }

  countDown(event: CountdownEvent) {
   
    if(event.action === 'done')
    {
       console.log(event);
        this.gameService.endGame();
        let userPoints: number = 0;
        this.pointService.points$.pipe(take(1)).subscribe(points => userPoints = points);
        alert(`Your Score: ${userPoints}`);
    }
  }
}
