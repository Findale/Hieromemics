"use strict";

//let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/* async function haha ()
{
await sleep(1000);
}
haha;*/
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var groupid = document.getElementById("grp").toString();

    
//Disable send button until connection is established
connection.on("ReceiveMessage", function(message) {
    
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var image = document.createElement("img");
    image.src = msg; 
    image.width = 600;
    image.height = 315;
    var li = document.createElement("li");
    li.append(image);
    document.getElementById("messages").appendChild(li);
    var sep = document.createElement('hr');
    document.getElementById("messages").appendChild(sep);
});

connection.on("UserConnected", function() {
    /*var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);*/
    connection.invoke("JoinGroup", groupid).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("UserDisconnected", function() {
    /*var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }*/
    connection.invoke("JoinGroup", groupid).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.start().catch(function(err) {
    return console.error(err.toString());
});
/*
document.getElementById("sendButton").addEventListener("click", function(event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
    
    if (groupValue === "All" || groupValue === "Myself") {
        var method = groupValue === "All" ? "SendMessageToAll" : "SendMessageToCaller";
        connection.invoke(method, message).catch(function (err) {
            return console.error(err.toString());
        });
    } else if (groupValue === "PrivateGroup") {
        connection.invoke("SendMessageToGroup", "PrivateGroup", message).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        connection.invoke("SendMessageToUser", groupValue, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    
    event.preventDefault();
});
*/

document.getElementById("chatButt").addEventListener("click", function(event) {
    var message = document.getElementById("message").value;

    connection.invoke("SendMessageToGroup", groupid, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
/*
document.getElementById("joinGroup").addEventListener("click", function(event) {
    connection.invoke("JoinGroup", "PrivateGroup").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
*/