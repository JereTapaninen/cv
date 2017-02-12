<?php
	/*
	 *	Used to grab the color from the URL
	 */
	 
	function GetContentColor()
	{
		if(isset($_GET["c"]) && !empty($_GET["c"]))
		{
			switch($_GET["c"])
			{
			 case 1:
				return "black";
			case 2:
				return "red";
			case 3:
				return "green";
			case 4:
				return "yellow";
			case 5:
				return "blue";
			case 6:
				return "notimplemented";
			default:
				return "white";
			}
		}

		return "white";
	}
?>