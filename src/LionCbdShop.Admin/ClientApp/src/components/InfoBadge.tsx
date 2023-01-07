import classNames from 'classnames'
import { MarkupElementState } from '../types'
import Button from './InputControls/Button'

interface InfoBadgeProps {
    text: string
    show: boolean
    type?: MarkupElementState
    closeButtonOnClick?: () => void
}

export default function InfoBadge(props: InfoBadgeProps) {
    const badgeContainerClasses = classNames('container-fluid', 'sticky-top', 'position-absolute', 'pt-10', { 'd-none': !props.show })
    const badgeClasses = classNames(`alert-${props.type}`, 'alert', 'fade', 'show')

    return (
        <div className={badgeContainerClasses}>
            <div className="col-lg-6 col-md-12 m-auto text-center">
                <div className={badgeClasses} role="alert">
                    <p>{props.text}</p>
                    {props.closeButtonOnClick && <Button text='Close' type={props.type ?? MarkupElementState.PRIMARY} cssClasses={['btn', `btn-${props.type}`]} onClick={props.closeButtonOnClick} />}
                </div>
            </div>
        </div>
    )
}
