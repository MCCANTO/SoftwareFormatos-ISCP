import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FONDO_PAGINA } from 'src/app/core/constants/general.constant';
import { UserData } from 'src/app/core/models/user-data.interface';
import { JwtTokenService } from 'src/app/services/jwt-token.service';

@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.scss']
})
export class DefaultComponent implements OnInit {

  constructor() {}
  
  ngOnInit(): void {
    document.body.style.backgroundColor = FONDO_PAGINA;
  }

}
