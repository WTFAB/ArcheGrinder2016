using ArcheBuddy.Bot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArcheGrinder
{
    class Loot
    {
        private Thread thread;
        private Preferences prefs;
        private Core core;
        
        public Loot(Core c, Preferences p)
        {
            prefs = p;
            core = c;

            thread = new Thread(LootThread);
            thread.Start();
        }

        public void Stop()
        {
            try
            {
                core.BlockClientDice(false);
                thread.Abort();
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                core.Log(Time() + "Couldn't abort loot thread: " + ex.Message);
            }
        }

        public string Time()
        {
            string A = DateTime.Now.ToString("[hh:mm:ss] ");
            return A;
        }


        public void LootThread()
        {
            Random r = new Random();
            List<uint> purses = new List<uint>() {29203, 29204, 29205, 29206, 29207, 32059, 34915, 34916, 35461 };
            List<uint> StolenBag = new List<uint>() { 34281, 34853, 35462, 35463, 35464, 35465, 35466, 35467, 35468 };
            List<uint> ScratchedSafe = new List<uint>() { 35469, 35470, 35471, 35472, 35473, 35474, 35474, 35475, 35476, 35477 };

            
try
            {
               // core.Log("<Tester> _____________Attempting to log all inv Items_____________", System.Drawing.Color.Red);
                
                
                //string pickup = "";
                //getInvItems(pickup);
                //string inbag = "";
                //core.me.getInvItem(inbag);
                //core.Log("<Tester> " + pickup, System.Drawing.Color.Green);
              //  core.Log("<Tester> _____________This is for Debuging donot be worried_____________", System.Drawing.Color.Red);
               
            }
                catch (ThreadAbortException) { }
            catch (Exception e)
            {
                core.Log(Time() + "Loot Exception: " + e.Message);
            }


            try
            {
                core.BlockClientDice(true);
                while (true)
                {
                    List<Item> rolls = core.me.getDiceItems();
                    foreach (Item item in rolls)
                    {
                        bool doRoll = prefs.lootUnknown;

                        if (purses.Contains(item.id))
                            doRoll = prefs.lootPurses;

                        else if (StolenBag.Contains(item.id))
                            doRoll = prefs.lootStolenBag;

                        else if (ScratchedSafe.Contains(item.id))
                            doRoll = prefs.lootScratchedSafe;

                        else if (item.name.StartsWith("Unidentified"))
                            doRoll = prefs.lootUnid;

                        else if (item.name.StartsWith("Неизвестное ")) //sollte nun auch auf unbekannte items würfeln wenn das Spiel auf russisch ist
                            doRoll = prefs.lootUnid;

                        else if (item.name.StartsWith("Предмет ")) //rollt auf russische unbekannte Rüstungen sie fangen mit nem anderen wort an als die waffen schuhe und co
                            doRoll = prefs.lootUnid;

                        else if (item.name.StartsWith("Unbekannte")) //sollte nun auch auf unbekannte items würfeln wenn das Spiel auf deutsch ist
                            doRoll = prefs.lootUnid;

                        else if (item.id == 26056)
                            doRoll = prefs.lootConviction;

                        else if (item.id == 26055)
                            doRoll = prefs.lootCourage;

                        else if (item.id == 35525)
                            doRoll = prefs.lootCompassion;

                        else if (item.id == 26057)
                            doRoll = prefs.lootFortitude;

                        else if (item.id == 26053)
                            doRoll = prefs.lootHonor;

                        else if (item.id == 26054)
                            doRoll = prefs.lootLoyalty;

                        else if (item.id == 26058)
                            doRoll = prefs.lootSacrifice;

                        else if (item.id == 29612)
                            doRoll = prefs.lootDragonChip;

                        else if (item.id == 32048)
                            doRoll = prefs.lootEternalLibraryWeapon;

                        else if (item.id == 32049)
                            doRoll = prefs.lootEternalLibraryArmor;

                        else if (item.id == 32050)
                            doRoll = prefs.lootEternalLibraryTome;

                        else if (item.id == 32060)
                            doRoll = prefs.lootAyanad;

                        else if (item.id == 27468)
                            doRoll = prefs.lootDivineClothGear;

                        else if (item.id == 27469)
                            doRoll = prefs.lootDivineLeatherGear;

                        else if (item.id == 27470)
                            doRoll = prefs.lootDivinePlateGear;

                        else if (item.id == 32050)
                            doRoll = prefs.lootHauntedChest;

                        else if (item.id == 32062)
                            doRoll = prefs.lootDisciplesTear;

                        else if (item.id == 32061)
                            doRoll = prefs.lootEnchantedSkein;

                        

                        item.Dice(doRoll);

                        if (doRoll)
                            core.Log(Time() + "+ Rolling on : " + item.name + " (" + item.id + ")");
                        else
                            core.Log(Time() + "- Passing on : " + item.name + " (" + item.id + ")");

                        Thread.Sleep(r.Next(1000, 1500));
                    }
                    Thread.Sleep(r.Next(1500, 2500));
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception e)
            {
                core.Log(Time() + "Loot Exception: " + e.Message);
            }
            finally
            {
                core.BlockClientDice(false);
            }
        }

        
    }
}
