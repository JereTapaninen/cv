const gui = require('nw.gui');
const win = gui.Window.get();

/**
 * Generates a random number from min to max
 * Remember: this function will NEVER return max in itself,
 * it can only return anything from min to max - 1.
 * This is due to how arrays work in JavaScript.
 **/
function rand(min, max) {
	return Math.floor((Math.random() * max) + min);
}

function toRadians(deg) {
	return deg * (Math.PI / 180);
}

function toDegrees(rad) {
	return rad * (180/ Math.PI);
}

class ImageLoader {
	constructor() {
		throw "ImageLoader is a static class, therefore it should not be constructed.";
	}

	static load(location) {
		return new Promise((resolve, reject) => {
			try {
				const img = new Image;
				img.onload = function(ev) {
					$("#game")[0].getContext("2d").drawImage(ev.path[0], -100000, -100000, 0, 0); // draw to the buffe--- BONEZONE
					resolve(img);
				}.bind(this);
				img.src = location;
			} catch (exception) {
				reject(Error("Image couldn't be loaded!"));
			}
		});
	}
}

class Spritebank {
	constructor() {
		this.spriteLocations = {
			meteorite: "../content/meteorite.png",
			hulldamage: "../content/hulldamage.png",
			explosion_spritesheet: "../content/explosion_spritesheet.png",
			ship: "../content/ship.png",
			logo: "../content/logo.png",
			flame_spritesheet: "../content/flame_spritesheet.png"
		};
		this.sprites = {
			"": ""
		};
	}

	loadSprite(key, val) {
		return new Promise((resolve, reject) => {
			ImageLoader.load(val).then(function(result) {
				resolve([key, result]);
			}.bind(this)).catch(function(err) {
				reject(err);
			}.bind(this));
		});
	}

	load() {
		return new Promise((resolve, reject) => {
			const promises = [];

			$.each(this.spriteLocations, function(key, val) {
				const deferred = $.Deferred();
				promises.push(deferred.promise());

				this.loadSprite(key, val).then(function(result) {
					this.sprites[result[0]] = result[1];
					deferred.resolve();
				}.bind(this)).catch(function(err) {
					deferred.reject();
				}.bind(this));
			}.bind(this));

			$.when.apply($, promises).then(function() {
				resolve(true);
			});
		});
	}
}

/**
 * Works both as a 2 dimensional point, as well as
 * a 2 dimensional vector (hence magnitude & length)
 **/
class Point {
	constructor() {
		this.x = arguments[0] || 0;
		this.y = arguments[1] || 0;
	}

	magnitude() {
		return new Point(this.x / this.length(), this.y / this.length());
	}

	/**
	 * This is here for compatibility reasons. Using magnitude() is preferred.
	 **/
	normalize() {
		return this.magnitude();
	}

	length() {
		return Math.sqrt(Math.pow(this.x, 2) + Math.pow(this.y, 2));
	}

	rotateDegrees(deg, origin) {
		const angleRads = toRadians(deg);

		this.x = Math.cos(angleRads) * (this.x - origin.x) - Math.sin(angleRads)
					* (this.y - origin.y) + origin.x;
		this.y = Math.sin(angleRads) * (this.x - origin.x) + Math.cos(angleRads)
					* (this.y - origin.y) + origin.y;
	}

	rotate(angleRads, origin) {
		this.x = Math.cos(angleRads) * (this.x - origin.x) - Math.sin(angleRads)
					* (this.y - origin.y) + origin.x;
		this.y = Math.sin(angleRads) * (this.x - origin.x) + Math.cos(angleRads)
					* (this.y - origin.y) + origin.y;
	}

	static subtract(v1, v2) {
		return new Point(v2.x - v1.x, v2.y - v1.y);
	}

	static cross(v1, v2) {
		return v1.x * v2.y - v2.x * v1.y;
	}

    static dot(v1, v2){
        return v1.x * v2.x + v1.y * v2.y;
    }

    static zero() {
    	return new Point(0, 0);
    }

	toString() {
		return this.x + ";" + this.y;
	}
}

/**
 * Alias for point. For compatibility reasons.
 **/
class Size extends Point {
	constructor() {
		super(arguments[0] || 0, arguments[1] || 0);
	}

	get width() {
		return this.x;
	}

	set width(val) {
		this.x = val;
	}

	get height() {
		return this.y;
	}

	set height(val) {
		this.y = val;
	}
}

class Entity {
	constructor(textureName) {
		this.texture = window.bank.sprites[textureName];

		this.position = arguments[1] || Point.zero();
		this.lookVector = new Point(0, 0);
		this.originalLookVector = new Point(0, 0);
		this.size = arguments[2] || Point.zero();

		this.maxHealth = 10;
		this.health = this.maxHealth;

		this.pleaseKillMe = false;
		this.pleaseDestroyMe = false;

		this.rotation = 0;

		this.targetLock = undefined;

		//this.defaultFacing = new Point(0, 1);
		//this.lastLookVector = this.defaultFacing;

		/**
		 * The player has custom setters and getters for movementSpeed
		 **/
		if (this instanceof Player)
			this._movementSpeed = Point.zero();
		else
			this.movementSpeed = Point.zero();
	}

	get healthMaxHealthRatio() {
		return this.health / this.maxHealth;
	}

	get isHit() {
		return this.health < this.maxHealth;
	}

	disableLockOn() {
		this.targetLock = undefined;

		this.lookVector = this.originalLookVector;

		if (this instanceof Player)
			this._movementSpeed.y = 1;
		else
			this.movementSpeed.y = 1;
	}

	enableLockOn() {
		this.superSpeed = false;

		if (this instanceof Player) {
			this._movementSpeed.y = 0;
			this._movementSpeed.x = 0;
		} else {
			this.movementSpeed.y = 0;
			this.movementSpeed.x = 0;
		}
	}

	damage() {
		this.health -= 1;

		if (this.health <= 0)
			this.pleaseDestroyMe = true;
	}

