<!DOCTYPE html>
<?php include_once "database.php" ?>
<?php
	function fallback()
	{
		header('Location: index.php');
	}

	if (isset($_POST["submit"], $_POST["esitys"]) && !empty($_POST["submit"]) && !empty($_POST["esitys"]))
	{
		//echo $_POST["esitys"];
		$esitysID = htmlentities($_POST["esitys"]);
	}
	else
		fallback();
?>
<html lang="en" xml:lang="en">
	<head>
		<title>Sirikus sirkus - Tilaa liput esitykseen</title>
		<meta charset="UTF-8">
		<meta name="author" content="Jere Tapaninen">
		<meta name="description" content="Sirikus sirkus">
		<meta name="keywords" content="sirkus, sirikus">
		<!-- Stylesheets -->
		<link rel="stylesheet" type="text/css" href="./style/reset.css" />
		<link rel="stylesheet" type="text/css" href="./style/tilaa.css" />
		<!-- JavaScript -->
		<script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
	</head>

	<body>
		<header>
			<div>
				<div>
					<img src="./content/circus.png" id="logoimage" alt="logo" />
					<h1 id="logotext">Sirikus sirkus</h1>
				</div>
			</div>
			<div>
				<h4 id="subtitle">Tilauslomake</h4>
			</div>
		</header>

		<main>
			<?php
				$esitys = getEsitysWithID($esitykset, $esitysID); 

				if (isset($_POST["vapaatpaikat"]))
				{
					if ($esitys->getVapaatpaikat() < $_POST["liput"])
					{
						echo '<p style="font-weight: bold; color: red; margin-bottom: 1vh">Tilaustasi ei vahvistettu: liian monta lippua!</p>';
						include_once "tilausform.php";
					}
					else if ($_POST["liput"] <= 0)
					{
						echo '<p style="font-weight: bold; color: red; margin-bottom: 1vh">Tilaustasi ei vahvistettu: liian vähän lippuja!</p>';
						include_once "tilausform.php";
					}
					else
					{
						pushTilaus(htmlentities($_POST["email"]), htmlentities($_POST["puhnro"]), htmlentities($_POST["liput"]), $esitys->getVapaatpaikat(), $esitysID);

						echo '<p style="font-weight: bold; color: forestgreen; margin-bottom: 1vh">Tilauksesi vastaanotettiin onnistuneesti!</p>';
						echo '<form action="./index.php" method="GET"><input type="submit" value="Palaa etusivulle" /></form>';
					}
				}
				else
					include_once "tilausform.php";
			?>
		</main>

		<footer>
			<div id="copyright">
				<p>&copy; Jere Tapaninen <?php echo Date("Y"); ?></p>
			</div>
		</footer>
	</body>
</html>