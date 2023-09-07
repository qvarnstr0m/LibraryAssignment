import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <nav className="bg-blue-500 p-4 text-white">
            <Link className="mx-2" to="/">Home</Link>
            <Link className="mx-2" to="/list-all-books">List All Books</Link>
        </nav>
    );
};

export default Navbar;
