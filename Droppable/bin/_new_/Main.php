<?php
	
	include 'functions.php';
	include 'BaseEngine.php';
	include 'EngineBeeMP3s.php';
	include 'EngineMP3raid.php';

	$additional = 'ondrop="Spotific.handleDrop(event)" ondragover="Spotific.allowDD(event)"';
	$downloadstr = '<h4 id="dragger">Drag and drop a song here</h4>';
	$loaderGIF = '<img src="./content/loading.svg" width="64px" height="64px" />';

	$engines = [new EngineBeeMP3s(), new EngineMP3raid()];

	$downloadstr .= '<br><h6 style="color: rgb(255, ' . (75 + (25 * count($engines))) . ', 75)">PLEASE NOTE: WE ONLY HAVE ' . 
					count($engines) . ' ENGINE(S) WORKING AT THE MOMENT</h6>';

	$content = "";
	// The non-space-changed song info
	$songInfo = "";
	// The song info where spaces -> +
	$filteredSongInfo = "";
	$artistName = "";
	$songName = "";
	$downloadURL = "";
	$fDownloadURL = "";

	$set = false;
	$toDownload = false;
	$goAhead = false;
	$letsDownload = false;
	$downloadCorrupted = false;

	if (isset($_POST["downloadURL"]) && !empty($_POST["downloadURL"]))
	{
		$filtered = htmlentities($_POST["downloadURL"]);

		if (strpos($filtered, " - ") !== false)
		{
			$content = $filtered;
		}
		else
		{
			$content = file_get_contents($filtered);

			$content = parseSongInfo($content);
		}

		$set = true;
	}

	if (isset($_POST["songinfo"]) && !empty($_POST["songinfo"]))
	{
		$songInfo = $filtered = htmlentities($_POST["songinfo"]);
		$filteredSongInfo = changeSpacesToPlus($filtered);

		// Artist Name
		$artistName = strtolower(trim(explode('-', $filteredSongInfo)[0]));
		// Song Name
		$songName = strtolower(trim(explode('-', $filteredSongInfo)[1]));

		$toDownload = true;
	}

	if ($toDownload)
	{
		foreach ($engines as $eng)
		{
			$result = $eng->Search($filteredSongInfo, $artistName, $songName);

			if ($result != null)
			{
				$downloadURL = $eng->ResolveDownloadURL($result);

				$downloadURL = URLFixer($downloadURL);

				if (strpos($downloadURL, ".mp3") !== false && $downloadURL != "%ERROR%")
					break;
			}
		}

		if ($downloadURL != "" && strpos($downloadURL, ".mp3") !== false && $downloadURL != "%ERROR%")
			$goAhead = true;
		else
			$downloadCorrupted = true;
	}

	if (isset($_POST["downloadpageurl"]) && !empty($_POST["downloadpageurl"]))
	{
		$toDownload = false;

		$fDownloadURL = htmlentities($_POST["downloadpageurl"]);

		if (true/*strpos($fDownloadURL, ".mp3") !== false/* && (checkMp3($fDownloadURL) || $mymp3found*/)
			$letsDownload = true;
		else
			$downloadCorrupted = true;
	}

	if ($toDownload || $set || $letsDownload || $downloadCorrupted)
	{
		$additional = "";
		$downloadstr = "";
	}
	else
		$loaderGIF = "";

	if ($downloadCorrupted || $letsDownload)
	{
		$loaderGIF = "";
	}

	if ($downloadCorrupted)
	{
		$downloadstr = '<h4 id="corrupted">The download is corrupted.</h4><h6>(Or a valid download was not found)</h6><small>We are sorry for the inconvenience :-/</small>'; 
	}
	else if ($set)
	{
		$downloadstr = '<h4 id="spotifyparsing">Parsing song info from Spotify URL...</h4><h6 id="slowservice"></h6>';
	}
	else if ($letsDownload)
	{
		$downloadstr = '<h4 id="done">Your song is now downloading!</h4><h6>Thank You for using Droppable!</h6><small><center>Droppable can not quarantee the quality nor the validity of the song.<br>If the downloaded song is a cover, report it to us.</center></small>' .
						'<br><h6>Manual download link:</h6><h6><a href="' . $fDownloadURL . '" target="_blank">' . $fDownloadURL . '</a></h6>';

		if (strpos($fDownloadURL, "records.") !== false)
			$downloadstr .= '<br><h6 style="color: rgb(100, 255, 100);">✓ This appears to be an official release!</h6>';
		else
			$downloadstr .= '<br><h6 style="color: rgb(255, 50, 50);">✘ This appears not to be an official release.<br>Beware when downloading unofficial versions!</h6>';
	}
	else if ($toDownload)
	{
		$downloadstr = '<h4 id="parsing">Parsing download URL from the third-party services...</h4><div></div>';
	}

	$downloadinfostr = '<main id="downloader" ' . $additional . '>' .
	  				'<div>' .
	  					$downloadstr .
	 					$loaderGIF .
	  				'</div>' .
	   	  		'</main>';
?>