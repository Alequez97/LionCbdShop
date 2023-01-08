interface IProduct {
    id: string
    productName: string
    productCategoryName: string
    originalPrice: number
    priceWithDiscount?: number
    image: string
}

export default IProduct