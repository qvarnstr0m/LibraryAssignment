import axios from 'axios';

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

const apiClient = axios.create({
    baseURL: BASE_URL,
    timeout: 10000,
    timeoutErrorMessage: 'Request timed out',
    headers: {
        'Content-Type': 'application/json',
    }
});

export const getBooks = async () => {
    try {
        const response = await apiClient.get('books');
        return response.data;
    } catch (error) {
        console.error("Error while fetching books", error);
        throw new Error(response.statusText);
    }
};

