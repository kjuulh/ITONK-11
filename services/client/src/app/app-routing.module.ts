import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {HomeComponent} from './pages/home/home.component';
import {LoginComponent} from './pages/login/login.component';
import {SignupComponent} from './pages/signup/signup.component';

const routes: Routes = [
  {path: 'login', pathMatch: 'full', component: LoginComponent},
  {path: 'sign-up', pathMatch: 'full', component: SignupComponent},
  {path: 'home', pathMatch: 'full', component: HomeComponent},
  {path: '', pathMatch: 'full', component: HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
