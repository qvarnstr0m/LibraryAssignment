import React, {useState, useEffect} from 'react';
import { getBooks} from "../services/apiService.js";

const ListAllBooks = () => {
    const [books, setBooks] = useState([])
    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const books = await getBooks()
                setBooks(books)
            } catch (error) {
                console.error("Error fetching books: ", error)
            }
        };
        
        fetchBooks();
    }, []);
    
    return (
        <div className="p-4">
            <h1>List all books</h1>
            <ul>
                {books.map(book => <li key={book.id}>{book.title}</li>)}
            </ul>
        </div>
    );
};

export default ListAllBooks;