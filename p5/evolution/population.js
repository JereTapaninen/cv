class Population {
	constructor() {
		this.rockets = [];
		this.popSize = 25;
		this.matingPool = [];

		for (let i = 0; i < this.popSize; i++) {
			this.rockets[i] = new Rocket();
		}
	}

	evaluate() {
		let maxFit = 0;

		for (let i = 0; i < this.popSize; i++) {
			this.rockets[i].calculateFitness();

			if (this.rockets[i].fitness > maxFit)
				maxFit = this.rockets[i].fitness;
		}

		for (let i = 0; i < this.popSize; i++) {
			if (this.maxFit != 0)
				this.rockets[i].fitness /= maxFit;
		}

		this.matingPool = [];

		for (let i = 0; i < this.popSize; i++) {
			const n = this.rockets[i].fitness * 100;

			for (let j = 0; j < n; j++) {
				this.matingPool.push(this.rockets[i]);
			}
		}
	}

	selection() {
		const newRockets = [];

		for (let i = 0; i < this.rockets.length; i++) {
			// @TODO: Fix this so that parentA and parentB can't be the same
			const parentA = random(this.matingPool).dna;
			const parentB = random(this.matingPool).dna;
			const child = parentA.crossover(parentB);
			child.mutation();
			newRockets[i] = new Rocket(child);
		}

		this.rockets = newRockets;
	}

	update() {
		for (let i = 0; i < this.popSize; i++) {
			this.rockets[i].update();
		}
	}

	show() {
		for (let i = 0; i < this.popSize; i++) {
			this.rockets[i].show();
		}
	}

	get averageFitness() {
		let rocketsAmount = 0;
		let values = 0;

		for (let i = this.rockets.length - 1; i >= 0; i--) {
			rocketsAmount += 1;
			values += this.rockets[i].fitness;
		}

		return values / rocketsAmount;
	}
}