	update(tick) { 
		if (typeof this.targetLock != "undefined") {
			const myPosition = (this instanceof Player) ? new Point(this.pointOnScreen.x - (this.size.x / 2),
				this.pointOnScreen.y - (this.size.y / 2)) : new Point(this.position.x - (this.size.x / 2),
				this.position.y - (this.size.y / 2));
			const targetPosition = (this.targetLock instanceof Player) ? new Point(this.targetLock.pointOnScreen.x - (this.targetLock.size.x / 2),
				this.targetLock.pointOnScreen.y - (this.targetLock.size.y / 2)) : new Point(this.targetLock.position.x - (this.targetLock.size.x / 2),
				this.targetLock.position.y - (this.targetLock.size.y / 2));

			const lookVectorNoMagnitude = new Point();

			let grad = 0;
			let omg = 0;

			if (this.targetLock instanceof Player || 
				this.targetLock instanceof Enemy && 
				window.game.gameStates[0]["game"].entities["enemies"].includes(this.targetLock)) {
				
				this.lookVector = new Point(targetPosition.x - myPosition.x,
						targetPosition.y - myPosition.y).magnitude();

				/*
				lookVectorNoMagnitude.x = targetPosition.x - myPosition.x;
				lookVectorNoMagnitude.y = targetPosition.y - myPosition.y;
				*/
				grad = Math.atan2((targetPosition.y - myPosition.y), (targetPosition.x - myPosition.x));
				omg = grad - this.rotation;

				this.enableLockOn();
			} else if (this.targetLock instanceof Enemy) {
				this.rotation = 0;

				this.disableLockOn();
			} else {
				console.log("this is a meme");
				this.lookVector = new Point(targetPosition.x - myPosition.x,
				targetPosition.y - myPosition.y).magnitude();

				this.enableLockOn();
			}

			this.rotation += omg;
			//this.rotation = Math.atan2(lookVectorNoMagnitude.y, lookVectorNoMagnitude.x);

			//this.lastLookVector = this.lookVector;
		} else {
			//this.lastLookVector = this.defaultFacing;
			this.rotation = 0;

			this.disableLockOn();
		}
	}

	draw(ctx) { 
		ctx.save();

		if (typeof this.targetLock != "undefined") {
			ctx.translate(this.position.x + (this.size.x / 2), this.position.y + (this.size.y / 2));
			ctx.rotate(this.rotation + 0.5 * Math.PI);
			ctx.translate(-(this.position.x + (this.size.x / 2)), -(this.position.y + (this.size.y / 2)));
		}

		if (window.game.gameStates[0]["game"].player.superSpeed) {
			ctx.save();
			ctx.globalAlpha = 0.33;
			ctx.drawImage(this.texture, this.position.x - 1, this.position.y - 8, this.size.x + 2, this.size.y + 2);
			ctx.restore();
		}

		ctx.drawImage(this.texture, this.position.x, this.position.y, this.size.x, this.size.y);

		ctx.restore();
	}

	/**
	 * Checks whether an entity collides with another entity
	 * Example: if e1's x position is 60, and e2's x position is 50,
	 * and e2's x position + 2's width is = 70, return true.
	 **/
	static collidesWith(e1, e2) {
		if (e1 instanceof Player) {
			return ((e1.pointOnScreen.x >= e2.position.x &&
					e1.pointOnScreen.x <= e2.position.x + e2.size.x &&
					e1.pointOnScreen.y >= e2.position.y &&
					e1.pointOnScreen.y <= e2.position.y + e2.size.y)
					||
					(e2.position.x >= e1.pointOnScreen.x &&
					e2.position.x <= e1.pointOnScreen.x + e1.size.x &&
					e2.position.y >= e1.pointOnScreen.y &&
					e2.position.y <= e1.pointOnScreen.y + e1.size.y
					));
		} else if (e2 instanceof Player) {
			return ((e2.pointOnScreen.x >= e1.position.x &&
					e2.pointOnScreen.x <= e1.position.x + e1.size.x &&
					e2.pointOnScreen.y >= e1.position.y &&
					e2.pointOnScreen.y <= e1.position.y + e1.size.y)
					||
					(e1.position.x >= e2.pointOnScreen.x &&
					e1.position.x <= e2.pointOnScreen.x + e2.size.x &&
					e1.position.y >= e2.pointOnScreen.y &&
					e1.position.y <= e2.pointOnScreen.y + e2.size.y
					));
		} else {
			return ((e1.position.x >= e2.position.x &&
					e1.position.x <= e2.position.x + e2.size.x &&
					e1.position.y >= e2.position.y &&
					e1.position.y <= e2.position.y + e2.size.y)
					||
					(e2.position.x >= e1.position.x &&
					e2.position.x <= e1.position.x + e1.size.x &&
					e2.position.y >= e1.position.y &&
					e2.position.y <= e1.position.y + e1.size.y
					));
		}
	}
}


class Meteorite extends Entity {
	constructor() {
		super("meteorite", new Point(rand(0, window.game.canvas.width), -32), new Point(32, 32));

		this.explosion = window.bank.sprites["explosion_spritesheet"];
		/*ImageLoader.load("../content/explosion_spritesheet.png").then(function(result) {
			this.explosion = result;
		}.bind(this), function(err) {
			throw err;
		}.bind(this));*/
		this.explosionSpritesheetWidth = 288;

		this.spin = rand(0, 10) == 9 ? rand(1, 4) : 0;
		this.oldSpin = 0;

		this.maxHealth = 2;
		this.health = this.maxHealth

		this.movementSpeed = new Point(0, rand(0, 3));

		this.damageOnImpact = (this.spin == 0) ? 1 : this.spin + 1;

		this.destroyAnimationFinished = false;
		this.destroyAnimationPhase = 0;
		this.destroyAnimationSpeed = 10; // the higher the number, the slower
	}

	update(tick) {
		this.position.y += window.game.gameStates[0]["game"].player.movementSpeed.y 
							+ this.spin
							+ this.movementSpeed.y;

		this.position.x += window.game.gameStates[0]["game"].player.movementSpeed.x
							+ this.movementSpeed.x;

		if (this.position.y > window.game.canvas.height) {
			this.pleaseKillMe = true;
		} else if (!this.pleaseDestroyMe && Entity.collidesWith(this, window.game.gameStates[0]["game"].player)) {
			window.game.gameStates[0]["game"].player.damage(this.damageOnImpact);
			this.pleaseDestroyMe = true;
		}

		for (let i = window.game.gameStates[0]["game"].player.projectiles.length; i >= 0; i--) {
			if (window.game.gameStates[0]["game"].player.projectiles[i] instanceof Laser) {
				if (!this.pleaseDestroyMe 
					&& Entity.collidesWith(this, window.game.gameStates[0]["game"].player.projectiles[i])) {
					this.damage();
					window.game.gameStates[0]["game"].player.projectiles[i].pleaseKillMe = true;
				}
			}
		}

		if (this.pleaseDestroyMe) {
			if (this.destroyAnimationPhase * 32 >= this.explosionSpritesheetWidth) {
				this.destroyAnimationFinished = true;
			} else {
				if (tick % this.destroyAnimationSpeed == 0) {
					this.destroyAnimationPhase++;
				}
			}
		}
	}

