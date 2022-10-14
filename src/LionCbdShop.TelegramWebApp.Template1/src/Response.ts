interface Response<T> {
    isSuccess: boolean
    message: string
    responseObject: T
}

export default Response