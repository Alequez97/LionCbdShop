import Product from "./Product"

interface CartItem
{
    product: Product
    productNameDuringOrderCreation: string
    quantity: number
}

export default CartItem
