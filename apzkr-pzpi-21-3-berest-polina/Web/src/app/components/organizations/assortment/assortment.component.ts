import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Good } from '../../../models/good.model';
import { Organization } from '../../../models/organization.model';
import { AuthService } from '../../../services/auth/auth.service';
import { OrganizationService } from '../../../services/organization/organization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GoodService } from '../../../services/organization/good.service';

@Component({
  selector: 'app-assortment',
  templateUrl: './assortment.component.html',
  styleUrl: './assortment.component.css',
})
export class AssortmentComponent {
  organizationId?: string;
  goods$?: Observable<Good[]>;
  selectedCategory: string = '';
  uniqueCategories: string[] = [];

  organizationSubscription$?: Subscription;
  organization?: Organization | null;

  constructor(
    private goodService: GoodService,
    private readonly authService: AuthService,
    private readonly organizationService: OrganizationService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const organizationId = this.route.snapshot.paramMap.get('organizationId');
    
    if (!organizationId) {
      this.router.navigateByUrl('/notFound');
      return;
    }

    this.organizationId = organizationId;

    this.organizationSubscription$ = this.organizationService
      .getById(organizationId)
      .subscribe((s) => {
        this.organization = s;

        if (this.organization?.isBlocked) {
          this.authService.logout();
          this.router.navigateByUrl('/blocked');
        }
      });

    this.goods$ = this.goodService.getAllOrganizationGoods(organizationId);
    this.goods$.subscribe(goods => {
      // Extract unique categories from goods
      this.uniqueCategories = Array.from(new Set(goods.map(good => good.category.name)));
    });
  }

  ngOnDestroy(): void {
    this.organizationSubscription$?.unsubscribe();
  }

  getStateColor(needsCooling: boolean = false): string {
    if (needsCooling) {
      return 'rgb(223, 255, 253)';
    } else {
      return 'white';
    }
  }

  filterGoods(goods: Good[]): Good[] {
    return goods.filter((good) =>
      good.category.name.toLowerCase().includes(this.selectedCategory.toLowerCase())
    );
  }
}
