import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { UserregisterComponent } from './Account/userregister/userregister.component';
import { LoginComponent } from './Account/login/login.component';
import { TaskformComponent } from './toTask/taskform/taskform.component';
import { TaskviewComponent } from './toTask/taskview/taskview.component';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [

    AppComponent,
    UserregisterComponent,
    LoginComponent,
    TaskformComponent,
    TaskviewComponent,
  
  ],
  imports: [
    BrowserModule, HttpClientModule, InputTextModule,
    PasswordModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
