import classNames from 'classnames'

interface InfoBadgeProps {
    text: string
    type: string
    show: boolean
    closeButtonOnClick?: () => void
}

export default function InfoBadge(props: InfoBadgeProps) {
    const badgeContainerClasses = classNames('container-fluid', 'sticky-top', 'position-absolute', 'pt-10', { 'd-none': !props.show })
    const badgeClasses = classNames(`alert-${props.type}`, 'alert', 'fade', 'show')
    const btnClasses = classNames('btn', `btn-${props.type}`)

    return (
        <div className={badgeContainerClasses}>
            <div className="col-lg-6 col-md-12 m-auto text-center">
                <div className={badgeClasses} role="alert">
                    <p>{props.text}</p>
                    {props.closeButtonOnClick && <button className={btnClasses} onClick={props.closeButtonOnClick}>Close</button>}
                </div>
            </div>
        </div>
    )
}
