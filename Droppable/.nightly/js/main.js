/* 
 * The main Spotific class
 */
var Spotific = {
	downloadURL : undefined,

	/*
	 * Allows the Drag and Drop event to happen
	 */
	allowDD : function(ev) 
	{
		ev.preventDefault();
	},

	/*
	 * Handles the drop event
	 */
	handleDrop : function(ev) 
	{
		ev.preventDefault();

		this.downloadURL = ev.dataTransfer.getData("text");

		if (this.downloadURL.indexOf(" - ") > -1 || (this.downloadURL.indexOf("http://open.spotify.com/track/") > -1 && this.downloadURL !== "http://open.spotify.com/track/"))
		{
			$("#dragger").text("Hang in there! Starting Droppable Engine! :-)");
			$("#downloadURL").attr("value", this.downloadURL);
			$("#submit").click();
		}
		else
			console.log("Invalid dragged object.");
	}
}

/*
 * Checks for the #debug parameter in the URL.
 * If found, adds the debug class to every element
 */
function checkDebug() {
	if ($(location).attr('href').indexOf("#debug") > -1)
		$("*").addClass("debug");
}

/* Global Variables */
var settingsOpen = false;
var initialSettings = {};

var noLivesCheck = undefined;
var dbCheck = undefined;

/*
 * Closes the settings menu and displays an animation
 */
function CloseSettings() {
	$("#settings").css({"animation": "Diminish 0.65s ease-in-out"});

	window.setTimeout(function() {
		$("#settings").css({"visibility": "hidden", "display": "none"});
		$("#downloader").css({"visibility": "visible", "display": "flex"});
	}, 650);
}

/*
 * Opens the settings menu and displays an animation
 */
function OpenSettings() {
	$("#settings").css("zIndex", -1);

	$("#settings").css({"animation": "SlideToCenter 0.5s ease-in-out", "visibility": "visible", "display": "flex"});
	$("#downloader").css({"visibility": "hidden", "display": "none"});

	window.setTimeout(function() {
		$("#settings").css("zIndex", 0);
	}, 500);
}

$(window).load(function() {
	if ($.browser.mobile && window.location.href.indexOf("?mobile=true") < 0)
	{
		window.location = window.location.href + "?mobile=true";
		return;
	}

	noLivesCheck = document.getElementById("noLives");
	dbCheck = document.getElementById("noDB");

	$(".closeParent").click(function() {
		$(this).parent().css({"display": "none", "visibility": "hidden"});
	});

	$("#settingslink").click(function() {
		console.log("Settings menu opened or closed");

		if (!settingsOpen)
		{
			this.OpenSettings();

			this.initialSettings.filterLive = this.noLivesCheck.checked;
			this.initialSettings.dbConnection = this.dbCheck.checked;
		}
		else
		{
			this.CloseSettings();

			this.noLivesCheck.checked = this.initialSettings.filterLive;
			this.dbCheck.checked = this.initialSettings.dbConnection;
		}

		settingsOpen = !settingsOpen;
	}.bind(this));

	$("#btnCancel").click(function() {
		this.CloseSettings();

		this.noLivesCheck.checked = this.initialSettings.filterLive;
		this.dbCheck.checked = this.initialSettings.dbConnection;

		this.settingsOpen = false;
	}.bind(this));

	$("#submitBTN").click(function() {
		this.CloseSettings();

		this.settingsOpen = false;
	}.bind(this));
}.bind(this))