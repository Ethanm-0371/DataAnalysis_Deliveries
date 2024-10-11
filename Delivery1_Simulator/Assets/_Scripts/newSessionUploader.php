<?php

// require_once 'DBConnection.php';
// startConnection();

include ("DBConnection.php");

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