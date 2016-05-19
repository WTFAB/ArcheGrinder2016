using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheGrinder
{
    public class Preferences
    {
        public bool lootCompassion;
        public bool lootConviction;
        public bool lootCourage;
        public bool lootFortitude;
        public bool lootHonor;
        public bool lootLoyalty;
        public bool lootSacrifice;

        //Auroria Settings
        public bool lootDivineClothGear;
        public bool lootDivineLeatherGear;
        public bool lootDivinePlateGear;
        public bool lootHauntedChest;

        //Library Settings
        public bool lootEternalLibraryWeapon;
        public bool lootEternalLibraryArmor;
        public bool lootEternalLibraryTome;
        public bool lootAyanad;
        public bool lootEnchantedSkein;
        public bool lootDisciplesTear;

        //Open Purses
        public bool OpenStolenBag;
        public bool OpenScratchedSafe;
        public bool openPurses;

        public bool lootScratchedSafe;
        public bool lootStolenBag;

        public bool lootPurses;
        public bool lootUnid;
        public bool lootDragonChip;
        public bool lootUnknown;

        //Play dead to reg mana
        public bool PlayDeadRegMana;

        //Buff Monitor
        public bool UseTyrenosIndex;
        public bool UseGoldenLibraryIndex;

        public bool UseGreedyDwarvenElixir;
        public bool UseStudiousDwarvenElixir;
        public bool UseBriskDwarvenElixir;

        public bool UseHonorBoostTonic;
        public bool UseVocationExpertiseTonic;
        public bool UseXPBoostPotion;
        public bool UseImmortalXPTonic;
        public bool UseQuicksilverTonic;

        public bool UseSpellbookBrickWall;
        public bool UseSpellbookUnstoppableForce;

        public bool UseKingdomHeart;
        public bool UseAncientLibraryRelic;

        public int zoneRadius;
        public int combatRange;
        public bool fastTagging;
        public bool healerMode;
        public bool useCC;
        public bool lootCorpses;

        public bool assistMode;

        public bool useShield;

        public uint luteId;
        public uint fluteId;
        public uint petId;
        public int minHP;
        public int minMP;

        public int MinMPplayDead;

        public string potionHP, potionMP, foodMP, foodHP;
        public int potionCooldown, foodCooldown;

        public bool autoLoot, autoFight;

        public bool debugBuffs;

        public List<string> ignoredMobs;

        public Preferences()
        {
            lootCompassion = false;
            lootConviction = false;
            lootCourage = false;
            lootFortitude = false;
            lootHonor = false;
            lootLoyalty = false;
            lootSacrifice = false;

            //Auroria
            lootDivineClothGear = false;
            lootDivineLeatherGear = false;
            lootDivinePlateGear = false;
            lootHauntedChest = false;

            //Library
            lootEternalLibraryWeapon = false;
            lootEternalLibraryArmor = false;
            lootEternalLibraryTome = false;
            lootAyanad = false;
            lootDisciplesTear = false;
            lootEnchantedSkein = false;
            
            openPurses = false;
            OpenScratchedSafe = false;
            OpenStolenBag = false;

            PlayDeadRegMana = false;

            lootStolenBag = false;
            lootScratchedSafe = false;

            lootPurses = true;
            lootUnid = true;
            lootDragonChip = true;
            lootUnknown = false;

            

            //Buff Monitor Settings
            UseTyrenosIndex = false;
            UseGoldenLibraryIndex = false;

            UseGreedyDwarvenElixir = false;
            UseStudiousDwarvenElixir = false;
            UseBriskDwarvenElixir = false;

            UseHonorBoostTonic = false;
            UseVocationExpertiseTonic = false;
            UseXPBoostPotion = false;
            UseImmortalXPTonic = false;
            UseQuicksilverTonic = false;

            UseSpellbookBrickWall = false;
            UseSpellbookUnstoppableForce = false;

            UseKingdomHeart = false;
            UseAncientLibraryRelic = false;

            luteId = fluteId = petId = 0;

            zoneRadius = 50;
            combatRange = 20;
            fastTagging = true;
            healerMode = false;
            useCC = false;
            lootCorpses = true;
            assistMode = false;

            useShield = false;

            minHP = 75;
            minMP = 40;
            MinMPplayDead = 50;

            potionHP = potionMP = foodMP = foodHP = "";
            potionCooldown = foodCooldown = 60;

            autoLoot = false;
            autoFight = false;

            debugBuffs = false;

            ignoredMobs = new List<string>();
        }
    }


}
