import React, { MutableRefObject } from 'react';

interface InputFieldProps {
    inputRef: MutableRefObject<HTMLInputElement | null>
    inputStateIsValid: boolean
    errorMessage: string
    defaultValue?: string | undefined
}

function getInputClass(isValid: boolean) {
    return isValid ? 'form-control' : 'form-control is-invalid'
}

export default function InputField({ inputRef, inputStateIsValid, errorMessage, defaultValue }: InputFieldProps) {
    return (
        <div className="mb-2">
            <label htmlFor="productName" className="form-label">Product name</label>
            <input ref={inputRef} type="text" className={getInputClass(inputStateIsValid)} name="productName" defaultValue={defaultValue ?? ''} />
            <small className="text-danger">{inputStateIsValid ? '' : errorMessage}</small>
        </div>
    )
};