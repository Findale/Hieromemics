"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("JoinGroup", function(connectionId) {
    
});