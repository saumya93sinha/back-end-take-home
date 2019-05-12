import { Component, OnInit } from '@angular/core';
import { AppService } from '../service/app.service';
import { Airport } from '../model/Airport';
import { RouteSearchParam } from '../model/RouteSearchParam';


@Component({
  selector: 'app-airport',
  templateUrl: './airport.component.html',
  styleUrls: ['./airport.component.css']
})
export class AirportComponent implements OnInit {

  result: string;
  Airports: Airport[];
  origin: string;
  destination: string;
  routeSearchParam: RouteSearchParam;
  errorMsg: string;


  constructor(private appService: AppService) {
    this.routeSearchParam = new RouteSearchParam();
    this.result = '';
    this.errorMsg = '';
  }

  ngOnInit(): void {
    this.appService.getAllAirports()
      .subscribe(
        (result) => {
          this.Airports = result;
          this.routeSearchParam.Origin = this.Airports[0].IATA3;
          this.routeSearchParam.Destination = this.Airports[0].IATA3;
        },
        (err) => { this.errorMsg = 'Something went wrong! Please see the following error: ' + err; console.log(err); });
  }

  SearchRoute() {
    this.errorMsg = '';

    this.appService.getShortestRoute(this.routeSearchParam)
      .subscribe((result) => { this.result = result; },
        (err) => { this.errorMsg = 'Something went wrong! Please see the following error: ' + err; console.log(err); });
  }
}
