import IProduct from "../models/Product";

export function getProducts(): IProduct[] {
  return [
    { id:"b479ef36-f7ef-4e73-81cc-e27d5e4f3dc2", productName: "Banana", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/03/600-pink-lemonade-web-300x300.jpg' },
    { id:"dd002871-dc14-47ff-94a6-7c68b3b5960a", productName: "Grape", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/03/600-grape-ice-web-600x600.jpg' },
    { id:"3da4c2d5-dd5c-4d88-9399-793a9f1153c1", productName: "Lychee", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/04/600-lychee-ice-web-300x300.jpg' },
    { id:"966e5a23-15c5-4a4e-bb0a-a407c8ef763e", productName: "Strawberry", originalPrice: 7.99, image: 'https://royal-mmxxi.com/wp-content/uploads/2021/04/600-strawberry-and-watermelon-ice-web-600x600.jpg' },
  ];
}
