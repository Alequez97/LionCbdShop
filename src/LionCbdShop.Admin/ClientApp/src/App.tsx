import { BrowserRouter, Routes, Route } from "react-router-dom";
import AddProduct from "./pages/Products/AddProduct";
import Home from "./pages/Home";
import Layout from "./pages/Layout";
import OrderDetails from "./pages/Orders/OrderDetails";
import Orders from "./pages/Orders/Orders";
import Products from "./pages/Products/Products";
import EditProduct from "./pages/Products/EditProduct";

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path="products" element={<Products />} />
            <Route path="products/add" element={<AddProduct />} />
            <Route path="products/edit/:id" element={<EditProduct />} />
            <Route path="orders" element={<Orders />} />
            <Route path="orders/:id" element={<OrderDetails />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
