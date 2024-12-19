<?php

// require_once 'DBConnection.php';
// startConnection();

include ("DBConnection.php");

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