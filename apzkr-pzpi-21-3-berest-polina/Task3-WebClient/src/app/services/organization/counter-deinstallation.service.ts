import { Injectable } from '@angular/core';
import { CounterDeinstallationRequest } from '../../models/counter-deinstall-request.model';
import { ODataServiceBase } from '../common/ODataServiceBase';
import { ODataServiceFactory } from 'angular-odata';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CounterDeinstallationService extends ODataServiceBase<CounterDeinstallationRequest> {
  protected override oDataEntityName: string = 'CounterDeinstallationRequests';

  constructor(factory: ODataServiceFactory, 
    private http: HttpClient) {
    super(factory);
  }

  getAllRequests(): Observable<CounterDeinstallationRequest[]> {
    return this.ODataService.entities()
    .query((q) => {
        q.expand('counter/ownerOrganization');
      }
    )
    .fetch()
    .pipe(this.mapODataEntities);
  }

  fulfillDeinstallRequest(id: string): Observable<CounterDeinstallationRequest> {
    return this.ODataService.destroy(id);
  }
}
