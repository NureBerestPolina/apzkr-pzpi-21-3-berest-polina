import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BackupFile } from '../../models/backup-file.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SystemAdministrationService {

  constructor(private http: HttpClient) { }

  getAllBackups(): Observable<BackupFile[]> {
    return this.http.get<BackupFile[]>(`${environment.apiBaseUrl}/api/backup/all`);
  }

  createBackup(): Observable<BackupFile> {
    return this.http.post<BackupFile>(`${environment.apiBaseUrl}/api/backup/create`, null);
  }
}
