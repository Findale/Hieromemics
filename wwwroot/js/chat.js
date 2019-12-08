"use strict";

//let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/* async function haha ()
{
await sleep(1000);
}
haha;*/
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var groupid = document.getElementById("grp").toString();
var name = document.getElementById("name").textContent;
    
//Disable send button until connection is established
connection.on("ReceiveMessage", function(message) {
    
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var colpos = msg.search(":");
    var senderName = msg.substring(0,colpos);
    var picLink = msg.substring(colpos+1);
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth();
    var day =date.getDate();
    var hour = date.getHours();
    var minutes =date.getMinutes();
    var image = document.createElement("img");
    image.src = picLink; 
    image.width = 600;
    image.height = 315;
    var li = document.createElement("li");
    var mesList = document.getElementById("messages");
    var dateString = document.createElement("p");
    var dateNode = document.createTextNode("Sent: " + day + "/" + month + "/" + year + "   "  + hour +":" + minutes);
    var sep = document.createElement('hr');
    li.append(image); 
    var bold = document.createElement('strong');
    bold.append(senderName);
    mesList.insertBefore(sep, mesList.childNodes[0]);
    mesList.insertBefore(dateNode, mesList.childNodes[0]);
    mesList.insertBefore(li, mesList.childNodes[0]);
    mesList.insertBefore(bold, mesList.childNodes[0]);
    //document.getElementById("messages").appendChild(li);
    //document.getElementById("messages").appendChild(sep);
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

    message = name + ":" + message;
    connection.invoke("SendMessageToGroup", groupid, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    document.getElementById("message").value = "";
});
/*
document.getElementById("joinGroup").addEventListener("click", function(event) {
    connection.invoke("JoinGroup", "PrivateGroup").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
*/