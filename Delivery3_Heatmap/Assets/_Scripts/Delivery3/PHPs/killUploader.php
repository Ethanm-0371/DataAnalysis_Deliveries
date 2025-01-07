<?php

// require_once 'DBConnection.php';
// startConnection();

include ("D3DBConnection.php");

$posX = $_POST["x"];
$posY = $_POST["y"];
$posZ = $_POST["z"];

$updatequery = "INSERT INTO kills (x, y, z) VALUES ('$posX', '$posY', '$posZ')";


if ($conn->query($updatequery) === TRUE){

    $lastID = $conn->insert_id;
    echo $lastID;

} else {
    echo "Error: " . $updatequery . "<br>" . $conn->error;
}
?>