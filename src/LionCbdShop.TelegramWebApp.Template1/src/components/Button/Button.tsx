import React, { MouseEventHandler } from 'react'
import './Button.scss'

interface ButtonProps {
    type: string,
    title: string,
    disabled: boolean,
    onClick: MouseEventHandler<HTMLElement>
}

function Button(props: ButtonProps) {
  return (
    <button 
        className={`btn ${
            (props.type === 'add' && 'add') || 
            (props.type === 'remove' && 'remove') ||
            (props.type === 'checkout' && 'checkout')
        }`}
        disabled={props.disabled}
        onClick={props.onClick}
    >
        {props.title}
    </button>
  )
}

export default Button