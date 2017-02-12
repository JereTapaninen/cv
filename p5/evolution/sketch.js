var population;
var lifeP;
var target;
var lifespan = 400;
var count = 0;
var maxForce = 0.2;
var mutationChance = 0.01;

var rewardMultiplier = 20;
var failDivider = 200;

var iteration = 0;
var iterationP;

var avgFitnessP;
var avgFitness = 0;

var simulationSpeed = 1;
var simulationSlider;
var simulationSpeedP;

var rx = 100;
var ry = 150;
var rw = 200;
var rh = 10;

function setup() {
	createCanvas(400, 300);

	lifeP = createP();
	iterationP = createP();
	avgFitnessP = createP();
	simulationSpeedP = createP();
	simulationSpeedP.html("simulation speed: " + simulationSpeed);


	simulationSlider = createSlider(1, 6, simulationSpeed, 1);
	simulationSlider.input(function() {
		simulationSpeed = simulationSlider.value();
		simulationSpeedP.html("simulation speed: " + simulationSpeed);
	});

	population = new Population();
	target = createVector(width / 2, 50);
}

function update() {
	if (count >= lifespan) {
		population.evaluate();
		avgFitness = population.averageFitness;
		population.selection();
		count = 0;
		iteration++;
	}

	lifeP.html(count);
	iterationP.html("iteration: " + iteration);
	avgFitnessP.html("average fitness: " + avgFitness);

	population.update();

	count++;
}

function draw() {
	update();

	background(0);

	population.show();

	fill(255);
	rect(rx, ry, rw, rh);

	ellipse(target.x, target.y, 16, 16);
}