import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App'; // Importujemy komponent App z App.js
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />  {/* Renderujemy komponent App */}
  </React.StrictMode>
);

reportWebVitals(); // Mierzenie wydajności aplikacji
