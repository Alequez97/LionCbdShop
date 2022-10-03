import CartItem from "./models/CartItem";

export function getCartItemsAsJsonString(cartItems: CartItem[]) {
    let products: string[] = [];

    cartItems.forEach(cartItem => {
        products.push(`{"productId":"${cartItem.product.id}","quantity":"${cartItem.quantity}"}`);
    })

    return `{"cartItems":[${products.join(',')}]}`;
}