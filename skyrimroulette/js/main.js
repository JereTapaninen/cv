const getRandom = function(min, max) {
  return Math.floor((Math.random() * max) + min);
}

const getTimeStamp = function() {
  return window.performance && window.performance.now && 
         window.performance.timing && 
         window.performance.timing.navigationStart ? window.performance.now() + 
         window.performance.timing.navigationStart : Date.now();
}

const Races = [
  "Argonian",
  "High Elf",
  "Dark Elf",
  "Wood Elf",
  "Orc",
  "Nord",
  "Khajiit",
  "Redguard",
  "Breton",
  "Imperial"
];

const Classes = [
  ["Wizard", [
  "Destruction", "Restoration", "Alteration", "Can't use armor", "Focused on Enchanting"]],
  ["Knight", [
  "Heavy Armor", "One-Handed & Blocking", "Two-Handed & Archery", "Can't use magic", "Focused on Smithing"]],
  ["Archer", [
  "Light Armor", "One-Handed", "Archery", "Can't use heavy armor", "Focused on Alchemy"]],
  ["Barbarian", [
  "Two-Handed/Dual-Wielding", "Light Armor", "Archery", "Can't use magic or heavy armor", "Focused on Alchemy"]],
  ["Spellsword", [
  "Conjuration", "One-Handed/Two-Handed", "Destruction", "Can't use non-conjured weapons", "Focused on Enchanting"]],
  ["Shaman", [
  "Restoration", "One-Handed", "Destruction", "Can't use heavy armor", "Focused on Alchemy"]],
  ["Necromancer", [
  "Conjuration", "Destruction", "Alteration", "Can't use restoration", "Focused on Alchemy"]],
  ["Assassin", [
  "Sneak", "Illusion", "One-Handed/Archery", "Can't use heavy armor", "Focused on Enchanting"]],
  ["Paladin", [
  "Heavy Armor", "Restoration", "One-Handed/Two-Handed", "Can't use magic other than Restoration", "Focused on Smithing"]],
  ["Bard", [
  "Illusion", "Restoration", "One-Handed", "Can't use Destruction", "Focused on Enchanting"]]
];

const Followers = [
  "Must be the same class as you",
  "Must be a wizard",
  "Must be a knight",
  "Must be a ranger",
  "Must be a barbarian",
  "Must be a spellsword",
  "Must be the same race as you",
  "Must be an argonian",
  "Must be a khajiit",
  "Must be Lydia"
];

const Factions = [
  "The Companions",
  "Mage's College",
  "Dark Brotherhood",
  "Thieves Guild",
  "Dawnguard",
  "Vampires",
  "Blades/Greybeards (Main Questline)",
  "Imperial Army",
  "Stormcloak Army",
  "Bard's College"
];

class Character {
  constructor() {
    this.race = "";
    this.class = [];
    this.follower = "";
    this.faction = "";
  }

  static create() {
    let nChar = new Character();

    nChar.race = Races[getRandom(0, Races.length)];
    nChar.class = Classes[getRandom(0, Classes.length)];
    nChar.follower = Followers[getRandom(0, Followers.length)];
    nChar.faction = Factions[getRandom(0, Factions.length)];

    return nChar;
  }
}

$(function() {
  $(".rollbtn").click(function() {
    const ch = Character.create();

    $("#race").text(ch.race);
    $("#raceimg").attr("src", "./content/races/" + ch.race.toLowerCase() + ".png");

    $("#class").text(ch.class[0]);

    $("#classrules").text("");

    let classRulesList = $("<ul></ul>");

    ch.class[1].forEach(ci => {
      classRulesList.append($("<li>- " + ci + "</li>"));
    });

    $("#classrules").append(classRulesList);

    $("#classimg").attr("src", "./content/classes/" + ch.class[0].toLowerCase() + ".svg");

    $("#follower").text(ch.follower);
    $("#faction").text(ch.faction);

    $("#container").css("display", "flex");
    $("#rolldicepls").css("display", "none");

    let chText = "SKYRIM ROULETTE CHARACTER\n" +
                 "=====================================\n\n" +
                 "Race: " + ch.race + "\n" +
                 "Class: " + ch.class[0] + "\n\n";

    ch.class[1].forEach(ci => {
      chText += "-" + ci + "\n";
    });

    chText += "\n" +
              "Follower: " + ch.follower + "\n" +
              "Faction: " + ch.faction + "\n" +
              "YOU MUST COMPLETE THIS FACTION FIRST BEFORE ANY OTHER\n\n\n" +
              "-YOU MUST PLAY ON EXPERT-\n" +
              "-DELETE SAVE IF YOU DIE AFTER THE FIRST DRAGON-";

    $("#dlLink").attr("href", "data:text/plain;charset=utf-8," + chText);
    $("#dlLink").attr("download", "character" + getTimeStamp() + ".txt");
  });
});