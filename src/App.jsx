import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Home from './Pages/Home';
import { DashboardLayout } from './Layout/DashboardLayout';
import Footerdown from './Pages/Footerdown';
import PrivateRoute from './Utils/PrivateRoute';
import Comment from './Pages/Comment';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Public routes */}
                <Route path="/" element={<Login />} />
                <Route path="/register" element={<Register />} />

                {/* Protected routes */}
                <Route element={<PrivateRoute />}>
                    <Route element={<DashboardLayout />}>
                        <Route path="/home" element={<Home />} />
                    </Route>
                    <Route path="/comment" element={<Comment />} />
                </Route>



            </Routes>
            <Footerdown />
        </BrowserRouter>
    );
}

export default App;
