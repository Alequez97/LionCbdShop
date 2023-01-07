import InfoBadge from "../../components/InfoBadge";
import Loader from "../../components/Loader/Loader";
import Error from '../../components/Error';
import { useProductCategories } from "../../hooks/useProductCategories";
import { Link } from "react-router-dom";
import Button from "../../components/InputControls/Button";
import Response from '../../Response'
import { MarkupElementState } from "../../types/MarkupElementState";
import ProductCategory from "../../types/ProductCategory";
import axios from "axios";
import { useState } from "react";

export default function ProductCategories() {
    const { productCategories, setProductCategories, loading, error } = useProductCategories()

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

    async function deleteOnClick(category: ProductCategory) {
        let performDelete = window.confirm(`Are you sure you want delete product with name "${category.name}"?`)

        if (!performDelete) {
            return;
        }

        try {
            const response = await axios.delete<Response<any>>(`/product-categories/${category.name}`)
            if (response.data.isSuccess) {
                setInfoBadgeType(MarkupElementState.SUCCESS)
                setProductCategories(prevState => prevState.filter(p => p.name !== category.name))
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


                {productCategories.length === 0 &&
                    <div className="alert alert-secondary text-center" role="alert">
                        <h3>No categories added yet</h3>
                    </div>
                }

                <div className="text-center mb-4">
                    <Link to="/product-categories/add">
                        <Button text='Add new category' type={MarkupElementState.PRIMARY} />
                    </Link>
                </div>


                <ul className="list-group">
                    {productCategories.map((productCategory, index) => (
                        <li className="list-group-item" key={index} >{productCategory.name}
                            <Button text='Delete' type={MarkupElementState.DANGER} cssClasses={['ml-1']} onClick={() => deleteOnClick(productCategory)} />
                        </li>
                    ))}
                </ul>
            </div>
        </>
    )
}