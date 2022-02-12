import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-tile',
  templateUrl: './tile.component.html',
  styleUrls: ['./tile.component.scss']
})
export class TileComponent implements OnInit {

  constructor() { }
  @Input() letter = '';
  @Input() position: number = -1;
  @Output() notifyGame: EventEmitter<any> = new EventEmitter();
  
  ngOnInit(): void {
  }

  letterPressed(){
    this.notifyGame.emit(this.letter);
  }

}
