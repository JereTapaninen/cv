<?php

	$additional = 'ondrop="Spotific.handleDrop(event)" ondragover="Spotific.allowDD(event)"';
	$downloadstr = '<h4 id="dragger">Drag and drop a song here</h4>';
	$loader = '<img src="./content/loading.svg" width="64px" height="64px" />';

	if ($toDownload || $set || $letsDownload || $downloadCorrupted)
	{
		$additional = "";
		$downloadstr = "";
	}
	else
		$loader = "";

	if ($downloadCorrupted || $letsDownload)
	{
		$loader = "";
	}

	if ($downloadCorrupted)
		$downloadstr = '<h4 id="corrupted">The download is corrupted.</h4><h6>(Or a valid download was not found)</h6><small>We are sorry for the inconvenience :-/</small>'; 
	else if ($set)
		$downloadstr = '<h4 id="spotifyparsing">Parsing song info from Spotify URL...</h4><h6 id="slowservice"></h6>';
	else if ($letsDownload)
		$downloadstr = '<h4 id="done">Your song is now downloading!</h4><h6>Thank You for using Droppable!</h6><small><center>Droppable can not quarantee the quality nor the validity of the song.<br>If the downloaded song is a cover, report it to us.</center></small>';
	else if ($toDownload)
		$downloadstr = '<h4 id="parsing">Parsing download URL from the third-party service...</h4><div><h6>Service: ' . $serv . '</h6></div>';


	print('<main id="downloader" ' . $additional . '>' .
		  '<div>' .
		  $downloadstr .
		  $loader .
		  '</div>' .
		  '</main>');

?>