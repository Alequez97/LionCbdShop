import classNames from 'classnames';
import { MarkupElementState } from '../types';

interface ButtonProps {
    text: string
    type: MarkupElementState
    cssClasses?: string[]
    onClick?: () => void
}

export default function InputField({ text, type, cssClasses, onClick }: ButtonProps) {
    return (
        <button
            type="button"
            className={classNames('btn', `btn-${type}`, cssClasses)}
            onClick={onClick}
        >
            {text}
        </button>
    )
};