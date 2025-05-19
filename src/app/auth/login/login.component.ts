import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../login-request';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
    standalone: true,
    imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/personas']);
    }
    
  }

  get email() {
    return this.loginForm.get('email')!;
  }

  get password() {
    return this.loginForm.get('password')!;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    this.loading = true;
    this.error = null;

    const credentials: LoginRequest = this.loginForm.value;
    this.authService.login(credentials).subscribe({
      next: () => {
        this.router.navigate(['/personas']);
      },
      error: (err) => {
        this.error = err.error || 'Error de autenticación';
        this.loading = false;
      }
    });
  }
}
