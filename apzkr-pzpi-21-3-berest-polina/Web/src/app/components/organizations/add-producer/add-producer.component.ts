import { Component, OnInit } from '@angular/core';
import { UpsertProducer } from '../../../models/dtos/upsert-producer.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ProducerService } from '../../../services/organization/producer.service';

@Component({
  selector: 'app-add-producer',
  templateUrl: './add-producer.component.html',
  styleUrl: './add-producer.component.css'
})
export class AddProducerComponent implements OnInit{
  model: UpsertProducer = {
    name: '',
    description: '',
    producerCountry: '',
  };

  organizationId?: string;
  
  constructor(
    private producerService: ProducerService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const organizationId = this.route.snapshot.paramMap.get('organizationId');

    if (!organizationId) {
      this.router.navigateByUrl('/notFound');
      return;
    }

    this.organizationId = organizationId;
  }


  onFormSubmit(): void {
    this.producerService.create(this.model).subscribe({
      next: (response) => {
        console.log('Successful creating!', this.model);
        this.router.navigate([
          '/shopManagement',
          this.organizationId,
          'assortment',
        ]);
      },
      error: (error) => {
        console.error('error', error);
      },
    });
}
}
