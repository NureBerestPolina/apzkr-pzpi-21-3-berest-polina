import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CounterInstallationService } from '../../../services/organization/counter-installation.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-add-counter-uri',
  templateUrl: './add-counter-uri.component.html',
  styleUrl: './add-counter-uri.component.css',
  providers: [MessageService]
})
export class AddCounterUriComponent implements OnInit, OnDestroy{
  requestId: string | null = "";
  uri: string | null = null;

  routeSubscription?: Subscription;
  updateSubscription?: Subscription;
  checkSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private counterInstallService: CounterInstallationService,
    private router: Router,
    public messageService: MessageService
  ) {}

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateSubscription?.unsubscribe();
    this.checkSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.requestId = params.get('requestId');
      },
    });
  }

  onFormSubmit(): void {
    console.log(this.uri);

    if (this.uri && this.requestId) {
      this.checkSubscription = this.counterInstallService.checkUriExists(this.uri).subscribe({ 

        next: urlExists => {
          if (urlExists) {
            this.showError();
          } 
          else {
            this.updateSubscription = this.counterInstallService.fulfillInstallation(this.requestId||'', this.uri||'').subscribe({
              next: () => console.log('URI saved.')
            });
            
          }
        }

      });
    };
  }

  showError() {
    this.messageService.add({ severity: 'danger', summary: 'Error: ', detail: $localize `The link is already taken! Make another one!` });
  }
}
