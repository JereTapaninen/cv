<?php

	// Check if the user is banned
	$clientIP = $_SERVER['REMOTE_ADDR'];
	if (strpos(file_get_contents("./.lists/bannedips"), $clientIP) !== false)
	{
		echo '<h1>You are banned from using Droppable.</h1>';
		echo '<h3>This might be due to a copyright infringement</h3>';
		die("Banned Exception");
	}

	// Check for mobile
	if (isset($_GET["mobile"]) && !empty($_GET["mobile"]))
	{
		if ($_GET["mobile"] == "true")
		{
			echo '<h1>Droppable is not compatible with mobile devices.</h1>';
			die("Mobile User Exception");
		}
	}

?>