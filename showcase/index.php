<?php
	include "./management/ContentColor.php";
	include "./management/GetPages.php";
?>
<!DOCTYPE html>
<html lang="en" class="<?php echo GetContentColor(); ?>">
	<head>
		<title>HTML5 Showcase Website</title>
		<meta charset="UTF-8">
		<meta name="author" content="Jere Tapaninen">
		<meta name="description" content="HTML5 Showcase Website">
		<meta name="viewport" content="user-scalable=yes, maximum-scale=1.0, width=device-width" />
		<!-- CSS -->
		<link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans" />
		<link rel="stylesheet" type="text/css" href="./style/reset.css" />
		<link rel="stylesheet" type="text/css" href="./style/style.css" />
		<!-- FAVICON -->
		<link rel="icon" href="./content/logo.png">
		<!-- JAVASCRIPT -->
		<script type="text/javascript" src="./js/jquery-2.1.4.min.js"></script>
		<script type="text/javascript" src="./js/main.js"></script>
	</head>
	
	<body class="<?php echo GetContentColor(); ?>">
		<header class="<?php echo GetContentColor(); ?>">
			<div id="logoholder">
				<img src="./content/logo.png" alt="logo" id="logo" />
			</div>
			<h1 id="title" class="title">HTML5 Showcase</h1>
			<nav role="navigation">
				<ul>
					<?php
						$pages = GetPages();
						
						foreach($pages as &$page)
						{
							printf('<li class="navbutton" id="' . $page[1] . '">' . $page[0] . "\n\t\t\t\t\t");
						}
					?>
				</ul>
			</nav>
		</header>
		
		<div id="container" class="<?php echo GetContentColor(); ?>">
			<div role="main">
				<h1>Lorem ipsum</h1>
				<p>Lorem ipsum dolor sit amet, consectetur adipisci elit, 
					sed eiusmod tempor incidunt ut labore et dolore magna aliqua. 
					Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris 
					nisi ut aliquid ex ea commodi consequat. 
					Quis aute iure reprehenderit in voluptate velit esse cillum dolore 
					eu fugiat nulla pariatur. 
					Excepteur sint obcaecat cupiditat non proident, 
					sunt in culpa qui officia deserunt mollit anim id <a href="./index.php">est laborum</a>.
				<h2>Ipsum?</h2>
				<p>Lorem ipsum dolor sit amet, consectetur adipisci elit, 
					sed eiusmod tempor incidunt ut labore et dolore magna aliqua. 
					Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris 
					nisi ut aliquid ex ea commodi consequat. 
					Quis aute iure reprehenderit in voluptate velit esse cillum dolore 
					eu fugiat nulla pariatur. 
					Excepteur sint obcaecat cupiditat non proident, 
					sunt in culpa qui officia deserunt mollit anim id est laborum.
			</div>
		</div>
		
		<footer class="<?php echo GetContentColor(); ?>">
			<p id="copyright">&copy; Jere Tapaninen 2015
		</footer>
		
		<div id="changecolor" class="widget">
			<p class="bold">Change the color style!
			<nav id="colors">
				<ul>
					<li><div class="rectangle" id="white"></div>
					<li><div class="rectangle" id="black"></div>
					<li><div class="rectangle" id="red"></div>
					<li><div class="rectangle" id="green"></div>
					<li><div class="rectangle" id="yellow"></div>
					<li><div class="rectangle" id="blue"></div>
				</ul>
			</nav>
		</div>
	</body>
</html>