<?php

include ("DBConnection.php");

$playername = $_POST["playername"];
$escapedName = str_replace("'", "''", $playername);
$country = $_POST["country"];
$age = $_POST["age"];
$gender = $_POST["gender"];
$installationdate = $_POST["date"];

$insertquery = "INSERT INTO player (playername, country, age, gender, installationdate) VALUES ('$escapedName', '$country', '$age', '$gender', '$installationdate')";


if ($conn->query($insertquery) === TRUE){

    $lastID = $conn->insert_id;
    echo $lastID;
    
} else {
    echo "Error: " . $insertquery . "<br>" . $conn->error;
}
?>