import { Component } from '@angular/core';
import { LoginRequest } from '../../../models/dtos/login-request.model';
import { Role } from '../../../models/auth/roles.enum';
import { AuthService, companyResponse, superAdminResponse } from '../../../services/auth/auth.service';
import { Router } from '@angular/router';
import { TokenStorageService } from '../../../services/auth/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginRequest;
  role = Role;

  constructor(
    private authService: AuthService,
    private router: Router,
    private tokenStorage: TokenStorageService
  ) {
    this.model = {
      email: '',
      password: ''
    };
  }

  onFormSubmit(): void {
    this.authService.login(this.model).subscribe({
      next: ({ token, refreshToken }) => {
        this.tokenStorage.saveToken(token);
        this.tokenStorage.saveRefreshToken(refreshToken);

        const user = this.tokenStorage.getUser();
        if (user) {
          this.authService.setUser(user);
        }
        //this.authService.setUser(superAdminResponse);

        // redirect to Home page after login
        this.router.navigateByUrl('/');
      },
    });
  }
}

