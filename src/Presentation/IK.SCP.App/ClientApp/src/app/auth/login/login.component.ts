import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  usuario: string = '';
  clave: string = '';

  constructor(
    private authService: AuthService,
    private jwtTokenService: JwtTokenService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
  }

  login() {

    this.spinner.show();

    this.authService.login(this.usuario, this.clave)
      .subscribe( (resp: any) => {

        this.spinner.hide();

        if( resp.ok ){

          this.jwtTokenService.removeToken();

          if (resp.token) {
            this.jwtTokenService.setToken(resp.token);
          }

          this.router.navigate(['/']);

        } else {

          this.messageService.add({severity:'error', summary: 'Error de Autenticación', detail: 'Usuario y/o contraseña incorrectos.'});

        }

      });

  }

}
