<?php
    $servername = "localhost";
    $username = "jonathancl1";
    $database = "jonathancl1";
    $password = "47877356g";

    // Create connection
    $conn = new mysqli($servername, $username, $password, $database);

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }
?>