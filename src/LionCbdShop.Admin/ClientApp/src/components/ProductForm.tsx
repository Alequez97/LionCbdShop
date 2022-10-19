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
    const productFormRef = useRef<HTMLFormElement | null>(null);

    const navigate = useNavigate();

    const [inputData, setInputData] = useState<any>({})
    const [inputImage, setInputImage] = useState<File>()
    const [inputDataErrors, setInputDataErrors] = useState<any>({})
    const [isSubmit, setIsSubmit] = useState(false)

    useEffect(() => {
        if (product) {
            setInputData({
                productName: product.productName,
                originalPrice: product.originalPrice,
                priceWithDiscount: product.priceWithDiscount
            })
        }
    }, [product])

    const [showInfoBadge, setShowInfoBadge] = useState(false)
    const [infoBadgeText, setShowInfoBadgeText] = useState('')
    const [infoBadgeType, setInfoBadgeType] = useState('')

    function validateForm() {
        console.log('ref data:')
        console.log(productNameRef.current?.value)

        let isUpdateOperation = product !== undefined;
        
        let errors: any = {};

        if (!inputData?.productName) {
            errors.productNameError = "Product name is required"
        }

        if (!inputData?.originalPrice) {
            errors.originalPriceError = "Original price is required"
        } else if (inputData?.originalPrice <= 0) {
            errors.originalPriceError = "Original price should be valid number and be more than 0"
        }

        if (inputData?.priceWithDiscount >= inputData?.originalPrice) {
            errors.priceWithDiscountError = "Price with discount should be less than original price"
        } else if (inputData?.priceWithDiscount < 0) {
            errors.priceWithDiscountError = "Price with discount shouldn't be negative number"
        } 

        if (!isUpdateOperation && !inputImage) {
            errors.productImageError = "Image is required"
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
        formData.append("productName", inputData?.productName);
        formData.append("originalPrice", inputData?.originalPrice);
        formData.append("priceWithDiscount", inputData?.priceWithDiscount ? inputData.priceWithDiscount : '');
        if (inputImage) {
            formData.append("productImage", inputImage, inputImage.name);
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

    const changeInputHandler = (event: ChangeEvent<HTMLInputElement>) => {
        setInputData((prevState: any) => ({
            ...prevState, ...{
                [event.target.name]: event.target.value
            }
        }))
    }

    const changeImageHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        const fileList = event.target.files;
        if (!fileList) return;

        setInputImage(fileList[0]);
    };

    return (
        <>
            <InfoBadge text={infoBadgeText} class={infoBadgeType} show={showInfoBadge} closeButtonOnClick={() => setShowInfoBadge(false)} />
            <h2 className="text-center">{product ? "Edit product" : "Add new product"}</h2>
            <div className="container pb-5">
                <form ref={productFormRef}>
                    <div className="mb-2">
                        <label htmlFor="productName" className="form-label">Product name</label>
                        <input ref={productNameRef} type="text" className="form-control" name="productName" defaultValue={inputData?.productName} onChange={changeInputHandler} />
                        <small className="text-danger">{inputDataErrors.productNameError}</small>
                    </div>
                    <div className="mb-2">
                        <label htmlFor="originalPrice" className="form-label">Original price</label>
                        <input ref={originalPriceRef} type="number" step=".01" min="0" className="form-control" name="originalPrice" defaultValue={inputData?.originalPrice} onChange={changeInputHandler} />
                        <small className="text-danger">{inputDataErrors.originalPriceError}</small>
                    </div>
                    <div className="mb-2">
                        <label htmlFor="priceWithDiscount" className="form-label">Price with discount</label>
                        <input ref={priceWithDiscountRef} type="number" step=".01" min="0" className="form-control" name="priceWithDiscount" defaultValue={inputData?.priceWithDiscount} onChange={changeInputHandler} />
                        <small className="text-danger">{inputDataErrors.priceWithDiscountError}</small>
                    </div>
                    <div className="mb-2">
                        <label htmlFor="image" className="form-label">Image</label>
                        <input className="form-control" type="file" name="productImage" onChange={changeImageHandler} />
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
