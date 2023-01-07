import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import ICartItem from "../types/CartItem";
import IProduct from "../types/Product";

interface CartItemsState {
  cartItems: ICartItem[];
}

export const cartItemsSlice = createSlice({
  name: "cartItems",
  initialState: {
    cartItems: [],
  } as CartItemsState,
  reducers: {
    addProduct: (state, action: PayloadAction<IProduct>) => {
      const existingCartItem = state.cartItems.find(
        (cartItem) => cartItem.product.id === action.payload.id
      );

      if (existingCartItem) {
        let newState = state.cartItems.map((cartItem) => {
          if (cartItem.product.id === action.payload.id) {
            return {
              ...existingCartItem,
              quantity: existingCartItem.quantity + 1,
            };
          }
          return cartItem;
        });

        return {
          cartItems: newState,
        };
      }

      let newState = [
        ...state.cartItems,
        { product: action.payload, quantity: 1 },
      ];

      return {
        cartItems: newState,
      };
    },
    removeProduct: (state, action: PayloadAction<IProduct>) => {
      const existingCartItem = state.cartItems.find(
        (cartItem) => cartItem.product.id === action.payload.id
      );

      if (existingCartItem?.quantity === 1) {
        let newState = state.cartItems.filter(
          (cartItem) => cartItem.product.id !== action.payload.id
        );

        return {
          cartItems: newState,
        };
      }

      let newState = state.cartItems.map((cartItem) => {
        if (cartItem.product.id === action.payload.id) {
          return { ...cartItem, quantity: cartItem.quantity - 1 };
        }
        return cartItem;
      });

      return {
        cartItems: newState,
      };
    },
  },
});

export const { addProduct, removeProduct } = cartItemsSlice.actions;