	draw(ctx) {
		ctx.save();
		ctx.translate(this.position.x + (this.size.x / 2), this.position.y + (this.size.y / 2));
		ctx.rotate((this.spin + this.oldSpin) * (Math.PI/180));
		this.oldSpin = (this.oldSpin + this.spin);
		ctx.translate(-(this.position.x + (this.size.x / 2)), -(this.position.y + (this.size.y / 2)));

		if (!this.pleaseDestroyMe) {
			super.draw(ctx);
		} else {
			ctx.drawImage(this.explosion, 32 * this.destroyAnimationPhase, 0, 32, 32,
				this.position.x, this.position.y, 32, 32);
		}

		ctx.restore();

		if (!this.pleaseDestroyMe) {
			if (this.isHit) {
				ctx.fillStyle = "darkred";
				ctx.lineWidth = "0.5";
				ctx.strokeStyle = "darkgray";

				const sizeOfHealthbar = new Point(this.size.x * 1.5, 7);
				ctx.fillRect(this.position.x - (sizeOfHealthbar.x / 2) + (this.size.x / 2), this.position.y - this.size.y / 2.5,
					sizeOfHealthbar.x * this.healthMaxHealthRatio, sizeOfHealthbar.y);
				ctx.strokeRect(this.position.x - (sizeOfHealthbar.x / 2) + (this.size.x / 2), this.position.y - this.size.y / 2.5,
					sizeOfHealthbar.x, sizeOfHealthbar.y);
			}
		}
	}
}

class Enemy extends Entity {
	constructor() {
		super("ship", new Point(rand(0, window.game.canvas.width), -32), new Point(32, 32));

		this.explosion = window.bank.sprites["explosion_spritesheet"];
		this.explosionSpritesheetWidth = 288;

		this.maxHealth = 10;
		this.health = this.maxHealth

		this.movementSpeed = new Point(0, rand(1, 4));

		this.defaultFacing = new Point(0, 1);

		this.destroyAnimationFinished = false;
		this.destroyAnimationPhase = 0;
		this.destroyAnimationSpeed = 10; // the higher the number, the slower

		this.lineOfSightCone = new LineOfSightCone(this);
	}

	update(tick) {
		this.position.y += this.movementSpeed.y
							+ window.game.gameStates[0]["game"].player.movementSpeed.y;

		this.position.x += this.movementSpeed.x
							+ window.game.gameStates[0]["game"].player.movementSpeed.x;

		if (this.position.y > window.game.canvas.height) {
			this.pleaseKillMe = true;
		} else if (!this.pleaseDestroyMe && Entity.collidesWith(this, window.game.gameStates[0]["game"].player)) {
			this.pleaseDestroyMe = true;
		}

		for (let i = window.game.gameStates[0]["game"].player.projectiles.length; i >= 0; i--) {
			if (window.game.gameStates[0]["game"].player.projectiles[i] instanceof Laser) {
				if (!this.pleaseDestroyMe 
					&& Entity.collidesWith(this, window.game.gameStates[0]["game"].player.projectiles[i])) {
					this.damage();
					window.game.gameStates[0]["game"].player.projectiles[i].pleaseKillMe = true;
				}
			}
		}

		if (this.pleaseDestroyMe) {
			if (this.destroyAnimationPhase * 32 >= this.explosionSpritesheetWidth) {
				this.destroyAnimationFinished = true;
			} else {
				if (tick % this.destroyAnimationSpeed == 0) {
					this.destroyAnimationPhase++;
				}
			}
		}

		this.lineOfSightCone.update(tick);

		if (this.lineOfSightCone.collidesWith(window.game.gameStates[0]["game"].player)) {
			this.targetLock = window.game.gameStates[0]["game"].player;
		}

		super.update(tick);
	}

	damage(val) {
		super.damage(val);

		this.targetLock = window.game.gameStates[0]["game"].player;
	}

	draw(ctx) {
		ctx.save();

		if (typeof this.targetLock == "undefined") {
			ctx.translate(this.position.x + (this.size.x / 2), this.position.y + (this.size.y / 2));
			ctx.rotate(toRadians(180));
			ctx.translate(-(this.position.x + (this.size.x / 2)), -(this.position.y + (this.size.y / 2)));
		}
		
		/*
		if (typeof this.targetLock != "undefined") {
			ctx.translate(this.position.x, this.position.y);
			ctx.rotate(this.rotation + 0.5 * Math.PI);
			ctx.translate(-(this.position.x), -(this.position.y));
		}
		*/

		if (!this.pleaseDestroyMe) {
			super.draw(ctx);
		} else {
			ctx.drawImage(this.explosion, 32 * this.destroyAnimationPhase, 0, 32, 32,
				this.position.x, this.position.y, 32, 32);
		}

		ctx.restore();

		if (!this.pleaseDestroyMe) {
			if (this.isHit) {
				ctx.fillStyle = "darkred";
				ctx.lineWidth = "0.5";
				ctx.strokeStyle = "darkgray";

				const sizeOfHealthbar = new Point(this.size.x * 1.5, 7);
				ctx.fillRect(this.position.x - (sizeOfHealthbar.x / 2) + (this.size.x / 2), this.position.y - this.size.y / 2.5,
					sizeOfHealthbar.x * this.healthMaxHealthRatio, sizeOfHealthbar.y);
				ctx.strokeRect(this.position.x - (sizeOfHealthbar.x / 2) + (this.size.x / 2), this.position.y - this.size.y / 2.5,
					sizeOfHealthbar.x, sizeOfHealthbar.y);
			}
		}

		this.lineOfSightCone.draw(ctx);
	}
}

class Laser extends Entity {
	constructor(parent, dir, rot, target) {
		super("", arguments[4] || Point.zero(), arguments[5] || Point.zero());

		this.parent = parent;
		this.direction = dir;
		this.rotation = rot;
		this.targetPos = (typeof target != "undefined") ? target.position : undefined;

		this.fillStyle = "rgb(255, 0, 0)";
		this.movementSpeed = new Point(20, 10);

		this.pleaseKillMe = false;
	}

	update(tick) {
		//this.position.y -= ((this.movementSpeed.y) - window.game.gameStates[0]["game"].player.movementSpeed.y);
		this.position.y += (-this.movementSpeed.y * -this.direction.y) + window.game.gameStates[0]["game"].player.movementSpeed.y;
		this.position.x += (this.direction.x * this.movementSpeed.y) + window.game.gameStates[0]["game"].player.movementSpeed.x;
		//this.position.x += (window.game.gameStates[0]["game"].player.movementSpeed.x
							//+ (this.movementSpeed.x))

		if (this.position.y + this.size.y < 0)
			this.pleaseKillMe = true;
	}

	draw(ctx) {
		ctx.save();
		// @TODO: FIX ROTATING WHEN UNDOING TARGET LOCK
		if (typeof this.targetPos != "undefined") {
			ctx.translate(this.position.x, this.position.y);
			ctx.rotate(this.rotation + 0.5 * Math.PI);
			ctx.translate(-(this.position.x), -(this.position.y));
		}

		ctx.fillStyle = this.fillStyle;
		ctx.fillRect(this.position.x, this.position.y, this.size.x, this.size.y);
		ctx.restore();
	}
}

