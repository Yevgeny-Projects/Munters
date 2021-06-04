// NG
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// APP
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from '@shared/components/layout';
import { FeatureModule } from './features';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FeatureModule,
    LayoutModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
