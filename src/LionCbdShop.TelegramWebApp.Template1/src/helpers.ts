import ICartItem from "./types/CartItem";

export function getCartItemsAsJsonString(cartItems: ICartItem[]) {
    let products: string[] = [];

    cartItems.forEach(cartItem => {
        products.push(`{"productId":"${cartItem.product.id}", "productName":"${cartItem.product.productName}", "originalPrice":${cartItem.product.originalPrice}, "priceWithDiscount":${cartItem.product?.priceWithDiscount}, "quantity":${cartItem.quantity}}`);
    })

    return `{"cartItems":[${products.join(',')}]}`;
}