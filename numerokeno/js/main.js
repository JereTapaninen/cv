function hasClass(element, cls) {
    return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
}

function money_round(num) {
    return Math.round(num * 10) / 10;
}

function clamp(num, min, max) {
	return Math.max(min, Math.min(num, max));
}

function toOne(num) {
	console.log(typeof num, num);
	return 1 * Math.round(num / 1);
}

function toTwo(num) {
	console.log(typeof num, num);
	return 0.2 * Math.round(num / 0.2);
}

function rand(min, max) {
	return Math.floor((Math.random() * max) + min);
}

function err(user, dev) {
	window.setImportantMessage(user, 3000);

	return console.error(dev);
}

$(function() {

	function Game()
	{
		this.stake = 0.20;
		this.maxSelectedCards = 12; // NEVER EDIT THESE DIRECTLY
		this.minSelectedCards = 4; // NEVER EDIT THESE DIRECTLY
		this.selectedCards = 0;
		this.saldo = 0;

		this.winningsTable = [];

		this.nMin = 1;
		this.nMax = 7;
		this.nMod = 1; // EDIT THIS IF YOU WANT TO MOD MAX SEL CARDS AND MIN SEL CARDS
		this.nValMod = 0.5;

		this.play = function()
		{
			if (this.saldo <= 0)
				return err("Sulla ei oo rahaa!", "Saldo is 0!");

			if (this.stake > this.saldo)
				return err("Sun panos on liian suuri!", "Stake is higher than the saldo!");

			if (this.selectedCards < this.minSelectedCards)
				return err("Sulla ei oo " + this.minSelectedCards + " vaadittavaa korttia valittuna!", "You don't have at least " + this.minSelectedCards + " selected cards!");

			console.log("congrats, no problems");

			window.setInfoboardPlaying();

			this.removeSaldo(this.stake);

			$(".card").removeClass("generated");

			const foundArr = this.winningsTable.find((val) => { return val[0] == this.selectedCards; });

			const cardsToSelect = foundArr[0];

			let generatedCards = [];

			for (let i = 0; i < cardsToSelect; i++)
			{
				generatedCards.push(rand(1, 50));
			}

			let cardsRight = 0;

			for (let i = 0; i < generatedCards.length; i++)
			{
				if (hasClass($("#card" + generatedCards[i])[0], "active"))
				{
					cardsRight++;
				}
			}

			const foundArr2Reversed = foundArr[2].reverse();

			window.foundMatch = false;
			window.matchMoney = 0;

			for (let i = 0; i < foundArr[0] - foundArr[1]; i++)
			{
				if (foundArr[0] - i == cardsRight)
				{
					window.foundMatch = true;
					window.matchMoney = foundArr2Reversed[i];

					break;
				}
			}

			const cbFunc = function() {
				if (window.foundMatch)
				{
					window.setImportantMessage("Voitit " + window.matchMoney + " €! Onneksi vitun olkoon, typerys!", 5000);
					this.addSaldo(window.matchMoney);
				}
				else
				{
					window.setImportantMessage("HAHAHA et voittanu mitää! eks dee", 5000);
				}

				console.log("played");

				window.setInfoboardPlaying();

				const clearBoard = function() {
					$(".generated").removeClass("generated");
				};

				setTimeout(clearBoard, 2000);
			};

			window.genCardFunc = function(cardNum) {
				$("#card" + cardNum).addClass("generated");
			};

			window.genCardsFunc = function(generatedCardsArray, next, cb) {
				window.genCardFunc(generatedCardsArray[next]);

				if (next + 1 < generatedCardsArray.length)
					setTimeout(window.genCardsFunc, 500, generatedCardsArray, next + 1, cb);
				else
					cb();
			};

			setTimeout(window.genCardsFunc, 500, generatedCards, 0, cbFunc);
		}

		this.addSaldo = function(cash)
		{
			if (cash >= 0.20)
				this.saldo = money_round(this.saldo + Number(cash));

			$("#_saldo").text(this.saldo);
		}

		this.removeSaldo = function(cash)
		{
			this.saldo = money_round(this.saldo - cash);

			$("#_saldo").text(this.saldo);
		}

		this.setStake = function(stake)
		{
			if (stake >= 0.20 && money_round(Number(stake)) <= this.saldo)
				this.stake = money_round(Number(stake));

			this.buildGoals();

			$("#stake").val(money_round(Number(this.stake)));
		}

		this.buildGoals = function() 
		{
			let nArray1 = [];

			for (let i = this.minSelectedCards * this.nMod; i <= this.maxSelectedCards * this.nMod; i++)
			{
				let nArray2 = [];
				let nArray3 = [];

				nArray2.push(i);
				const count1 = toOne(clamp(((i - this.minSelectedCards) + this.nValMod), this.nMin, this.maxSelectedCards - this.nMax));
				nArray2.push(count1);
				const count2 = i - count1;

				for (let j = 0; j < count2; j++)
				{
					nArray3.push(money_round(toTwo(clamp(((j + 1) * 0.2), (0.2 + money_round((count2 - (j)) % (count2 % 0.4))), 1.2)) * (this.stake / 0.2)));
				}
				console.log("stake " + (this.stake / 0.2));

				nArray2.push(nArray3);

				nArray1.push(nArray2);
			}

			console.log(nArray1);
			this.winningsTable = nArray1;

			$("#goals").html("");

			const foundArr = this.winningsTable.find((val) => { return val[0] == this.selectedCards; });
			console.log(foundArr);

			if (typeof foundArr != "undefined")
			{
				const foundArr2Reversed = foundArr[2].reverse();

				for (let i = 0; i < foundArr[0] - foundArr[1]; i++)
				{
					$("#goals").html($("#goals").html() + "<div class=\"row statbox\"><p class=\"statboxtitle\">" + (foundArr[0] - i) + "</p><p>-</p><p>" + foundArr2Reversed[i] + "</p></div>");
				}
			}
			/*for (let i = 0; i < this.winningsTable[this.selectedCards][2].length; i++)
			{

			}*/
			/*
			for (let i = 0; i < this.winningsTable[this.selectedCards, 2].length; i++)
			{
				$("#goals").html($("#goals").html() + "<div class=\"row statbox\"><p class=\"statboxtitle\">" + i - this.winningsTable[this.selectedCards, 1] + "</p></div>");
			}*/
		}

		this.cardChanged = function()
		{
			this.buildGoals();
		}

		this.initialize = function()
		{
			const self = this;

			$(".card").click(function() {
				if (hasClass(this, "active"))
				{
					$(this).removeClass("active");
					self.selectedCards--;
					self.cardChanged();
				}
				else
				{
					if (self.selectedCards < self.maxSelectedCards)
					{
						$(this).addClass("active");
						self.selectedCards++;
						self.cardChanged();
					}
				}
			});

			$("#playBtn").click(function() {
				self.play();
			});

			$("#addMoneyBtn").click(function() {
				self.addSaldo($("#rahaa").val());
			});

			$("#rahaa").change(function() {
				if (money_round(Number($(this).val())) % 0.2 != 0)
					$("#rahaa").val(money_round(Number(0.2 * Math.round(money_round(Number($(this).val())) / 0.2))));
			});

			$("#stake").change(function() {
				if (money_round(Number($(this).val())) % 0.2 != 0)
					$("#stake").val(money_round(Number(0.2 * Math.round(money_round(Number($(this).val())) / 0.2))));

				self.setStake($(this).val());
			});
		}

		this.buildGoals();
	}

	window.g = new Game();
	window.g.initialize();

	if (window.location.href.includes("#debug"))
		$("*").addClass("debug");
});