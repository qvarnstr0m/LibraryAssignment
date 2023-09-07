import React, { useState, useEffect } from 'react';
import { useParams } from "react-router-dom";
import { getBookById } from "../services/apiService.js";
// ... other imports ...

const BookDetails = () => {
    const { bookId } = useParams();
    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const fetchedBook = await getBookById(bookId)
                setBook(fetchedBook)
                setLoading(false)
            } catch (error) {
                setError("There was an error fetching the book details")
                setLoading(false)
                console.error("Error fetching book details: ", error)
            }
        };

        fetchBook();
    }, [bookId]);

    // To handle input changes
    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setBook(prevState => ({ ...prevState, [name]: value }));
    }

    return (
        <div className="p-4 w-full md:w-1/2">
            <h1 className="text-4xl pb-4">Book details</h1>
            {loading && <p className="text-red-500">Loading...</p>}
            {error && <p className="text-red-500">{error}</p>}
            {!loading && !error && book &&
                <form className="w-full">
                    <div className="mb-4">
                        <label htmlFor="bookId" className="mb-2 text-lg">Id</label>
                        <input type="text" id="bookId" className="block p-4 text-md border rounded-lg text-gray-500" value={book.id} readOnly />
                    </div>
                    <div className="mb-4">
                        <label htmlFor="title" className="mb-2 text-lg">Title</label>
                        <input type="text" id="title" name="title" className="block w-full p-4 text-md border rounded-lg" value={book.title} onChange={handleInputChange} />
                    </div>
                    <div className="mb-4">
                        <label htmlFor="author" className="mb-2 text-lg">Author</label>
                        <input type="text" id="author" name="author" className="block w-full p-4 text-md border rounded-lg" value={book.author} onChange={handleInputChange} />
                    </div>
                    <div className="mb-4">
                        <label className="mb-2 text-lg mr-3">Is available</label>
                        <input type="checkbox" name="isAvailable" checked={book.isAvailable} onChange={(e) => setBook({...book, isAvailable: e.target.checked})} />
                    </div>
                    <div className="mb-4">
                        <label htmlFor="description" className="mb-2 text-lg">Description</label>
                        <textarea id="description" name="description" className="block w-full p-4 text-md border rounded-lg" value={book.description} onChange={handleInputChange} />
                    </div>
                    <div className="mb-4 flex">
                        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Save changes</button>
                        <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded ml-4">Delete book</button>
                    </div>
                </form>
            }
        </div>
    );
};

export default BookDetails;
