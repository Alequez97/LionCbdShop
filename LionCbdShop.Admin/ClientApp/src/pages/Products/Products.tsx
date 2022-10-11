import Loader from '../../components/Loader';
import Error from '../../components/Error';
import { useProducts } from '../../hooks/products';
import ProductCard from '../../components/ProductCard';
import { Link } from 'react-router-dom';
import axios from "axios";
import Response from '../../Response'
import InfoBadge from '../../components/InfoBadge';
import { useState } from 'react';

export default function Products() {
    const { products, setProducts, error, loading } = useProducts();

    const [showInfoBadge, setShowInfoBadge] = useState(false)
    const [infoBadgeText, setShowInfoBadgeText] = useState('')
    const [infoBadgeType, setInfoBadgeType] = useState('')

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

    async function deleteProductClick(id: string) {
        let performDelete = window.confirm('Are you sure you want delete product?')

        if (!performDelete) {
            return;
        }

        try {
            const response = await axios.delete<Response<any>>(`/products/${id}`)
            if (response.data.isSuccess) {
                setInfoBadgeType('success')
                setProducts(prevState => prevState.filter(product => product.id !== id))
            } else {
                setInfoBadgeType('danger')
            }

            setShowInfoBadgeText(response.data.message)
            setShowInfoBadge(true)
        } catch (e) {
            console.log(e);
            setShowInfoBadge(true)
            setInfoBadgeType('danger')
            setShowInfoBadgeText('Error occured. Check internet connection or try again later')
        }
    }

    return (
        <>
            <InfoBadge text={infoBadgeText} class={infoBadgeType} show={showInfoBadge} closeButtonOnClick={() => setShowInfoBadge(false)} />

            {products.length === 0 &&
                <div className="alert alert-secondary text-center" role="alert">
                    <h3>No products yet</h3>
                </div>
            }
            
            <div className="text-center mb-4">
                <Link to="/products/add">
                    <button type="button" className="btn btn-primary">Add new product</button>
                </Link>
            </div>
            
            <div className="card-group">
                <div className="row">
                    {products.map(product => <ProductCard key={product.id} product={product} deleteOnClick={() => deleteProductClick(product.id)} />)}
                </div>
            </div>
        </>
    )
}
