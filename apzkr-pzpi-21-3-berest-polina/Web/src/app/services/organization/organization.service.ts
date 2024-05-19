import { Injectable } from '@angular/core';
import { Organization } from '../../models/organization.model';
import { Observable, first, map } from 'rxjs';
import { ODataServiceFactory } from 'angular-odata';
import { ODataServiceBase } from '../common/ODataServiceBase';

@Injectable({
  providedIn: 'root',
})
export class OrganizationService extends ODataServiceBase<Organization> {
  protected override oDataEntityName: string = 'Organizations';

  constructor(factory: ODataServiceFactory) {
    super(factory);
  }

  getOrganizationByManagerId(managerId: string): Observable<Organization> {
    debugger;
    return this.ODataService.entities()
      .query((c) => c.filter(({ e }) => e().eq('managerId', managerId, 'none')))
      .fetch()
      .pipe(
        first(),
        map((c) => c.entities![0])
      );
  }

  getAllOrganizations(): Observable<Organization[]> {
    return this.ODataService.entities()
      .query((q) => {
        q.expand('manager');
      })
      .fetch()
      .pipe(this.mapODataEntities);
  }

  updateOrganizationsState(id: string): Observable<Organization> {
    return this.ODataService.destroy(id);
  }
}