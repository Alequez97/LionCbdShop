import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import Response from '../Response'
import ProductCategory from "../types/ProductCategory";

export function useProductCategories() {
  const [productCategories, setProductCategories] = useState<ProductCategory[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function fetchProductCategories() {
    try {
      setLoading(true);
      const response = await axios.get<Response<ProductCategory[]>>('/product-categories');
      setProductCategories(response.data.responseObject);
      setLoading(false);
    } catch (e: unknown) {
      const error = e as AxiosError;
      setLoading(false);
      setError(error.message);
    }
  }

  useEffect(() => {
    fetchProductCategories();
  }, []);

  return { productCategories, setProductCategories, error, loading };
}
