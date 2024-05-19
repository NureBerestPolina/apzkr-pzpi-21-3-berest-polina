import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { ODataModule } from 'angular-odata';

import { authInterceptorProviders as AuthInterceptorProviders } from './interceptors/auth.interceptor';

import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeUk from '@angular/common/locales/uk';

import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ToastModule } from 'primeng/toast';
import { PasswordModule } from 'primeng/password';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FieldsetModule } from 'primeng/fieldset';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessagesModule } from 'primeng/messages';
import { StyleClassModule } from 'primeng/styleclass';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';

import { AppComponent } from './app.component';
import { FooterComponent } from './components/common/footer/footer.component';
import { HeaderComponent } from './components/common/header/header.component';
import { NotFoundComponent } from './components/common/not-found/not-found.component';
import { BlockedComponent } from './components/common/blocked/blocked.component';
import { HomeComponent } from './components/public/home/home.component';
import { environment } from '../environments/environment';
import { AppClientListComponent } from './components/public/app-client-list/app-client-list.component';
import { RegisterComponent } from './components/public/register/register.component';
import { LoginComponent } from './components/public/login/login.component';
import { AssortmentComponent } from './components/organizations/assortment/assortment.component';
import { AddGoodComponent } from './components/organizations/add-good/add-good.component';
import { EditGoodComponent } from './components/organizations/edit-good/edit-good.component';
import { AddProducerComponent } from './components/organizations/add-producer/add-producer.component';
import { CountersListComponent } from './components/organizations/counters-management/counters-list/counters-list.component';
import { MakeInstallCounterRequestComponent } from './components/organizations/counters-management/make-install-counter-request/make-install-counter-request.component';
import { BackupsManagementComponent } from './components/sysadmin/backups-management/backups-management.component';
import { ManageCounterInstallRequestsComponent } from './components/admin/manage-counter-install-requests/manage-counter-install-requests.component';
import { AddCounterUriComponent } from './components/admin/add-counter-uri/add-counter-uri.component';
import { ManageOrganizationsComponent } from './components/admin/manage-organizations/manage-organizations.component';
import { SystemStatisticsComponent } from './components/admin/system-statistics/system-statistics.component';
import { StatisticsComponent } from './components/organizations/statistics/statistics.component';
import { ManageTechinspectionRequestsComponent } from './components/admin/manage-techinspection-requests/manage-techinspection-requests.component';


registerLocaleData(localeUk);

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    NotFoundComponent,
    BlockedComponent,
    HomeComponent,
    AppClientListComponent,
    RegisterComponent,
    LoginComponent,
    AssortmentComponent,
    AddGoodComponent,
    EditGoodComponent,
    AddProducerComponent,
    CountersListComponent,
    MakeInstallCounterRequestComponent,
    BackupsManagementComponent,
    ManageCounterInstallRequestsComponent,
    AddCounterUriComponent,
    ManageOrganizationsComponent,
    SystemStatisticsComponent,
    StatisticsComponent,
    ManageTechinspectionRequestsComponent
  ],
  // ...

    imports: [
      BrowserModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      FormsModule,
      HttpClientModule,
      RouterModule,
      InputTextModule,
      InputTextareaModule,
      PasswordModule,
      RadioButtonModule,
      DropdownModule, 
      CheckboxModule,
      FieldsetModule,
      InputNumberModule,
      InputTextareaModule,
      ToastModule,
      MessagesModule,
      StyleClassModule,
      ButtonModule,
      ODataModule.forRoot({
        config: {
          serviceRootUrl: `${environment.apiBaseUrl}/odata/`
        }
      })
    ],
  providers: [ AuthInterceptorProviders,
    { provide: LOCALE_ID, useValue: 'en-US' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
