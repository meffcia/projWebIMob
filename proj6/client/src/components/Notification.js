import React, { useEffect, useState } from 'react';

const Notification = ({ connection }) => {
  const [notifications, setNotifications] = useState([]);

  useEffect(() => {
    connection.on("ReceiveTicketUpdate", (message) => {
      setNotifications((prev) => [...prev, message]);
    });
  }, [connection]);

  return (
    <div>
      <h2>Notifications</h2>
      <ul>
        {notifications.map((note, index) => (
          <li key={index}>{note}</li>
        ))}
      </ul>
    </div>
  );
};

export default Notification;