/**
 * Star
 * An entity used mainly in the MainMenu - gamestate
 **/
class Star extends Entity {
	constructor() {
		super("", new Point(rand(0, window.game.canvas.width), -4), new Point(4, 4));

		this.fillStyle = "rgba(" + rand(0, 255) + "," + rand(0, 255) + "," + rand(0, 255) + "," + (rand(1,10) / 10) +")";
		this.movementSpeed = new Point(0, rand(1, 6));
	}

	update(tick) {
		this.position.y += window.game.gameStates[0]["game"].player.movementSpeed.y
							+ this.movementSpeed.y;

		this.position.x += window.game.gameStates[0]["game"].player.movementSpeed.x
							+ this.movementSpeed.x;

		if (this.position.y > window.game.canvas.height)
			this.pleaseKillMe = true;
	}

	draw(ctx) {
		ctx.fillStyle = this.fillStyle;
		ctx.fillRect(this.position.x, this.position.y, this.size.x, this.size.y);
	}
}

/**
 * GameStar
 * An entity used mainly in the Game - gamestate
 **/
class GameStar extends Entity {
	constructor() {
		super("", new Point(rand(0, window.game.canvas.width), -4), new Point(4, 4));

		const randomNumber = rand(0, 16);

		let randomRGBs = {
			0: [0, 0, 0], 1: [157, 157, 157], 2: [255, 255, 255],
			3: [190, 38, 51], 4: [224, 111, 139], 5: [73, 60, 43],
			6: [164, 100, 34], 7: [235, 137, 49], 8: [247, 226, 107],
			9: [47, 72, 78], 10: [68, 137, 26], 11: [163, 206, 39],
			12: [27, 38, 50], 13: [0, 87, 132], 14: [49, 162, 242],
			15: [178, 220, 239]
		};

		this.fillStyle = "rgb(" + randomRGBs[randomNumber][0] + "," 
							    + randomRGBs[randomNumber][1] + "," 
							    + randomRGBs[randomNumber][2] + ")";

		this.movementSpeed = new Point(0, rand(0, 4));
	}

	update(tick) {
		this.position.y += this.movementSpeed.y;
		this.position.x += this.movementSpeed.x;

		if (this.position.y > window.game.canvas.height)
			this.pleaseKillMe = true;
	}

	draw(ctx) {
		ctx.fillStyle = this.fillStyle;
		ctx.fillRect(this.position.x, this.position.y, this.size.x, this.size.y);
	}
}

class LineOfSightCone {
	constructor(parent) {
		this.parent = parent;
		this.rotation = arguments[2] || 0;
		this.scale = arguments[3] || new Point(1, 1);

		this.size = new Point(250, 250);

		this.position = new Point(this.parent.position.x + (this.parent.size.x / 2), 
			this.parent.position.y + (this.parent.size.y / 2));

		this.cone = new Path2D();
	}

	update(tick) {
		this.position = new Point(this.parent.position.x + (this.parent.size.x / 2), 
			this.parent.position.y + (this.parent.size.y / 2));
		this.rotation = this.parent.rotation;

		this.cone = new Path2D();

		this.cone.moveTo(this.position.x, this.position.y);
		this.cone.lineTo(this.position.x + -(this.size.x / 2), this.position.y + this.size.y);
		this.cone.lineTo(this.position.x + (this.size.x / 2), this.position.y + this.size.y);
		this.cone.lineTo(this.position.x, this.position.y);
	}

	draw(ctx) {
		ctx.beginPath();
		ctx.save();

		if (typeof this.parent.targetLock != "undefined") {
			ctx.translate(this.position.x, this.position.y);
			ctx.rotate(this.rotation + -0.5 * Math.PI);
			ctx.translate(-this.position.x, -this.position.y);
		}

		ctx.fillStyle = "gold";
		ctx.fill(this.cone);

		ctx.restore();
		ctx.closePath();
	}

	collidesWith(e) {
		if (e instanceof Player) {
			return window.game.context.isPointInPath(this.cone, Math.round(e.pointOnScreen.x), Math.round(e.pointOnScreen.y));
		}

		throw "not implemented";
	}
}

class Flame {
	constructor(owner) {
		this.owner = owner;

		this.texture = window.bank.sprites["flame_spritesheet"];

		this.size = arguments[1] || new Point(16, 24);

		this.maxFlameStage = 1;
		this.minFlameStage = 0;
		this.flameStage = this.minFlameStage;
		this.changeTick = Math.round(8 / (this.owner.movementSpeed.y / 2));
	}

	update(tick) {
		this.changeTick = Math.round(8 / (this.owner.movementSpeed.y / 2));

		if (tick % this.changeTick == 0)
		{
			if (this.flameStage >= this.maxFlameStage)
				this.flameStage = this.minFlameStage;
			else
				this.flameStage++;
		}
	}

	draw(ctx) {
		if (this.owner instanceof Player)
			ctx.drawImage(this.texture, 8 * this.flameStage, 0, 8, 16, 
				this.owner.pointOnScreen.x - (this.owner.size.x / 2) + (16 / 2), this.owner.pointOnScreen.y + (this.owner.size.y) - (this.size.y / 2) - 10, 
				this.size.x, this.size.y);
		else
			ctx.drawImage(this.texture, 8 * this.flameStage, 0, 8 * this.flameStage, 16, 
				this.owner.position.x - (this.owner.size.x / 2) + (16 / 2), this.owner.position.y + (this.owner.size.y) - (this.size.y / 2) - 10, 
				this.size.x, this.size.y);
	}
}

class Player extends Entity {
	constructor() {
		super("ship", new Point(0, 0), new Point(32, 32));

		this.flame = new Flame(this);

		this.pointOnScreen = new Point($(window).width() / 2, $(window).height() / 1.25);

		this._movementSpeed = new Point(0, 1);
		this.lookVector = new Point(0, -1);
		this.originalLookVector = this.lookVector;

		this.superSpeed = false;

		this.damageTaken = false;
		this.damageTakenFadeout = 1000;

		$(window).mouseup(function(ev) {
			this.shoot(ev.which);
		}.bind(this));

		$(window).keyup(function(ev) {
			if (ev.key.toLowerCase() == " ") {
				if (ev.ctrlKey) {
					this.disableLockOn();
				} else {
					const curArray = window.game.gameStates[0]["game"].entities["enemies"].slice(0);

					if (typeof this.targetLock != "undefined")
						curArray.splice(window.game.gameStates[0]["game"].entities["enemies"].indexOf(this.targetLock), 1);

					if (curArray.length > 1) {
						const sortedArray = curArray.sort((a, b) => {
							return (a.position.x - this.pointOnScreen.x < b.position.x - this.pointOnScreen.x
								&& a.position.y - this.pointOnScreen.y < b.position.y - this.pointOnScreen.y);
						});

						this.targetLock = sortedArray[0];
					} else if (curArray.length == 1) {
						this.targetLock = curArray[0];
					}
				}
			}
		}.bind(this));

		this.projectiles = [];
	}

