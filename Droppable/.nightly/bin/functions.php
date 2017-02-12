<?php

	return;

	/*
	 * Throws a fatal exception with a message
	 */
	function throwException($msg)
	{
		die("ERROR: " . $msg);
	}
	
	function removeMP3($text)
	{
		return str_replace(".mp3", "", $text);
	}
	
	function removeMP3AndTrim($text)
	{
		return trim(str_replace(".mp3", "", $text));
	}

	function areSessionVarsSet()
	{
		return (isset($_SESSION["noLives"])) || (isset($_SESSION["noDB"]));
	}

	/*
	 * Functions that PHP doesn't have because fuck programmers
	 */
	function startsWith($haystack, $needle) {
    	// search backwards starting from haystack length characters from the end
    	return $needle === "" || strrpos($haystack, $needle, -strlen($haystack)) !== FALSE;
	}
	function endsWith($haystack, $needle) {
	    // search forward starting from end minus needle length characters
	    return $needle === "" || (($temp = strlen($haystack) - strlen($needle)) >= 0 && strpos($haystack, $needle, $temp) !== FALSE);
	}

	/* 
	 * Parses the given Spotify page's content
	 * Returns the song information
	 */
	function parseSongInfo($cont) {
		$description = explode("<meta property=\"description\" content=\"", $cont)[1];
		$description = explode("\">", $description)[0];

		$songName = explode(", a song by ", $description)[0];
		$artistName = explode(", a song by ", $description)[1];
		$artistName = explode(" on Spotify", $artistName)[0];

		return $artistName . " - " . $songName;
	}

	/* Second Phase */

	function changeSpacesToPlus($text) {
		return str_replace(" ", "+", $text);
	}

	function doubleToOne($text) {
		return str_replace("__", "_", str_replace("__", "_", $text));
	}

	function removeLineTrim($text) {
		return trim(str_replace(" - ", "", $text));
	}

	function textUnderlined($text) {
		return str_replace("__", "_", str_replace(" ", "_", str_replace("+", "_", $text)));
	}

	function artistSongNameUnderlined($a, $s) {
		return str_replace("__", "_", str_replace("+", "_", $a . $s));
	}

	function normalizeString($text) {
		return str_replace("+", " ", $text);
	}

	function stringWithLines($text) {
		return str_replace("_", "-", str_replace(" ", "-", str_replace("+", "-", $text)));
	}

	function isIgnoredMyMP3($sTitle) {
		return (strpos(file_get_contents("./.lists/ignored_mymp3"), $sTitle) !== false);
	}

	function plusesToMemes($text) {
		return str_replace("+", "%20", $text);
	}

	/*
	 * Checks whether the given domain is available or not
	 */
  	function isDomainAvailable($domain)
   	{
           //check, if a valid url is provided
           if(!filter_var($domain, FILTER_VALIDATE_URL))
           {
                   return false;
           }

           //initialize curl
           $curlInit = curl_init($domain);
           curl_setopt($curlInit,CURLOPT_CONNECTTIMEOUT,10);
           curl_setopt($curlInit,CURLOPT_HEADER,true);
           curl_setopt($curlInit,CURLOPT_NOBODY,true);
           curl_setopt($curlInit,CURLOPT_RETURNTRANSFER,true);

           //get answer
           $response = curl_exec($curlInit);

           curl_close($curlInit);

           if ($response) return "Yes";

           return "No";
   }

   /*
    * Checks the given URL to see whether it is a valid MP3 file or not
    */
	function checkMp3($url) {
		$a = parse_url($url);
		if(checkdnsrr(str_replace("www.","",$a['host']),"A") || checkdnsrr(str_replace("www.","",$a['host']))) {
		    $ch = @curl_init();
		    curl_setopt($ch, CURLOPT_URL, $url);
		    curl_setopt($ch, CURLOPT_HEADER, 1);
		    curl_setopt($ch, CURLOPT_NOBODY, 1);
		    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
		    curl_setopt($ch, CURLOPT_TIMEOUT, 15);
		    $results = explode("\n", trim(curl_exec($ch)));
		    $mime = "";
		    foreach($results as $line) {
		        if (strtok($line, ':') == 'Content-Type') {
		            $parts = explode(":", $line);
		            $mime = trim($parts[1]);
		        }
		    }
		    return $mime=="audio/mpeg";
		} else {
		    return false;
		}
	}

?>