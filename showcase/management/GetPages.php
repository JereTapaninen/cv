<?php

	function GetPages()
	{
		$lines = explode("\n", file_get_contents("sites"));
		
		$newArray = array();
		
		foreach($lines as &$line)
		{
			if(substr($line, 0, 2) != "//")
			{
				$line = str_replace('"', "", $line);
				$parts = explode("-", $line);
				
				$newArray[sizeof($newArray)] = $parts;
			}
		}
		
		return $newArray;
	}

?>