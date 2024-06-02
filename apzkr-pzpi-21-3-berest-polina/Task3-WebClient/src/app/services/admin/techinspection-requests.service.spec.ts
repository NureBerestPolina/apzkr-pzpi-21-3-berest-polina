import { TestBed } from '@angular/core/testing';

import { TechinspectionRequestsService } from './techinspection-requests.service';

describe('TechinspectionRequestsService', () => {
  let service: TechinspectionRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TechinspectionRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
