import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Episodes } from '../interfaces/episodes';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ApiServices {
  private myAppUrl: string;
  private url = 'https://localhost:7106/api/Episode';/*conexión con la api*/
  constructor(private http: HttpClient) {
    this.myAppUrl = environment.endpoint;
  }

  getEpisodes(): Observable<Episodes[]> {/*conexión con la tabla de episodios*/
    return this.http.get<Episodes[]>(this.url);
  }

}
