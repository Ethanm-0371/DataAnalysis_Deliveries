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

$playername = $_POST["playerID"];
$newsessiondate = $_POST["loginDate"];

$insertquery = "INSERT INTO playersessions (playerID, loginDate) VALUES ('$playername', '$newsessiondate')";


if ($conn->query($insertquery) === TRUE){

    $lastID = $conn->insert_id;
    echo $lastID;
    
} else {
    echo "Error: " . $insertquery . "<br>" . $conn->error;
}
?>