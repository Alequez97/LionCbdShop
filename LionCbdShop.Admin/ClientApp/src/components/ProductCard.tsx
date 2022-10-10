import IProduct from "../models/Product"

interface ProductCardProps {
    product: IProduct
    editOnClick: () => void
    deleteOnClick: () => void
}

export default function ProductCard({ product, editOnClick, deleteOnClick }: ProductCardProps) {
    return (
        <div className="col-sm-3 text-center pb-3 border">
            <img src={product.image} className="card-img-top" alt={product.productName} />
            <div className="card-body">
                <h5 className="card-title">{product.productName}</h5>
                <p className="card-text">Original price: {product.originalPrice}</p>
                <p className="card-text">Price with discount: {product.priceWithDiscount}</p>
                <div className="d-flex justify-content-between">
                    <a className="btn btn-warning mr-1" onClick={editOnClick}>Edit</a>
                    <a className="btn btn-danger ml-1" onClick={deleteOnClick}>Delete</a>
                </div>
            </div>
        </div>
    )
}
