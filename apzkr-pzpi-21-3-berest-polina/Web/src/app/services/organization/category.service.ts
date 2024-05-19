import { Injectable } from '@angular/core';
import { ODataServiceBase } from '../common/ODataServiceBase';
import { Category } from '../../models/category.model';
import { ODataServiceFactory } from 'angular-odata';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends ODataServiceBase<Category> {
  protected override oDataEntityName: string = 'Categories';

  constructor(factory: ODataServiceFactory) {
    super(factory);
  }
}
