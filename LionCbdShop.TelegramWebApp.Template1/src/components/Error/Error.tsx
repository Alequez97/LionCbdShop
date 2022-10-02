import React from 'react'
import './Error.scss'

interface ErrorProps {
    message: string
}

export default function Error({ message }: ErrorProps) {
  return (
    <div id="error-text">{message}</div>
  )
}
