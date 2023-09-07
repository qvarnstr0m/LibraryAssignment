import React from 'react';
import {useParams} from "react-router-dom";

const BookDetails = () => {
    const {bookId} = useParams();
    
    return (
        <div className="p-4">
            <h1 className="text-4xl pb-4">Book details for book id: {bookId}</h1>
        </div>
    );
};

export default BookDetails;