import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import Product from "../models/Product";
import Response from '../Response'

export function useProducts() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function fetchProducts() {
    try {
      setLoading(true);
      const response = await axios.get<Response<Product[]>>('https://localhost:44398/api/products');
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
