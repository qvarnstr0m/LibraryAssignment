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

export const getBookById = async (id) => {
    try {
        const response = await apiClient.get(`books/${id}`);
        return response.data;
    } catch (error) {
        console.error("Error while fetching book", error);
        throw new Error(response.statusText);
    }
}

export const updateBook = async (bookId) => {
    try {
        const response = await apiClient.put(`books/${bookId}}`);
        return response.data;
    } catch (error) {
        console.error("Error while updating book", error);
        throw new Error(response.statusText);
    }
}

export const deleteBook = async (bookId) => {
    try {
        const response = await apiClient.delete(`books/${bookId}`);
        return response.data;
    } catch (error) {
        console.error("Error while deleting book", error);
        throw new Error(response.statusText);
    }
}

