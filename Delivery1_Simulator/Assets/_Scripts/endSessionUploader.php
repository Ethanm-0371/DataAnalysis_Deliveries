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

$sessionid = $_POST["sessionID"];
$logoutdate = $_POST["logoutDate"];

$updatequery = "UPDATE playersessions SET logoutDate = '$logoutdate' WHERE sessionID = $sessionid AND logoutDate IS NULL";


if ($conn->query($updatequery) === TRUE){

    $lastID = $conn->insert_id;
    echo $lastID;
    
} else {
    echo "Error: " . $updatequery . "<br>" . $conn->error;
}
?>