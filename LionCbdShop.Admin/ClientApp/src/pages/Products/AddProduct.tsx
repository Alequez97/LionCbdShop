import { ChangeEvent, useState } from "react"
import axios from "axios";

interface InputData {
  productName: string
  originalPrice: number
  priceWithDiscount: number
}

export default function AddProduct() {
  const [inputData, setInputData] = useState<any>()
  const [inputImage, setInputImage] = useState<File>()

  const changeInputHandler = (event: ChangeEvent<HTMLInputElement>) => {
    setInputData((prevState: InputData) => ({...prevState, ...{
      [event.target.name]: event.target.value
    }}))
  }

  const changeImageHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    const fileList = event.target.files;
    if (!fileList) return;
    
    setInputImage(fileList[0]);
  };

  async function createNewProduct() {
    try
    {
      let formData = new FormData();
      formData.append("productName", inputData.productName);
      formData.append("originalPrice", inputData.originalPrice);
      formData.append("priceWithDiscount", inputData.priceWithDiscount);
      if (inputImage) {
        formData.append("productImage", inputImage, inputImage.name);
      }

      const response = await axios.post<Response>('https://localhost:44398/api/products', formData);
      console.log(response);
    } 
    catch 
    {
      console.log('Error creating new product')
    }
  }

  return (
    <>
      <h2 className="text-center">Add new product</h2>
      <div className="mb-2">
        <label htmlFor="productName" className="form-label">Product name</label>
        <input type="text" className="form-control" name="productName" onChange={changeInputHandler} />
      </div>
      <div className="mb-2">
        <label htmlFor="originalPrice" className="form-label">Original price</label>
        <input type="number" step=".01" className="form-control" name="originalPrice" onChange={changeInputHandler} />
      </div>
      <div className="mb-2">
        <label htmlFor="priceWithDiscount" className="form-label">Price with discount</label>
        <input type="number" step=".01" className="form-control" name="priceWithDiscount" onChange={changeInputHandler} />
      </div>
      <div className="mb-2">
        <label htmlFor="image" className="form-label">Image</label>
        <input className="form-control" type="file" name="productImage" onChange={changeImageHandler} />
      </div>
      <div className="text-center">
        <input type="submit" value="Add" className="btn btn-primary" onClick={createNewProduct} />
      </div>
    </>
  )
}
