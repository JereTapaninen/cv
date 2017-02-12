<?php

	if (isset($_POST["submitBTN"]))
	{
		if (isset($_POST["noLives"]))
			$_SESSION["noLives"] = true;
		else
			$_SESSION["noLives"] = false;

		if (isset($_POST["noDB"]))
			$_SESSION["noDB"] = true;
		else
			$_SESSION["noDB"] = false;
	}

?>