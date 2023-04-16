import {Component, Renderer2, ElementRef, OnInit} from '@angular/core';
import { GetApiService } from './get-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private getApi:GetApiService) { }
  title = 'GymTEC';

  ngOnInit() {
    this.getApi.call_AgregarTratamientoSPA().subscribe((data)=>{
      console.log(data);
    });

    this.getApi.call_EliminarTratamientoSPA().subscribe((data)=>{
      console.log(data);
    });
  }
}
