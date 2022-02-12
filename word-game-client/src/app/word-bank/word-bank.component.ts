import { Component, OnInit } from '@angular/core';
import { WordBankService } from '../services/word-bank.service';

@Component({
  selector: 'app-word-bank',
  templateUrl: './word-bank.component.html',
  styleUrls: ['./word-bank.component.scss']
})
export class WordBankComponent implements OnInit {

  constructor(public wordBankService: WordBankService) { }

  ngOnInit(): void {
  }



}
