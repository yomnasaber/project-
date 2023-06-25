var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/*start SendMessage*/
connection.on("ReceiveMessage", function (user, message) {
    var msg = user + " :-" + message;
    var li = document.createElement("li");
    li.textContent = msg;

    $("#list").prepend(li);
});

connection.start();

$("#btnSend").on("click", function () {
    var user = $("#txtUser").val();
    var message = $("#txtMessage").val();

    connection.invoke("SendMessage", user, message);

});

/*end SendMessage*/

/*start JoinGroup*/
connection.on("GroupMessage", function (name, group) {  
    var msg = name + " joinned " + group; 
    var li = document.createElement("li");
    li.textContent = msg;

    $("#list").prepend(li);
});

$("#btngroup").on("click", function () {
    var name = $("#txtUser").val(); 
    var group = $("#txtgroup").val();

    connection.invoke("JoinGroup", group, name);

});
/*end JoinGroup*/

/*start GroupSendToMessage*/
connection.on("GroupSendToMessage", function (group,name, message) {
    var msg = name + "(" + group + "):" + message; 
    var li = document.createElement("li");
    li.textContent = msg;

    $("#list").prepend(li);  
});
 
$("#txtSendGroup").on("click", function () {    
   
    var name = $("#txtUser").val();
    var group = $("#txtgroup").val(); 
    var message = $("#txtMessage").val();
    connection.invoke("SendToGroup", name, group, message);  
});
/*end GroupSendToMessage*/