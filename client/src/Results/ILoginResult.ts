export interface ILoginResult {
  value: {
    token: string
    userId: string
    email: string
    username: string
  }
  isSuccess: boolean
  statusCode: number
  message: string
}
