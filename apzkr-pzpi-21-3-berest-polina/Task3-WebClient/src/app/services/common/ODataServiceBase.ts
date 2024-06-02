import {
    ODataEntities,
    ODataEntity,
    ODataEntitySetService,
    ODataServiceFactory,
  } from 'angular-odata';
  import { Observable, map } from 'rxjs';
  
  export abstract class ODataServiceBase<TEntity> {
    protected abstract oDataEntityName: string;
  
    constructor(protected readonly factory: ODataServiceFactory) {}
  
    public get ODataService(): ODataEntitySetService<TEntity> {
      return this.factory.entitySet<TEntity>(this.oDataEntityName);
    }
  
    public getAll(): Observable<TEntity[]> {
      return this.ODataService.fetchAll().pipe(this.mapODataEntities);
    }
  
    public getById(id: string): Observable<TEntity | null> {
      return this.ODataService.entity(id).fetchEntity();
    }
  
    public create(entity: Partial<TEntity>): Observable<TEntity> {
      return this.ODataService.create(entity).pipe(this.mapODataEntity);
    }
  
    public update(id: string, enitity: Partial<TEntity>): Observable<TEntity> {
      return this.ODataService.update(id, enitity).pipe(this.mapODataEntity);
    }
  
    protected mapODataEntity = map<ODataEntity<TEntity>, TEntity>(
      (c) => c.entity as TEntity
    );
  
    protected mapODataEntities = map<ODataEntities<TEntity>, TEntity[]>(
      (c) => c.entities as TEntity[]
    );
  }
  