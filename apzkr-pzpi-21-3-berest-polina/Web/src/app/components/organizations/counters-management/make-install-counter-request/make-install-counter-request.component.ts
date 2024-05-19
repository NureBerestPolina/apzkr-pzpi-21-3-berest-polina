import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { CounterInstallationRequest } from '../../../../models/dtos/counter-install-request.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { CounterInstallationService } from '../../../../services/organization/counter-installation.service';

@Component({
  selector: 'app-make-install-counter-request',
  templateUrl: './make-install-counter-request.component.html',
  styleUrl: './make-install-counter-request.component.css',
  providers: [MessageService],
})
export class MakeInstallCounterRequestComponent {
  organizationId: string | null = null;
  model: CounterInstallationRequest;

  routeSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private counterInstallService: CounterInstallationService,
    private router: Router,
    public messageService: MessageService
  ) {
    this.model = {
      address: '',
      placementDescription: '',
      cellCount: 0,
      cellWithTempControlCount: 0,
      organizationId: '',
    };
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    const organizationId = this.route.snapshot.paramMap.get('organizationId');

    if (!organizationId) {
      this.router.navigateByUrl('/notFound');
      return;
    }

    this.model.organizationId = organizationId;
  }

  onFormSubmit(): void {
    if (this.model.cellCount >= this.model.cellWithTempControlCount) {
      this.showSuccess();

      this.counterInstallService.create(this.model).subscribe({
        next: (response) => {
          setTimeout(() => {
            this.router.navigateByUrl(
              '/shopManagement/' + this.organizationId + '/counters'
            );
          }, 3000);
        },
      });
    } else {
      this.showError();
    }
  }

  showSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Success: ',
      detail: $localize`The installation request is accepted.`,
    });
  }

  showError() {
    this.messageService.add({
      severity: 'error',
      summary: 'Error: ',
      detail: $localize`Amount of cells with temperature controlling unit must be less than the genral amount of cells.`,
    });
  }
}
