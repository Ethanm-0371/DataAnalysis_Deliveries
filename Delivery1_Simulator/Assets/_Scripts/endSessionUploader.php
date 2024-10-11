<?php
$servername = "localhost";
$username = "ethanmp";
$database = "ethanmp";

// Create connection
$conn = new mysqli( $servername,  $username,  $password, $database);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully <br>";


$sessionid = $_POST["sessionID"];
$logoutdate = $_POST["logoutDate"];

//$insertquery = "INSERT INTO playersessions (playerID, loginDate) VALUES ('$playername', '$logoutdate')";

$updatequery = "UPDATE playersessions SET logoutDate = '$logoutdate' WHERE sessionID = $sessionid AND logoutDate IS NULL";


if ($conn->query($updatequery) === TRUE){
    //echo "Player registered <br>";

    $lastID = $conn->insert_id;
    echo $lastID;
    //echo "Player inserted with id: " . $lastID . "<br>";
} else {
    echo "Error: " . $updatequery . "<br>" . $conn->error;
}
?>