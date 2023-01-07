import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { getProducts } from "../data/products";
import IProduct from "../types/Product";

export function useProductsMock() {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function fetchProducts() {
    try {
      setLoading(true);
      const products = getProducts();
      setProducts(products);
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
