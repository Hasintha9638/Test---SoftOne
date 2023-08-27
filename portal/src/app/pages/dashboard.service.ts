import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Environment } from 'src/environments/environments';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = Environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  getStudentData() {
    return this.httpClient.get(`${this.apiUrl}`);
  }

  getStudentById(id: number) {
    return this.httpClient.get(`${this.apiUrl}/${id}`);
  }

  addStudent(student: any) {
    return this.httpClient.post(`${this.apiUrl}`, student);
  }

  updateStudent(id: number, student: any) {
    return this.httpClient.put(`${this.apiUrl}/${id}`, student);
  }

  deleteStudent(id: number) {
    return this.httpClient.delete(`${this.apiUrl}/${id}`);
  }


}
