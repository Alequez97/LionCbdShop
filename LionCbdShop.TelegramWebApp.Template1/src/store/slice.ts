import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import ICartItem from "../models/CartItem";

export interface State {
    cartItems: ICartItem[];
  }
  
  const slice = createSlice({
    name: 'cartItems',
    initialState: {
        cartItems: []
    } as State,
    reducers: {
        addProduct(state: State, {payload: key}: PayloadAction<string>) {

        },
        removeProduct(state: State, {payload: key}: PayloadAction<string>) {

        }
    }
  });
  
  export const {addProduct, removeProduct} = slice.actions;
  export const reducer = slice.reducer;