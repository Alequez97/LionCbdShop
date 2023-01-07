interface Product {
    id: string
    productName: string
    category?: string
    originalPrice: number
    priceWithDiscount?: number
    image: string
}

export default Product