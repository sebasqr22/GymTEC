import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ClienteComponent } from './cliente/cliente.component';
import { AdminComponent } from './admin/admin.component';


const appRoutes:Routes=[
  {path:'', component: LoginComponent},
  {path:'cliente/:cedula', component:ClienteComponent},
  {path:'admin/:cedula', component:AdminComponent},
  {path:'**', component: NotFoundComponent},
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NotFoundComponent,
    ClienteComponent,
    AdminComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
