import CartItem from "./CartItem";

interface Order {
     id: string
     orderNumber:string
     customerUsername: string
     status: string
     creationDate: Date
     paymentDate: Date
     cartItems: CartItem[]
}

export default Order