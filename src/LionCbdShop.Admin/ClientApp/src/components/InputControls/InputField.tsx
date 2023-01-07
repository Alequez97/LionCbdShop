import React, { HTMLInputTypeAttribute, MutableRefObject } from 'react';

interface InputFieldProps {
    label: string
    inputType: HTMLInputTypeAttribute
    inputRef: MutableRefObject<HTMLInputElement | null>
    inputStateIsValid: boolean
    errorMessage: string
    defaultValue?: string | undefined
}

function toCamelCase(str: string) {
    return str
        .replace(/\s(.)/g, function ($1) { return $1.toUpperCase(); })
        .replace(/\s/g, '')
        .replace(/^(.)/, function ($1) { return $1.toLowerCase(); });
}

function getInputClass(isValid: boolean) {
    return isValid ? 'form-control' : 'form-control is-invalid'
}

export default function InputField({ label, inputType, inputRef, inputStateIsValid, errorMessage, defaultValue }: InputFieldProps) {
    return (
        <div className="mb-2">
            <label htmlFor={toCamelCase(label)} className="form-label">{label}</label>
            <input ref={inputRef} type={inputType} className={getInputClass(inputStateIsValid)} name={toCamelCase(label)} defaultValue={defaultValue ?? ''} />
            <small className="text-danger">{inputStateIsValid ? '' : errorMessage}</small>
        </div>
    )
};