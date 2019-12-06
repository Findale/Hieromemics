"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says:";  
    var image = document.createElement("img");
    image.src = msg; 
    image.width = 600;
    image.height = 315;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.append(image);
    document.getElementById("messagesList").appendChild(li);
});
//start the hub
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.invoke("OnConnectedAsync", user);

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageToGroup", message).catch(function (err) {
        return console.error(err.toString());
    });

connection.invoke("OnDisconnectedAsync", user)
    event.preventDefault();
});
