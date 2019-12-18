using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DnD_Toolkit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //DICE ROLL SIMULATION
        private void NumberRoll(int maxRange)

        {
            //generate a random number between 1 and the max range
            Random rand = new Random();
            var result = rand.Next(1, maxRange + 1);

            //add bonus to roll if any
            int.TryParse(userBonusTextBox.Text, out int userBonus);
            result += userBonus;

            //show the number rolled to the user
            resultLabel.Text = result.ToString();

            //if user wants to roll more than one die at once...
            if (numericUpDown1.Value != 1)
            {
                //roll specified dice the selected number of times, adding the result each time
                for (int i = 1; i < numericUpDown1.Value; i++)
                {
                    result += rand.Next(1, maxRange + 1);
                }
                resultLabel.Text = result.ToString();
            }
        }

        private void D20Button_Click(object sender, EventArgs e)
        {
            NumberRoll(20);
        }
        private void D12Button_Click(object sender, EventArgs e)
        {
            NumberRoll(12);
        }
        private void D10Button_Click(object sender, EventArgs e)
        {
            NumberRoll(10);
        }
        private void D8Button_Click(object sender, EventArgs e)
        {
            NumberRoll(8);
        }
        private void D6Button_Click(object sender, EventArgs e)
        {
            NumberRoll(6);
        }
        private void D4Button_Click(object sender, EventArgs e)
        {
            NumberRoll(4);
        }

        //--------------------------------------------------------------------------------------------------------------

        //TURN ORDER TRACKER
        struct CharTurn
        {
            public string name;
            public int initiative;
        }
        List<CharTurn> turnOrder = new List<CharTurn>();
        private void TurnOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                //takes input from user
                string line = initiativeTextBox.Text;

                //splits line and assigns data to new CharTurn object
                var result = line.Split(' ');
                CharTurn charTurn = new CharTurn { name = result[0], initiative = int.Parse(result[1]) };

                //adds input to list and returns a copy of the list sorted by initiative
                turnOrder.Add(charTurn);
                var results = turnOrder.OrderByDescending(i => i.initiative);

                //Clears listbox of previous data
                initiativeListBox.Items.Clear();

                //show sorted list in listbox
                foreach (var item in results)
                {
                    initiativeListBox.Items.Add($"{item.name} - {item.initiative}");
                }
            }
            catch (Exception) { }

            //clear text and refocus for additional input
            initiativeTextBox.Focus();
            initiativeTextBox.Text = "";
        }
        private void ClearTOButton_Click(object sender, EventArgs e)
        {
            //clears listbox
            initiativeListBox.Items.Clear();

            //clears List itself to prevent adding old data to the listbox when cleared
            turnOrder.Clear();
        }

        //--------------------------------------------------------------------------------------------------------------

        //TAB 1 -- PLAYER COMBAT ACTIONS
        private void AttackLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "1. Choose a target. Pick a target within your attack's " +
                "range: a creature, an object, or a location.\r\n" +
                "\r\n2. Determine modifiers. The DM determines whether the target has cover and whether you have advantage " +
                "or disadvantage against the target. In addition, spells, special abilities and other effects can apply penalties " +
                "or bonuses to your attack roll.\r\n" +
                "\r\n3. Resolve the attack. You make the attack roll. On a hit, you roll damage, unless" +
                " the particular attack has rules that specify otherwise. Some attacks cause special effects in addition to or instead of damage.";
        }
        private void CastSpellLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "Spellcasters such as wizards and clerics, as well as many monsters, have access " +
                 "to spells and can use them to great effect in combat. Each spell has a casting time, range, components, and duration.";
        }
        private void DashLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "When you take the Dash action, you gain extra movement for the current turn. The increase equals " +
                "your speed, after applying any modiliers.\r\n\r\nWith a speed of 30 feet, for example, you can move up to 60 feet on your turn if you dash.";
        }
        private void DisengageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "If you take the Disengage action, your movement doesn't provoke opportunity attacks for the rest of the turn.";
        }
        private void DodgeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "When you take the Dodge action, you focus entirely on avoiding attacks. Until the start of your next turn, any " +
                "attack roll made against you has disadvantage if you can see the attacker, and you make Dexterity saving " +
                "throws with advantage. You lose this benefit if you are incapacitated or if your speed drops to O.";
        }
        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "You can lend your aid to another creature in the completion of a task. When you take the Help action," +
                " the creature you aid gains advantage on the next ability check it makes to perform the task you are helping with, " +
                "provided that it makes the check before the start of your next turn. " +
                "\r\n\r\nFor example, you can aid an ally in attacking a creature within 5 feet of you. You feint, distract the target," +
                " or in some other way team up to make your ally's attack more effective. If your ally attacks the target before your next turn, " +
                "the first attack roll is made with advantage.";
        }
        private void HideLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "Hiding in combat is tricky and its rulings are inconsistent. The DM decides when circumstances are appropriate for hiding.\r\n\r\n" +
                "To hide, a creature must be at least heavily obscured and must pass a Stealth check vs the Passive Perception of the enemies(10 + wisdom mod). " +
                "If the creature leaves the cover that allows to Stealth they are auto - detected, otherwise they are not detected until they reveal themselves. Like after " +
                "firing a ranged weapon from cover." +
                "\r\n\r\nWhen a creature can't see you, you have advantage on attack rolls against it.";
        }
        private void SearchLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "When you take the Search action, you devote your attention to finding something. " +
                "Depending on the nature of your search, the DM might have you make a Wisdom (Perception) check or " +
                "an Intelligence (Investigation) check.";
        }
        private void UseObjectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "When an object requires your action for its use, you take the Use an Object action. " +
                "This action is also useful when you want to interact with more than one object on your turn." +
                "\r\n\r\n For example, the enemy’s at the gates! You need to operate the drawbridge wheel to keep them from getting in. " +
                "Or...you’re in the midst of combat and you’re at death’s door. You might choose to use your turn to drink a healing potion." +
                "\r\n\r\nIf the object is minor enough the DM will likely say it is incorporated into your movement etc. You could also just wing it and say " +
                "the character performing the deed is using their 'bonus' action for the turn";
        }
        private void RdyActionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            infoTextBox.Text = "The Ready action lets you act using your reaction before the start of your next turn. " +
                "First, you decide what perceivable circumstance will trigger your reaction. Then, you choose the action you will take in " +
                "response to that trigger, or you choose to move up to your speed in response to it." +
                "\r\n\r\nExamples include “If the cultist steps on the trapdoor, I’ll pull the lever that opens it,” and " +
                "“If the goblin steps next to me, I move away.”" +
                "\r\n\r\nYou can also ready a spell with the 'Ready' action. To be readied, a spell must have a casting time of 1 action, and holding " +
                "onto the spell’s magic requires concentration. If your concentration is broken, the spell dissipates " +
                "without taking effect.";
        }

        //--------------------------------------------------------------------------------------------------------------

        //TAB 2 -- PLAYER SKILLS
        private void PlayerSkillsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (playerSkillsListBox.SelectedItem.ToString())
            {
                case "Athletics":
                    infoTextBox.Text = "Your Strength (Athletics) check covers difficult situations you encounter while climbing, jumping, or swimming.";
                    break;

                case "Other Strength Checks":
                    infoTextBox.Text = "The DM might also call for a Strength check when you try to accomplish tasks like the following:" +
                        "\r\n\r\n• Force open a stuck, locked, or barred door" +
                        "\r\n• Break free of bonds " +
                        "\r\n• Push through a tunnel that is too small " +
                        "\r\n• Hang on to a wagon while being dragged behind it" +
                        "\r\n• Tip over a statue " +
                        "\r\n• Keep a boulder from rolling";
                    break;

                case "Acrobatics":
                    infoTextBox.Text = "Your Dexterity (Acrobatics) check covers your attempt to stay on your feet in a tricky situation, such " +
                        "as when you’re trying to run across a sheet of ice, balance on a tightrope, or stay upright on a rocking ship’s deck. " +
                        "\r\n\r\nThe DM might also call for a Dexterity (Acrobatics) check to see if you can perform acrobatic stunts, including " +
                        "dives, rolls, somersaults, and flips.";
                    break;

                case "Sleight of Hand":
                    infoTextBox.Text = "Whenever you attempt an act of legerdemain or manual trickery, such as planting something " +
                        "on someone else or concealing an object on your person, make a Dexterity (Sleight of Hand) check." +
                        "\r\n\r\nThe DM might also call for a Dexterity (Sleight of Hand) check to determine whether you can lift a coin purse off another " +
                        "person or slip something out of another person’s pocket";
                    break;

                case "Stealth":
                    infoTextBox.Text = "Make a Dexterity (Stealth) check when you attempt to conceal yourself from enemies, slink past" +
                        " guards, slip away without being noticed, or sneak up on someone without being seen or heard.";
                    break;

                case "Other Dexterity Checks":
                    infoTextBox.Text = "The DM might call for a Dexterity check when you try to accomplish tasks like the following: " +
                        "\r\n\r\n• Control a heavily laden cart on a steep descent " +
                        "\r\n• Steer a chariot around a tight turn" +
                        "\r\n• Pick a lock" +
                        "\r\n• Disable a trap" +
                        "\r\n• Securely tie up a prisoner" +
                        "\r\n• Wriggle free of bonds" +
                        "\r\n• Play a stringed instrument" +
                        "\r\n• Craft a small or detailed object";
                    break;

                case "Constitution Checks":
                    infoTextBox.Text = "The DM might call for a Constitution check when you try to accomplish tasks like the following:" +
                        "\r\n\r\n• Hold your breath " +
                        "\r\n• March or labor for hours without rest" +
                        "\r\n• Go without sleep" +
                        "\r\n• Survive without food or water" +
                        "\r\n• Quaff an entire stein of ale in one go";
                    break;

                case "Arcana":
                    infoTextBox.Text = "Your Intelligence (Arcana) check measures your ability to recall lore about spells, magic items, " +
                        "eldritch symbols, magical traditions, the planes of existence, and the inhabitants of those planes.";
                    break;

                case "History":
                    infoTextBox.Text = "Your Intelligence (History) check measures your ability to recall lore about historical events, " +
                        "legendary people, ancient kingdoms, past disputes, recent wars, and lost civilizations.";
                    break;

                case "Investigation":
                    infoTextBox.Text = "When you look around for clues and make deductions based on those clues, you make an Intelligence (Investigation)" +
                        " check.You might deduce the location of a hidden object, discern from the appearance of a wound what kind of weapon dealt it, or " +
                        "determine the weakest point in a tunnel that could cause it to collapse. Poring through ancient scrolls in search of a hidden " +
                        "fragment of knowledge might also call for an Intelligence (Investigation) check." +
                        "\r\n\r\nNote: This skill has some overlap with Investigation. If you are trying to find/sense clues use perception (spotting faded carvings on a" +
                        " stone archway). If you are piecing these clues together to deduce new knowledge then use Investigation (study these carvings to notice they're " +
                        "an archaic map).";
                    break;

                case "Nature":
                    infoTextBox.Text = "Your Intelligence (Nature) check measures your ability to recall lore about terrain, plants and animals, the weather," +
                        " and natural cycles.";
                    break;

                case "Religion":
                    infoTextBox.Text = "Your Intelligence (Religion) check measures your ability to recall lore about deities, rites and prayers, religious hierarch";
                    break;

                case "Other Intelligence Checks":
                    infoTextBox.Text = "The DM might call for an Intelligence check when you try to accomplish tasks like the following:" +
                        "\r\n\r\n• Communicate with a creature without using words" +
                        "\r\n• Estimate the value of a precious item" +
                        "\r\n• Pull together a disguise to pass as a city guard" +
                        "\r\n• Forge a document" +
                        "\r\n• Recall lore about a craft or trade" +
                        "\r\n• Win a game of skill";
                    break;

                case "Animal Handling":
                    infoTextBox.Text = "When there is any question whether you can calm down a domesticated animal, keep a mount from getting spooked, " +
                        "or intuit an animal’s intentions, the DM might call for a Wisdom (Animal Handling) check. You also make a Wisdom (Animal Handling)" +
                        " check to control your mount when you attempt a risky maneuver.";
                    break;

                case "Insight":
                    infoTextBox.Text = "Your Wisdom (Insight) check decides whether you can determine the true intentions of a creature, such " +
                        "as when searching out a lie or predicting someone’s next move. Doing so involves gleaning clues from body language, speech habits, " +
                        "and changes in mannerisms.";
                    break;

                case "Medicine":
                    infoTextBox.Text = "A Wisdom (Medicine) check lets you try to stabilize a dying companion or diagnose an illness." +
                        "\r\n\r\nIf someone is at 0 hit points you can make a DC10 Medicine skill check on them. If you are successful they become stabilized but remain" +
                        " knocked out at 0 hit points.";
                    break;

                case "Perception":
                    infoTextBox.Text = "Your Wisdom (Perception) check lets you spot, hear, or otherwise detect the presence of something. It measures your" +
                        " general awareness of your surroundings and the keenness of your senses. Perception determines your ability to detect something unknown to you." +
                        "\r\n\r\nNote: This skill has some overlap with Investigation. If you are trying to find/sense clues use perception (spotting faded carvings on a" +
                        " stone archway). If you are piecing these clues together to deduce new knowledge then use Investigation (study these carvings to notice they're " +
                        "an archaic map).";
                    break;

                case "Survival":
                    infoTextBox.Text = "The DM might ask you to make a Wisdom (Survival) check to follow tracks, hunt wild game, guide your group through" +
                        " frozen wastelands, identify signs that owlbears live nearby, predict the weather, or avoid quicksand and other natural hazards.";
                    break;

                case "Other Wisdom Checks":
                    infoTextBox.Text = "The DM might call for a Wisdom check when you try to accomplish tasks like the following:" +
                        "\r\n\r\n• Get a gut feeling about what course of action to follow" +
                        "\r\n• Discern whether a seemingly dead or living creature is undead" +
                        "\r\n• Know when a foe is too strong to fight and ought to be avoided";
                    break;

                case "Deception":
                    infoTextBox.Text = " Your Charisma (Deception) check determines whether you can convincingly hide the truth, either verbally or through" +
                        " your actions. This deception can encompass everything from misleading others through ambiguity to telling outright lies. " +
                        "\r\n\r\nTypical situations include trying to fast-talk a guard, con a merchant, earn money through gambling, pass yourself off in a" +
                        " disguise, dull someone’s suspicions with false assurances, or maintain a straight face while telling a blatant lie.";
                    break;

                case "Intimidation":
                    infoTextBox.Text = "When you attempt to influence someone through overt threats, hostile actions, and physical violence, the DM might" +
                        " ask you to make a Charisma (Intimidation) check." +
                        "\r\n\r\nExamples include trying to pry information out of a prisoner, convincing street thugs to back down from a confrontation," +
                        " or using the edge of a broken bottle to convince a sneering vizier to reconsider a decision.";
                    break;

                case "Performance":
                    infoTextBox.Text = "Your Charisma (Performance) check determines how well you can delight an audience with music, dance, acting, storytelling, " +
                        "or some other form of entertainment";
                    break;

                case "Persuasion":
                    infoTextBox.Text = " When you attempt to influence someone or a group of people with tact, social graces, or good nature, the DM might ask" +
                        " you to make a Charisma (Persuasion) check.Typically, you use persuasion when acting in good faith, to foster friendships, make cordial " +
                        "requests, or exhibit proper etiquette." +
                        "\r\n\r\nExamples of persuading others include convincing a chamberlain to let your party see the king, negotiating peace between " +
                        "warring tribes, or inspiring a crowd of townsfolk.";
                    break;

                case "Other Charisma Checks":
                    infoTextBox.Text = "The DM might call for a Charisma check when you try to accomplish tasks like the following:" +
                        "\r\n\r\n• Find the best person to talk to for news, rumors, and gossip" +
                        "\r\n• Blend into a crowd to get the sense of key topics of conversation";
                    break;

                default:
                    infoTextBox.Text = "";
                    break;
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        //TAB 3 -- KEYWORDS AND SITUATIONS
        private void StatusKeywordListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (keywordListBox.SelectedItem.ToString())
            {
                case "Concentration":
                    infoTextBox.Text = "Some spells require you to maintain concentration in order to keep their magic active. If you lose concentration," +
                        "such a spell ends. You can end concentration at any time (no action required). Normal activity, such as moving and attacking, doesn’t " +
                        "interfere with concentration. The following factors can break concentration:" +
                        "\r\n\r\n• Casting another spell that requires concentration" +
                        "\r\n• Taking damage. Make a Constitution saving throw to maintain your concentration. The DC equals 10 or half the damage you take, " +
                        "whichever number is higher." +
                        "\r\n• Being incapacitated or killed.";
                    break;

                case "Cover":
                    infoTextBox.Text = "Walls, trees, creatures, and other obstacles can provide cover during combat, making a target more difficult to harm." +
                        "\r\n\r\nHalf cover. A target with half cover has a +2 bonus to AC and Dexterity saving throws. A target has half cover if an obstacle " +
                        "blocks at least half of its body. The obstacle might be a low wall, a large piece of furniture, a narrow tree etc..." +
                        "\r\n\r\nThree quarters cover. A target with three-quarters cover has a +5 bonus to AC and Dexterity saving throws. A target has three-quarters " +
                        "cover if about three - quarters of it is covered by an obstacle. The obstacle might be a portcullis, an arrow slit, or a thick tree trunk." +
                        "\r\n\r\nTotal cover. A target with total cover can’t be targeted directly by an attack or a spell, although some spells can reach such a target " +
                        "by including it in an area of effect. A target has total cover if it is completely concealed by an obstacle.";
                    break;

                case "Critical Hit/Miss":
                    infoTextBox.Text = "If you roll a natural 20 you automatically hit even if the enemy’s AC is higher than 20. You roll damage dice twice." +
                        "\r\n\r\nIf the d20 roll for an attack is a 1, the attack misses regardless of any modifiers or the target's AC." +
                        "\r\n\r\nHouse Rule: Deal max damage from your normal attack on a critical hit. Add an additional damage roll." +
                        "\r\n\r\nHouse Rule: Have something funny/bad happen on a critical failure. The player is knocked out of position, falls over, hits " +
                        "a teammate instead etc...";
                    break;

                case "Death Saving Throw":
                    infoTextBox.Text = "Whenever you start your turn with 0 hit points, you must make a special saving throw, called a death saving throw. " +
                        "Roll a d20. If the roll is 10 or higher, you succeed. Otherwise, you fail. A success or failure has no effect by itself. On your " +
                        "third success, you become stable. On your third failure, you die. The successes and failures don’t need to be consecutive." +
                        "\r\n\r\n When you make a death saving throw and roll a 1 on the d20, it counts as two failures. If you roll a 20 on the d20, you regain 1 " +
                        "hit point and are stable.";
                    break;

                case "Grappling":
                    infoTextBox.Text = "When you want to grab a creature or wrestle with it, you can use the Attack action to make a special melee attack, " +
                        "a grapple.\r\n\r\n For this make a grapple check, a Strength (Athletics) check contested by the target’s Strength (Athletics) or Dexterity " +
                        "(Acrobatics) check (the target chooses the ability to use). You succeed automatically if the target is incapacitated. If you succeed, " +
                        "you subject the target to the grappled condition and can move them with you at half speed." +
                        "\r\n\r\n The target can use its action to escape. Target contests with a athletics/acrobatics check versus the grappler's Athletics check.";
                    break;

                case "Knocking a Creature Out":
                    infoTextBox.Text = "Sometimes an attacker wants to incapacitate a foe, rather than deal a killing blow. When an attacker reduces a creature to" +
                        " 0 hit points with a melee attack, the attacker can knock the creature out. The attacker can make this choice the instant the damage is dealt. " +
                        "The creature falls unconscious and is stable." +
                        "\r\n\r\nNote: You cannot choose to do this with a ranged attack.";
                    break;

                case "Opportunity Attack":
                    infoTextBox.Text = "You can make an opportunity attack when a hostile creature that you can see moves out of your reach. To make the opportunity " +
                        "attack, you use your reaction to make one melee attack against the provoking creature. The attack occurs right before the creature leaves your" +
                        " reach." +
                        "\r\n\r\nNote: You cannot make an opportunity attack with a ranged weapon attack.";
                    break;

                case "Proficiency":
                    infoTextBox.Text = "A Proficiency Bonus is numerical representation of your character's skill. If your character is proficient with a weapon, spell, " +
                        "ability check, or saving throw that means you add your Proficiency Bonus to any of these rolls." +
                        "\r\n\r\nYour bonus starts at 2 and will increase as you reach certain levels.";
                    break;

                case "Ranged Atk in Close Combat":
                    infoTextBox.Text = "Aiming a ranged attack is more difficult when a foe is next to you. When you make a ranged attack with a weapon, a spell," +
                        " or some other means, you have disadvantage on the attack roll if you are within 5 feet of a hostile creature who can see you and who " +
                        "isn’t incapacitated.";
                    break;

                case "Resistance & Vulnerability":
                    infoTextBox.Text = "Some creatures and objects are exceedingly difficult or unusually easy to hurt with certain types of damage. " +
                        "\r\n\r\nIf a creature or an object has resistance to a damage type, damage of that type is halved against it (Shooting fire at a lava monster)." +
                        "\r\n\r\nIf a creature or an object has vulnerability to a damage type, damage of that type is doubled against it (Shooting fire at a snowman).";
                    break;

                case "Resting":
                    infoTextBox.Text = "A short rest is a period of downtime, at least 1 hour long, during which a character does nothing more strenuous than eating," +
                        " tending to wounds etc. A character can spend one or more Hit Dice at the end of a short rest, up to the character’s maximum" +
                        " (which is = the character’s level). The player rolls their hit die and adds the " +
                        "character’s Constitution modifier to it. The character regains hit points equal to the total (minimum of 0). The player can decide to spend an " +
                        "additional Hit Die after each roll." +
                        "\r\n\r\nA long rest is a period of extended downtime, at least 8 hours long, during which a character sleeps for at least 6 hours and performs" +
                        " no more than 2 hours of light activity, such as reading, talking, eating, or standing watch. If the rest is interrupted by a period of strenuous" +
                        " activity or similar adventuring activity the characters must begin the rest again to gain any benefit from it. At the end of a long rest," +
                        " a character regains all lost hit points. The character also regains up to half their total Hit Dice.";
                    break;

                case "Two-Weapon Fighting":
                    infoTextBox.Text = "When you take the Attack action and attack with a light melee weapon that you’re holding in one hand, you can use a bonus action " +
                        "to attack with a different light melee weapon that you’re holding in the other hand. You don’t add your ability modifier to the damage of the bonus" +
                        " attack, unless that modifier is negative. " +
                        "\r\n\r\nIf either weapon has the thrown property, you can throw the weapon, instead of making a melee attack with it.";
                    break;

                case "Falling":
                    infoTextBox.Text = "A fall from a great height is one of the most common hazards facing an adventurer. At the end of a fall, a creature takes 1d6 " +
                        "bludgeoning damage for every 10 feet it fell, to a maximum of 20d6. The creature lands prone, unless it avoids taking damage from the fall.";
                    break;

                case "Suffocating":
                    infoTextBox.Text = "A creature can hold its breath for a number of minutes equal to 1 + its Constitution modifier (minimum of 30 seconds).\r\n\r\nWhen a " +
                        "creature runs out of breath or is choking, it can survive for a number of rounds equal to its Constitution modifier (minimum of 1 round). At the" +
                        " start of its next turn, it drops to 0 hit points and is dying, and it can’t regain hit points or be stabilized until it can breathe again.";
                    break;

                case "Difficult Terrain":
                    infoTextBox.Text = "Every foot of movement in difficult terrain costs 1 extra foot. This rule is true even if multiple things in a space count" +
                        " as difficult terrain. Low furniture, rubble, undergrowth, steep stairs, snow, and shallow bogs are examples of difficult terrain.";
                    break;

                case "Improvised Weapons":
                    infoTextBox.Text = "Sometimes characters don’t have their weapons and have to attack with whatever is close at hand. An improvised weapon includes " +
                        "any object you can wield in one or two hands, such as broken glass, a table leg, a frying pan, a wagon wheel, or a dead goblin. In many cases, " +
                        "an improvised weapon is similar to an actual weapon and can be treated as such. For example, a table leg is akin to a club. At the DM’s option," +
                        " a character proficient with a weapon can use a similar object as if it were that weapon and use his or her proficiency bonus. " +
                        "\r\n\r\nAn object that bears no resemblance to a weapon deals 1d4 damage (the DM assigns a damage type appropriate to the object).\r\n\r\nIf a " +
                        "character uses a ranged weapon to make a melee attack, or throws a melee weapon that does not have the thrown property, it also deals " +
                        "1d4 damage. An improvised thrown weapon has a normal range of 20 feet and a long range of 60 feet.";
                    break;

                case "Spell Attack Modifier":
                    infoTextBox.Text = "Spell attack modifier = your proficiency bonus + your Spellcasting Ability Modifier." +
                        "\r\n\r\nFor example, Eric the Cleric has a Wisdom score of 14(+2), and a proficiency bonus of 2 so their spell attack modifier = 4.";
                    break;

                case "Spell Save DC":
                    infoTextBox.Text = "Many spells specify that a target can make a saving throw to avoid some or all of a spell’s effects. The spell specifies the " +
                        "ability that the target uses for the save and what happens on a success or failure. " +
                        "\r\n\r\nThe DC to resist one of your spells equals 8 + your spellcasting ability modifier + your proficiency bonus + any special modifiers.";
                    break;

                case "Spellcasting Ability Modifier":
                    infoTextBox.Text = "Many spells and effects rely on a character's Spellcasting Ability Modifier. This is simply asking for your attribute bonus from" +
                        "the stat your class uses to cast spells. Below is a listing of each class and their spellcasting ability" +
                        "\r\n\r\nAttribute\t\tClass" +
                        "\r\nWisdom\t\tClerics, Druids, and Rangers" +
                        "\r\nIntelligence\tWizards" +
                        "\r\nCharisma\t\tBards, Paladins, Sorcerers, and Warlocks" +
                        "\r\n\r\nFor example, Eric the Cleric has a Wisdom score of 14, so their modifier would be 2.";
                    break;

                case "Underwater Combat":
                    infoTextBox.Text = "Underwater the following rules apply:" +
                        "\r\nWhen making a melee weapon attack, a creature that doesn’t have a swimming speed (either natural or granted by magic) has disadvantage on" +
                        " the attack roll unless the weapon is a dagger, javelin, shortsword, spear, or trident." +
                        "\r\n\r\nA ranged weapon attack automatically misses a target beyond the weapon’s normal range. Even against a target within normal range, the" +
                        " attack roll has disadvantage unless the weapon is a crossbow, a net, or a weapon that is thrown like a javelin (including a spear, trident, " +
                        "or dart)." +
                        "\r\n\r\nCreatures and objects that are fully immersed in water have resistance to fire damage." +
                        "\r\n\r\nWhile swimming, each foot of movement costs 1 extra foot (2 extra feet in difficult terrain), unless a creature has a swimming speed.";
                    break;

                case "Mounted Combat":
                    infoTextBox.Text = "Go google that, good luck champ.";
                    break;

                default:
                    infoTextBox.Text = "";
                    break;
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        //TAB 4 -- STATUS CONDITIONS
        private void ConditionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (conditionsListBox.SelectedItem.ToString())
            {
                case "Blinded":
                    infoTextBox.Text = "• A blinded creature can’t see and automatically fails any ability check that requires sight. " +
                        "\r\n\r\n• Attack rolls against the creature have advantage, and the creature’s attack rolls have disadvantage.";
                    break;

                case "Charmed":
                    infoTextBox.Text = "• A charmed creature can’t attack the charmer or target the charmer with harmful abilities or magical effects. " +
                        "\r\n\r\n• The charmer has advantage on any ability check to interact socially with the creature.";
                    break;

                case "Deafened":
                    infoTextBox.Text = "• A deafened creature can’t hear and automatically fails any ability check that requires hearing.";
                    break;

                case "Frightened":
                    infoTextBox.Text = "• A frightened creature has disadvantage on ability checks and attack rolls while the source of its fear is within line of sight." +
                        "\r\n\r\n• The creature can’t willingly move closer to the source of its fear.";
                    break;

                case "Grappled":
                    infoTextBox.Text = "• A grappled creature’s speed becomes 0, and it can’t benefit from any bonus to its speed. " +
                        "\r\n\r\n• The condition ends if the grappler is incapacitated (see the condition)." +
                        "\r\n\r\n• The condition also ends if an effect removes the grappled creature from the reach of the grappler or grappling effect, such as " +
                        "when a creature is hurled away by the thunderwave spell.";
                    break;

                case "Incapacitated":
                    infoTextBox.Text = "• An incapacitated creature can’t take actions or reactions.";
                    break;

                case "Invisible":
                    infoTextBox.Text = "• An invisible creature is impossible to see without the aid of magic or a special sense. For the purpose of hiding, the creature" +
                        " is heavily obscured. The creature’s location can be detected by any noise it makes or any tracks it leaves. " +
                        "\r\n\r\n• Attack rolls against the creature have disadvantage, and the creature’s attack rolls have advantage.";
                    break;

                case "Paralyzed":
                    infoTextBox.Text = "• A paralyzed creature is incapacitated (see the condition) and can’t move or speak. " +
                        "\r\n\r\n• The creature automatically fails Strength and Dexterity saving throws. " +
                        "\r\n\r\n• Attack rolls against the creature have advantage." +
                        "\r\n\r\n• Any attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
                    break;

                case "Petrified":
                    infoTextBox.Text = "• A petrified creature is transformed, along with any nonmagical object it is wearing or carrying, into a solid inanimate " +
                        "substance (usually stone). Its weight increases by a factor of ten, and it ceases aging. " +
                        "\r\n\r\n• The creature is incapacitated (see the condition), can’t move or speak, and is unaware of its surroundings." +
                        "\r\n\r\n• Attack rolls against the creature have advantage." +
                        "\r\n\r\n• The creature automatically fails Strength and Dexterity saving throws. " +
                        "\r\n\r\n• The creature has resistance to all damage." +
                        "\r\n\r\n• The creature is immune to poison and disease, although a poison or disease already in its system is suspended, not neutralized.";
                    break;

                case "Poisoned":
                    infoTextBox.Text = "• A poisoned creature has disadvantage on attack rolls and ability checks.";
                    break;

                case "Prone":
                    infoTextBox.Text = "• A prone creature’s only movement option is to crawl, unless it stands up and thereby ends the condition." +
                        "\r\n\r\n• The creature has disadvantage on attack rolls." +
                        "\r\n\r\n• An attack roll against the creature has advantage if the attacker is within 5 feet of the creature. Otherwise, the attack roll" +
                        " has disadvantage." +
                        "\r\n\r\nNote: A character may willingly drop prone for no movement cost. To stand up requires half your normal speed. To move while prone, " +
                        "you must crawl or use magic such as teleportation. Every foot of movement while crawling costs 1 extra foot. Crawling 1 foot in difficult " +
                        "terrain, therefore, costs 3 feet of movement.";
                    break;

                case "Restrained":
                    infoTextBox.Text = "• A restrained creature’s speed becomes 0, and it can’t benefit from any bonus to its speed. " +
                        "\r\n\r\n• Attack rolls against the creature have advantage, and the creature’s attack rolls have disadvantage." +
                        "\r\n\r\n• The creature has disadvantage on Dexterity saving throws.";
                    break;

                case "Stunned":
                    infoTextBox.Text = "• A stunned creature is incapacitated (see the condition), can’t move, and can speak only falteringly." +
                        "\r\n\r\n• The creature automatically fails Strength and Dexterity saving throws." +
                        "\r\n\r\n• Attack rolls against the creature have advantage.";
                    break;

                case "Unconscious":
                    infoTextBox.Text = "• An unconscious creature is incapacitated (see the condition), can’t move or speak, and is unaware of its surroundings. " +
                        "\r\n\r\n• The creature drops whatever it’s holding and falls prone." +
                        "\r\n\r\n• The creature automatically fails Strength and Dexterity saving throws." +
                        "\r\n\r\n• Attack rolls against the creature have advantage." +
                        "\r\n\r\n• Any attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.";
                    break;

                case "Exhaustion":
                    infoTextBox.Text = "Some special abilities and environmental hazards, such as starvation and the long-term effects of freezing or scorching " +
                        "temperatures, can lead to a special condition called exhaustion. Exhaustion is measured in six levels. \r\n\r\nLevel\tEffect " +
                        "\r\n1\tDisadvantage on ability checks" +
                        "\r\n2\tSpeed halved" +
                        "\r\n3\tDisadvantage on attack rolls and saving throws" +
                        "\r\n4\tHit point maximum halved" +
                        "\r\n5\tSpeed reduced to 0" +
                        "\r\n6\tDeath" +
                        "\r\n\r\nFinishing a long rest reduces a creature’s exhaustion level by 1, provided that the creature has also ingested some food " +
                        "and drink. Also, being raised from the dead reduces a creature’s exhaustion level by 1.";
                    break;

                default:
                    infoTextBox.Text = "";
                    break;
            }
        }

        //TAB 5 -- Spell List WebBrowser

        private void TabPage5_Selected(object sender, EventArgs e)
        {
            infoTabs.Dock = DockStyle.None;
            infoTabs.Height = 662;
            webBrowser1.Document.Body.Style = "zoom: 90%";
        }
        private void TabPage5_leave(object sender, EventArgs e)
        {
            infoTabs.Dock = DockStyle.Top;
            infoTabs.Height = 313;
        }
    }
}