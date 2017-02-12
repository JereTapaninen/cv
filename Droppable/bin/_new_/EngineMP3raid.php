<?php

	class EngineMP3raid extends BaseEngine
	{
		protected $name = "MP3raid";
		protected $baseURL = "http://www.mp3raid.ca/";
		protected $searchURL = "download/";
		protected $baseIndex = 1;

		public function Search($songInfo, $an, $sn)
		{
			$realArtistName = str_replace("+", "_", $an);
			$realSongName = str_replace("+", "_", $sn);
			$realSongInfo = str_replace("+", "_", $songInfo);

			$pageContent = file_get_contents($this->GetBaseURL() . $this->searchURL . $realSongInfo . ".html");

			$foundSongs = explode('<div class=\'index1\'>', $pageContent);

			$loopIndex = $this->baseIndex;

			while (true)
			{
				if ($loopIndex >= count($foundSongs))
					break;

				$currentSongId = explode('\'>', explode('<a href=\'javascript:;\' class=\'dl\' id=\'', $foundSongs[$loopIndex])[1])[0];
				$currentSongId = str_replace("dl", "", $currentSongId);

				$currentSongPageContent = file_get_contents('http://www.mp3raid.ca/search/ddl/' . $currentSongId . '/' . $realArtistName . '.html');

				$downloadURL = explode('</td></tr>', explode('<b>Location:</b></td><td style=\'word-wrap:break-word;\'>', $currentSongPageContent)[1])[0];

				if (true)
				{
					return $downloadURL;
				}

				$loopIndex++;
			}

			return null;
		}

		public function ResolveDownloadURL($url)
		{
			return $url;
		}
	}

?>