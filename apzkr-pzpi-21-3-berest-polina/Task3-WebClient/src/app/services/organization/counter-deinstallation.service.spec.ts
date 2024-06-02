import { TestBed } from '@angular/core/testing';

import { CounterDeinstallationService } from './counter-deinstallation.service';

describe('CounterDeinstallationService', () => {
  let service: CounterDeinstallationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CounterDeinstallationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
