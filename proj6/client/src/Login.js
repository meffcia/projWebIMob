import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleLogin = (e) => {
        e.preventDefault();

        // Prosta weryfikacja loginu i has³a
        if (username === 'admin' && password === 'admin123') {
            navigate('/admin'); // Admin idzie do dashboardu
        } else if (username === 'user' && password === 'user123') {
            navigate('/main'); // User idzie do g³ównego widoku
        } else {
            alert('Invalid username or password! Try again.');
        }
    };

    return (
        <div>
            <h1>Logowanie</h1>
            <form onSubmit={handleLogin}>
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <button type="submit">Zaloguj</button>
            </form>
        </div>
    );
};

export default Login;
