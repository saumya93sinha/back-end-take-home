import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Airport } from '../model/Airport';
import { RouteSearchParam } from '../model/RouteSearchParam';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  ApiBaseUrl: string = 'http://localhost:54243';
  constructor(private http: HttpClient) { }

  getShortestRoute(routeSearchParam: RouteSearchParam): Observable<string> {
    let httpParams = new HttpParams()
                        .set('origin', routeSearchParam.Origin)
                  .set('destination', routeSearchParam.Destination);
                  
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      params: httpParams
    };

    return this.http
      .get<string>(this.ApiBaseUrl + '/route/getshortestroute', httpOptions)
      .pipe(catchError(this.handleError));
  }

  getAllAirports(): Observable<Airport[]> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http
      .get<Airport[]>(this.ApiBaseUrl + '/route/getairports', httpOptions)
      .pipe(catchError(this.handleError));
  }

  private handleError(errorResponse: HttpErrorResponse) {
    return throwError(errorResponse.message);
  }
}