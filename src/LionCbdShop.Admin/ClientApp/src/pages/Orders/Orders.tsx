import moment from 'moment';
import Loader from '../../components/Loader/Loader';
import Error from '../../components/Error';
import { useOrders } from '../../hooks/useOrders';
import { Link } from 'react-router-dom';

const Orders = () => {
  const { orders, error, loading } = useOrders();

  function orderDetailsPath(orderId: string) {
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
      <div className='container'>
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
                <th scope="row">{order.orderNumber.split('-')[1]}</th>
                <td>{(moment(order.creationDate)).format('DD-MMM-YYYY hh:mm')}</td>
                <td>{order.status}</td>
                <td><Link to={orderDetailsPath(order.id)}>Details</Link></td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    )
  }

  return (
    <div className="alert alert-secondary text-center" role="alert">
      <h3>No orders yet</h3>
    </div>
  )
}

export default Orders;
