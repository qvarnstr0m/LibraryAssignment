import {useState} from 'react'
import {
    BrowserRouter as Router,
    Route,
    Routes,
} from 'react-router-dom';

import Navbar from './components/Navbar.jsx'
import Home from './pages/Home.jsx'
import ListAllBooks from './pages/ListAllBooks.jsx'

function App() {
    const [count, setCount] = useState(0)

    return (<>
            <Router>
                <Navbar/>
                <Routes>
                    <Route path="/" Component={Home}/>
                    <Route path="/list-all-books" Component={ListAllBooks}/>
                </Routes>
            </Router>
        </>
    )
}

export default App
