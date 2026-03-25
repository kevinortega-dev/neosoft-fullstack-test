import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'http://localhost:5263/api';

  constructor(private http: HttpClient) {}

  // Roles
  getRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/roles`);
  }

  getRol(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/roles/${id}`);
  }

  crearRol(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/roles`, data);
  }

  actualizarRol(id: number, data: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/roles/${id}`, data);
  }

  eliminarRol(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/roles/${id}`);
  }

  // Usuarios
  getUsuarios(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/usuarios`);
  }

  getUsuario(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/usuarios/${id}`);
  }

  crearUsuario(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/usuarios`, data);
  }

  actualizarUsuario(id: number, data: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/usuarios/${id}`, data);
  }

  eliminarUsuario(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/usuarios/${id}`);
  }

  // Variables
  getVariables(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/variables`);
  }

  getVariable(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/variables/${id}`);
  }

  crearVariable(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/variables`, data);
  }

  actualizarVariable(id: number, data: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/variables/${id}`, data);
  }

  eliminarVariable(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/variables/${id}`);
  }
}