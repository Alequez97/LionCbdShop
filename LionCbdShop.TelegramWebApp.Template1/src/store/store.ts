import { configureStore, createSlice, PayloadAction } from "@reduxjs/toolkit";
import ICartItem from "../models/CartItem";
import IProduct from "../models/Product";

export interface CartItemsState {
  cartItems: ICartItem[];
}

const cartItemsSlice = createSlice({
  name: "cartItems",
  initialState: {
    cartItems: [],
  } as CartItemsState,
  reducers: {
    addProduct: (state, action: PayloadAction<string>) => {
      const existingCartItem = state.cartItems.find(
        (cartItem) => cartItem.productId === action.payload
      );

      if (existingCartItem) {
        let newState = state.cartItems.map((cartItem) => {
          if (cartItem.productId === action.payload) {
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
        { productId: action.payload, quantity: 1 },
      ];
      return {
        cartItems: newState,
      };
    },
    removeProduct: (state, action: PayloadAction<string>) => {
      const existingCartItem = state.cartItems.find(
        (cartItem) => cartItem.productId === action.payload
      );

      if (existingCartItem?.quantity === 1) {
        let newState = state.cartItems.filter(
          (cartItem) => cartItem.productId !== action.payload
        );

        return {
          cartItems: newState,
        };
      }

      let newState = state.cartItems.map((cartItem) => {
        if (cartItem.productId === action.payload) {
          return { ...cartItem, quantity: cartItem.quantity - 1 };
        }
        return cartItem;
      });

      return {
        cartItems: newState
      }
    },
  },
});

export const { addProduct, removeProduct } = cartItemsSlice.actions;

export const store = configureStore({
  reducer: {
    cartItems: cartItemsSlice.reducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
