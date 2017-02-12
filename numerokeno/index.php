<!DOCTYPE html>
<html>
	<head>
		<title>Numeroita_kenoita</title>
		<meta charset="UTF-8">
		<!-- CSS -->
		<link rel="stylesheet" type="text/css" href="./style/reset.css" />
		<link rel="stylesheet" type="text/css" href="./style/main.css" />
		<!-- JavaScript -->
		<script type="text/javascript" src="./js/jquery-3.1.0.min.js"></script>
		<script>
			window.infoboardMsgs = ["Tervetuloa, hölmö!", "Pelaa laittamalla omaisuutesi tähän peliin!"];
			window.infoboardPlayingMsgs = ["Arvotaan kortteja !!!11 XD"];
			window.infoboardPlaying = false;
			window.importantMessage = "";

			window.setInfoboardPlaying = function() {
				window.infoboardPlaying = !window.infoboardPlaying;
			};

			window.setImportantMessage = function(msg, dur) {
				window.importantMessage = msg;

				setTimeout(function() { window.importantMessage = ""; }, dur);
			};

			$(function() {
				const infoboardUpdater = function(n) {
					let cur = 0;

					if (window.infoboardPlaying)
					{
						if (n >= window.infoboardPlayingMsgs.length)
							n = 0;

						if (importantMessage == "")
							$("#infoboard").text(window.infoboardPlayingMsgs[n]);
						else
							$("#infoboard").text(importantMessage);

						cur = (n + 1 < window.infoboardPlayingMsgs.length) ? n + 1 : 0;
					}
					else
					{
						if (n >= window.infoboardMsgs.length)
							n = 0;

						if (importantMessage == "")
							$("#infoboard").text(window.infoboardMsgs[n]);
						else
							$("#infoboard").text(importantMessage);

						cur = (n + 1 < window.infoboardMsgs.length) ? n + 1 : 0;
					}

					setTimeout(infoboardUpdater, 2000, cur);
				};

				infoboardUpdater(0);
			});
		</script>
		<script type="text/javascript" src="./js/main.js"></script>
	</head>

	<body>
		<div id="sidebar">
			<div id="logo">
				<img src="./content/logo.png" alt="logo" />
			</div>
			<div id="stats">
				<div class="row statbox">
					<p class="statboxtitle">Saldo:</p><p id="_saldo">0</p><p>€</p>
				</div>
			</div>
			<div id="other">
				<div class="inputbox">
					<label for="rahaa">Lisää rahaa:</label>
					<input type="number" min="0.20" pattern="^\d*[02468]$" step="0.20" value="0.20" id="rahaa" />
					<input type="button" value="Lisää" id="addMoneyBtn" />
				</div>
				<div class="inputbox">
					<label for="stake">Panos:</label>
					<input type="number" min="0.20" step="0.20" value="0.20" id="stake" />
				</div>
			</div>
			<div id="goals">
				
			</div>
		</div>

		<div class="content">
			<main>
				<?php
					$inc = 1;

					for ($i = 0; $i < 5; $i++)
					{
						echo "<div>";
						for ($i2 = 0; $i2 < 10; $i2++)
						{
							echo "<div id=\"card$inc\" class=\"card\">$inc</div>";
							$inc++;
						}
						echo "</div>";
					}
				?>
			</main>

			<footer>
				<ul>
					<li>
						<button id="playBtn">
						</button>
					</li>
					<li>
						<button id="tuplaa">
						</button>
					</li>
					<li>
						<div id="infoboard">
						</div>
					</li>
				</ul>
			</footer>
		</div>
	</body>
</html>