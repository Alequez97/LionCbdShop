import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import IProduct from "../types/Product";
import Response from '../Response'

export function useProducts() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [productCategories, setProductCategories] = useState<string[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function fetchProducts() {
    try {
      setLoading(true);
      const response = await axios.get<Response<IProduct[]>>('/products');
      setProducts(response.data.responseObject);
      setLoading(false);
    } catch (e: unknown) {
      const error = e as AxiosError;
      setLoading(false);
      setError(error.message);
    }
  }

  useEffect(() => {
    fetchProducts();
  }, []);

  useEffect(() => {
    setProductCategories(products.map(product => product.productCategoryName).filter(categoryName => categoryName !== undefined && categoryName !== null && categoryName !== ''))
  }, [products]);

  return { products, productCategories, error, loading };
}
