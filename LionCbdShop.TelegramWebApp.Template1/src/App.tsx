import { useEffect, useState } from 'react'
import './App.css'
import Card from './components/Card/Card'
import Cart from "./components/Cart/Cart"
import Loader from './components/Loader/Loader';
import Error from './components/Error/Error';
import { useProductsMock } from './hooks/productsMock';
import { getCartItemsAsJsonString } from './helpers'
import { addProduct, removeProduct } from './store/cartItems';
import { useAppDispatch } from './store/store';
import { useCartItems } from './hooks/cartItems';

const telegramWebApp = window.Telegram.WebApp;

function App() {
  useEffect(() => {
    telegramWebApp.ready();
    // telegramWebApp.onEvent('mainButtonClicked', () => {
    //   const json = getCartItemsAsJsonString(cartItems);
    //   telegramWebApp.sendData(json);
    // })
  }, []);

  const { cartItems } = useCartItems();
  useEffect(() => { 
    cartItems.length === 0 ? telegramWebApp.MainButton.hide() : telegramWebApp.MainButton.show()
  }, [cartItems]);

  const dispatch = useAppDispatch();
  const { products, error, loading } = useProductsMock();

  let sendDataToTelegramWebApp = () => {
    const json = getCartItemsAsJsonString(cartItems);
    telegramWebApp.sendData(json);
  }

  const onAdd = (productId: string) => {
    dispatch(addProduct(productId))

    telegramWebApp.MainButton.offClick(sendDataToTelegramWebApp);
    telegramWebApp.MainButton.onClick(sendDataToTelegramWebApp);
  };

  const onRemove = (productId: string) => {
    dispatch(removeProduct(productId))

    telegramWebApp.MainButton.offClick(sendDataToTelegramWebApp);
    telegramWebApp.MainButton.onClick(sendDataToTelegramWebApp);
  };

  const onCheckout = () => {
    // const json = getCartItemsAsJsonString(cartItems);
    // telegramWebApp.sendData(json);
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
      <br />
      {JSON.stringify(cartItems)}

      {/* <Cart cartItems={cartItems} onCheckout={onCheckout} /> */}

      <div className="cards__container">
        {products.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
      </div>

      {/* <TelegramMainButton /> */}
    </>
  );
}

export default App;