	set movementSpeed(val) {
		this._movementSpeed = val;
		window.game.gameStates[0]["game"].meteoriteSpawnFreq /= val.y; 
	}

	get movementSpeed() {
		return this._movementSpeed;
	}

	get isDead() {
		return this.health <= 0;
	}

	shoot(cannon) {
		if (cannon === 1) {
			this.projectiles.push(new Laser(this, this.lookVector, this.rotation, this.targetLock, new Point(this.pointOnScreen.x, this.pointOnScreen.y + 8),
				new Point(2, 16)));
			console.log(this.lookVector, this.rotation, this.targetLock);
		} else if (cannon === 2) {

		}
	}

	damage(dmg) {
		this.health -= dmg;

		new GameEvent(function() { 
			this.damageTaken = true;
		}.bind(this), function() {
			this.damageTaken = false;
			window.game.gameStates[0]["game"].overlays["damage"].reset();
		}.bind(this), this.damageTakenFadeout).run();
	}

	/**
	 * Update the player
	 *
	 * - Check if the player is dead
	 * - Move the player with arrow keys
	 **/
	update(tick) {
		if (this.isDead)
			return console.log("You are dead!");

		for (let i = this.projectiles.length - 1; i >= 0; i--) {
			this.projectiles[i].update(tick);

			if (this.projectiles[i] instanceof Laser && this.projectiles[i].pleaseKillMe)
				this.projectiles.splice(i, 1);
		}

		super.update(tick);

		if (typeof this.targetLock == "undefined") {
			if (window.game.keys.includes("arrowup")) {
				this.superSpeed = true;
				this._movementSpeed.y = 4;
			} else if (window.game.keys.includes("arrowdown")) {
				this.superSpeed = false;
				this._movementSpeed.y = 0.25;
			} else {
				this.superSpeed = false;
				this._movementSpeed.y = 1;
			}

			if (window.game.keys.includes("arrowleft")) {
				this._movementSpeed.x = 1;
			} else if (window.game.keys.includes("arrowright")) {
				this._movementSpeed.x = -1;
			} else {
				this._movementSpeed.x = 0;
			}
		}

		this.flame.update(tick);
	}

	draw(ctx) {
		for (let i = this.projectiles.length - 1; i >= 0; i--) {
			this.projectiles[i].draw(ctx);
		}

		ctx.save();

		if (typeof this.targetLock != "undefined") {
			ctx.translate(this.pointOnScreen.x, this.pointOnScreen.y);
			ctx.rotate(this.rotation + 0.5 * Math.PI);
			ctx.translate(-(this.pointOnScreen.x), -(this.pointOnScreen.y));
		}

		this.flame.draw(ctx);
		ctx.drawImage(this.texture, this.pointOnScreen.x - (this.size.x / 2), this.pointOnScreen.y - (this.size.y / 2), this.size.x, this.size.y);

		ctx.restore();
	}
}

class Overlay {
	constructor(textureName) {
		this.texture = window.bank.sprites[textureName];
	}

	reset() {
		console.warn("Reset is not implemented for this overlay.");
	}

	update(ctx) {
		console.warn("Update is not implemented for this overlay.");
	}

	draw(ctx) {
		ctx.drawImage(this.texture, 0, 0, window.game.canvas.width, window.game.canvas.height);
	}
}

class DamageOverlay extends Overlay {
	constructor() {
		super("hulldamage");

		this.currentAlpha = 1;
		this.goDown = false;
		this.waitTick = 25;
		this.tick2 = 4;
	}

	reset() {
		this.currentAlpha = 1;
		this.goDown = false;
		this.waitTick = 25;
	}

	update(tick) {
		if (tick % this.waitTick == 0)
			this.goDown = true;

		if (this.goDown && this.currentAlpha - 0.1 >= 0 && tick % this.tick2 == 0)
			this.currentAlpha -= 0.1;
	}

	draw(ctx) {
		ctx.save();
		ctx.globalAlpha = this.currentAlpha;
		ctx.drawImage(this.texture, 0, 0, window.game.canvas.width, window.game.canvas.height);
		ctx.restore();
	}
}

class GameEvent {
	constructor(beginFunc, endFunc, timeout) {
		this.beginFunc = beginFunc;
		this.endFunc = endFunc;
		this.timeout = timeout;
	}

	run() {
		this.beginFunc();
		setTimeout(this.endFunc, this.timeout);
	}
}

class GameButton {
	constructor() {
		this.text = arguments[0] || "Button";

		this.position = arguments[1] || Point.zero();

		this.fontSize = arguments[2] || 12;
		this.fillStyle = arguments[3] || "white";

		this.bindedFunction = arguments[4] || function() { 
			console.warn("No function has been binded to button " + this)
		};

		this._selected = false;
	}

	select() {
		const deselectOthers = arguments[0] || false;

		if (!deselectOthers) {
			$.each(window.game.gameStates[0]["mainMenu"].buttons, (key, val) => {
				val.deselect();
			});
		}

		this._selected = true;
	}

	deselect() {
		this._selected = false;
	}

	press() {
		this.bindedFunction();
	}

	update(tick) { }

	draw(ctx) {
		ctx.font = ((this._selected) ? this.fontSize + 0.75 : this.fontSize) + "vmax joystix";
		ctx.fillStyle = this.fillStyle;

		const measurement = ctx.measureText((this._selected ? ">" : "") + this.text);

		ctx.fillText((this._selected ? ">" : "") + this.text, this.position.x - (measurement.width / 2),
			this.position.y);
	}
}

/**
 * Default, abstract class for gamestates
 * NEVER directly create this
 **/
class State {
	constructor() { }

	update(tick) { }

	draw(ctx) { }
}

