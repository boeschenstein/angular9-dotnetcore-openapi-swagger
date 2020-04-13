import { Component } from '@angular/core';
import { WeatherForecastService } from './core/backend';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MyFrontend';
  // constructor(http: HttpClient) {
  //   http.get<any[]>('https://localhost:5001/weatherforecast').subscribe(result => {
  //     console.warn("weatherforecast", result);
  //   }, error => console.error(error));
  // }
  constructor(service: WeatherForecastService) {
    service.weatherForecastGet().subscribe(result => {
      console.warn('weatherforecast', result);
    }, error => console.error(error));
  }
}
