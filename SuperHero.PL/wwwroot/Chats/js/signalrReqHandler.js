﻿var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Chat/Index')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(message) {
    connection.invoke('SendMessage', message);
}