
$(function () {
    $.connection.solutionsHub.client.send = function (message) {

        $("#feedback").append($("<p></p>").html(message));
        //alert(message);
    };

    $.connection.hub.start()
        .done(function () {
            console.log("Signalr connection worked");
            $.connection.solutionsHub.server.announce("connected.!");
        })
        .fail(function () { alert("ERROR!!"); });
});


