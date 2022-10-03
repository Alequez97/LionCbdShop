import { useEffect } from 'react'
import './App.css'
import Card from './components/Card/Card'
import Cart from "./components/Cart/Cart"
import Loader from './components/Loader/Loader';
import Error from './components/Error/Error';
import { useProductsMock } from './hooks/productsMock';
import { getCartItemsAsJsonString } from './helpers'
import { addProduct, removeProduct } from './store/cartItems';
import { useAppDispatch, useAppSelector } from './store/store';

const telegramWebApp = window.Telegram.WebApp;

function App() {
  const dispatch = useAppDispatch();
  const cartItems = useAppSelector((state) => state.cartItems.cartItems)

  useEffect(() => {
    telegramWebApp.ready();
  }, []);
  
  telegramWebApp.MainButton.onClick(() => {
    const json = getCartItemsAsJsonString(cartItems);
    telegramWebApp.sendData(json);
  });

  const { products, error, loading } = useProductsMock();

  const onAdd = (productId: string) => {
    dispatch(addProduct(productId))
  };

  const onRemove = (productId: string) => {
    dispatch(removeProduct(productId))
  };

  const onCheckout = () => {
    // const json = getCartItemsAsJsonString(cartItems);
    // telegramWebApp.sendData(json);

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

      <p>Cart items:</p>
      <br/>
      {JSON.stringify(cartItems)}

      <Cart cartItems={cartItems} onCheckout={onCheckout} />

      <div className="cards__container">
        {products.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
      </div>
    </>
  );
}

export default App;
