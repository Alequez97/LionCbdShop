import { ChangeEvent, useCallback, useEffect, useState } from 'react'
import './App.css'
import Card from './components/Card/Card'
import Loader from './components/Loader/Loader';
import Error from './components/Error/Error';
import { getCartItemsAsJsonString } from './helpers'
import { addProduct, removeProduct } from './store/cartItems';
import { useAppDispatch } from './store/store';
import { useCartItems } from './hooks/cartItems';
import IProduct from './types/Product';
import { useTelegramWebApp } from './hooks/telegram';
import { useProducts } from './hooks/products';

function App() {
  const { telegramWebApp } = useTelegramWebApp()

  useEffect(() => {
    telegramWebApp.expand();
    // eslint-disable-next-line
  }, []);

  const dispatch = useAppDispatch();
  const { cartItems } = useCartItems();
  const { products, productCategories, error, loading } = useProducts();
  const [filteredProducts, setFilteredProducts] = useState<IProduct[] | undefined>(undefined);

  useEffect(() => {
    if (cartItems.length === 0) {
      telegramWebApp.MainButton.hide()
    } else {
      telegramWebApp.MainButton.show()
    }
    // eslint-disable-next-line
  }, [cartItems])

  const sendDataToTelegram = useCallback(() => {
    const json = getCartItemsAsJsonString(cartItems);
    telegramWebApp.sendData(json);
    // eslint-disable-next-line
  }, [cartItems])

  useEffect(() => {
    telegramWebApp.onEvent('mainButtonClicked', sendDataToTelegram)
    return () => {
      telegramWebApp.offEvent('mainButtonClicked', sendDataToTelegram)
    }
    // eslint-disable-next-line
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

  const DEFAULT_SELECTOR_VALUE = 'All categories';

  const selectOnChangeHandler = (event: ChangeEvent<HTMLSelectElement>) => {
    const selectedOption = event.target.value;
    if (selectedOption === DEFAULT_SELECTOR_VALUE) {
      setFilteredProducts(undefined)
      return
    }

    setFilteredProducts(products.filter(product => product.productCategoryName === selectedOption));
  }

  return (
    <>
      <h2 className="heading">Royal MMXXI</h2>

      {products.length === 0 && <span>No available products at this moment</span>}

      {products.length !== 0 &&
        <div id="product-categories-wrapper">
          <select name="product-categories" id="product-categories-select" defaultValue={DEFAULT_SELECTOR_VALUE} onChange={selectOnChangeHandler}>
            <option value={DEFAULT_SELECTOR_VALUE}>{DEFAULT_SELECTOR_VALUE}</option>
            {productCategories?.map(category => (<option value={category}>{category}</option>))}
          </select>
        </div>
      }

      <div className="cards__container">
        {filteredProducts === undefined && products.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
        {filteredProducts !== undefined && filteredProducts.map(product => <Card product={product} key={product.id} onAdd={onAdd} onRemove={onRemove} />)}
      </div>
    </>
  );
}

export default App;
