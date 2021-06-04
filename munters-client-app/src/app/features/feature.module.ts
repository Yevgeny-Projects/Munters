// NG
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// APP
import { HomePageModule } from './components/home-page/home-page.module';

@NgModule({
	imports: [
		CommonModule,
	],
	exports: [
		HomePageModule,
	],
	declarations: [
	]
})
export class FeatureModule {
}
