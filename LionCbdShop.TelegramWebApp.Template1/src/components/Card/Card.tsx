import React, { useState } from "react";
import "./Card.css";
import Button from "../Button/Button";
import IProduct from "../../models/Product";

interface CardProps {
    product: IProduct
    onAdd: (product: IProduct) => void;
    onRemove: (product: IProduct) => void;
}

function Card({ product, onAdd, onRemove }: CardProps) {
  const [productQuantity, setProductQuantity] = useState(0);
  const { productName, image, originalPrice } = product;

  const handleIncrement = () => {
    setProductQuantity(productQuantity + 1);
    onAdd(product);
  };
  const handleDecrement = () => {
    setProductQuantity(productQuantity - 1);
    onRemove(product);
  };

  return (
    <div className="card">
      <div className="image__container">
        <img src={image} alt={productName} />
      </div>
      <h4 className="card__title">
        {productName}
        <br/>
        <span className="card__price">{originalPrice} €</span>
      </h4>

      <div className="btn-container">
        {productQuantity === 0 && <Button title={"Add"} type={"add"} onClick={handleIncrement} disabled={false} />}

        {productQuantity !== 0 && <Button title={"-"} type={"remove"} onClick={handleDecrement} disabled={false} />}
        <span
          className={`${productQuantity !== 0 ? "card__badge" : "card__badge--hidden"}`}
        >
          {productQuantity}
        </span>
        {productQuantity !== 0 && <Button title={"+"} type={"add"} onClick={handleIncrement} disabled={false} />}
      </div>
    </div>
  );
}

export default Card;