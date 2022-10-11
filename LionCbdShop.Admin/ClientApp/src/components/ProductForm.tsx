import { ChangeEvent, useRef, useState } from "react"
import Product from "../models/Product"
import Response from '../Response'
import axios from 'axios'
import InfoBadge from "./InfoBadge"

interface ProductFormProps {
    product?: Product
}

interface InputData {
    productName: string
    originalPrice: number
    priceWithDiscount: number
}

export default function ProductForm({ product }: ProductFormProps) {
    const productNameRef = useRef<HTMLInputElement | null>(null);
    const originalPriceRef = useRef<HTMLInputElement | null>(null);
    const priceWithDiscountRef = useRef<HTMLInputElement | null>(null);

    if (product) {
        if (productNameRef.current) {
            productNameRef.current.value = product.productName
            productNameRef.current.focus()
        }
        if (originalPriceRef.current) {
            originalPriceRef.current.value = product.originalPrice.toString()
        }
        if (priceWithDiscountRef.current && product.priceWithDiscount) {
            priceWithDiscountRef.current.value = product.priceWithDiscount.toString()
        }
    }

    const [inputData, setInputData] = useState<any>()
    const [inputImage, setInputImage] = useState<File>()

    const [showInfoBadge, setShowInfoBadge] = useState(false)
    const [infoBadgeText, setShowInfoBadgeText] = useState('')
    const [infoBadgeType, setInfoBadgeType] = useState('')

    const [responseIsSuccess, setResponseIsSuccess] = useState(false)
    const [responseMessage, setResponseMessage] = useState('')

    async function storeProduct() {
        let isUpdateOperation = product !== undefined;

        let formData = new FormData();
        formData.append("productName", inputData.productName);
        formData.append("originalPrice", inputData.originalPrice);
        formData.append("priceWithDiscount", inputData.priceWithDiscount);
        if (inputImage) {
            formData.append("productImage", inputImage, inputImage.name);
        }

        if (isUpdateOperation) {
            formData.append("id", product!.id);
            await handleStoreProduct("PUT", formData);
        } else {
            await handleStoreProduct("POST", formData);
        }

        if (responseIsSuccess) {
            setInfoBadgeType('success')
        } else {
            setInfoBadgeType('danger')
        }

        setShowInfoBadgeText(responseMessage)
        setShowInfoBadge(true)
    }

    async function handleStoreProduct(httpMethod: string, formData: FormData) {
        let response: Response<any>

        try {
            if (httpMethod === "POST") {
                const createResponse = await axios.post<Response<any>>('/products', formData);
                response = createResponse.data
            } else {
                const createResponse = await axios.put<Response<any>>('/products', formData);
                response = createResponse.data
            }
    
            if (response.isSuccess) {
                setResponseIsSuccess(true)
            } else {
                setResponseIsSuccess(false)
            }
    
            setResponseMessage(response.message)
        } catch {
            setResponseIsSuccess(false)
            setResponseMessage('Error. Check your network connection or try again later')
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
            <div className="container pb-5">
                <div className="mb-2">
                    <label htmlFor="productName" className="form-label">Product name</label>
                    <input ref={productNameRef} type="text" className="form-control" name="productName" onChange={changeInputHandler} />
                </div>
                <div className="mb-2">
                    <label htmlFor="originalPrice" className="form-label">Original price</label>
                    <input ref={originalPriceRef} type="number" step=".01" min="0" className="form-control" name="originalPrice" value={inputData?.originalPrice} onChange={changeInputHandler} />
                </div>
                <div className="mb-2">
                    <label htmlFor="priceWithDiscount" className="form-label">Price with discount</label>
                    <input ref={priceWithDiscountRef} type="number" step=".01" min="0" className="form-control" name="priceWithDiscount" value={inputData?.priceWithDiscount} onChange={changeInputHandler} />
                </div>
                <div className="mb-2">
                    <label htmlFor="image" className="form-label">Image</label>
                    <input className="form-control" type="file" name="productImage" onChange={changeImageHandler} />
                </div>
                <div className="text-center">
                    <input type="submit" value={product ? "Update" : "Add"} className="btn btn-primary" onClick={storeProduct} />
                </div>
            </div>
        </>
    )
}
