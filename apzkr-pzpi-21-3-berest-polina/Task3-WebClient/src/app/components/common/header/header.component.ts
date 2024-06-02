import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../services/auth/auth.service';
import { User } from '../../../models/auth/user.model';
import { Organization } from '../../../models/organization.model';
import { Role } from '../../../models/auth/roles.enum';
import { OrganizationService } from '../../../services/organization/organization.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit, OnDestroy{
  user?: User;
  organization?: Organization;
  role = Role;

  userSubscription$?: Subscription;
  organizationSubscription$?: Subscription;

  constructor(
    private authService: AuthService,
    private router: Router,
    private readonly organizationService: OrganizationService
  ) {}

  ngOnInit(): void {
    this.userSubscription$ = this.authService.user().subscribe({
      next: (user) => {
        this.user = user;

        if (user && user?.roles?.includes(Role.OrganizationManager)) {
          this.organizationSubscription$ = this.organizationService
            .getOrganizationByManagerId(user?.id)
            .subscribe((s) => (this.organization = s));
        }
      },
    });
  }

  ngOnDestroy(): void {
    this.userSubscription$?.unsubscribe();
    this.organizationSubscription$?.unsubscribe();
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }
}
