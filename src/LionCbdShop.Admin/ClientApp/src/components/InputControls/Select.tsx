import { ChangeEvent } from 'react';

interface SelectProps {
    label: string
    options: string[]
    selectedOption?: string
    onOptionSelect?: (option: string) => void
    ariaLabel?: string
}

export default function Select({ label, options, selectedOption, onOptionSelect, ariaLabel }: SelectProps) {
    function onSelectedValueChange(event: ChangeEvent<HTMLSelectElement>) {
        const selectedOption = event.target.value;
        if (onOptionSelect) onOptionSelect(selectedOption);
    }

    return (
        <div className="mb-2">
            <label htmlFor={`${label}SelectLabel`}>{label}</label>
            <select className="form-select" aria-label={ariaLabel} defaultValue={selectedOption ?? ''} onChange={onSelectedValueChange}>
                <option hidden></option>
                {options.map(option => (<option key={option} value={option}>{option}</option>))}
            </select>
        </div>
    )
};