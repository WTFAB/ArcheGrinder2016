using ArcheBuddy.Bot.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ArcheGrinder
{
    public partial class FormMain : Form
    {
        //ArcheGrinder main;

        public bool isFighting = false;
        public bool isLooting = false;

        public Preferences prefs;
        private Core core;

        private Loot loot;
        private Combat combat;
        public string Time()
        {
            string A = DateTime.Now.ToString("[hh:mm:ss] ");
            return A;
        }

        private DateTime combatStart;
        private int xp, kills, purses, tokens, petXp;
        List<uint> purseList, tokenList;

        public static int size { get; private set; }

        public FormMain(ArcheGrinder main)
        {
            InitializeComponent();
            this.FormClosing += FormMain_FormClosing;

            tooltips.SetToolTip(chkFastTag, "Will engage mobs with instant skills to try to win tagging battles");
            tooltips.SetToolTip(chkCrazyEnigmatist, "Will use skill canceling from God's Whip to do a high dps combo, skillbuild: http://archeagedatabase.net/us/calc/328563/");
            tooltips.SetToolTip(chkHealerMode, "Work in Progress");
            tooltips.SetToolTip(chkLoot, "Will attempt to loot close targets even while in group");
            tooltips.SetToolTip(chkOpenPurses, "Will open coinpurses every now and then when you have a few and the labor to do so");
            tooltips.SetToolTip(chkUseCC, "Will attempt to use crowd control skills when aggroed by more than one mob");
            tooltips.SetToolTip(chkAssist, "EXPERIMENTAL: will only assist party leader or retaliate when attacked");

            tooltips.SetToolTip(chkPlayDeadMana, "Will use PlayDead if you have no aggro and are below your % MP that you can set right next to the checkbox");
            tooltips.SetToolTip(chkPlayDeadHP, "Will use PlayDead if you have no aggro and are below your % HP that you can set right next to the checkbox");


            tooltips.SetToolTip(lootCompassion, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootCourage, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootConviction, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootHonor, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootFortitude, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootSacrifice, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootLoyalty, "Will roll on that Hasla token");
            tooltips.SetToolTip(lootPurses, "Will roll on all coinpurses");
            tooltips.SetToolTip(lootDragonChip, "Will roll on Karkasses's Dragon Bone Chips");
            tooltips.SetToolTip(lootUnid, "Will roll on unindentified items");
            tooltips.SetToolTip(lootUnknown, "Will roll on anything that didn't match the other loot options");

            //Auroria Tooltips
            tooltips.SetToolTip(lootDivineClothGear, "Will roll on Divine Cloth Gear, drops from Auroria Mobs");
            tooltips.SetToolTip(lootDivineLeatherGear, "Will roll on Divine Leather, Gear drops from Auroria Mobs");
            tooltips.SetToolTip(lootDivinePlateGear, "Will roll on Divine Plate Gear, drops from Auroria Mobs");
            tooltips.SetToolTip(lootHauntedChest, "Will roll on Haunted Chest, drops from Auroria Mobs");

            //Library Tooltips
            tooltips.SetToolTip(lootEternalLibraryWeapon, "Will roll on Eternal Library Weapon, drops from Library Mobs");
            tooltips.SetToolTip(lootEternalLibraryArmor, "Will roll on Eternal Library Armor, drops from Library Mobs");
            tooltips.SetToolTip(lootEternalLibraryTome, "Will roll on Eternal Library Tome, drops from Library Mobs");
            tooltips.SetToolTip(lootDisciplesTear, "Will roll on Disciple's Tear, drops from Library Mobs");
            tooltips.SetToolTip(lootEnchantedSkein, "Will roll on Enchanted Skein, drops from Library Mobs");

            tooltips.SetToolTip(labelMinHP, "Minimum HP% to engage new mobs");
            tooltips.SetToolTip(labelMinMP, "Minimum Mana% to engage new mobs");
            tooltips.SetToolTip(labelZoneRadius, "How far from where combat was started the bot will look for new targets");
            tooltips.SetToolTip(labelCombatRange, "How close the bot will get to targets before using skills");

            tooltips.SetToolTip(labelFlute, "Which flute should be used when below your 'Min MP%' value");
            tooltips.SetToolTip(labelLute, "Which lute should be used when below your 'Min HP%' value");
            tooltips.SetToolTip(labelPet, "Which pet (battle pet or mount) should be summoned and kept alive while fighting?");

            tooltips.SetToolTip(labelFoodHP, "Enter a HP food item's name to use when you need to regen a lot (Leave empty to not use any)");
            tooltips.SetToolTip(labelFoodMP, "Enter a Mana food item's name to use when you need to regen a lot (Leave empty to not use any)");
            tooltips.SetToolTip(labelPotionHP, "Enter a HP Potion item's name to use in-combat when your health drops low (Leave empty to not use any)");
            tooltips.SetToolTip(labelPotionMP, "Enter a Mana Potion item's name to use in-combat when your mana drops low (Leave empty to not use any)");

            tooltips.SetToolTip(labelPotionCooldown, "How long should the plugin wait between each potion use? (0 for no cooldown)");
            tooltips.SetToolTip(labelFoodCooldown, "How long should the plugin wait between each food use? (0 for no cooldown");


            tooltips.SetToolTip(chkDebugBuffs, "Check forum thread if you need this one");

            UpdateCombatOptions();

            // simulate control+H to put the plugin at the front if it didn't load correctly
            // core_onKeyDown(Keys.H, true, false, false);
        }

        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            core.Log(Time() + "Settings Saved");
            core.onKeyDown -= core_onKeyDown;

            try
            {
                if(loot != null)
                    loot.Stop();
            }
            catch { }

            try
            {
                if(combat != null)
                    combat.Stop();
            }
            catch { }
        }

        

        public void SetCore(Core core)
        {
            this.core = core;

            prefs = new Preferences();
            purseList = new List<uint>() { 29203, 35462, 35469, 29204, 35463, 35470, 29205, 35464, 35471, 29206, 35465, 35472, 29207, 35466, 35473, 35461, 34853, 35474, 34915, 35467, 35476, 32059, 34281, 35475, 34916, 35468, 35477 };
            tokenList = new List<uint>() { 26056, 26055, 35525, 26057, 26053, 26054, 26058, 29612 };

            // scan inventory for flute/lute/pets and add them to the dropdown
            dropdownFlute.Items.Add(new ComboBoxItem("Don't use flute", 0));
            dropdownLute.Items.Add(new ComboBoxItem("Don't use lute", 0));
            dropdownPet.Items.Add(new ComboBoxItem("Don't use a pet", 0));
         

            List<Item> inventory = core.getAllInvItems();
            inventory.AddRange(core.me.getAllEquipedItems());

            foreach (Item item in inventory)
            {
                if (item.weaponType == WeaponType.TubeInstrument)
                    dropdownFlute.Items.Add(new ComboBoxItem(item.name, item.id));

                else if (item.weaponType == WeaponType.StringInstument)
                    dropdownLute.Items.Add(new ComboBoxItem(item.name, item.id));

                else if (item.mountLevel > 0)
                    dropdownPet.Items.Add(new ComboBoxItem(item.name, item.id));
            }

            LoadSettings();
            
            btnCheckPots_Click();

            if (prefs.autoLoot)
                btnLoot_Click();

            if (prefs.autoFight)
                btnCombat_Click();

            core.onKeyDown += core_onKeyDown;
            core.onExpChanged += core_onExpChanged;
            core.onNewInvItem += core_onNewInvItem;
            core.onCreatureDied += core_onCreatureDied;
        }

        private void btnCombat_Click()
        {
            if (!isFighting)
                SaveSettings();

            isFighting = !isFighting;
            btnCombat.Text = isFighting ? "ON" : "OFF";

            UpdateCombatOptions();

            if (isFighting)
            {
                combat = new Combat(core, prefs);
                xp = kills = purses = petXp = tokens = 0;
                combatStart = DateTime.UtcNow;
                UpdateStats();
            }
            else
            {
                UpdateStats(); // update stats a last time to get an accurate value for the session
                if (combat != null)
                {
                    combat.Stop();
                    combat = null;
                }
            }
        }

        void core_onCreatureDied(Creature obj)
        {
            if (obj != null && core != null && obj.firstHitter == core.me)
                kills++;
        }

        void core_onNewInvItem(Item item, int count)
        {
            if (item == null)
                return;
            
            if (purseList.Contains(item.id))
                purses += count;
            else if (tokenList.Contains(item.id))
                tokens += count;

            UpdateStats();
        }

        void core_onExpChanged(Creature obj, int value)
        {
            if (obj == core.me)
                xp += value;
            else if (obj == core.getMount())
                petXp += value;
            else
                return;

            UpdateStats();
        }

        void core_onKeyDown(Keys key, bool isCtrlPressed, bool isShiftPressed, bool isAltPressed)
        {
            if (key == Keys.F1)
                btnLoot_Click();
            else if (key == Keys.F2)
                btnCombat_Click();
            else if (key == Keys.H && isCtrlPressed)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        public void LoadSettings()
        {
            core.Log(Time() + "Loading settings", System.Drawing.Color.Green);
            
            if (File.Exists(Application.StartupPath + ("\\ArcheGrinder"+core.me.name+".xml")))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(Preferences));
                FileStream myFileStream = new FileStream(Application.StartupPath + "\\ArcheGrinder"+core.me.name+".xml", FileMode.Open);

                prefs = (Preferences)mySerializer.Deserialize(myFileStream);
            }

            lootCompassion.Checked = prefs.lootCompassion;
            lootConviction.Checked = prefs.lootConviction;
            lootCourage.Checked = prefs.lootCourage;
            lootFortitude.Checked = prefs.lootFortitude;
            lootHonor.Checked = prefs.lootHonor;
            lootLoyalty.Checked = prefs.lootLoyalty;
            lootSacrifice.Checked = prefs.lootSacrifice;

            lootDivineClothGear.Checked = prefs.lootDivineClothGear;
            lootDivineLeatherGear.Checked = prefs.lootDivineLeatherGear;
            lootDivinePlateGear.Checked = prefs.lootDivinePlateGear;
            lootHauntedChest.Checked = prefs.lootHauntedChest;

            lootEternalLibraryWeapon.Checked = prefs.lootEternalLibraryWeapon;
            lootEternalLibraryArmor.Checked = prefs.lootEternalLibraryArmor;
            lootEternalLibraryTome.Checked = prefs.lootEternalLibraryTome;
            lootAyanad.Checked = prefs.lootAyanad;
            lootDisciplesTear.Checked = prefs.lootDisciplesTear;
            lootEnchantedSkein.Checked = prefs.lootEnchantedSkein;

            lootStolenBag.Checked = prefs.lootStolenBag;
            lootScratchedSafe.Checked = prefs.lootScratchedSafe;

            lootUnknown.Checked = prefs.lootUnknown;

            lootDragonChip.Checked = prefs.lootDragonChip;
            lootUnid.Checked = prefs.lootUnid;
            lootPurses.Checked = prefs.lootPurses;
            lootUnknown.Checked = prefs.lootUnknown;

            chkFastTag.Checked = prefs.fastTagging;
            chkCrazyEnigmatist.Checked = prefs.EnigmatistCombo;
            chkHealerMode.Checked = prefs.healerMode;
            chkUseCC.Checked = prefs.useCC;
            chkLoot.Checked = prefs.lootCorpses;            
            chkAssist.Checked = prefs.assistMode;

            //open Purses
            chkOpenPurses.Checked = prefs.openPurses;
            chkOpenScratchedSafe.Checked = prefs.OpenScratchedSafe;
            chkOpenStolenBag.Checked = prefs.OpenStolenBag;

            chkPlayDeadMana.Checked = prefs.PlayDeadRegMana;
            chkPlayDeadHP.Checked = prefs.PlayDeadRegHP;

            chkAutoCombat.Checked = prefs.autoFight;
            chkAutoLoot.Checked = prefs.autoLoot;

            
                        
            //Buff Monitor Settings
            chkTyrenosIndex.Checked = prefs.UseTyrenosIndex;
            chkGoldenLibraryIndex.Checked = prefs.UseGoldenLibraryIndex;

            chkExperienceGR.Checked = prefs.UseExperienceGR; //GR = Grimoire
            chkFrenzyGR.Checked = prefs.UseFrenzyGR;
            chkHasteGR.Checked = prefs.UseHasteGR;
            chkPromiseGR.Checked = prefs.UsePromiesGR;
            chkWardGR.Checked = prefs.UseWardGR;
            chkZealGR.Checked = prefs.UseZealGR;
            chkGreedyGR.Checked = prefs.UseGreedyGR;

            chkGreedyDwarvenElixir.Checked = prefs.UseGreedyDwarvenElixir;
            chkStudiousDwarvenElixir.Checked = prefs.UseStudiousDwarvenElixir;
            chkBriskDwarvenElixir.Checked = prefs.UseBriskDwarvenElixir;

            chkHonorBoostTonic.Checked = prefs.UseHonorBoostTonic;
            chkVocationExpertiseTonic.Checked = prefs.UseVocationExpertiseTonic;
            chkXPBoostPotion.Checked = prefs.UseXPBoostPotion;
            chkImmortalXPTonic.Checked = prefs.UseImmortalXPTonic;
            chkLuckyQuicksilverTonic.Checked = prefs.UseQuicksilverTonic;
            
            chkSpellbookBrickWall.Checked = prefs.UseSpellbookBrickWall;
            chkSpellbookUnstoppableForce.Checked = prefs.UseSpellbookUnstoppableForce;

            chkKingdomHeart.Checked = prefs.UseKingdomHeart;
            chkAncientLibraryRelic.Checked = prefs.UseAncientLibraryRelic;

            textCombatRange.Text = prefs.combatRange.ToString();
            textZoneRadius.Text = prefs.zoneRadius.ToString();

            textMinMP.Text = prefs.minMP.ToString();
            textMinHP.Text = prefs.minHP.ToString();
            textMinMPplayDead.Text = prefs.MinMPplayDead.ToString();
            textMinHPplayDead.Text = prefs.MinHPplayDead.ToString();

            textPotionCooldown.Text = prefs.potionCooldown.ToString();
            textFoodCooldown.Text = prefs.foodCooldown.ToString();
            textFoodHP.Text = prefs.foodHP;
            textFoodMP.Text = prefs.foodMP;
            textPotionHP.Text = prefs.potionHP;
            textPotionMP.Text = prefs.potionMP;

            boxIgnoreList.Text = "";
            foreach (string mob in prefs.ignoredMobs)
                if(mob.Length > 0)
                    boxIgnoreList.Text += mob + "\n";

            dropdownFlute.SelectedIndex = 0;
            dropdownLute.SelectedIndex = 0;
            dropdownPet.SelectedIndex = 0;

            for (int i = 0; i < dropdownFlute.Items.Count; i++)
            {
                if ((dropdownFlute.Items[i] as ComboBoxItem).Value == prefs.fluteId)
                {
                    dropdownFlute.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < dropdownLute.Items.Count; i++)
            {
                if ((dropdownLute.Items[i] as ComboBoxItem).Value == prefs.luteId)
                {
                    dropdownLute.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < dropdownPet.Items.Count; i++)
            {
                if ((dropdownPet.Items[i] as ComboBoxItem).Value == prefs.petId)
                {
                    dropdownPet.SelectedIndex = i;
                    break;
                }
            }

            chkDebugBuffs.Checked = prefs.debugBuffs;
        }
        public void SaveSettings()
        {
            int combatRange = 0;
            int zoneRadius = 0;
            int minHP = 0;
            int minMP = 0;
            int MinMPplayDead = 0;
            int MinHPplayDead = 0;
            int potionCooldown = 0;
            int foodCooldown = 0;

            try
            {

                prefs.lootCompassion = lootCompassion.Checked;
                prefs.lootConviction = lootConviction.Checked;
                prefs.lootCourage = lootCourage.Checked;
                prefs.lootDragonChip = lootDragonChip.Checked;
                prefs.lootFortitude = lootFortitude.Checked;
                prefs.lootHonor = lootHonor.Checked;
                prefs.lootLoyalty = lootLoyalty.Checked;
                prefs.lootPurses = lootPurses.Checked;
                prefs.lootSacrifice = lootSacrifice.Checked;
                prefs.lootUnid = lootUnid.Checked;
                prefs.lootUnknown = lootUnknown.Checked;

                prefs.lootDivineClothGear = lootDivineClothGear.Checked;
                prefs.lootDivineLeatherGear = lootDivineLeatherGear.Checked;
                prefs.lootDivinePlateGear = lootDivinePlateGear.Checked;
                prefs.lootHauntedChest = lootHauntedChest.Checked;

                prefs.lootEternalLibraryWeapon = lootEternalLibraryWeapon.Checked;
                prefs.lootEternalLibraryArmor = lootEternalLibraryArmor.Checked;
                prefs.lootEternalLibraryTome = lootEternalLibraryTome.Checked;
                prefs.lootAyanad = lootAyanad.Checked;
                prefs.lootEnchantedSkein = lootEnchantedSkein.Checked;
                prefs.lootDisciplesTear = lootDisciplesTear.Checked;             

                prefs.lootStolenBag = lootStolenBag.Checked;
                prefs.lootScratchedSafe = lootScratchedSafe.Checked;

                prefs.OpenStolenBag = chkOpenStolenBag.Checked;
                prefs.OpenScratchedSafe = chkOpenScratchedSafe.Checked;
                prefs.openPurses = chkOpenPurses.Checked;

                prefs.PlayDeadRegMana = chkPlayDeadMana.Checked;
                prefs.PlayDeadRegHP = chkPlayDeadHP.Checked;

                prefs.UseTyrenosIndex = chkTyrenosIndex.Checked;
                prefs.UseGoldenLibraryIndex = chkGoldenLibraryIndex.Checked;

                prefs.UseExperienceGR = chkExperienceGR.Checked; //GR = Grimoire
                prefs.UseFrenzyGR = chkFrenzyGR.Checked;
                prefs.UseHasteGR = chkHasteGR.Checked;
                prefs.UsePromiesGR = chkPromiseGR.Checked;
                prefs.UseWardGR = chkWardGR.Checked;
                prefs.UseZealGR = chkZealGR.Checked;
                prefs.UseGreedyGR = chkGreedyGR.Checked;

                prefs.UseGreedyDwarvenElixir = chkGreedyDwarvenElixir.Checked;
                prefs.UseStudiousDwarvenElixir = chkStudiousDwarvenElixir.Checked;
                prefs.UseBriskDwarvenElixir = chkBriskDwarvenElixir.Checked;

                prefs.UseHonorBoostTonic = chkHonorBoostTonic.Checked;
                prefs.UseVocationExpertiseTonic = chkVocationExpertiseTonic.Checked;
                prefs.UseXPBoostPotion = chkXPBoostPotion.Checked;
                prefs.UseImmortalXPTonic = chkImmortalXPTonic.Checked;
                prefs.UseQuicksilverTonic = chkLuckyQuicksilverTonic.Checked;

                prefs.UseSpellbookBrickWall = chkSpellbookBrickWall.Checked;
                prefs.UseSpellbookUnstoppableForce = chkSpellbookUnstoppableForce.Checked;

                prefs.UseKingdomHeart = chkKingdomHeart.Checked;
                prefs.UseAncientLibraryRelic = chkAncientLibraryRelic.Checked;

                int.TryParse(textCombatRange.Text, out combatRange);
                prefs.combatRange = combatRange;
                textCombatRange.Text = prefs.combatRange.ToString();

                int.TryParse(textZoneRadius.Text, out zoneRadius);
                prefs.zoneRadius = zoneRadius;
                textZoneRadius.Text = prefs.zoneRadius.ToString();

                prefs.fastTagging = chkFastTag.Checked;
                prefs.EnigmatistCombo = chkCrazyEnigmatist.Checked;
                prefs.healerMode = chkHealerMode.Checked;
                prefs.useCC = chkUseCC.Checked;
                prefs.lootCorpses = chkLoot.Checked;
                prefs.assistMode = chkAssist.Checked;

                prefs.autoLoot = chkAutoLoot.Checked;
                prefs.autoFight = chkAutoCombat.Checked;

                int.TryParse(textMinHP.Text, out minHP);
                prefs.minHP = minHP > 0 ? minHP : 75;
                textMinHP.Text = prefs.minHP.ToString();

                int.TryParse(textMinMP.Text, out minMP);
                prefs.minMP = minMP > 0 ? minMP : 40;
                textMinMP.Text = prefs.minMP.ToString();

                int.TryParse(textMinMPplayDead.Text, out MinMPplayDead);
                prefs.MinMPplayDead = MinMPplayDead > 0 ? MinMPplayDead: 50;
                textMinMPplayDead.Text = prefs.MinMPplayDead.ToString();

                int.TryParse(textMinHPplayDead.Text, out MinHPplayDead);
                prefs.MinHPplayDead = MinHPplayDead > 0 ? MinHPplayDead : 50;
                textMinHPplayDead.Text = prefs.MinHPplayDead.ToString();

                prefs.ignoredMobs = (boxIgnoreList.Text + "\n").Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

                int.TryParse(textPotionCooldown.Text, out potionCooldown);
                prefs.potionCooldown = potionCooldown > 0 ? potionCooldown : 60;
                textPotionCooldown.Text = prefs.potionCooldown.ToString();

                int.TryParse(textFoodCooldown.Text, out foodCooldown);
                prefs.foodCooldown = foodCooldown > 0 ? foodCooldown : 60;
                textFoodCooldown.Text = prefs.foodCooldown.ToString();

                prefs.potionHP = textPotionHP.Text;
                prefs.potionMP = textPotionMP.Text;
                prefs.foodHP = textFoodHP.Text;
                prefs.foodMP = textFoodMP.Text;

                prefs.debugBuffs = chkDebugBuffs.Checked;

                try
                {
                    prefs.fluteId = (dropdownFlute.SelectedItem as ComboBoxItem).Value;
                    prefs.luteId = (dropdownLute.SelectedItem as ComboBoxItem).Value;
                    prefs.petId = (dropdownPet.SelectedItem as ComboBoxItem).Value;
                }
                catch
                {
                    core.Log(Time() + "Failed parsing flute/lute/pet values, resetting these to Don't use");
                    prefs.fluteId = prefs.luteId = prefs.petId = 0;
                }

                core.Log(Time() + "Saving settings");
                try
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(Preferences));
                   
                    StreamWriter myWriter = new StreamWriter(Application.StartupPath + "\\ArcheGrinder"+core.me.name+".xml");
                    mySerializer.Serialize(myWriter, prefs);
                    myWriter.Close();
                }
                catch (IOException)
                {
                    core.Log(Time() + "ArcheGrinder" + core.me.name+".xml is already opened, settings saving failed");
                }
                catch (Exception e)
                {
                    core.Log(Time() + e.ToString());
                }
            }
            catch (Exception e)
            {
                core.Log(Time() + "Failed saving settings: " + e.ToString());
            }
        }

        private void btnLoot_Click(object sender = null, EventArgs e = null)
        {
            if(!isLooting)
                SaveSettings();

            isLooting = !isLooting;
            btnLoot.Text = isLooting ? "ON" : "OFF";

            UpdateLootOptions();

            if (isLooting)
            {
                // start loot thread
                if (loot == null)
                {
                    loot = new Loot(core, prefs);
                }
            }
            else
            {
                // stop loot thread
                if (loot != null)
                {
                    loot.Stop();
                    loot = null;
                }
            }
        }

        private void btnCombat_Click(object sender, EventArgs e)
        {
            if (!isFighting)
                SaveSettings();

            isFighting = !isFighting;
            btnCombat.Text = isFighting ? "ON" : "OFF";

            UpdateCombatOptions();

            if (isFighting)
            {
                combat = new Combat(core, prefs);
                xp = kills = purses = petXp = tokens = 0;
                combatStart = DateTime.UtcNow;
                UpdateStats();
            }
            else
            {
                UpdateStats(); // update stats a last time to get an accurate value for the session
                if (combat != null)
                {
                    combat.Stop();
                    combat = null;
                }
            }
        }

        private void btnCheckPots_Click(object sender = null, EventArgs e = null)
        {
            labelQtyFoodHP.Text = labelQtyFoodMP.Text = labelQtyPotionHP.Text = labelQtyPotionMP.Text = "";

            List<Item> inventory = core.getAllInvItems();
            int foodHP = 0, foodMP = 0, potionHP = 0, potionMP = 0;
            foreach (Item item in inventory)
            {
                if (item.name.Length <= 1)
                    continue;

                if (item.name.Equals(textPotionHP.Text, StringComparison.CurrentCultureIgnoreCase))
                    potionHP += item.count;
                else if (item.name.Equals(textPotionMP.Text, StringComparison.CurrentCultureIgnoreCase))
                    potionMP += item.count;
                else if (item.name.Equals(textFoodHP.Text, StringComparison.CurrentCultureIgnoreCase))
                    foodHP += item.count;
                else if (item.name.Equals(textFoodMP.Text, StringComparison.CurrentCultureIgnoreCase))
                    foodMP += item.count;
            }

            labelQtyFoodHP.Text = foodHP + " left";
            labelQtyFoodMP.Text = foodMP + " left";
            labelQtyPotionHP.Text = potionHP + " left";
            labelQtyPotionMP.Text = potionMP + " left";
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            //main.isFormOpen = false;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            
            this.Text = "ArcheGrinder[Alpha] 1.0.1.4 | Current User: " + core.me.name;
            try { 
            this.Icon                                       = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\archeageicon.ico");
            this.pictureLibraryRelic.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\AncientLibraryRelic.jpg");
            this.pictureBriskDwarven.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\BriskDwarvenElixir.jpg");
            this.pictureLibraryIndex.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\GoldenLibraryIndex.jpg");
            this.pictureGreedyDwarven.Image                 = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\GreedyDwarvenElixir.jpg");
            this.pictureHonorBoost.Image                    = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\HonorBoostTonic.jpg");
            this.pictureImmortalXPTonic.Image               = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ImmortalXPTonic.jpg");
            this.pictureKingdomsHeart.Image                 = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\Kingdom'sHeart.jpg");
            this.pictureLuckyQuicksilverTonic.Image         = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\LuckyQuicksilverTonic.jpg");
            this.pictureBrickwall.Image                     = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\SpellbookBrickWall.jpg");
            this.pictureUnstoppableForce.Image              = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\SpellbookUnstoppableForce.jpg");
            this.pictureStudiousDwarven.Image               = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\StudiousDwarvenElixir.jpg");
            this.pictureTyrenosIndex.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\TyrenosIndex.jpg");
            this.pictureExpertiseTonic.Image                = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\VocationExpertiseTonic.jpg");
            this.pictureXPBoostPotion.Image                 = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\XPBoostPotion.jpg");
            this.pictureCompassion.Image                    = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedCompassionToken.jpg");
            this.pictureHonor.Image                         = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedHonorToken.jpg");
            this.pictureFortitude.Image                     = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedFortitudeToken.jpg");
            this.pictureConviction.Image                    = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedConvictionToken.jpg");
            this.pictureSacrifice.Image                     = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedSacrificeToken.jpg");
            this.pictureCourage.Image                       = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedCourageToken.jpg");
            this.pictureLoyalty.Image                       = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FadedLoyaltyToken.jpg");
            this.pictureDisciplesTear.Image                 = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\DisciplesTear.PNG");
            this.pictureHauntedChest.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\HauntedChest.jpg");
            this.pictureDivineClothGear.Image               = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ArmorIcon.PNG");
            this.pictureDivineLeatherGear.Image             = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ArmorIcon.PNG");
            this.pictureDivinePlatGear.Image                = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ArmorIcon.PNG");
            this.pictureEternalLibraryTome.Image            = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\EternalLibraryTome.jpg");
            this.pictureEternalLibraryArmor.Image           = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ArmorIcon.PNG");
            this.pictureEternalLibraryWeapon.Image          = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\EternalWeapon.jpg");
            this.pictureEnchantedSkein.Image                = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\EnchantedSkein.PNG");
            this.pictureDesignScrap.Image                   = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\AyanadDesignScrap.png");
            this.pictureUnidentifiedEquipment.Image         = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\Fragezeichen.jpg");
            this.pictureDragonBoneChip.Image                = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\DragonBoneChip.PNG");
            this.pictureUnknownItems.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\Fragezeichen.jpg");
            this.pictureCoinpurses.Image                    = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\Coinpurse.PNG");
            this.pictureStolenBag.Image                     = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\StolenBag.PNG");
            this.pictureScratchedSafe.Image                 = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\scratchedsafe.PNG");
            this.pictureExperienceGR.Image                  = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ExperienceGR.png");
            this.pictureFrenzyGR.Image                      = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\FrenzyGR.png");
            this.pictureHasteGR.Image                       = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\HasteGR.png");
            this.picturePromiseGR.Image                     = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\PromiseGR.png");
            this.pictureWardGR.Image                        = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\WardGR.png");
            this.pictureZealGR.Image                        = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\ZealGR.png");
            this.pictureGreedyGR.Image                      = Image.FromFile(Application.StartupPath + "\\Plugins\\ArcheGrinder\\Bilder\\GreedyGR.png");


            }
            catch { core.Log(Time() + "Error You are Missing The Bilder Folder.This Folder Contians Images ", System.Drawing.Color.OrangeRed);
                core.Log(Time() + "Bilder Folder Contians Images used in ArcheGrinder Please Download It From The Forum link in Help", System.Drawing.Color.OrangeRed);
                core.Log(Time() + "You need to place the Bilder Folder in the ArcheGrinder Plugin Folder with ArcheGrinder.dll ", System.Drawing.Color.OrangeRed);
                core.Log(Time() + " " + "''I will patch this out in the future'' " + "--WTFAB ");
            }
            }

        private void chkCrazyEnigmatist_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void tabMain_Click(object sender, EventArgs e)
        {
            core.Log(Time() + "on loot page page", System.Drawing.Color.Orange);
            FormMain.ActiveForm.Size = new Size(645, 549);
        }

        private void tabBuff_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         
            if (tabControl.TabPages[0] == tabControl.SelectedTab)
            {
               
                //FormMain.
            }
            if (tabControl.TabPages[1] == tabControl.SelectedTab)
            {
                core.Log(Time() + "on combat page", System.Drawing.Color.Orange);
                FormMain.ActiveForm.Size = new Size(645, 549);
                //FormMain.
            }
            if (tabControl.TabPages[2] == tabControl.SelectedTab)
            {
                core.Log(Time() + "on buff page", System.Drawing.Color.Orange);
                FormMain.ActiveForm.Size = new Size(645, 549);
                //FormMain.
            }
            if (tabControl.TabPages[3] == tabControl.SelectedTab)
            {
                core.Log(Time() + "on ignored page", System.Drawing.Color.Orange);
                FormMain.ActiveForm.Size = new Size(645, 549);
                //FormMain.
            }
            if (tabControl.TabPages[4] == tabControl.SelectedTab)
            {
                core.Log(Time() + "on gear page", System.Drawing.Color.Orange);
                FormMain.ActiveForm.Size = new Size(645, 549);
                //FormMain.
            }
            if (tabControl.TabPages[6] == tabControl.SelectedTab)
            {
                core.Log(Time() + "on help page", System.Drawing.Color.Orange);
                FormMain.ActiveForm.Size = new Size(645, 549);
                //FormMain.
            }
            



            //  else            { ActiveForm.Size = new Size(645, 549); }

        }

        private void tabStats_Click(object sender, EventArgs e)
        {

            core.Log(Time() + "on main page", System.Drawing.Color.Orange);
            FormMain.ActiveForm.Size = new Size(527, 210);

        }

        private void chkAssist_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateLootOptions()
        {
            lootCompassion.Enabled = !isLooting;
            lootConviction.Enabled = !isLooting;
            lootCourage.Enabled = !isLooting;
            lootFortitude.Enabled = !isLooting;
            lootHonor.Enabled = !isLooting;
            lootLoyalty.Enabled = !isLooting;
            lootSacrifice.Enabled = !isLooting;


            lootDivineClothGear.Enabled = !isLooting;
            lootDivineLeatherGear.Enabled = !isLooting;
            lootDivinePlateGear.Enabled = !isLooting;
            lootHauntedChest.Enabled = !isLooting;

            lootEternalLibraryWeapon.Enabled = !isLooting;
            lootEternalLibraryArmor.Enabled = !isLooting;
            lootEternalLibraryTome.Enabled = !isLooting;
            lootAyanad.Enabled = !isLooting;
            lootDisciplesTear.Enabled = !isLooting;
            lootEnchantedSkein.Enabled = !isLooting;                        

            lootStolenBag.Enabled = !isLooting;
            lootScratchedSafe.Enabled = !isLooting;
            lootUnknown.Enabled = !isLooting;
                
            lootUnid.Enabled = !isLooting;
            lootPurses.Enabled = !isLooting;
            lootDragonChip.Enabled = !isLooting;
            lootHonor.Enabled = !isLooting;

            chkAutoLoot.Enabled = !isLooting;
        }
        private void UpdateCombatOptions()
        {
            textZoneRadius.Enabled = !isFighting;
            textCombatRange.Enabled = !isFighting;

            boxIgnoreList.Enabled = !isFighting;

            chkFastTag.Enabled = !isFighting;
            chkCrazyEnigmatist.Enabled = false;
            chkHealerMode.Enabled = false;
            chkUseCC.Enabled = !isFighting;
            chkLoot.Enabled = !isFighting;
            chkAssist.Enabled = !isFighting;
            

            chkOpenScratchedSafe.Enabled = !isFighting;
            chkOpenStolenBag.Enabled = !isFighting;
            chkOpenPurses.Enabled = !isFighting;

            chkPlayDeadMana.Enabled = !isFighting;
            chkPlayDeadHP.Enabled = !isFighting;

            chkTyrenosIndex.Enabled = !isFighting;
            chkGoldenLibraryIndex.Enabled = !isFighting;

            chkExperienceGR.Enabled = !isFighting;
            chkFrenzyGR.Enabled = !isFighting;
            chkHasteGR.Enabled = !isFighting;
            chkPromiseGR.Enabled = !isFighting;
            chkWardGR.Enabled = !isFighting;
            chkZealGR.Enabled = !isFighting;
            chkGreedyGR.Enabled = !isFighting;

            chkGreedyDwarvenElixir.Enabled = !isFighting;
            chkStudiousDwarvenElixir.Enabled = !isFighting;
            chkBriskDwarvenElixir.Enabled = !isFighting;

            chkHonorBoostTonic.Enabled = !isFighting;
            chkVocationExpertiseTonic.Enabled = !isFighting;
            chkXPBoostPotion.Enabled = !isFighting;
            chkImmortalXPTonic.Enabled = !isFighting;
            chkLuckyQuicksilverTonic.Enabled = !isFighting;
            
            chkSpellbookBrickWall.Enabled = !isFighting;
            chkSpellbookUnstoppableForce.Enabled = !isFighting;

            chkKingdomHeart.Enabled = !isFighting;
            chkAncientLibraryRelic.Enabled = !isFighting;

            textMinHP.Enabled = !isFighting;
            textMinMP.Enabled = !isFighting;
            textMinMPplayDead.Enabled = !isFighting;
            textMinHPplayDead.Enabled = !isFighting;

            dropdownFlute.Enabled = !isFighting;
            dropdownLute.Enabled = !isFighting;
            dropdownPet.Enabled = !isFighting;

            textFoodHP.Enabled = textFoodMP.Enabled = textPotionHP.Enabled = textPotionMP.Enabled = !isFighting;
            textPotionCooldown.Enabled = textFoodCooldown.Enabled = !isFighting;

            chkAutoCombat.Enabled = !isFighting;

            chkDebugBuffs.Enabled = !isFighting;
        }
        private void UpdateStats()
        {
            double duration = Math.Max(1, (DateTime.UtcNow - combatStart).TotalSeconds);

            double xpT = Math.Round(xp / 1000d, 1);
            double xpH = Math.Round((3.6 * xp) / duration, 1);
            double killsH = Math.Round((3600 * kills) / duration);
            double pursesH = Math.Round((3600 * purses) / duration);
            double tokensH = Math.Round((3600 * tokens) / duration);

            double pet = Math.Round(petXp / 1000d, 1);
            double petH = Math.Round((3.6 * petXp) / duration, 1);

            labelXpTotal.Text = xpT + "k";
            labelXpHour.Text = xpH  + "k";

            labelKillsTotal.Text = kills.ToString();
            labelKillsHour.Text = killsH.ToString();

            labelPurseTotal.Text = purses.ToString();
            labelPurseHour.Text = pursesH.ToString();

            labelTokenHour.Text = tokensH.ToString();
            labelTokenTotal.Text = tokens.ToString();

            labelPetHour.Text = petH + "k";
            labelPetTotal.Text = pet + "k";
        }

        private void linkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.thebuddyforum.com/archebuddy-forum/archebuddy-plugins/plugins-in-development/246220-plugin-ann-return-archegrinder-developer-wtfab.html#post2203416");
        }
        private void linkDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WTFAB/ArcheGrinder2016");
        }


    }

    class ComboBoxItem
    {
        public string Name;
        public uint Value;
        public ComboBoxItem(string name, uint value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
