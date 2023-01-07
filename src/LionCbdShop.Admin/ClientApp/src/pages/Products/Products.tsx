import Loader from '../../components/Loader/Loader';
import Error from '../../components/Error';
import { useProducts } from '../../hooks/useProducts';
import { Link } from 'react-router-dom';
import axios from "axios";
import Response from '../../Response'
import InfoBadge from '../../components/InfoBadge';
import { useState } from 'react';
import ProductListGroupItem from '../../components/ProductListGroupItem';
import Product from '../../types/Product';
import Button from '../../components/InputControls/Button';
import { MarkupElementState } from '../../types/MarkupElementState';

export default function Products() {
    const { products, setProducts, error, loading } = useProducts();

    const [showInfoBadge, setShowInfoBadge] = useState(false)
    const [infoBadgeText, setShowInfoBadgeText] = useState('')
    const [infoBadgeType, setInfoBadgeType] = useState<MarkupElementState>()

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

    async function deleteProductClick(product: Product) {
        let performDelete = window.confirm(`Are you sure you want delete product with name "${product.productName}"?`)

        if (!performDelete) {
            return;
        }

        try {
            const response = await axios.delete<Response<any>>(`/products/${product.id}`)
            if (response.data.isSuccess) {
                setInfoBadgeType(MarkupElementState.SUCCESS)
                setProducts(prevState => prevState.filter(p => p.id !== product.id))
            } else {
                setInfoBadgeType(MarkupElementState.DANGER)
            }

            setShowInfoBadgeText(response.data.message)
            setShowInfoBadge(true)
        } catch (e) {
            console.log(e);
            setShowInfoBadge(true)
            setInfoBadgeType(MarkupElementState.DANGER)
            setShowInfoBadgeText('Error occured. Check internet connection or try again later')
        }
    }

    return (
        <>
            <InfoBadge text={infoBadgeText} type={infoBadgeType} show={showInfoBadge} closeButtonOnClick={() => setShowInfoBadge(false)} />
            <div className='container'>
                {products.length === 0 &&
                    <div className="alert alert-secondary text-center" role="alert">
                        <h3>No products yet</h3>
                    </div>
                }

                <div className="text-center mb-4">
                    <Link to="/products/add">
                        <Button text='Add new product' type={MarkupElementState.PRIMARY} />
                    </Link>
                </div>

                <div className="row">
                    {products.map(product => <ProductListGroupItem key={product.id} product={product} deleteOnClick={() => deleteProductClick(product)} />)}
                </div>
            </div>
        </>
    )
}
