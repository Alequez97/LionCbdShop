import React from 'react'
import Loader from '../components/Loader';
import Error from '../components/Error';
import { useOrders } from '../hooks/orders';
import { Link } from 'react-router-dom';

const Orders = () => {
  const { orders, error, loading } = useOrders();

  function orderLink(orderId: string) {
    return `/orders/${orderId}/`;
  }

  if (loading) {
    return (
      <Loader />
    )
  }

  if (error) {
    return (
      <Error />
    )
  }

  if (orders.length > 0) {
    return (
      <table className="table">
        <thead>
          <tr>
            <th scope="col">Order number</th>
            <th scope="col">Creation date</th>
            <th scope="col">Status</th>
            <th scope="col">Details</th>
          </tr>
        </thead>
        <tbody className="table-group-divider">
          {orders.map(order =>
            <tr key={order.id}>
              <th scope="row">{order.orderNumber}</th>
              <td>{order.creationDate.toString()}</td>
              <td>{order.status}</td>
              <td><Link to={orderLink(order.id)}>Details</Link></td>
            </tr>
          )}
        </tbody>
      </table>
    )
  }

  return (
    <div className="p-3 mb-2 bg-light text-dark text-center">
      <h3>No orders yet</h3>
    </div>
  )
}

export default Orders;
