<?php

// require_once 'DBConnection.php';
// startConnection();

include ("D3DBConnection.php");

$updatequery = "SELECT * FROM position";

$queryResult = $conn->query($updatequery);

if ($queryResult->num_rows > 0){

    $rows = array();
    while($r = mysqli_fetch_assoc($queryResult)) {
        $rows[] = $r;
    }

    echo json_encode($rows);

} else {
    echo "Error: " . $updatequery . "<br>" . $conn->error;
}
?>