import CartItem from "./models/CartItem";

export function getCartItemsAsJsonString(cartItems: CartItem[]) {
    let products: string[] = [];

    cartItems.forEach(cartItem => {
        products.push(`{"productId":"${cartItem.productId}","quantity":"${cartItem.quantity}"}`);
    })

    return `{"cartItems":[${products.join(',')}]}`;
}