class MainMenuState extends State {
	constructor() {
		super();

		this.logo = window.bank.sprites["logo"];
		/*
		this.logo = undefined;
		ImageLoader.load("../content/logo.png").then(function(result) {
			this.logo = result;
		}.bind(this), function(err) {
			throw err;
		}.bind(this));*/

		// whether logo should be drawn with globalCompositeOperation
		this.flick = false;

		const buttonDefaultPos = new Point(window.game.canvas.width / 2,
			((window.game.canvas.height / 6) - ((window.game.canvas.height / 8) / 2)) + (window.game.canvas.height / 4)
				+ (window.game.canvas.height / 8));

		const buttonMargin = 60;

		this.buttons = {
			playButton: new GameButton("Play!", buttonDefaultPos,
				2, "white", function() {
					window.game.gameStates[1] = "game";
				}),
			exitButton: new GameButton("Exit", new Point(buttonDefaultPos.x, buttonDefaultPos.y + buttonMargin),
				2, "white", () => win.close(true))
		};

		this.stars = [];
		this.starSpawnFrequency = 2;

		this.buttons["playButton"].select(true);

		$(window).keyup(function(ev) {
			if (ev.key.toLowerCase() == "arrowup") {
				const buttonArray = Object.values(this.buttons);
				const indexOfSelectedButton = buttonArray.indexOf(buttonArray.find(b => b._selected));

				let newIndexOf = 0;

				if (indexOfSelectedButton == 0)
					newIndexOf = buttonArray.length - 1;
				else if (indexOfSelectedButton >= buttonArray.length - 1)
					newIndexOf = 0;
				else
					newIndexOf--;

				buttonArray[newIndexOf].select();
			} else if (ev.key.toLowerCase() == "arrowdown") {
				const buttonArray = Object.values(this.buttons);
				const indexOfSelectedButton = buttonArray.indexOf(buttonArray.find(b => b._selected));

				let newIndexOf = 0;

				if (indexOfSelectedButton == 0)
					newIndexOf = buttonArray.length - 1;
				else if (indexOfSelectedButton >= buttonArray.length - 1)
					newIndexOf = 0;
				else
					newIndexOf++;

				buttonArray[newIndexOf].select();
			} else if (ev.key.toLowerCase() == "enter") {
				$.each(this.buttons, (key, val) => {
					if (val._selected)
						val.press();
				});
			}
		}.bind(this));
	}

	/**
	 * Updates the main menu
	 *
	 * - Flicker the logo
	 * - Spawn new stars
	 * - Update the stars
	 * -- Destroy old stars
	 **/ 
	update(tick) {
		if (tick % 100 == 0)
			this.flick = !this.flick;

		if (tick % this.starSpawnFrequency == 0)
			this.stars.push(new Star());

		for (let i = this.stars.length - 1; i >= 0; i--) {
			this.stars[i].update(tick);

			if (this.stars[i].pleaseKillMe)
				this.stars.splice(i, 1);
		}

		$.each(this.buttons, (key, val) => {
			val.update(tick);
		});
	}

	draw(ctx) {
		ctx.fillStyle = "black";
		ctx.fillRect(0, 0, window.game.canvas.width, window.game.canvas.height);

		for (let i = this.stars.length - 1; i >= 0; i--) {
			this.stars[i].draw(ctx);
		}

		if (!this.flick) {
			ctx.drawImage(this.logo, (window.game.canvas.width / 2) - ((window.game.canvas.width / 2) / 2),
									(window.game.canvas.height / 6) - ((window.game.canvas.height / 8) / 2),
									(window.game.canvas.width / 2), (window.game.canvas.height / 4));
		} else {
			ctx.save();
			ctx.drawImage(this.logo, (window.game.canvas.width / 2) - ((window.game.canvas.width / 2) / 2),
						(window.game.canvas.height / 6) - ((window.game.canvas.height / 8) / 2),
						(window.game.canvas.width / 2), (window.game.canvas.height / 4));
			ctx.globalCompositeOperation='soft-light';
			ctx.fillStyle='darkred';
			ctx.fillRect((window.game.canvas.width / 2) - ((window.game.canvas.width / 2) / 2),
						(window.game.canvas.height / 6) - ((window.game.canvas.height / 8) / 2),
						(window.game.canvas.width / 2), (window.game.canvas.height / 4));
			ctx.restore();
		}

		$.each(this.buttons, (key, val) => {
			val.draw(ctx);
		});
	}
}

class GameState extends State {
	constructor() {
		super();

		this.paused = false;

		this.player = new Player();

		/**
		 * Vars
		 **/
		this._meteoriteSpawnFreq = 50 / Math.round(this.player.movementSpeed.y / 2);
		this.starSpawnFrequency = 4;
		this._enemySpawnFreq = 175 / Math.round(this.player.movementSpeed.y / 2);

		this.entities = {
			meteorites: [],
			stars: [],
			enemies: []
		};

		this.overlays = {
			damage: new DamageOverlay(),
		};

		$(window).keyup(function(ev) {
			if (ev.key.toLowerCase() == "escape") {
				this.paused = !this.paused;
			}
		}.bind(this));
	}

	/**
	 * Update the game
	 *
	 * - Spawn new entities
	 * - Discard old entities
	 * - Update the entities
	 **/
	update(tick) {
		if (!this.paused) {
			if (tick % this.meteoriteSpawnFreq == 0)
				this.entities["meteorites"].push(new Meteorite());
			if (tick % this.starSpawnFrequency == 0)
				this.entities["stars"].push(new Star());
			if (tick % this.enemySpawnFreq == 0)
				this.entities["enemies"].push(new Enemy());

			for (let i = this.entities["meteorites"].length - 1; i >= 0; i--) {
				this.entities["meteorites"][i].update(tick);

				if (this.entities["meteorites"][i].pleaseKillMe)
					this.entities["meteorites"].splice(i, 1);
				else if (this.entities["meteorites"][i].pleaseDestroyMe &&
					this.entities["meteorites"][i].destroyAnimationFinished)
					this.entities["meteorites"].splice(i, 1);
			}

			for (let i = this.entities["enemies"].length - 1; i >= 0; i--) {
				this.entities["enemies"][i].update(tick);

				if (this.entities["enemies"][i].pleaseKillMe)
					this.entities["enemies"].splice(i, 1);
				else if (this.entities["enemies"][i].pleaseDestroyMe &&
					this.entities["enemies"][i].destroyAnimationFinished)
					this.entities["enemies"].splice(i, 1);
			}

			for (let i = this.entities["stars"].length - 1; i >= 0; i--) {
				this.entities["stars"][i].update(tick);

				if (this.entities["stars"][i].pleaseKillMe)
					this.entities["stars"].splice(i, 1);
			}

			if (this.player.damageTaken)
				this.overlays["damage"].update(tick);

			this.player.update(tick);
		}
	}

	draw(ctx) {
		ctx.fillStyle = "black";
		ctx.fillRect(0, 0, window.game.canvas.width, window.game.canvas.height);

		this.player.draw(ctx);

		for (let i = this.entities["meteorites"].length - 1; i >= 0; i--) {
			this.entities["meteorites"][i].draw(ctx);
		}

		for (let i = this.entities["enemies"].length - 1; i >= 0; i--) {
			this.entities["enemies"][i].draw(ctx);
		}

		for (let i = this.entities["stars"].length - 1; i >= 0; i--) {
			this.entities["stars"][i].draw(ctx);
		}

		if (this.paused) {
			ctx.fillStyle = "rgba(50, 100, 225, 0.5)";
			ctx.font = "2.75vmax joystix";
			let measurement = ctx.measureText("PAUSED");
			ctx.fillRect(0, 0, window.game.canvas.width, window.game.canvas.height);
			ctx.fillStyle = "white";
			ctx.fillText("PAUSED", (window.game.canvas.width / 2) - (measurement.width / 2),
				(window.game.canvas.height / 2));
			ctx.font = "1.25vmax joystix";
			measurement = ctx.measureText("! Press ESC to return !");
			ctx.fillText("! Press ESC to return !", (window.game.canvas.width / 2) - (measurement.width / 2),
				(window.game.canvas.height / 2) + 72 + 30);
		} else {
			if (this.player.damageTaken)
				this.overlays["damage"].draw(ctx);
		}
	}

