import { useEffect, useState } from 'react'
import axios from 'axios'
import { useParams } from 'react-router-dom'
import Order from '../../models/Order'
import Response from '../../Response'
import CartItem from '../../models/CartItem'

export default function OrderDetails() {
    let params = useParams();
    const [order, setOrder] = useState<Order>();

    async function fetchOrder() {
        try {
            const response = await axios.get<Response<Order>>(`/orders/${params.id}`);
            setOrder(response.data.responseObject)
        }
        catch
        {
            console.log('Cant fetch data at this moment')
        }
    }

    useEffect(() => {
        fetchOrder(); // eslint-disable-next-line
    }, []);

    return (
        <>
            <div>
                <h2 className="text-center pb-3">Order {order?.orderNumber}</h2>
            </div>

            <div>
                <table className="table table-bordered">
                    <tbody>
                        <tr>
                            <th>Customer</th>
                            <td>{order?.customerUsername}</td>
                        </tr>
                        <tr>
                            <th>Creation date</th>
                            <td>{order?.creationDate.toString()}</td>
                        </tr>
                        <tr>
                            <th>Status</th>
                            <td>{order?.status}</td>
                        </tr>
                        <tr>
                            <th>Payment date</th>
                            <td>{order?.paymentDate == null ? "-" : order?.paymentDate.toString()}</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div>
                <ul className="list-group">
                    <li className="list-group-item active">Order items</li>
                    {order?.cartItems.map((cartItem: CartItem, index: number) =>
                        <li className="list-group-item" key={index}>{cartItem.product.productName} x<span className="fw-bold">{cartItem.quantity}</span></li>
                    )}
                </ul>
            </div>
        </>
    )
}
