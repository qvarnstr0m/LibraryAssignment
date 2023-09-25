import React, {useState} from 'react';
import { searchBooks } from "../services/apiService.js";
import {useNavigate} from "react-router-dom";

const Search = () => {
    const [searchTerm, setSearchTerm] = useState("");
    const navigate = useNavigate();

    const handleSearchSubmit = async (event) => {
        event.preventDefault();
        try {
            navigate(`/search-result?term=${searchTerm}`);
        } catch (error) {
            console.error("Error searching books: ", error)
        }
    };

    const handleInputChange = (event) => {
        setSearchTerm(event.target.value);
    }

    return (
        <div className="p-4 w-full md:w-1/2">
            <h1 className="text-4xl pb-4">Search</h1>
            <p className="text-lg pb-4">Search for a book by title or author</p>
            <form className="flex flex-col">
                <label className="pb-2" htmlFor="searchTerm">Search</label>
                <input className="block w-full p-2 text-md border rounded-lg mb-2" type="text" id="searchTerm"
                       name="searchTerm" onChange={handleInputChange}/>
                <button onClick={handleSearchSubmit}
                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                        type="submit">Search
                </button>
            </form>
        </div>
    );
};

export default Search;