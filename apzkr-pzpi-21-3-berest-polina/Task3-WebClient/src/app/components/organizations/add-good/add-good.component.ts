import { Component } from '@angular/core';
import { UpsertGood } from '../../../models/dtos/upsert-good.model';
import { GoodService } from '../../../services/organization/good.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Producer } from '../../../models/producer.model';
import { Observable } from 'rxjs';
import { Category } from '../../../models/category.model';
import { ProducerService } from '../../../services/organization/producer.service';
import { CategoryService } from '../../../services/organization/category.service';

@Component({
  selector: 'app-add-good',
  templateUrl: './add-good.component.html',
  styleUrl: './add-good.component.css'
})
export class AddGoodComponent {
  model: UpsertGood = {
    name: '',
    description: '',
    measurementUnit: '',
    needsCooling: false,
    price: 0,
    amountInStock: 0,
    producerId: '',
    categoryId: '',
    organizationId: ''
  };

  producers$?: Observable<Producer[]>;
  categories$?: Observable<Category[]>;

  constructor(
    private goodService: GoodService,
    private producerService: ProducerService,
    private categoryService: CategoryService,
    private router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit() {
    const organizationId = this.route.snapshot.paramMap.get('organizationId');

    if (!organizationId) {
      this.router.navigateByUrl('/notFound');
      return;
    }

    this.model.organizationId = organizationId;

    this.producers$ = this.producerService.getAll();
    this.categories$ = this.categoryService.getAll();
  }

  onFormSubmit(): void {
      this.model.amountInStock = Number(this.model.amountInStock);
      this.model.price = Number(this.model.price);
      this.goodService.create(this.model).subscribe({
        next: (response) => {
          console.log('Successful good creating!', this.model);
          this.router.navigate([
            '/shopManagement',
            this.model.organizationId,
            'assortment',
          ]);
        },
        error: (error) => {
          console.error('error', error);
        },
      });
  }
}
