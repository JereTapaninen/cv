<!DOCTYPE html>
<html lang="en">
  <head>
    <title>Skyrim Roulette</title>
    <meta charset="UTF-8">
    <link rel="stylesheet" type="text/css" href="./style/reset.css" />
    <link href="https://fonts.googleapis.com/css?family=Cinzel" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="./style/main.css" />
    <script type="text/javascript" src="./js/jquery.js"></script>
    <script type="text/javascript" src="./js/main.js"></script>
  </head>

  <body>
    <header>
      <div>
        <img src="./content/skyrim-logo.svg" alt="skyrimlogo" class="logo" />
        <h1>Skyrim Roulette</h1>
        <img src="./content/skyrim-logo.svg" alt="skyrimlogo" class="logo" />
      </div>
    </header>

    <div id="container">
      <main>
        <div class="section">
          <div>
            <p class="sectiontitle">You are a...</p>
            <p id="race">RACE</p>
          </div>
          <img src="" alt="race" id="raceimg" class="sectionicon smallheighticon" />
        </div>

        <div class="section">
          <div>
            <p class="sectiontitle">Your class is...</p>
            <p id="class">CLASS</p>
            <div id="classrules">
            </div>
          </div>
          <img src="" alt="class" id="classimg" class="sectionicon" />
        </div>

        <div class="section">
          <div>
            <p class="sectiontitle">Your follower...</p>
            <p id="follower">FOLLOWER</p>
          </div>
        </div>

        <div class="section">
          <div>
            <p class="sectiontitle">Your first faction will be...</p>
            <p id="faction">FACTION</p>
            <p class="small">You must complete this faction first, before any other!</p>
          </div>
        </div>

        <div class="section" id="info">
          <div>
            <h6>-YOU MUST PLAY ON EXPERT-</h6>
            <div>
              <h6>-DELETE SAVE ON DEATH-</h6>
            </div>
          </div>
        </div>

        <div class="section" id="dl">
          <div>
            <p><a id="dlLink" download>Download file</a></p>
          </div>
        </div>
      </main>
    </div>

    <div id="rolldicepls">
      <h5>Roll the dice to create a new character!</h5>
    </div>

    <footer>
      <div class="rollbtn">
        <img src="./content/rolling-dices.svg" alt="rollingdice" id="dice" />
        Roll
      </div>
      <div class="small">
        &copy; Jere Tapaninen 2017
      </div>
    </footer>
  </body>
</html>