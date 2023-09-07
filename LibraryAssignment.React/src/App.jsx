import {useState} from 'react'
import {
    BrowserRouter as Router,
    Route,
    Routes,
} from 'react-router-dom';

import Navbar from './components/Navbar.jsx'
import Home from './pages/Home.jsx'
import ListAllBooks from './pages/ListAllBooks.jsx'
import BookDetails from "./pages/BookDetails.jsx";
import ResultPage from "./pages/ResultPage.jsx";

function App() {
    const [count, setCount] = useState(0)

    return (<>
            <Router>
                <Navbar/>
                <Routes>
                    <Route path="/" Component={Home}/>
                    <Route path="/list-all-books" Component={ListAllBooks}/>
                    <Route path="/book-details/:bookId" Component={BookDetails}/>
                    <Route path="/result/:message" Component={ResultPage}/>
                </Routes>
            </Router>
        </>
    )
}

export default App
