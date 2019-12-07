"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//works with all and myself chat
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

connection.on("FriendConnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);
});

connection.on("FriendDisconnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.clientHeight; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
});

connection.on("ReceiveFrMessage", function (connectionId, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = connectionId + " says:";  
    var image = document.createElement("img");
    image.src = msg; 
    image.width = 600;
    image.height = 315;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.append(image);
    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var groupElement =  document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
    
    if (groupValue == "All" || "Myself") {
        var method = groupValue == "All" ? "SendMessage" : "SendMessageToCaller"
        connection.invoke(method, user, message).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        connection.invoke("SendMessageToFriend", connectionId, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    

    event.preventDefault();
});
