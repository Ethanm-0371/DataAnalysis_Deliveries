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

$itemid = $_POST["itemID"];
$purchasedate = $_POST["purchaseDate"];
$sessionid = $_POST["sessionID"];

$insertquery = "INSERT INTO purchases (itemID, purchaseDate, sessionID) VALUES ('$itemid', '$purchasedate', '$sessionid')";


if ($conn->query($insertquery) === TRUE){

    $lastID = $conn->insert_id;
    echo $lastID;

} else {
    echo "Error: " . $insertquery . "<br>" . $conn->error;
}
?>