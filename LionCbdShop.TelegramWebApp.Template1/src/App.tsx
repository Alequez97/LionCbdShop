import { useState, useEffect } from 'react'
import './App.css'
import Card from './components/Card/Card'
import Cart from "./components/Cart/Cart"
import ICartItem from './models/CartItem';
import IProduct from './models/Product';
import { useProducts } from './hooks/products';
import Loader from './components/Loader/Loader';
import Error from './components/Error/Error';

const telegramWebApp = window.Telegram.WebApp;

function App() {

  useEffect(() => {
    telegramWebApp.ready();
  });

  const { products, error, loading } = useProducts();
  const [cartItems, setCartItems] = useState<ICartItem[]>([]);

  const onAdd = (product: IProduct) => {
    const existingCartItem = cartItems.find((cartItem) => cartItem.product.id === product.id);

    if (existingCartItem) {
      setCartItems(
        cartItems.map((cartItem) =>
          cartItem.product.id === product.id ? { ...existingCartItem, quantity: existingCartItem.quantity + 1 } : cartItem
        )
      );
    } else {
      setCartItems(prevState => [...prevState, { product: product, quantity: 1 }]);
    }
  };

  const onRemove = (product: IProduct) => {
    const existingCartItem = cartItems.find((cartItem) => cartItem.product.id === product.id);

    if (existingCartItem?.quantity === 1) {
      setCartItems(cartItems.filter((cartItem) => cartItem.product.id !== product.id));
    } else {
      setCartItems(
        cartItems.map((cartItem) =>
          cartItem.product.id === product.id ? { ...cartItem, quantity: cartItem.quantity - 1 } : cartItem
        )
      );
    }
  };

  const onCheckout = () => {
    telegramWebApp.MainButton.text = "Pay :)";
    telegramWebApp.MainButton.show();
  };

  if (loading) {
    return (
      <Loader />
    )  
  }

  if (error) {
    return (
      <Error message={error} />
    )  
  }

  return (
    <>
      <h2 className="heading">Royal MMXXI</h2>

      <Cart cartItems={cartItems} onCheckout={onCheckout} />

      <div className="cards__container">
        {products.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
      </div>
    </>
  );
}

export default App;
