import { useEffect, useRef, useState } from "react"
import Response from '../Response'
import axios from 'axios'
import InfoBadge from "./InfoBadge"
import InputField from "./InputControls/InputField"
import { MarkupElementState } from "../types/MarkupElementState"
import ProductCategory from "../types/ProductCategory"
import { useNavigate } from "react-router-dom"

interface ProductCategoryFormProps {
    productCategory?: ProductCategory
}

export default function ProductCategoryForm({ productCategory }: ProductCategoryFormProps) {
    const categoryNameRef = useRef<HTMLInputElement | null>(null);

    const [inputDataErrors, setInputDataErrors] = useState<any>({})
    const [isSubmit, setIsSubmit] = useState(false)

    const navigate = useNavigate();

    const [showInfoBadge, setShowInfoBadge] = useState(false)
    const [infoBadgeText, setShowInfoBadgeText] = useState('')
    const [infoBadgeType, setInfoBadgeType] = useState<MarkupElementState>()

    function validateForm() {
        let errors: any = {};

        let categoryName = categoryNameRef.current?.value
        if (!categoryName) {
            errors.categoryNameError = "Category name is required"
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
        } // eslint-disable-next-line
    }, [inputDataErrors])

    async function storeProduct() {
        let isUpdateOperation = productCategory !== undefined;

        let formData = new FormData();
        formData.append("name", categoryNameRef.current!.value ?? '');

        if (isUpdateOperation) {
            formData.append("id", productCategory!.id);
            await handleStoreProduct("UPDATE", formData);
        } else {
            await handleStoreProduct("CREATE", formData);
        }
    }

    async function handleStoreProduct(action: string, formData: FormData) {
        let response: Response<any>

        try {
            if (action === "CREATE") {
                const createResponse = await axios.post<Response<any>>('/product-categories', formData);
                response = createResponse.data
            } else {
                const updateResponse = await axios.put<Response<any>>('/product-categories', formData);
                response = updateResponse.data;
            }

            if (response.isSuccess) {
                setInfoBadgeType(MarkupElementState.SUCCESS)
            } else {
                setInfoBadgeType(MarkupElementState.DANGER)
            }

            setShowInfoBadgeText(response.message)
        } catch {
            setInfoBadgeType(MarkupElementState.DANGER)
            setShowInfoBadgeText('Error. Check your network connection or try again later')
        }

        setShowInfoBadge(true)

        if (action === "UPDATE") {
            navigate('/product-categories');
        }
    }

    return (
        <>
            <InfoBadge text={infoBadgeText} type={infoBadgeType} show={showInfoBadge} closeButtonOnClick={() => setShowInfoBadge(false)} />
            <h2 className="text-center">{productCategory ? "Edit category" : "Add new category"}</h2>
            <div className="container pb-5">
                <form>
                    <InputField
                        label={'Category name'}
                        inputType={"text"}
                        inputRef={categoryNameRef}
                        inputStateIsValid={inputDataErrors.categoryNameError === undefined}
                        errorMessage={inputDataErrors.categoryNameError}
                        defaultValue={productCategory?.name}
                    />
                    <div className="text-center">
                        <input type="submit" value={productCategory ? "Update" : "Add"} className="btn btn-primary" onClick={handleSubmit} />
                    </div>
                </form>
            </div>
        </>
    )
}
