<?php

	class EngineBeeMP3s extends BaseEngine
	{
		protected $name = "BeeMP3s";
		protected $baseURL = "http://beemp3s.org/";
		protected $searchURL = "search?query=";
		protected $baseIndex = 1;

		public function Search($songInfo, $an, $sn)
		{
			$pageContent = file_get_contents($this->GetBaseURL() . $this->searchURL . $songInfo);

			$foundSongs = explode('<span>Download song:</span>', $pageContent);

			$loopIndex = $this->baseIndex;

			while (true)
			{
				if ($loopIndex >= count($foundSongs))
					break;

				$currentSongURL = explode('">', explode('<a href="', $foundSongs[$loopIndex])[1])[0];

				$currentSongPageContent = file_get_contents($currentSongURL);

				$downloadURL = explode('" target=', explode('<a class="big-green-btn mgLockedElement" id="download-button" href="', $currentSongPageContent)[1])[0];

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