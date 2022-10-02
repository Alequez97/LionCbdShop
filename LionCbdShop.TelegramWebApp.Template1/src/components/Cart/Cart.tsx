import React, { MouseEventHandler } from "react";
import "./Cart.scss";
import Button from "../Button/Button";
import ICartItem from "../../models/CartItem";

interface CartProps {
    cartItems: ICartItem[]
    onCheckout: MouseEventHandler<HTMLElement>
}

function Cart({ cartItems, onCheckout }: CartProps) {
  const totalPrice = cartItems.reduce((a, c) => a + c.product.originalPrice * c.quantity, 0);

  return (
    <div className="cart__container">
      {cartItems.length === 0 && "No items in cart"}
      {cartItems.length > 0 && <span className="">Total Price: ${totalPrice.toFixed(2)}</span>}
      <br />

      <Button
        title={"Checkout"}
        type={"checkout"}
        disabled={cartItems.length === 0}
        onClick={onCheckout}
      />
    </div>
  );
}

export default Cart;