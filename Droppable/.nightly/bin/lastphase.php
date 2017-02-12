<?php

	$letsDownload = false;
	$downloadCorrupted = false;
	// The final download URL
	$fdownloadurl = "";

	if ((isset($_POST["downloadpageurl"]) && !empty($_POST["downloadpageurl"])))
	{
		$toDownload = false;

		if (!$beemp3found && !$mp3raidfound && !$mymp3found && !$mp3basefound)
		{
			$downloadCorrupted = true;
		}
		else
		{
			$filtered = htmlentities($_POST["downloadpageurl"]);

			$filtered = str_replace("&amp;", "&", $filtered);

			$c = file_get_contents($filtered);

			if ($downloadengine == 0)
			{	// BEEMP3
				$exploded = explode('"  target="_blank" rel="nofollow" class="a-proceed-big lol">', $c);

				$subc = $exploded[0];
				$subc = explode('target="_blank" rel="nofollow">download</a><a href="', $subc)[1];

				$fdownloadurl = $subc;
			}
			else if ($downloadengine == 1)
			{	// MP3RAID
				$innershit = explode("<table style='width:100%;table-layout:fixed;'>", $c)[1];
				$fdownloadurl = explode('</td>', explode("<td style='word-wrap:break-word;'>", $innershit)[1])[0];
			}
			else if ($downloadengine == 2)
			{	// MP3Base
				$filedlbase = "http://get.filedl.li/";
				$fdownloadurl = $filedlbase . explode('" target="', explode('<a href="http://get.filedl.li/', $c)[1])[0];
			}
			else if ($downloadengine == 4)
			{	// mymp3file
				$mymp3base = "http://song.mymp3file.com";
				$fdownloadurl = $mymp3base . explode('" rel=', explode('Download : <a class="dwnLink" href="', $c)[1])[0];
			}

			if (isDomainAvailable($fdownloadurl) === "Yes")
			{
				if (strpos($fdownloadurl, ".mp3") !== false && (checkMp3($fdownloadurl) || $mymp3found))
					$letsDownload = true;
				else
					$downloadCorrupted = true;
			}
			else
				$downloadCorrupted = true;
		}
	}

?>