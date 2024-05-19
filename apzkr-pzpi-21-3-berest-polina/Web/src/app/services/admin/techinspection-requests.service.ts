import { Injectable } from '@angular/core';
import { ODataServiceBase } from '../common/ODataServiceBase';
import { TechInspectionRequest } from '../../models/tech-inspection-request.model';
import { ODataServiceFactory } from 'angular-odata';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TechinspectionRequestsService extends ODataServiceBase<TechInspectionRequest> {
  protected override oDataEntityName: string = 'TechInspectionRequests';

  constructor(factory: ODataServiceFactory) {
    super(factory);
  }

  getAllTechInspectionRequests(): Observable<TechInspectionRequest[]> {
    return this.ODataService.entities()
    .query((q) => {
        q.expand('cell');
      }
    )
    .fetch()
    .pipe(this.mapODataEntities);
  }
  
  fulfillRequest(id: string): Observable<TechInspectionRequest> {
    return this.ODataService.destroy(id);
  }
}
