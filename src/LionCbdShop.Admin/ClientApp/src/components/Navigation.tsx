import { Link } from 'react-router-dom';

export default function Navigation() {
    return (
        <>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <Link to="/" className="navbar-brand">Lion CBD Admin</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <Link to="/products" className="nav-link text-dark">Products</Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/orders" className="nav-link text-dark">Orders</Link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </>
    )
}
