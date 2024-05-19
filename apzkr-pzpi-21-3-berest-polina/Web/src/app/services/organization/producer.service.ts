import { Injectable } from '@angular/core';
import { ODataServiceBase } from '../common/ODataServiceBase';
import { Producer } from '../../models/producer.model';
import { ODataServiceFactory } from 'angular-odata';

@Injectable({
  providedIn: 'root'
})
export class ProducerService extends ODataServiceBase<Producer> {
  protected override oDataEntityName: string = 'Producers';

  constructor(factory: ODataServiceFactory) {
    super(factory);
  }
}
