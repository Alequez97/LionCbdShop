import { Link } from "react-router-dom"
import IProduct from "../models/Product"
import Button from "./Button"

interface ProductListGroupItemProps {
    product: IProduct
    deleteOnClick: () => void
}

export default function ProductCard({ product, deleteOnClick }: ProductListGroupItemProps) {
    function editProductPath(id: string) {
        return `edit/${id}`
    }

    return (
        <div className="col-xl-4 col-lg-6 mb-4">
            <div className="card">
                <div className="card-body">
                    <div className="d-flex align-items-center">
                        <img src={product.image} alt={product.productName} height={150} width={150} />
                        <div className="ms-3">
                            <p className="fw-bold mb-1">{product.productName}</p>
                            {product.priceWithDiscount 
                                ?
                                <div>
                                    <p className="text-muted mb-0 text-decoration-line-through">{product.originalPrice}</p>
                                    <p className="text-muted mb-0">{product.priceWithDiscount}</p>
                                </div>
                                :
                                <p className="text-muted mb-0">{product.originalPrice}</p>
                            }
                        </div>
                    </div>
                </div>
                <div className="card-footer border-0 bg-light p-2 d-flex justify-content-around">
                    <Link to={editProductPath(product.id)} className={'btn btn-warning ml-1'}>Edit</Link>
                    <Button text='Delete' type='danger' cssClasses={['ml-1']} onClick={deleteOnClick} />
                </div>
            </div>
        </div>
    )
}
