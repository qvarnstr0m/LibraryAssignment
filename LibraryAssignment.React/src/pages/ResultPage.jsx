import React from 'react';
import { useParams } from "react-router-dom";

const ResultPage = () => {
    const { message } = useParams();
    return (
        <div className="p-4">
            <h1 className="text-4xl pb-4">Result</h1>
            <p className="text-lg mt-5 pb-4">Here's how it went:</p>
            <p className="text-lg pb-4">{!message ? "You didn't do anything" : message}</p>
        </div>
    );
};

export default ResultPage;