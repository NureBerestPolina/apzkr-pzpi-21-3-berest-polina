import { Injectable } from '@angular/core';
import { PickupCounter } from '../../models/pickup-counter.model';
import { ODataServiceBase } from '../common/ODataServiceBase';
import { ODataServiceFactory } from 'angular-odata';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CounterService extends ODataServiceBase<PickupCounter> {
  protected override oDataEntityName: string = 'PickupCounters';

  constructor(factory: ODataServiceFactory, 
    private http: HttpClient) {
    super(factory);
  }

  getOrganizationsCounters(organizationId: string): Observable<PickupCounter[]> {
    debugger;
    return this.ODataService.entities()
    .query((q) => {
      q.expand('ownerOrganization');
      q.filter(({ e }) => e().eq('organizationId', organizationId, 'none'))
    }
    )
    .fetch()
    .pipe(this.mapODataEntities);
  }

  deinstallCounter(id: string) : Observable<PickupCounter>{
    return this.ODataService.destroy(id); 
  }
}
