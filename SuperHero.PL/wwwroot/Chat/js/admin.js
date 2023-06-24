var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


connection.on("ReceiveMessage", function (user, message) {
    var msg = user + ":" + message;
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
