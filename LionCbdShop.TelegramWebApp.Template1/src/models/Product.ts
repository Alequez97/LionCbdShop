interface IProduct {
    id: number
    productName: string
    originalPrice: number
    priceWithDiscount?: number
    image: string
}

export default IProduct