import IProduct from "../models/Product";

export function getData(): IProduct[] {
  return [
    { id:1, productName: "Banana", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/03/600-pink-lemonade-web-300x300.jpg' },
    { id:2, productName: "Grape", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/03/600-grape-ice-web-600x600.jpg' },
    { id:3, productName: "Lychee", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/04/600-lychee-ice-web-300x300.jpg' },
    { id:4, productName: "Strawberry", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/04/600-strawberry-and-watermelon-ice-web-600x600.jpg' },
  ];
}
