"use strict";

/* async function haha ()
{
await sleep(9000);
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
    var month = date.getMonth() + 1;
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
});

connection.on("UserConnected", function() {
    connection.invoke("JoinGroup", groupid).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("UserDisconnected", function() {
    connection.invoke("JoinGroup", groupid).catch(function (err) {
        return console.error(err.toString());
    });
});
/*IMPORTANT, DO NOT DELETE OR YOU WILL RUIN US ALL*/
connection.start().catch(function(err) {
    return console.error(err.toString());
});

document.getElementById("chatButt").addEventListener("click", function(event) {
    var message = document.getElementById("message").value;

    message = name + ":" + message;
    connection.invoke("SendMessageToGroup", groupid, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    document.getElementById("message").value = "";
});