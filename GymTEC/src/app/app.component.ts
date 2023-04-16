import {Component, Renderer2, ElementRef, OnInit} from '@angular/core';
import { GetApiService } from './get-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private api:GetApiService) { }
  title = 'GymTEC';

  ngOnInit() {
    this.api.apiCall().subscribe((data)=>{
      console.log(data);
    });
  }
}
