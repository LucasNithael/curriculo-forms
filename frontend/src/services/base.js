import axios from 'axios'

function getBaseUrl() {
  if (import.meta.env.DEV) {
    return 'https://localhost:7283/'
  }
  return ''
}

const axiosInstance = axios.create({
  baseURL: getBaseUrl(),
  timeout: 30000,
  headers: { 'Content-Type': 'application/json' }
})

export default axiosInstance