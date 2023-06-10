import { TestBed } from '@angular/core/testing';

import { RezeptService } from './rezept.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('RezeptService', () => {
  let service: RezeptService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
    service = TestBed.inject(RezeptService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
