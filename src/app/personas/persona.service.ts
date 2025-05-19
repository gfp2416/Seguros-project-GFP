import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Persona } from './persona';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  private apiUrl = 'https://localhost:7231/api/personas';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Persona[]> {
    return this.http.get<Persona[]>(this.apiUrl);
  }

  getById(id: string): Observable<Persona> {
    return this.http.get<Persona>(`${this.apiUrl}/${id}`);
  }

  create(persona: Persona): Observable<Persona> {
    return this.http.post<Persona>(this.apiUrl, persona);
  }

  update(id: string, persona: Persona): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, persona);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
