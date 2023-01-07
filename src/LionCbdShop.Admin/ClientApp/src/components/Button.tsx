import classNames from 'classnames';

interface ButtonProps {
    text: string
    type: "primary" | "warning" | "danger"
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