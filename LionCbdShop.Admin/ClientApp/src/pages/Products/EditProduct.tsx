import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import Product from '../../models/Product';
import Response from '../../Response'
import axios from 'axios'
import ProductForm from '../../components/ProductForm';

export default function EditProduct() {
  let params = useParams();
  const [product, setProduct] = useState<Product>();

  async function fetchProduct() {
    try {
      const response = await axios.get<Response<Product>>(`/products/${params.id}`);
      setProduct(response.data.responseObject)
    }
    catch
    {
      console.log('Cant fetch data at this moment')
    }
  }

  useEffect(() => {
    fetchProduct();
  }, []);


  return (
    <>
      <h2 className="text-center">Edit product</h2>
      <ProductForm product={product} />
    </>
  )
}
