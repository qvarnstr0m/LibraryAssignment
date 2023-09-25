import React, {useState, useEffect} from 'react';
import {searchBooks} from "../services/apiService.js";
import {Link} from "react-router-dom";
import { useLocation } from "react-router-dom";

const SearchResult = () => {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const location = useLocation();
    const searchTerm = new URLSearchParams(location.search).get("term");

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const books = await searchBooks(searchTerm)
                setBooks(books)
                setLoading(false)
            } catch (error) {
                setError("There was an error fetching books")
                setLoading(false)
                console.error("Error fetching books: ", error)
            }
        };

        fetchBooks();
    }, [searchTerm]);

    return (
        <div className="p-4">
            <h1 className="text-4xl pb-4">Search results</h1>
            <p className="text-lg pb-4">Click on a title for details</p>
            {loading && <p className="text-red-500">Loading...</p>}
            {error && <p className="text-red-500">Error fetching books.</p>}
            {!loading && !error &&
                <table>
                    <thead>
                    <tr>
                        <th className="px-4 py-2 flex justify-start">Title</th>
                        <th className="px-4 py-2">Author</th>
                        <th className="px-4 py-2">Is available</th>
                    </tr>
                    </thead>
                    <tbody>
                    {books.map((book, index) => <tr key={book.id} className={index % 2 === 0 ? "bg-gray-100" : ""}>
                        <td className="border px-4 py-2 hover:underline"><Link className="mx-2" to={`/book-details/${book.id}`}>{book.title}</Link></td>
                        <td className="border px-4 py-2">{book.author}</td>
                        <td className="border px-4 py-2">{book.isAvailable ? "Yes" : "No"}</td>
                    </tr>)}
                    </tbody>
                </table>
            }
        </div>
    );
};

export default SearchResult;