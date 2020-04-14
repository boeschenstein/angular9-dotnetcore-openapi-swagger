import { Component } from '@angular/core';
import { WeatherForecastService } from './core/backend';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MyFrontend';
  constructor(service: WeatherForecastService) {
    service.weatherForecastGet().subscribe(result => {
      console.warn('weatherforecast', result);
    }, error => console.error(error));
  }
}
