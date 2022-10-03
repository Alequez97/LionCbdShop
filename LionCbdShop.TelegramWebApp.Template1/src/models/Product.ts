interface IProduct {
    id: string
    productName: string
    originalPrice: number
    priceWithDiscount?: number
    image: string
}

export default IProduct