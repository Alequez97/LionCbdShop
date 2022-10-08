import { useAppSelector } from "../store/store";

export function useCartItems() {
    const cartItems = useAppSelector((state) => state.cartItems.cartItems)

    return { cartItems }
} 