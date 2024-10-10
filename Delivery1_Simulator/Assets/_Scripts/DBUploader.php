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
echo "Connected successfully";


$playername = $_POST["playername"];
$escapedName = str_replace("'", "''", $playername);
$country = $_POST["country"];
$age = $_POST["age"];
$gender = $_POST["gender"];
$installationdate = $_POST["date"];

$insertquery = "INSERT INTO player (playername, country, age, gender, installationdate) VALUES ('$escapedName', '$country', '$age', '$gender', '$installationdate')";


if ($conn->query($insertquery) === TRUE){
    echo "Player registered";
} else {
    echo "Error: " . $insertquery . "<br>" . $conn->error;
}
?>