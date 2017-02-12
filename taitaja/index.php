<!DOCTYPE html>
<!--
	PLEASE NOTE: this website has been made for the Taitaja web development-competition semifinals
	Since then, chrome has changed and this website is no longer fully functional.
	Also, the database doesn't exist anymore.


	Main HTML document of my Taitaja 2016 website.
	(C) Jere Tapaninen 2015
	All Rights Reserved.
-->
<?php include_once "database.php" ?>
<html lang="en" xml:lang="en">
	<head>
		<title>Sirikus sirkus</title>
		<!-- That's meta as **** -->
		<meta charset="UTF-8">
		<meta name="author" content="Jere Tapaninen">
		<meta name="description" content="Sirikus sirkus">
		<meta name="keywords" content="sirkus, sirikus">
		<!-- Stylesheets -->
		<link rel="stylesheet" type="text/css" href="./style/reset.css" />
		<link rel="stylesheet" type="text/css" href="./style/main.css" />
		<!-- JavaScript -->
		<script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
		<script type="text/javascript" src="js/main.js"></script>
	</head>

	<body>
		<div id="gallerycanvas" style="opacity: 0; display: flex; flex-direction: column; justify-content: center; align-items: center; visibility: hidden; background-color: rgba(0, 0, 0, 0.5); position: fixed; top: 0; left: 0; width: 100%; height: 100%; text-align: center; vertical-align: middle">
			<img src="" alt="" id="galleryimg" style="position: relative; height: 63vh" />
		</div>

		<header>
			<div>
				<div>
					<img alt="sirkus" id="logo" src="./content/circus.png" />
					<h1 id="title">Sirikus</h1>
					<h6 id="subtitle">Iloa koko perheelle!</h6>
				</div>

				<nav id="navigation">
					<ul>
						<li data-link="introduction">Sirikus sirkus</li>
						<li data-link="operation">Toiminta</li>
						<li data-link="contacts">Yhteystiedot</li>
						<li data-link="ticketbooking">Lipunvaraus</li>
						<li data-link="gallery">Kuvagalleria</li>
					</ul>
				</nav>
			</div>
		</header>

		<main>
			<article id="introduction">
				<h2>Sirikus sirkus</h2>
				<div>
					<p>
						Tervetuloa Sirikus sirkuksen kotisivuille!<br>
						Me täällä Sirikus sirkuksessa nautimme huvista, ja tiedämme, että myös sinä nautit.
						Siksi me tarjoammekin esityksiä suoraan kotiisi huokeilla hinnoilla!<br>
						Katso nyt heti meidän hinnastomme, ja rakastu huiman halpoihin hintoihimme!
					</p>
				</div>
			</article>

			<article id="operation">
				<h2>Toiminta</h2>
				<div>
					<p>
						Sirikus on pieni sirkus, joka voi järjestää esityksensä missä tahansa. 
						Esitys voi olla isossa teatterissa tai vaikka omassa kodissasi. 
						Katsojamäärät vaihtelevat muutamasta ihmisestä jopa satoihin. 
						Esitysten teema voi vaihdella asiakkaan toiveiden mukaan. 
						Teemat vaihtelevat rauhallisesta ja eteerisestä jooga teemasta vaikka räjähtävään avaruuden valloitukseen.
					</p>
					<p>
						Sirkus Sirikuksessa työskentelee 5 innokasta ja taitavaa henkilöä. 
						He vastaavat kaikesta esitysten vaatimista järjestelyistä. 
						Sirkuksella on käytössään oma pakettiauto jonka kyydissä kulkee kaikki esityksessä tarvittava rekvisiitta.
					</p>
				</div>
			</article>

			<article id="contacts">
				<h2>Yhteystiedot</h2>
				<div>
					<p>
						Sirkuskoulu Sirkus sijaitsee osoitteessa <b>Kivenlahdentie 7</b>, meidän postinumeromme on <b>02320</b> ja toimipaikka <b>Espoo</b>.<br>
						Maailmanlaajuinen puhelinnumeromme on <b>+358 50 567123</b>.<br>
						Sähköpostiosoitteemme on <b><a id="mail" href="mailto:sirikus@sirikus.fi">sirikus@sirikus.fi</a></b>.<br>
					</p>
					<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1985.7240853552498!2d24.658631916105737!3d60.152191081954264!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x468df4b1b436aa45%3A0x82d74da59421beb8!2sKivenlahdentie+7%2C+02320+Espoo!5e0!3m2!1sfi!2sfi!4v1453232479707" width="600" height="450" frameborder="0" style="border:0" allowfullscreen>
					</iframe>
				</div>
			</article>

			<article id="ticketbooking">
				<h2>Lipunvaraus</h2>
				<div>
					<table>
						<tr>
							<th>Teema</th>
							<th>Esityspaikka</th>
							<th>Kaupunki</th>
							<th>Päivämäärä</th>
							<th>Paikat</th>
							<th>Vapaat paikat</th>
						</tr>
						<?php
							foreach ($esitykset as &$esitys)
							{
								print "<tr>" .
									  "<td style=\"visibility: hidden; display: none\" class=\"esitysID\">" . $esitys->getID() . "</td>" .
									  "<td>" . $esitys->getTeema() . "</td>" .
									  "<td>" . $esitys->getEsityspaikka() . "</td>" .
									  "<td>" . $esitys->getKaupunki() . "</td>" .
									  "<td>" . $esitys->getPvm() . "</td>" .
									  "<td>" . $esitys->getPaikat() . "</td>" .
									  "<td>" . $esitys->getVapaatpaikat() . "</td>" .
									  "</tr>";
							}
						?>
					</table>
					<form style="visibility: hidden; display: none" method="POST" action="tilaa.php">
						<input type="text" id="esitys" name="esitys" value="-1" />
						<input type="submit" id="submit" name="submit" value="confirm" />
					</form>
				</div>
			</article>

			<article id="gallery">
				<h2>Kuvagalleria</h2>
				<div>
					<ul id="imagegallery">
						<?php
							$files = scandir("./content/gallery/");

							foreach ($files as $file)
							{
								if (strpos($file, ".jpg"))
									echo '<li><img class="galleryimage" src="./content/gallery/' . $file . '" alt="' . $file . '" /></li>';
							}
						?>
					</ul>
				</div>
			</article>
		</main>

		<footer>
			<div><p>&copy; Jere Tapaninen <?php echo date("Y"); ?></p></div>
		</footer>
	</body>
</html>