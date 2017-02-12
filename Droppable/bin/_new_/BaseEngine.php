<?php

	abstract class BaseEngine 
	{
		abstract public function Search($songInfo, $an, $sn);
		abstract public function ResolveDownloadURL($url);

		public function GetEngineName()
		{
			return $this->name;
		}

		public function GetBaseURL()
		{
			return $this->baseURL;
		}
	}

?>