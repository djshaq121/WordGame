import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PointsService {

  private pointsSource = new BehaviorSubject<number>(0);
  points$ = this.pointsSource.asObservable();

  constructor() { }

  addPoints(points: number) {
    var points = points + this.pointsSource.value;
    this.pointsSource.next(points);
  }
}
