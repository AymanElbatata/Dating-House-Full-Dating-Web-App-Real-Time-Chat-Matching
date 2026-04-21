
    // Create connection
const connection2 = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();

    connection2.start().then(function () {
    });
    // When like notification received
    connection2.on("ReceiveLikeNotification", function (CurrentCounter) {
        document.getElementById("currentUserCounter_Likes").textContent = CurrentCounter;
    });
connection2.on("ReceiveViewNotification", function (CurrentCounter) {
        document.getElementById("currentUserCounter_Views").textContent = CurrentCounter;
        });
connection2.on("ReceiveMessageNotification", function (CurrentCounter) {
        document.getElementById("currentUserCounter_Messages").textContent = CurrentCounter;
        });
connection2.on("ReceiveFavoriteNotification", function (CurrentCounter) {
        document.getElementById("currentUserCounter_Favorites").textContent = CurrentCounter;
        });
connection2.on("ReceiveBlockNotification", function (CurrentCounter) {
        document.getElementById("currentUserCounter_Blocks").textContent = CurrentCounter;
    });