	set meteoriteSpawnFreq(val) {
		this._meteoriteSpawnFreq = Math.ceil(val);
	}

	get meteoriteSpawnFreq() {
		return this._meteoriteSpawnFreq;
	}

	set enemySpawnFreq(val) {
		this._enemySpawnFreq = Math.ceil(val);
	}

	get enemySpawnFreq() {
		return this._enemySpawnFreq;
	}
}

class Game {
	constructor() {
		this.canvas = document.getElementById("game");

		this.canvas.width = window.innerWidth;
		this.canvas.height = window.innerHeight;

		this.context = this.canvas.getContext("2d");
		this.context.imageSmoothingEnabled = false;

		this.closeDown = false;

		this.everythingDoneFunc = function() {
			throw "not implemented";
		};
	}

	run() {
		this.ticker = 0;

		this.gameStates = [{
			mainMenu: new MainMenuState(),
			game: new GameState(),
		}, "mainMenu"];

		this.keys = [];
		this.keypresses = [];

		$(window).keydown((ev) => {
			if (!this.keys.includes(ev.key.toLowerCase()))
				this.keys.push(ev.key.toLowerCase());
		});

		$(window).keyup((ev) => {
			if (this.keys.includes(ev.key.toLowerCase()))
				this.keys.splice(this.keys.indexOf(ev.key.toLowerCase()), 1);
		});

		window.requestAnimationFrame(this.draw.bind(this));
	}

	close(callbackFunc) {
		$(window).unbind();

		this.everythingDoneFunc = callbackFunc;

		this.closeDown = true;
	}

	update() {
		this.gameStates[0][this.gameStates[1]].update(this.ticker);

		this.ticker++;
	}

	draw() {
		if (this.closeDown) {
			this.everythingDoneFunc();
			return;
		}

		this.update();

		this.clearCanvas();

		// start drawing here
		this.gameStates[0][this.gameStates[1]].draw(this.context);6

		window.requestAnimationFrame(this.draw.bind(this));
	}

	clearCanvas() {
		// Store the current transformation matrix
		this.context.save();

		// Use the identity matrix while clearing the canvas
		this.context.setTransform(1, 0, 0, 1, 0, 0);
		this.context.clearRect(0, 0, window.game.canvas.width, window.game.canvas.height);

		// Restore the transform
		this.context.restore();
	}
}

$(function() {
	window.initGame = function() {
		window.bank = new Spritebank();
		window.bank.load().then(function(result) {
			window.game = new Game();
			window.game.run();
		}).catch(function(err) {
			throw err;
		});
	};

	$(window).resize(function() {
		window.game.close(window.initGame);
	});

	window.initGame();
});

