import ICartItem from "./models/CartItem";

export function getCartItemsAsJsonString(cartItems: ICartItem[]) {
    let products: string[] = [];

    cartItems.forEach(cartItem => {
        products.push(`{"productId":"${cartItem.product.id}", "productName":"${cartItem.product.productName}", "originalPrice":${cartItem.product.originalPrice}, "quantity":${cartItem.quantity}}`);
    })

    return `{"cartItems":[${products.join(',')}]}`;
}