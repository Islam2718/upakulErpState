import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/administration/auth/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { Store } from '@ngrx/store';
import { setLoginData } from '../../state/auth.actions';
import { selectAuthState } from '../../state/auth.selectors';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  errorMessage: string = '';

  showPassword = false;
  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService, 
    private router: Router,
    private toastr: ToastrService,  // Inject ToastrService here
    private store: Store
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      UserId: ['', [Validators.required]],
      Password: ['', [Validators.required, Validators.minLength(6)]],
      rememberMe: [false]
    });
  }
  onSubmit(): void {
    // alert('Log here...');
    if (this.loginForm.valid) {
      const loginData = {
        UserId: this.loginForm.value.UserId,
        Password: this.loginForm.value.Password
      };

      this.authService.login(loginData).subscribe({
        next: (response) => {
          console.log('_respLogin:', response);
          this.router.navigate(['/dashboard']);
          // alert('i am here now...');
          this.store.dispatch(setLoginData({
            stateToken: 'mytoken',
            statePersonal: 'Islam Hossain - State',
            // stateModules: ['mod1', 'mod2', 'mod3'],
            stateNotification: ['notify1', 'notify2'],
            stateMenu: response.data.menus,
            // stateToken: response.stateToken ?? 'mytoken',
            // statePersonal: response.statePersonal ?? 'Islam Hossain - State',
            stateModules: response.data.modules,
            // stateNotification: response.stateNotification ?? ['notify1', 'notify2'],
            // stateMenu: response.stateMenu ?? ['m1', 'm2', 'm3']
          }));
          // console.log('___', this.store.dispatch);
          this.store.select(selectAuthState).subscribe(state => console.log('__LoginTS:', state));
        },
        error: (error) => {
          // This assumes your backend returns structured error messages
          const msg = error?.error?.message?.toLowerCase() ?? '';

          if (msg.includes('username')) {
            this.toastr.error('Username not found', 'Login Failed');
          } else if (msg.includes('password')) {
            this.toastr.error('Incorrect password', 'Login Failed');
          } else {
            this.toastr.error('Login failed. Check your credentials.', 'Login Failed');
          }
        }
      });
    } else {
      this.toastr.warning('Please fix form errors before submitting.', 'Invalid Input');
    }
  }
  
}


