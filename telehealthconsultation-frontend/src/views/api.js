import axios from 'axios'

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7036', 
  timeout: 5000 
})

export default axiosInstance
