import React from 'react'
import Loader from '../components/Loader';
import Error from '../components/Error';
import { useProducts } from '../hooks/products';
import ProductCard from '../components/ProductCard';
import { Link } from 'react-router-dom';

export default function Products() {
    const { products, error, loading } = useProducts();

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

    return (
        <>
            <div className="text-center mb-4">
                <Link to="/products/add">
                    <button type="button" className="btn btn-primary">Add new product</button>
                </Link>
            </div>
            <div className="card-group">
                <div className="row">
                    {products.map(product => <ProductCard key={product.id} product={product} editOnClick={() => console.log('edit')} deleteOnClick={() => console.log('delete')} />)}
                </div>
            </div>
        </>
    )
}