/*$(function() {
	window.canvas = $("#game")[0];

	function toRadians(deg) {
		return deg * (Math.PI / 180);
	}

	function toDegrees(rad) {
		return rad * (180/ Math.PI);
	}

	function drawCircleEz(ctx, position, radius, lineWidth, fillStyle, strokeStyle) {
		ctx.beginPath();
		ctx.arc(position.x, position.y, radius, 0, 2 * Math.PI, false);
		ctx.fillStyle = fillStyle;
		ctx.fill();
		ctx.lineWidth = lineWidth;
		ctx.strokeStyle = strokeStyle;
		ctx.stroke();
		ctx.closePath();
	}

	function rand(min, max) {
		return Math.floor((Math.random() * max) + min);
	}

	/**
	 * Contains some helpful debugging classes etc for developers
	 **/
	 /*
	window.devComponents = {
		point: class Point {
			constructor() {
				this.x = arguments[0] || 0;
				this.y = arguments[1] || 0;
			}

			magnitude() {
				return new Point(this.x / this.length(), this.y / this.length());
			}

			/**
			 * This is here for compatibility reasons. Using magnitude() is preferred.
			 **/

			 /*
			normalize() {
				return this.magnitude();
			}

			length() {
				return Math.sqrt(Math.pow(this.x, 2) + Math.pow(this.y, 2));
			}

			rotateDegrees(deg, origin) {
				const angleRads = toRadians(deg);

				this.x = Math.cos(angleRads) * (this.x - origin.x) - Math.sin(angleRads)
							* (this.y - origin.y) + origin.x;
				this.y = Math.sin(angleRads) * (this.x - origin.x) + Math.cos(angleRads)
							* (this.y - origin.y) + origin.y;
			}

			rotate(angleRads, origin) {
				this.x = Math.cos(angleRads) * (this.x - origin.x) - Math.sin(angleRads)
							* (this.y - origin.y) + origin.x;
				this.y = Math.sin(angleRads) * (this.x - origin.x) + Math.cos(angleRads)
							* (this.y - origin.y) + origin.y;
			}

			static subtract(v1, v2) {
				return new Point(v2.x - v1.x, v2.y - v1.y);
			}

			static cross(v1, v2) {
				return v1.x * v2.y - v2.x * v1.y;
			}

		    static dot(v1, v2){
		        return v1.x * v2.x + v1.y * v2.y;
		    }


			toString() {
				return this.x + ";" + this.y;
			}
		},
	};

	/**
	 * Works both as a 2 dimensional point, as well as
	 * a 2 dimensional vector (hence magnitude & length)
	 **/
	 /*
	class Point {
		constructor() {
			this.x = arguments[0] || 0;
			this.y = arguments[1] || 0;
		}

		magnitude() {
			return new Point(this.x / this.length(), this.y / this.length());
		}

		/**
		 * This is here for compatibility reasons. Using magnitude() is preferred.
		 **/
		 /*
		normalize() {
			return this.magnitude();
		}

		length() {
			return Math.sqrt(Math.pow(this.x, 2) + Math.pow(this.y, 2));
		}

		rotateDegrees(deg, origin) {
			const angleRads = toRadians(deg);

			this.x = Math.cos(angleRads) * (this.x - origin.x) - Math.sin(angleRads)
						* (this.y - origin.y) + origin.x;
			this.y = Math.sin(angleRads) * (this.x - origin.x) + Math.cos(angleRads)
						* (this.y - origin.y) + origin.y;
		}

		rotate(angleRads, origin) {
			this.x = Math.cos(angleRads) * (this.x - origin.x) - Math.sin(angleRads)
						* (this.y - origin.y) + origin.x;
			this.y = Math.sin(angleRads) * (this.x - origin.x) + Math.cos(angleRads)
						* (this.y - origin.y) + origin.y;
		}

		static subtract(v1, v2) {
			return new Point(v2.x - v1.x, v2.y - v1.y);
		}

		static cross(v1, v2) {
			return v1.x * v2.y - v2.x * v1.y;
		}

	    static dot(v1, v2){
	        return v1.x * v2.x + v1.y * v2.y;
	    }


		toString() {
			return this.x + ";" + this.y;
		}
	}

	class Line {
		constructor() {
			this.points = arguments[0] || [ new Point(0, 0), new Point(0, 0) ];
		}

		draw(ctx, positionOnCanvas, scale) {
			//console.log("called", ctx, positionOnCanvas, this.points[0], this.points[1]);
			ctx.lineTo((this.points[1].x * scale.x) + positionOnCanvas.x, 
				(this.points[1].y * scale.y) + positionOnCanvas.y);
		}
	}

	class Shape {
		constructor() {
			this.lines = arguments[0] || [];
			this.position = arguments[1] || new Point(0, 0);
			this.scale = arguments[2] || new Point(1, 1);
			this.fillStyle = arguments[3] || "white";
			this.rotation = arguments[4] || 0;
		}

		draw(ctx) {
			ctx.beginPath();

			ctx.save();

			ctx.translate(this.position.x, this.position.y);
			ctx.rotate(this.rotation);
			ctx.translate(-this.position.x, -this.position.y);

			this.lines.forEach(l => l.draw(ctx, this.position, this.scale));

			ctx.fillStyle = this.fillStyle;
			//console.log(this.fillStyle);
			ctx.fill();
			ctx.closePath();
			ctx.restore();
		}

		rotate(angle) {
			this.rotation = angle;
		}
	}

	class Entity {
		constructor() {
			this.base_size = 8;
		}

		update(tick) { }

		draw(ctx) { }
	}

	class HealthBlob extends Entity {
		constructor() {
			super();

			this.position = arguments[0] || new Point(rand(-2000, 2000), rand(-2000, 2000));

			this.size = 1;
		}

		update(tick) {

		}

		draw(ctx) {
			ctx.save();
			ctx.globalCompositeOperation = "color-dodge";
			drawCircleEz(ctx, Point.subtract(this.position, window.game.player.position), 
				this.base_size * this.size, 2, "green", "forestgreen");
			ctx.restore();
		}
	}

	class Player extends Entity {
		constructor() {
			super();

			this.drawPosition = new Point(window.canvas.width / 2, 
				window.canvas.height / 2);

			this.position = new Point(0, 0);
			this.movementSpeed = 0.25;

			this.lineOfShitCone = new Shape([ new Line([ new Point(0, 0), new Point(-125, 250) ]),
											  new Line([ new Point(-125, 250), new Point(125, 250) ]),
											  new Line([ new Point(125, 250), new Point(0, 0) ])
											], this.drawPosition, new Point(1, 1), "yellow");

			this.lookVectorNoMagnitude = new Point();
			this.lookVector = new Point();

			this.size = 2;
		}

		update(tick) {
			this.lookVectorNoMagnitude = new Point(
				window.game.MouseHandler.position.x - this.drawPosition.x,
				window.game.MouseHandler.position.y - this.drawPosition.y
				);
			this.lookVector = new Point(
				window.game.MouseHandler.position.x - this.drawPosition.x,
				window.game.MouseHandler.position.y - this.drawPosition.y
				).magnitude();

			/*
			 * Rotate the line of >shit< cone
			 */
			 /*
			let directionA = new Point(0, -1);

			let rotationAngle = (Math.atan2( -this.lookVector.y-directionA.y, -this.lookVector.x-directionA.x) * 2);

			this.lineOfShitCone.rotate(rotationAngle);

			if (window.game.KeyboardHandler.downKeys.includes("w"))
				this.position.y += this.movementSpeed;
			else if (window.game.KeyboardHandler.downKeys.includes("s"))
				this.position.y -= this.movementSpeed;

			if (window.game.KeyboardHandler.downKeys.includes("a"))
				this.position.x += this.movementSpeed;
			else if (window.game.KeyboardHandler.downKeys.includes("d"))
				this.position.x -= this.movementSpeed;
		}

		draw(ctx) {
			ctx.save();
			ctx.globalCompositeOperation = "destination-out";
			this.lineOfShitCone.draw(ctx);
			ctx.restore();

			drawCircleEz(ctx, this.drawPosition,
				this.base_size * this.size, 2, "white", "whitesmoke");
		}
	}

	class Game {
		constructor() {
			// init canvas to avoid fuzzy shapes
			window.canvas.width  = window.innerWidth;
			window.canvas.height = window.innerHeight;

			this.context = window.canvas.getContext("2d");

			// initialize event-handling classes (mouse, etc)
			this.MouseHandler = new (class {
				constructor() {
					this.mousePosition = new Point(0, 0);

					$("#game")[0].addEventListener("mousemove", function(evt) {
						this.mousePosition = new Point(evt.clientX, evt.clientY);
		 			}.bind(this), false);
				}

				get position() {
					return new Point(this.mousePosition.x,
						this.mousePosition.y);
				}
			});

			this.KeyboardHandler = new (class {
				constructor() {
					this.keys = [];

					window.addEventListener("keydown", function(evt) {
						if (!this.keys.includes(evt.key))
							this.keys.push(evt.key);
					}.bind(this), false);

					window.addEventListener("keyup", function(evt) {
						console.log(evt.key);
						const keyIndex = this.keys.indexOf(evt.key);

						if (keyIndex > -1)
							this.keys = this.keys.splice(keyIndex, 1);
					}.bind(this), false);
				}

				get downKeys() {
					return this.keys;
				}
			});

			// misc
			this.ticker = 0;

			// entities etc
			this.player = new Player();

			this.healthBlobs = [];

			setInterval(function() {
				this.healthBlobs.push(new HealthBlob());
			}.bind(this), 100);

			window.requestAnimationFrame(this.draw.bind(this));
		}

		update() {
			this.player.update(this.ticker);

			this.ticker++;
		}

		draw() {
			this.update();

			this.clearCanvas();

			this.context.fillStyle = "black";
			this.context.fillRect(0, 0, window.canvas.width, window.canvas.height);

			this.player.draw(this.context);

			this.healthBlobs.forEach(b => b.draw(this.context));

			window.requestAnimationFrame(this.draw.bind(this));
		}

		clearCanvas() {
			// Store the current transformation matrix
			this.context.save();

			// Use the identity matrix while clearing the canvas
			this.context.setTransform(1, 0, 0, 1, 0, 0);
			this.context.clearRect(0, 0, window.canvas.width, window.canvas.height);

			// Restore the transform
			this.context.restore();
		}
	}

	window.game = new Game();
});
*/