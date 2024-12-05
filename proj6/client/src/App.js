import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './Login';
import MainView from './MainView';
import AdminDashboard from './AdminDashboard';

const App = () => {
    return (
        <Router>
            <Routes>
                {/* Strona logowania */}
                <Route path="/" element={<Login />} />

                {/* G��wne widoki */}
                <Route path="/main" element={<MainView />} />
                <Route path="/admin" element={<AdminDashboard />} />

                {/* Obs�uga nieznanych �cie�ek */}
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </Router>
    );
};

export default App;
