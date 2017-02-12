<?php
	$serv = "";

	$content = "";
	$set = false;

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

	$toDownload = false;
	$goAhead = false;
	// The non-space-changed song info
	$songInfo = "";
	// The song info where spaces -> +
	$filteredSongInfo = "";
	$downloadpurl = "";
	$downloadengine = 0;

	if (isset($_POST["songinfo"]) && !empty($_POST["songinfo"]))
	{
		$songInfo = $filtered = htmlentities($_POST["songinfo"]);
		$filteredSongInfo = changeSpacesToPlus($filtered);

		$toDownload = true;
	}

	if ($toDownload)
	{
		$beemp3found = false;
		$mp3raidfound = false;
		$mymp3found = false;
		$mp3basefound = false;

		$doContinue = false;
		$currIndex = 2;

		$baseurl = "http://beemp3s.org/";
		$beemp3URL = $baseurl . "index.php?q=" . $filteredSongInfo;
		$c = file_get_contents($beemp3URL);

		// Artist Name
		$an = strtolower(trim(explode('-', $filteredSongInfo)[0]));
		// Song Name
		$sn = strtolower(trim(explode('-', $filteredSongInfo)[1]));

		/*echo "<script>document.getElementById('serviceid').innerHTML = 'BeeMP3s';</script>";*/
		$serv = "BeeMP3s";

		// Loop through every song that is there and find a song that can be downloaded
		do 
		{
			// Download the songs page
			$exploded = explode('<meta itemprop="url" content="', $c);

			// If the current index is too much, we want to break out of the loop
			if ($currIndex >= count($exploded))
				break;

			$subc = $exploded[$currIndex];
			$subc = explode('">', $subc)[0];

			// Check the URL
			$filtered = htmlentities($downloadpurl);

			$filtered = str_replace("&amp;", "&", $filtered);

			$c = file_get_contents($filtered);
			// Elegant code right here vvvvv
			$songTitle = trim(strtolower(explode('</h1>', explode('<h1 class="h1-title-sing" itemprop="name">', $c)[1])[0]));
			$nexploded = explode('"  target="_blank" rel="nofollow" class="a-proceed-big lol">', $c);

			$nsubc = $nexploded[0];
			$nsubc = explode('target="_blank" rel="nofollow">download</a><a href="', $nsubc)[1];

			if ((startsWith($songTitle, $an) && endsWith($songTitle, $sn)) && isDomainAvailable($nsubc) === "Yes")
			{
				if (strpos($nsubc, ".mp3") !== false && (($_SESSION["noLives"] == true && strpos($songTitle, "live") === false && strpos($filteredSongInfo, "live") === false) || $_SESSION["noLives"] == false) && checkMp3($nsubc))
				{
					$downloadpurl = $baseurl . $subc;

					$beemp3found = true;
					$doContinue = true;
				}
			}

			$currIndex++;
		} while(!$doContinue);

		if (!$beemp3found)
		{
			/*echo "<script>document.getElementById('serviceid').innerHTML = 'MP3raid';</script>";*/
			$serv = "MP3raid";

			$currIndex = 1;

			do
			{
				$mp3raidbase = "http://www.mp3raid.info/download/";
				$underlined = artistSongNameUnderlined($an, $sn);
				$mp3raidcontent = file_get_contents($mp3raidbase . $underlined . ".html");

				$exploded = explode("<div class='index1'>", $mp3raidcontent);

				if ($currIndex >= count($exploded))
					break;

				// The current text inside the div class block </div>
				$currContent = $exploded[$currIndex];
				$fromspan = explode('</span>', explode('<span>', $currContent)[1])[0];
				$songTitle = trim(strtolower(explode('</strong>', explode('<strong>', $currContent)[1])[0]));

				$isGood = false;

				$isGood = (strpos($fromspan, "| size:") !== false);

				if ($isGood)
					$isGood = (strpos($fromspan, " MB") !== false);
				// If the download size is not in megabytes, it's probably not the highest quality product. Don't give shit to the user please.

				if (strpos($songTitle, normalizeString($an)) !== false && strpos($songTitle, normalizeString($sn)) !== false && strpos($fromspan, "from: youtube.com") === false && $isGood
					&& (($_SESSION["noLives"] == true && strpos($songTitle, "live") === false && strpos($filteredSongInfo, "live") === false) || $_SESSION["noLives"] == false))
				{
					$innershit = explode('</span>', explode("<span class='mtc'>", $currContent)[1])[0];
					$dl_id = explode("'>", explode("<a href='javascript:;' class='dl' id='", $innershit)[1])[0];

					$dl_link = "http://www.mp3raid.info/search/ddl/" . $dl_id . "/" . $underlined . ".html";
					$tempDL = file_get_contents($dl_link);

					$innershit = explode("<table style='width:100%;table-layout:fixed;'>", $tempDL)[1];
					$tryout = explode('</td>', explode("<td style='word-wrap:break-word;'>", $innershit)[1])[0];
				
					if (isDomainAvailable($tryout) === "Yes")
					{
						$downloadpurl = $dl_link;

						$downloadengine = 1;
						$mp3raidfound = true;
					}
				}

				$currIndex++;
			} while (!$mp3raidfound);
		}

		if (!$mp3raidfound)
		{
			/*echo "<script>document.getElementById('serviceid').innerHTML = 'MP3base';</script>";*/
			$serv = "MP3base";

			$currIndex = 1;

			do 
			{
				$baseurl = "http://mp3base.cc";
				$searchurl = "http://mp3base.cc/search?q=";
				$searchurl .= $an . "+" . $sn;

				$mp3basecontent = file_get_contents($searchurl);

				$exploded = explode('<td><a href="javascript:downloadSong(', $mp3basecontent);

				if ($currIndex >= count($exploded))
					break;

				// The current content
				$currContent = $exploded[$currIndex];
				$song_id = explode(');', $currContent)[0];
				$semidl_link = $baseurl . "/user/download/?song=" . $song_id;
				$semidl_content = file_get_contents($semidl_link);

				$filedlbase = "http://filedl.li/download/";
				$dl_link = $filedlbase . explode('" target=', explode('<a href="http://filedl.li/download/', $semidl_content)[1])[0];

				if (strpos($dl_link, stringWithLines($an)) !== false && strpos($dl_link, stringWithLines($sn)) !== false
					&& (($_SESSION["noLives"] == true && strpos($dl_link, "live") === false && strpos($filteredSongInfo, "live") === false) || $_SESSION["noLives"] == false))
				{
					$downloadpurl = $dl_link;

					$downloadengine = 2;
					$mp3basefound = true;
				}

				$currIndex++;
			} while (!$mp3basefound);
		}

		if (!$mp3basefound)
		{
			/*echo "<script>document.getElementById('serviceid').innerHTML = 'MyMP3File';</script>";*/
			$serv = "MyMP3File";

			$currIndex = 1;

			do 
			{
				$mymp3base = "http://song.newmp3file.com";
				$searchurl = $mymp3base . "/search/";
				$searchurl .= stringWithLines($an) . "-" . stringWithLines($sn) . ".html";

				$mymp3filecontent = file_get_contents($searchurl);

				if (strpos($mymp3filecontent, "We are unable to get any result for this query. Please use another keyword to search or visit the above link for more results.") !== false)
					break;

				$exploded = explode('<div class="fl odd">', $mymp3filecontent);

				if ($currIndex >= count($exploded))
					break;

				// The current text inside the div class block </div>
				$currContent = $exploded[$currIndex];
				$dl_link = $mymp3base . explode('" ', explode('<a href="', $currContent)[1])[0];
				$title = explode('">', explode('title="', $currContent)[1])[0];
				$title = trim(strtolower($title));

				$normalizedAn = normalizeString($an);
				$normalizedSn = normalizeString($sn);

				$tempDL = file_get_contents($dl_link);

				$tryout = $mymp3base . explode('" rel=', explode('Download : <a class="dwnLink" href="', $c)[1])[0];

				if (startsWith($title, $normalizedAn) && endsWith($title, $normalizedSn) && !isIgnoredMyMP3($title)
					&& (($_SESSION["noLives"] == true && strpos($title, "live") === false && strpos($filteredSongInfo, "live") === false) || $_SESSION["noLives"] == false))
				{
					$tempDL = file_get_contents($dl_link);
					$tryout = $mymp3base . explode('" rel=', explode('Download : <a class="dwnLink" href="', $tempDL)[1])[0];

					if (isDomainAvailable($tryout) === "Yes")
					{
						$downloadpurl = $dl_link;

						$downloadengine = 3;
						$mymp3found = true;
					}
				}

				$currIndex++;
			} while (!$mymp3found);
		}

		if (empty($downloadpurl) || strlen($downloadpurl) < 5)
			$downloadpurl = "NULL";

		$goAhead = true;
	}

	require_once('./bin/lastphase.php');

?>