import './MainButton.scss'

interface MainButtonProps {
    text: string;
    onClick: () => void;
}

export default function MainButton({ text, onClick }: MainButtonProps) {
  return (
    <div className="wrap">
      <button className="custom-main-button" onClick={onClick}>{text}</button>
    </div>
  )
}
