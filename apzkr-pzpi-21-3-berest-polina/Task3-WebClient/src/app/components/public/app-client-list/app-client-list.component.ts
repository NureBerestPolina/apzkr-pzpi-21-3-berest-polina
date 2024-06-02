import { Component } from '@angular/core';

@Component({
  selector: 'app-client-list',
  templateUrl: './app-client-list.component.html',
  styleUrl: './app-client-list.component.css'
})
export class AppClientListComponent {
  clients = [
    {
      name: 'Infinity Store',
      description: $localize `The best gadget store of Kharkiv city.`,
      img: 'assets/logos/gadgets.png'
    },
    {
      name: 'No brand brand',
      description: $localize `The leading fashion boutique of the city.`,
      img: 'assets/logos/online-shopping.png'
    },
    {
      name: 'Wow Store',
      description: $localize `Best sweets from all over the world.`,
      img: 'assets/logos/cupcake.png'
    },
    {
      name: 'Korean Cosmetics',
      description: $localize `That will make you shine.`,
      img: 'assets/logos/products.png'
    }
  ];
}
