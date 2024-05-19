import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Observable, Subscription } from 'rxjs';
import { BackupFile } from '../../../models/backup-file.model';
import { SystemAdministrationService } from '../../../services/sysadmin/system-administration.service';

@Component({
  selector: 'app-backups-management',
  templateUrl: './backups-management.component.html',
  styleUrl: './backups-management.component.css',
  providers: [MessageService]
})
export class BackupsManagementComponent {
  backups$?: Observable<BackupFile[]>;

  createBackupSubscription?: Subscription;

  private readonly successMsg = $localize `Backup created!`;

  constructor(
    private readonly sysadminService: SystemAdministrationService,
    private readonly router: Router,
    public messageService: MessageService,
    private readonly route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.backups$ = this.sysadminService.getAllBackups();
  }

  ngOnDestroy(): void {
    this.createBackupSubscription?.unsubscribe();
  }

  onCreateBackup(): void {
    this.showSuccess(this.successMsg);
    this.createBackupSubscription = this.sysadminService.createBackup().subscribe({
      next: (response) => {
        this.showSuccess(this.successMsg);
      },
    });
    setTimeout(() => {
      this.backups$ = this.sysadminService.getAllBackups();
    }, 3000);
  }

  showSuccess(message: string) {
    this.messageService.add({ severity: `success`, summary: `Success: `, detail: message });
  }
}

