import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import IProduct from "../models/Product";
import Response from '../Response'

export function useProducts() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function fetchProducts() {
    try {
      setLoading(true);
      const response = await axios.get<Response<IProduct[]>>('https://localhost:44398/api/products');
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

  return { products, error, loading };
}
