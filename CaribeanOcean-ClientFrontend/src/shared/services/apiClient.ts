import axios from "axios";

const apiClient = axios.create({
  baseURL: "https://caribbeanoceanapi20260616221010-hwcmbkfzb7fqcthw.eastus-01.azurewebsites.net//api",
  headers: {
    "Content-Type": "application/json",
  },
});

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("API Error:", error);
    return Promise.reject(error);
  }
);

export default apiClient;