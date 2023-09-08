import React, {useState} from 'react';
import {useNavigate} from "react-router-dom";
import {addBook} from "../services/apiService.js";
import {validateBook} from "../utils/validations.js";

const AddBook = () => {
    const [book, setBook] = useState({});
    const [errors, setErrors] = useState(null);
    const navigate = useNavigate();

    const handleInputChange = (event) => {
        const {name, value} = event.target;
        setBook(prevState => ({...prevState, [name]: value}));
    }

    const handleAddSubmit = async () => {
        try {
            let validationErrors = validateBook(book)

            if (Object.keys(validationErrors).length > 0) {
                setErrors(Object.values(validationErrors).join(", "));
                return
            }

            await addBook(book)
            navigate("/result/Book added");
        } catch (error) {
            navigate("/result/There was an error adding the book");
        }
    }

    return (
        <div className="p-4 w-full md:w-1/2">
            <h1 className="text-4xl pb-4">Add book</h1>
            <div>
                <form className="w-full">
                    <div className="mb-4">
                        <label htmlFor="title" className="mb-2 text-lg">Title</label>
                        <input type="text" id="title" name="title"
                               className="block w-full p-4 text-md border rounded-lg"
                               onChange={handleInputChange}/>
                    </div>
                    <div className="mb-4">
                        <label htmlFor="author" className="mb-2 text-lg">Author</label>
                        <input type="text" id="author" name="author"
                               className="block w-full p-4 text-md border rounded-lg"
                               onChange={handleInputChange}/>
                    </div>
                    <div className="mb-4">
                        <label className="mb-2 text-lg mr-3">Is available</label>
                        <input type="checkbox" name="isAvailable"
                               onChange={(e) => setBook({...book, isAvailable: e.target.checked})}/>
                    </div>
                    <div className="mb-4">
                        <label htmlFor="description" className="mb-2 text-lg">Description</label>
                        <textarea id="description" name="description"
                                  className="block w-full p-4 text-md border rounded-lg"
                                  onChange={handleInputChange}/>
                    </div>
                </form>
                <div className="mb-4 flex">
                    <button onClick={handleAddSubmit}
                            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Save
                        changes
                    </button>
                </div>
                {errors && <p className="text-red-500">{errors}</p>}
            </div>
        </div>
    );
};

export default AddBook;
