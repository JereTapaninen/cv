class Rocket {
	constructor(dna) {
		this.position = createVector(width / 2, height - 30);
		this.velocity = createVector();
		this.acceleration = createVector();

		this.crashCount = 0;
		this.completeCount = 0;

		this.completed = false;
		this.crashed = false;

		if (typeof dna != "undefined")
			this.dna = dna;
		else
			this.dna = new DNA();

		this.fitness = 0;
	}

	applyForce(force) {
		this.acceleration.add(force);
	}

	calculateFitness() {
		const d = dist(this.position.x, this.position.y, target.x, target.y);

		this.fitness = map(d, 0, width, width, 0);

		if (this.completed) {
			this.fitness += this.completeCount / 100;
			this.fitness *= rewardMultiplier;
		}

		if (this.crashed) {
			var old = this.fitness;
			this.fitness /= failDivider;
			//this.fitness -= (-((count / 2) / 100) + (count / 2) / 100);
			// crashcount = 350
			// fitness = 20
			// lifespan = 400
			this.fitness += ((lifespan - this.crashCount) / ((lifespan - this.crashCount) / 2));
		}
	}

	update() {
		const d = dist(this.position.x, this.position.y, target.x, target.y);

		if (d < 10) {
			this.completed = true;
			this.position = target.copy();
		}

		if (this.completed && this.completeCount == 0) {
			this.completeCount = count;
		}

		if (this.position.x > rx && this.position.x < rx + rw &&
				this.position.y > ry && this.position.y < ry + rh) {
			this.crashed = true;
		} else if (this.position.x > width || this.position.x < 0) {
			this.crashed = true;
		} else if (this.position.y > height || this.position.y < 0) {
			this.crashed = true;
		}

		if (this.crashed && this.crashCount == 0)
			this.crashCount = count;

		this.applyForce(this.dna.genes[count]);

		if (!this.completed && !this.crashed) {
			this.velocity.add(this.acceleration);
			this.position.add(this.velocity);
			this.acceleration.mult(0);
			this.velocity.limit(4);
		}
	}

	getFitness() {
		return this.fitness;
	}

	show() {
		push();
		noStroke();
		if (this.crashed)
			fill(255, 0, 0, 150);
		else if (this.completed)
			fill(0, 255, 0, 150);
		else
			fill(255, 150);
		translate(this.position.x, this.position.y);
		rotate(this.velocity.heading());
		rectMode(CENTER);
		rect(0, 0, 25, 5);
		pop();
	}
}