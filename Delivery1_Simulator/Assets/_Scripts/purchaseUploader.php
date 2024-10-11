<?php

include ("DBConnection.php");

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