import Axios from 'axios';

const http = Axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});

export default http;