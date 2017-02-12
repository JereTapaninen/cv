class DNA {
	constructor(genes) {
		if (typeof genes != "undefined") {
			this.genes = genes;
		} else {
			this.genes = [];

			for (let i = 0; i < lifespan; i++) {
				this.genes[i] = p5.Vector.random2D();
				this.genes[i].setMag(maxForce);
			}
		}
	}

	crossover(partner) {
		const newGenes = [];
		const mid = floor(random(this.genes.length));

		for (let i = this.genes.length - 1; i >= 0; i--) {
			if (i > mid)
				newGenes[i] = this.genes[i];
			else
				newGenes[i] = partner.genes[i];
		}

		return new DNA(newGenes);
	}

	mutation() {
		for (let i = this.genes.length - 1; i >= 0; i--) {
			if (random(1) < mutationChance) {
				this.genes[i] = p5.Vector.random2D();
				this.genes[i].setMag(maxForce);
			}
		}
	}
}