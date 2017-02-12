<?php

	/*
	 * The new error handler
	 * We do not want to show anything to the user
	 */
	function errorHandler($errno, $errstr, $errfile, $errline) {
		return true;
	}

	// Set the old error handler
	$old_error_handler = set_error_handler("errorHandler");

?>