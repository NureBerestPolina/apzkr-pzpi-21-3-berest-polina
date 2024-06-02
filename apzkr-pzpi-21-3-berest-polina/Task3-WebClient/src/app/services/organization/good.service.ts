import { Injectable } from '@angular/core';
import { ODataServiceBase } from '../common/ODataServiceBase';
import { Good } from '../../models/good.model';
import { ODataServiceFactory } from 'angular-odata';
import { Observable, first, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GoodService extends ODataServiceBase<Good> {
  protected override oDataEntityName: string = 'Goods';

  constructor(factory: ODataServiceFactory) {
    super(factory);
  }

  getAllOrganizationGoods(organizationId: string): Observable<Good[]> {
    return this.ODataService.entities()
      .query((q) => {
          q.expand('producer,category');
          q.filter(({ e }) => e().eq('organizationId', organizationId, 'none'))
        }
      )
      .fetch()
      .pipe(this.mapODataEntities);
  }

  getGoodById(id: string): Observable<Good> {
    return this.ODataService.entities()
      .query((q) => {
          q.expand('producer,category,organization');
          q.filter(({ e }) => e().eq('id', id, 'none'))
        }
      )
      .fetch()
      .pipe(
        first(),
        map((c) => c.entities![0])
      );
  }

  deleteGood(id: string): Observable<any> {
    throw new Error('Method not implemented.');
  }
}
