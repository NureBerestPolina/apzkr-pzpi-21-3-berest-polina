import { Component, ElementRef } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private elementRef: ElementRef) {}

  ngOnInit() {
    const fragment = window.location.hash;
    if (fragment) {
      this.scrollToElement(fragment.replace('#', ''));
    }
  }

  scrollToElement(id: string) {
    const element = this.elementRef.nativeElement.querySelector(`#${id}`);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  }
}
