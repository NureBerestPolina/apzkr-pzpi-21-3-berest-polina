import { TestBed } from '@angular/core/testing';

import { CounterInstallationService } from './counter-installation.service';

describe('CounterInstallationService', () => {
  let service: CounterInstallationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CounterInstallationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
