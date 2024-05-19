import { Component, OnDestroy, OnInit } from '@angular/core';
import { Status } from '../../../utils/request-status';
import { Observable, Subscription } from 'rxjs';
import { CounterInstallationRequest } from '../../../models/counter-install-request.model';
import { CounterDeinstallationRequest } from '../../../models/counter-deinstall-request.model';
import { UpdateCounterInstallationRequest } from '../../../models/dtos/update-counter-install-request.model';
import { CounterDeinstallationService } from '../../../services/organization/counter-deinstallation.service';
import { CounterInstallationService } from '../../../services/organization/counter-installation.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-manage-counter-install-requests',
  templateUrl: './manage-counter-install-requests.component.html',
  styleUrl: './manage-counter-install-requests.component.css',
  providers: [MessageService]
})
export class ManageCounterInstallRequestsComponent implements OnInit, OnDestroy {
  status = Status;
  requestsInstall$?: Observable<CounterInstallationRequest[]>;
  requestsDeinstall$?: Observable<CounterDeinstallationRequest[]>;
  updateRequest?: UpdateCounterInstallationRequest;

  approveRequestSubscription?: Subscription;
  rejectRequestSubscription?: Subscription;
  fulfillRequestSubscription?: Subscription;

  private readonly approveMsg = $localize `Request approved!`;
  private readonly rejectMsg = $localize `Request rejected!`;
  private readonly fulfiilledMsg = $localize `Request status changed to "fulfilled"!`;

  constructor(
    private readonly counterDeinstallService: CounterDeinstallationService,
    private readonly counterInstallService: CounterInstallationService,
    private readonly router: Router,
    public messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.requestsInstall$ = this.counterInstallService.getAllRequests();
    this.requestsDeinstall$ = this.counterDeinstallService.getAllRequests();
  }

  ngOnDestroy(): void {
    this.approveRequestSubscription?.unsubscribe();
    this.rejectRequestSubscription?.unsubscribe();
    this.fulfillRequestSubscription?.unsubscribe();
  }

  onAccept(request: CounterInstallationRequest): void {
    this.showSuccess(this.approveMsg);

    request.requestStatus = this.status.Approved;

    this.approveRequestSubscription = this.counterInstallService.updateInstallRequest(request.id, request).subscribe({
      next: (response) => {
        setTimeout(() => {
          this.router.navigateByUrl('/admin/counter-installation-requests');
        }, 3000);
      },
    });
  }

  onReject(request: CounterInstallationRequest): void {
    this.showSuccess(this.rejectMsg);

    request.requestStatus = this.status.Rejected;

    this.approveRequestSubscription = this.counterInstallService.updateInstallRequest(request.id, request).subscribe({
      next: (response) => {
        setTimeout(() => {
          this.router.navigateByUrl('/admin/counter-installation-requests');
        }, 3000);
      },
    });
  }

  onFulfill(request: CounterInstallationRequest): void {
    this.showSuccess(this.fulfiilledMsg);

    request.requestStatus = this.status.Fulfilled;

    this.approveRequestSubscription = this.counterInstallService.updateInstallRequest(request.id, request).subscribe({
      next: (response) => {
        setTimeout(() => {
          this.router.navigateByUrl('/admin/counter-installation-requests');
        }, 3000);
      },
    });
  }

  onDeinstall(requestId: string): void {
    this.showSuccess(this.fulfiilledMsg);

    this.approveRequestSubscription = this.counterDeinstallService.fulfillDeinstallRequest(requestId).subscribe({
      next: (response) => {
        setTimeout(() => {
          this.router.navigateByUrl('/admin/counter-installation-requests');
        }, 3000);
      },
    });
  }

  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success: ', detail: message });
  }

  getRequestStatusColor(requestStatus: string): string {
    switch (requestStatus) {
      case this.status.Approved:
        return 'rgb(150, 255, 150)';
      case this.status.Rejected:
        return 'rgb(255, 146, 146)';
      case this.status.Fulfilled:
        return 'rgb(220,220,220)';
      default:
        return 'white'; 
    }
  }
}
