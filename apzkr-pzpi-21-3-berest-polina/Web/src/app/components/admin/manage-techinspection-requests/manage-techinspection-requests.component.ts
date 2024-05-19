import { Component } from '@angular/core';
import { Status } from '../../../utils/request-status';
import { TechInspectionRequest } from '../../../models/tech-inspection-request.model';
import { Observable, Subscription } from 'rxjs';
import { TechinspectionRequestsService } from '../../../services/admin/techinspection-requests.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-manage-techinspection-requests',
  templateUrl: './manage-techinspection-requests.component.html',
  styleUrl: './manage-techinspection-requests.component.css',
  providers: [MessageService]
})
export class ManageTechinspectionRequestsComponent {
  status = Status;
  requests$?: Observable<TechInspectionRequest[]>;

  fulfillRequestSubscription?: Subscription;

  private readonly fulfiilledMsg = $localize `Request status changed to "fulfilled"!`;

  constructor(
    private readonly techInspectionService: TechinspectionRequestsService,
    private readonly router: Router,
    public messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.requests$ = this.techInspectionService.getAllTechInspectionRequests();
  }

  ngOnDestroy(): void {
    this.fulfillRequestSubscription?.unsubscribe();
  }

  onFulfill(requestId: string): void {
    this.showSuccess(this.fulfiilledMsg);

    this.fulfillRequestSubscription = this.techInspectionService.fulfillRequest(requestId).subscribe({
      next: (response) => {
        setTimeout(() => {
          this.router.navigateByUrl('admin/techinspection-requests-management');
        }, 3000);
      },
    });
  }

  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success: ', detail: message });
  }
}
