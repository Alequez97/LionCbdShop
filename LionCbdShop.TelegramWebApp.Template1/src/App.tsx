import { useEffect, useState } from 'react'
import './App.css'
import Card from './components/Card/Card'
import Loader from './components/Loader/Loader';
import Error from './components/Error/Error';
import { useProductsMock } from './hooks/productsMock';
import { getCartItemsAsJsonString } from './helpers'
import { addProduct, removeProduct } from './store/cartItems';
import { useAppDispatch } from './store/store';
import { useCartItems } from './hooks/cartItems';
import Footer from './components/Footer/Footer';

const telegramWebApp = window.Telegram.WebApp;

function App() {
  useEffect(() => {
    telegramWebApp.ready();
    telegramWebApp.expand();
  }, []);

  const dispatch = useAppDispatch();
  const { cartItems } = useCartItems();
  const { products, error, loading } = useProductsMock();

  function sendDataToTelegramWebApp() {
    console.log('send data');
    const json = getCartItemsAsJsonString(cartItems);
    telegramWebApp.sendData(json);
  }

  const onAdd = (productId: string) => {
    dispatch(addProduct(productId))
  };

  const onRemove = (productId: string) => {
    dispatch(removeProduct(productId))
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

      <div className="cards__container">
        {products.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
      </div>

      {<Footer 
        mainButtonText={'Checkout'}
        mainButtonOnClick={sendDataToTelegramWebApp} 
        visible={cartItems.length !== 0} 
      />}
    </>
  );
}

export default App;
