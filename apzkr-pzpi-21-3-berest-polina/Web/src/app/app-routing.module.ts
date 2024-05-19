import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlockedComponent } from './components/common/blocked/blocked.component';
import { NotFoundComponent } from './components/common/not-found/not-found.component';
import { HomeComponent } from './components/public/home/home.component';
import { LoginComponent } from './components/public/login/login.component';
import { RegisterComponent } from './components/public/register/register.component';
import { AssortmentComponent } from './components/organizations/assortment/assortment.component';
import { AddGoodComponent } from './components/organizations/add-good/add-good.component';
import { EditGoodComponent } from './components/organizations/edit-good/edit-good.component';
import { AddProducerComponent } from './components/organizations/add-producer/add-producer.component';
import { BackupsManagementComponent } from './components/sysadmin/backups-management/backups-management.component';
import { CountersListComponent } from './components/organizations/counters-management/counters-list/counters-list.component';
import { MakeInstallCounterRequestComponent } from './components/organizations/counters-management/make-install-counter-request/make-install-counter-request.component';
import { ManageCounterInstallRequestsComponent } from './components/admin/manage-counter-install-requests/manage-counter-install-requests.component';
import { AddCounterUriComponent } from './components/admin/add-counter-uri/add-counter-uri.component';
import { ManageOrganizationsComponent } from './components/admin/manage-organizations/manage-organizations.component';
import { SystemStatisticsComponent } from './components/admin/system-statistics/system-statistics.component';
import { StatisticsComponent } from './components/organizations/statistics/statistics.component';
import { ManageTechinspectionRequestsComponent } from './components/admin/manage-techinspection-requests/manage-techinspection-requests.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'notFound',
    component: NotFoundComponent,
  },
  {
    path: 'blocked',
    component: BlockedComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'shopManagement/:organizationId/assortment',
    component: AssortmentComponent,
  },
  {
    path: 'shopManagement/:organizationId/counters',
    component: CountersListComponent,
  },
  {
    path: 'shopManagement/:organizationId/make-counter-installation-request',
    component: MakeInstallCounterRequestComponent,
  },
  {
    path: 'shopManagement/:organizationId/add-good',
    component: AddGoodComponent,
  },
  {
    path: 'shopManagement/:organizationId/edit-good/:goodId',
    component: EditGoodComponent,
  },
  {
    path: 'shopManagement/:organizationId/counters/statistics',
    component: StatisticsComponent,
  },
  {
    path: 'shopManagement/:organizationId/add-producer',
    component: AddProducerComponent,
  },
  {
    path: 'admin/system-administration',
    component: BackupsManagementComponent,
  },
  {
    path: 'admin/counters-installation-requests',
    component: ManageCounterInstallRequestsComponent,
  },
  {
    path: 'admin/counter-installation-requests/:requestId/add-uri',
    component: AddCounterUriComponent,
  },
  {
    path: 'admin/billing/statistics',
    component: SystemStatisticsComponent,
  },
  {
    path: 'admin/organizations-management',
    component: ManageOrganizationsComponent,
  },
  {
    path: 'admin/techinspection-requests-management',
    component: ManageTechinspectionRequestsComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { anchorScrolling: 'enabled' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
