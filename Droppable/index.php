<?php
	// Set the max time limit for everything
	set_time_limit(120); // 2 minutes

	// Import all the shiz
	//require_once('./bin/functions.php');
	//require_once('./bin/checkprivileges.php');
	include_once('./bin/errorhandler.php');

	// Start the PHP session
	session_start();

	// Settings shiz
	include_once('./bin/settings.php');

	// Start the steamrolling! TRUUT TRUUT
	require_once('./bin/_new_/Main.php');

?>
<!DOCTYPE html>
<!--[if IE]>
<html>
	<head>
		<title>Your web browser is out-of-date!</title>
	</head>

	<body>
		<h1>Sorry!<h1>
		<h2>You can't use Droppable because your web browser is out-of-date!</h2>
	</body>
</html>
<![endif]-->
<![if !IE]>
<!-- That causes a validation error but in my defence fuck validators -->
<html>
	<head>
		<title>Droppable&trade; Remastered</title>
		<meta charset="UTF-8">
		<meta name="author" content="Jere Tapaninen">
		<meta name="description" content="Droppable Spotify Music Downloader">
		<meta name="keywords" content="Droppable, music, download, mp3">
		<!-- CSS -->
		<link href='http://fonts.googleapis.com/css?family=Signika:400,600,700,300&amp;subset=latin,latin-ext' rel='stylesheet' type='text/css'>
		<link rel="stylesheet" type="text/css" href="./style/reset.css" />
		<link rel="stylesheet" type="text/css" href="./style/customcheckbox.css" />
		<link rel="stylesheet" type="text/css" href="./style/main.css" />
		<!-- JS -->
		<script type="text/javascript" src="./js/jquery-2.1.4.min.js"></script>
		<script type="text/javascript" src="./js/detectmobilebrowser.js"></script>
		<script type="text/javascript" src="./js/main.js"></script>
		<!-- OTHER -->
		<link rel="icon" href="./favicon.ico" />
	</head>

	<body>
		<header>
			<ul>
				<li>
					<div>
						<div>
							<img alt="logo" id="logo" src="./content/headset.svg" />
							<h1>Droppable&trade; Remastered</h1>
						</div>
						<h6>Just drag and drop, and the song is yours!</h6>
					</div>
				</li>
				<li>
					<nav>
						<ul>
							<li>
								<a href="./info.txt" title="See information" target="_blank"><img alt="info" id="infoimg" src="./content/info.svg" /></a>
							</li>
							<li>
								<a href="./update.txt" title="See update logs" target="_blank"><img alt="updates" id="updatesimg" src="./content/update.svg" /></a>
							</li>
							<li style="display: none; visibility: hidden">
								<a style="cursor: pointer" id="settingslink" title="Manage settings"><img alt="settings" class="settingsimg" src="./content/settings.svg" /></a>
							</li>
							<li style="display: none; visibility: hidden">
								<a href="./downloads/Droppable.exe" title="Download Droppable&trade; Desktop Version for Windows!" target="_blank"><img alt="download" id="downloadimg" src="./content/download.svg" /></a>
							</li>
						</ul>
					</nav>
				</li>
			</ul>
			<div class="infopanel" style="display: none; visibility: hidden">
				<div class="innerholder">
					<img src="./content/info.svg" />
					<p>Droppable 0.160 Public Alpha Release is getting close!</p>			
				</div>
				<img class="closeParent" src="./content/close.svg" />
			</div>
		</header>

		<div id="settings" style="display: none; visibility: hidden">
			<form id="settingsform" action="./" method="POST">
				<article>
					<header>
						<img alt="settings logo" class="settingsimg" src="./content/settings.svg" />
						<h2>Settings</h2>
					</header>

					<section>
						<h3>General</h3>
						<input type="checkbox" id="noLives" <?php if (areSessionVarsSet()) { if ($_SESSION["noLives"]) echo 'checked="checked"'; } else echo 'checked="checked"'; ?> name="noLives" />
   						<label for="noLives">Filter live songs</label>
   						<input type="checkbox" id="noDB" name="noDB" <?php if (areSessionVarsSet()) { if ($_SESSION["noDB"]) echo 'checked="checked"'; } ?> />
   						<label for="noDB">Disable database connection (does nothing at the moment)</label>
					</section>

					<footer>
						<div>
							<input type="submit" id="submitBTN" name="submitBTN" title="Save the settings" value="Save settings" />
							<input type="button" title="Cancel" value="Cancel" id="btnCancel" />
						</div>
					</footer>
				</article>
			</form>
		</div>

		<?php
			print($downloadinfostr);
		?>

		<footer>
			<div>
				<ul>
					<li>
						<p>&copy; Droppable 2015</p>
					</li>
					<li>
						<p>Droppable uses Third-Party Services</p>
					</li>
					<li>
						<p>Downloading music <i title="Only download songs that are PUBLIC DOMAIN." style="cursor: pointer">illegally</i> is a serious crime. Do not use this service for that.</p>
					</li>
				</ul>
				<p id="smalltext">Droppable&trade; for Web - Version 0.20 (Stable Alpha)</p>
			</div>
		</footer>

		<div id="hiddenstuff" style="display: none; visibility: hidden">
			<form action="./" method="POST">
				<input id="downloadURL" name="downloadURL" type="text" value="" />
				<input id="submit" type="submit" />
			</form>

			<form action="./" method="POST">
				<input id="spotifycontent" name="songinfo" type="text" value="<?php if ($set) echo $content ?>" />
				<input id="downloadsong" type="submit" />
			</form>

			<form action="./" method="POST">
				<input id="downloadpageurl" name="downloadpageurl" type="text" value="<?php if ($toDownload && $goAhead) echo $downloadURL; ?>" />
				<input name="songinfo" type="text" value="<?php if ($toDownload && $goAhead) echo $songInfo; ?>" />
				<input id="todownloadpage" type="submit" />
			</form>

			<a id="finalDL" <?php if ($letsDownload) echo 'download="' . $_POST["songinfo"] . '"' ?> href="<?php if ($letsDownload) echo $fDownloadURL; ?>"></a> 
		</div>

		<!-- Main JS part -->
		<script type="text/javascript">
			// Check whether we want to enter debug mode
			checkDebug();

			if ($("#spotifycontent").attr("value").indexOf(" - ") > -1)
				$("#downloadsong").click();

			if ($("#downloadpageurl").attr('value').indexOf(".mp3") > -1)
				$("#todownloadpage").click();

			$(document).ready(function() {
				if ($("#finalDL").attr('href').length > 5)
				{
					$("#finalDL")[0].click();
					return;
				}

				if ($("#corrupted").length)
				{
					window.setTimeout(function() {
						location.href = "./";
					}, 5000);
				}
			});

			if ($("#done").length)
			{
				window.setTimeout(function() {
					location.href = "./";
				}, 7500);
			} 
			else if ($("#parsing").length)
			{
				window.setTimeout(function() {
					$("#parsing").text("Something went wrong. :-/ Redirecting...");
					setTimeout(function() {
						location.href = "./";
					}, 3000);
				}, 60000);
			}
			else if ($("#spotifyparsing").length)
			{
				window.setTimeout(function() {
					$("#slowservice").text("Droppable Service is a little bit slow, hang in there! :-)");
				}, 7500);
			}
		</script>
	</body>
</html>
<!-- That causes a validation error but in my defence fuck validators -->
<![endif]>