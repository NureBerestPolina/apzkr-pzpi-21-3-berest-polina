import { Component, OnDestroy, OnInit } from '@angular/core';
import { Good } from '../../../models/good.model';
import { Observable, Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { GoodService } from '../../../services/organization/good.service';
import { UpsertGood } from '../../../models/dtos/upsert-good.model';
import { Producer } from '../../../models/producer.model';
import { Category } from '../../../models/category.model';
import { CategoryService } from '../../../services/organization/category.service';
import { ProducerService } from '../../../services/organization/producer.service';

@Component({
  selector: 'app-edit-good',
  templateUrl: './edit-good.component.html',
  styleUrl: './edit-good.component.css'
})
export class EditGoodComponent implements OnInit, OnDestroy{
  id: string | null = null;
  organizationId: string | null = null;
  model?: Good;

  producers$?: Observable<Producer[]>;
  categories$?: Observable<Category[]>;

  routeSubscription?: Subscription;
  updateGoodSubscription?: Subscription;
  getGoodSubscription?: Subscription;
  deleteGoodSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private goodService: GoodService,
    private producerService: ProducerService,
    private categoryService: CategoryService,
    private router: Router
  ) {}

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateGoodSubscription?.unsubscribe();
    this.getGoodSubscription?.unsubscribe();
    this.deleteGoodSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('goodId');
        this.organizationId = params.get('organizationId');

        console.log(this.id);
        console.log(this.organizationId);

        if (this.id) {
          this.getGoodSubscription = this.goodService
            .getGoodById(this.id)
            .subscribe({
              next: (response) => {
                if (response) {
                  this.model = response;
                } else {
                  console.error(`Good with id: ${this.id} is not found!`);
                }
              },
            });
        }

        this.producers$ = this.producerService.getAll();
        this.categories$ = this.categoryService.getAll();
      },
    });
  }

  onFormSubmit(): void {
    console.log(this.model);

    if (this.model && this.id) {
      var updateGood: UpsertGood = {
        name: this.model.name,
        description: this.model.description,
        price: this.model.price,
        organizationId: this.organizationId!,
        categoryId: this.model.category.id,
        producerId: this.model.producer.id,
        amountInStock: this.model.amountInStock,
        measurementUnit: this.model.measurementUnit,
        needsCooling: this.model.needsCooling,
      };

      this.updateGoodSubscription = this.goodService
        .update(this.id, updateGood)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl(
              '/shopManagement/' + this.organizationId + '/assortment'
            );
          },
        });
    }
  }

  onDelete(): void {
    if (this.id) {
      this.deleteGoodSubscription = this.goodService
        .deleteGood(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl(
              '/shopManagement/' + this.organizationId + '/assortment'
            );
          },
        });
    }
  }
}
