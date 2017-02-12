var noun = ["paper", "tray", "user", "button", "ink", "toner", "rack", "spooler", "printer", "feeder"];
var fault = ["jam", "error", "fault", "overload", "spill", "failure", "spill", "low", "leak", "malfunction"];
var action = ["remove", "reset", "recalibrate", "push", "adjust", "reload", "dislodge", "insert", "retrieve", "feed"];

$(window).load(function() {
	if ($(location).attr('href').indexOf("?id=") > -1)
	{
		var id = $(location).attr('href').split("?id=")[1];

		if (id.length < 8 || !(id.indexOf("n") > -1 && id.indexOf("f") > -1 && id.indexOf("a") > -1))
		{
			GenerateRandomError();
		}
		else
		{
			var nounId = id.split("n")[1].split("f")[0];
			var faultId = id.split("f")[1].split("a")[0];
			var actionId = id.split("a")[1].split("n")[0];
			var lastNounId = id.split("n")[2];

			var error = noun[nounId] + " " + fault[faultId];
			var please = action[actionId] + " " + noun[lastNounId];

			$("#error").text(error);
			$("#please").text(please);
		}
	}
	else
	{
		GenerateRandomError();
	}
});

function GenerateRandomError() {
	var randNounFirst = GetRandomNumber(0, noun.length - 1);
	var randNounSecond = GetRandomNumber(0, noun.length - 1);
	var randFault = GetRandomNumber(0, fault.length - 1);
	var randAction = GetRandomNumber(0, action.length - 1);


	var error = noun[randNounFirst] + " " + fault[randFault];
	var please = action[randAction] + " " + noun[randNounSecond];

	$("#error").text(error);
	$("#please").text(please);

	$("#sharelink").attr('href', $(location).attr('href') + "?id=n" + randNounFirst + "f" + randFault + "a" + randAction + "n" + randNounSecond);
}

function GetRandomNumber(min, max) {
	return Math.floor(Math.random() * (max - min + 1)) + min;
}