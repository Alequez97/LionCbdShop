import { ChangeEvent, useEffect, useRef, useState } from "react"
import Product from "../models/Product"
import Response from '../Response'
import axios from 'axios'
import InfoBadge from "./InfoBadge"
import { useNavigate } from "react-router-dom"

interface ProductFormProps {
    product?: Product
}

export default function ProductForm({ product }: ProductFormProps) {
    const productNameRef = useRef<HTMLInputElement | null>(null);
    const originalPriceRef = useRef<HTMLInputElement | null>(null);
    const priceWithDiscountRef = useRef<HTMLInputElement | null>(null);
    const productImageRef = useRef<HTMLInputElement | null>(null);

    const navigate = useNavigate();

    const [inputDataErrors, setInputDataErrors] = useState<any>({})
    const [isSubmit, setIsSubmit] = useState(false)

    const [showInfoBadge, setShowInfoBadge] = useState(false)
    const [infoBadgeText, setShowInfoBadgeText] = useState('')
    const [infoBadgeType, setInfoBadgeType] = useState('')

    function validateForm() {
        let isUpdateOperation = product !== undefined;

        let errors: any = {};

        let productName = productNameRef.current?.value
        if (!productName) {
            errors.productNameError = "Product name is required"
        }

        let originalPrice = originalPriceRef.current?.value;
        if (!originalPrice) {
            errors.originalPriceError = "Original price is required"
        } else if (Number(originalPrice) <= 0) {
            errors.originalPriceError = "Original price should be valid number and be more than 0"
        }

        let priceWithDiscount = priceWithDiscountRef.current?.value;
        console.log(priceWithDiscount)
        console.log(originalPrice)

        if (priceWithDiscount !== undefined && priceWithDiscount !== '') {
            if (Number(priceWithDiscount) >= Number(originalPrice)) {
                errors.priceWithDiscountError = "Price with discount should be less than original price"
            } else if (+priceWithDiscount < 0) {
                errors.priceWithDiscountError = "Price with discount shouldn't be negative number"
            }
        }

        let inputImage = productImageRef?.current?.files?.[0];
        let allowedImageExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

        if (!isUpdateOperation && !inputImage) {
            errors.productImageError = "Image is required"
        } else if (!allowedImageExtensions.exec(inputImage!.name)) {
            errors.productImageError = "Allowed file extensions - jpeg, jpg, png, gif"
        }

        return errors;
    }

    const handleSubmit = (event: any) => {
        event.preventDefault();
        const inputDataErrors = validateForm();
        setInputDataErrors(inputDataErrors);
        setIsSubmit(true);
    }

    useEffect(() => {
        if (Object.keys(inputDataErrors).length === 0 && isSubmit) {
            storeProduct();
        }
    }, [inputDataErrors])

    async function storeProduct() {
        let isUpdateOperation = product !== undefined;

        let formData = new FormData();
        formData.append("productName", productNameRef.current!.value);
        formData.append("originalPrice", originalPriceRef.current!.value);
        formData.append("priceWithDiscount", priceWithDiscountRef.current?.value ? priceWithDiscountRef.current!.value : '');

        let inputImage = productImageRef?.current?.files![0];
        if (inputImage) {
            formData.append("productImage", inputImage,);
        }

        if (isUpdateOperation) {
            formData.append("id", product!.id);
            await handleStoreProduct("UPDATE", formData);
        } else {
            await handleStoreProduct("CREATE", formData);
        }
    }

    async function handleStoreProduct(action: string, formData: FormData) {
        let response: Response<any>

        try {
            if (action === "CREATE") {
                const createResponse = await axios.post<Response<any>>('/products', formData);
                response = createResponse.data
            } else {
                const updateResponse = await axios.put<Response<any>>('/products', formData);
                response = updateResponse.data;
            }

            if (response.isSuccess) {
                setInfoBadgeType('success')
            } else {
                setInfoBadgeType('danger')
            }

            setShowInfoBadgeText(response.message)
        } catch {
            setInfoBadgeType('danger')
            setShowInfoBadgeText('Error. Check your network connection or try again later')
        }

        setShowInfoBadge(true)

        if (action === "UPDATE") {
            navigate('/products');
        }
    }

    function getInputClass(isValid: boolean) {
        return isValid ? 'form-control' : 'form-control is-invalid'
    }

    return (
        <>
            <InfoBadge text={infoBadgeText} class={infoBadgeType} show={showInfoBadge} closeButtonOnClick={() => setShowInfoBadge(false)} />
            <h2 className="text-center">{product ? "Edit product" : "Add new product"}</h2>
            <div className="container pb-5">
                <form>
                    <div className="mb-2">
                        <label htmlFor="productName" className="form-label">Product name</label>
                        <input ref={productNameRef} type="text" className={getInputClass(inputDataErrors.productNameError === undefined)} name="productName" defaultValue={product?.productName} />
                        <small className="text-danger">{inputDataErrors.productNameError}</small>
                    </div>
                    <div className="mb-2">
                        <label htmlFor="originalPrice" className="form-label">Original price</label>
                        <input ref={originalPriceRef} type="number" step=".01" min="0" className={getInputClass(inputDataErrors.originalPriceError === undefined)} name="originalPrice" defaultValue={product?.originalPrice} />
                        <small className="text-danger">{inputDataErrors.originalPriceError}</small>
                    </div>
                    <div className="mb-2">
                        <label htmlFor="priceWithDiscount" className="form-label">Price with discount</label>
                        <input ref={priceWithDiscountRef} type="number" step=".01" min="0" className={getInputClass(inputDataErrors.priceWithDiscountError === undefined)} name="priceWithDiscount" defaultValue={product?.priceWithDiscount ? product?.priceWithDiscount : ''} />
                        <small className="text-danger">{inputDataErrors.priceWithDiscountError}</small>
                    </div>
                    <div className="mb-2">
                        <label htmlFor="image" className="form-label">Image</label>
                        <input ref={productImageRef} className={getInputClass(inputDataErrors.productImageError === undefined)} type="file" name="productImage" />
                        <small className="text-danger">{inputDataErrors.productImageError}</small>
                    </div>
                    <div className="text-center">
                        <input type="submit" value={product ? "Update" : "Add"} className="btn btn-primary" onClick={handleSubmit} />
                    </div>
                </form>
            </div>
        </>
    )
}
