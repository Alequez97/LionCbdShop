import React from 'react'

interface InfoBadgeProps {
    text: string
    class: string
    show: boolean
    closeButtonOnClick?: () => void
}

export default function InfoBadge(props: InfoBadgeProps) {
    const badgeContainerClasses = props.show ? "container sticky-top p-5" : "container sticky-top p-5 d-none"

    const badgeClass = `alert-${props.class}`
    const badgeClasses = ['alert', 'fade', 'show', badgeClass]

    const btnClass = `btn-${props.class}`
    const btnClasses = ['btn', btnClass]

    return (
        <div className={badgeContainerClasses}>
            <div className="row no-gutters">
                <div className="col-lg-6 col-md-12 m-auto text-center">
                    <div className={badgeClasses.join(" ")} role="alert">
                        <p>{props.text}</p>
                        {props.closeButtonOnClick && <button className={btnClasses.join(" ")} onClick={props.closeButtonOnClick}>Close</button>}
                    </div>
                </div>
            </div>.
        </div>
    )
}
