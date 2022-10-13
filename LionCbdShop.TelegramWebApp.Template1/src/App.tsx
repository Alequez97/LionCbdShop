import { useCallback, useEffect } from 'react'
import './App.css'
import Card from './components/Card/Card'
import Loader from './components/Loader/Loader';
import Error from './components/Error/Error';
import { getCartItemsAsJsonString } from './helpers'
import { addProduct, removeProduct } from './store/cartItems';
import { useAppDispatch } from './store/store';
import { useCartItems } from './hooks/cartItems';
import Footer from './components/Footer/Footer';
import IProduct from './models/Product';
import { useProductsMock } from './hooks/productsMock';
import { useTelegramWebApp } from './hooks/telegram';

function App() {
  const { telegramWebApp } = useTelegramWebApp()

  useEffect(() => {
    telegramWebApp.expand();
  }, []);

  const dispatch = useAppDispatch();
  const { cartItems } = useCartItems();
  const { products, error, loading } = useProductsMock();

  useEffect(() => {
    if (cartItems.length === 0) {
      telegramWebApp.MainButton.hide()
    } else {
      telegramWebApp.MainButton.show()
    }
  }, [cartItems])

  const sendDataToTelegram = useCallback(() => {
    const json = getCartItemsAsJsonString(cartItems);
    telegramWebApp.sendData(json);
  }, [cartItems])

  useEffect(() => {
    telegramWebApp.onEvent('mainButtonClicked', sendDataToTelegram)
    return () => {
      telegramWebApp.offEvent('mainButtonClicked', sendDataToTelegram)
    }
  }, [sendDataToTelegram])

  const onAdd = (product: IProduct) => {
    dispatch(addProduct(product))
  };

  const onRemove = (product: IProduct) => {
    dispatch(removeProduct(product))
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

      {products.length === 0 && <span>No available products at this moment</span>}

      <div className="cards__container">
        {products.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
      </div>
    </>
  );
}

export default App;
