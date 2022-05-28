document.getElementById("moveToRoom").addEventListener("click", function (event) {
    var roomName = document.getElementById("roomName").value;
    window.location.href = "Room/" + roomName;
});