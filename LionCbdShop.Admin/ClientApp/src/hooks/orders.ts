import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import Order from "../models/Order";
import Response from '../Response'

export function useOrders() {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function fetchOrders() {
    try {
      setLoading(true);
      const response = await axios.get<Response<Order[]>>('https://localhost:44398/api/orders');
      setOrders(response.data.responseObject);
      setLoading(false);
    } catch (e: unknown) {
      const error = e as AxiosError;
      setLoading(false);
      setError(error.message);
    }
  }

  useEffect(() => {
    fetchOrders();
  }, []);

  return { orders, error, loading };
}
