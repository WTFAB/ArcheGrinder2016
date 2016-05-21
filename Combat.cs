using ArcheBuddy.Bot.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArcheGrinder
{
    class Combat
    {
        private Thread thread;
        private Preferences prefs;
        private Core core;

       

        #region Skills
        // Sorcery Zauberei
        const uint _FLAMEBOLT = 10752;
        const uint _FREEZING_ARROW = 10667;
        const uint _INSULATING_LENS = 10153;
        const uint _ARC_LIGHTNING = 10670;
        const uint _Freezing_Earth = 10151;
        const uint _SEARING_RAIN = 11939;
        const uint _FRIGID_TRACKS = 11314;
        const uint _MAGIC_CIRCLE = 12796;
        const uint _CHAIN_LIGHTNING = 11967;
        const uint _FLAME_BARRIER = 14774;
        const uint _METEOR_STRIKE = 10664;
        const uint _GODS_WHIP = 23593;

        // Witchcraft Hexerei
        const uint _EARTHEN_GRIP = 14376;
        const uint _ENERVATE = 10159;
        const uint _BUBBLE_TRAP = 10154;
        const uint _INSIDIOUS_WHISPER = 10409;
        const uint _PURGE = 10712;
        const uint _PLAY_DEAD = 10488;
        const uint _COURAGEOUS_ACTION = 11424;
        const uint _LASSITUDE = 10134;
        const uint _BANSHE_WAIL = 12001;
        const uint _FOCAL_CONCUSSION = 11353;
        const uint _DAHUTAS_BREATH = 11443;
        const uint _FIEND_KNELL = 23588;

        // Auramancy Auramantie
        const uint _THWART = 16486;
        const uint _COMETS_BOON = 18222;
        const uint _CONVERSION_SHIELD = 11869;
        const uint _VICIOUS_IMPLOSION = 10710;
        const uint _TELEPORTATION = 10152;
        const uint _HEALTH_LIFT = 11991;
        const uint _MEDIDATE = 11989;
        const uint _SHRUF_IT_OFF = 11429;
        const uint _LIBERATION = 11380;
        const uint _LEECH = 10104;
        const uint _PROTECTIVE_WINGS = 10714;
        const uint _MIRROR_WARP = 23934;

        // Archery Bogenschießen
        const uint _CHARGED_BOLT = 16210;
        const uint _PIERCING_SHOT = 13564;
        const uint _ENDLESS_ARROWS = 14835;
        const uint _DOUBLE_RECURVE = 11368;
        const uint _DEADEYE = 15073;
        const uint _SNARE = 12133;
        const uint _FLOAT = 10694;
        const uint _BONEYARD = 14760;
        const uint _CONCUSSIVE_ARROW = 11933;
        const uint _MISSILE_RAIN = 13281;
        const uint _INTENSITY = 10708;
        const uint _SNIPE = 23592;

        // Shadowplay Schattenspiel
        const uint _RAPID_STRIKE = 18125;
        const uint _OVERWHELM = 10648;
        const uint _DROP_BACK = 12049;
        const uint _WALLOP = 12029;
        const uint _STALKERS_MARK = 12139;
        const uint _STEALTH = 10082;
        const uint _TOXIC_SHOT = 10481;
        const uint _PIN_DOWN = 13344;
        const uint _SHADOWSMITE = 10496;
        const uint _FREERUNNER = 10189;
        const uint _SHADOW_STEP = 12075;
        const uint _THROW_DAGGER = 23594;

        // Battlerage Kampfeszorn
        const uint _TRIPLE_SLASH = 18132;
        const uint _CHARGE = 11918;
        const uint _WHIRLWIND_SLASH = 13282;
        const uint _SUNDER_EARTH = 10644;
        const uint _LASSO = 12039;
        const uint _TERRIFYING_ROAR = 18308;
        const uint _BOND_BREAKER = 12034;
        const uint _PRECISION_STRIKE = 12026;
        const uint _FRENZY = 10455;
        const uint _BATTLE_FOCUS = 10377;
        const uint _TIGER_STRIKE = 13315;
        const uint _BEHIND_ENEMY_LINES = 23587;


        // Defense Verteidigung
        const uint _SHIELD_SLAM = 10399;
        const uint _REFRESHMENT = 10645;
        const uint _BULL_RUSH = 10501;
        const uint _BOASTFUL_ROAR = 12048;
        const uint _TOUGHEN = 11365;
        const uint _REVITALIZING_CHEER = 12046;
        const uint _REDOUBT = 10375;
        const uint _OLLOS_HAMMER = 18757;
        const uint _MOCKING_HOWL = 10436;
        const uint _IMPRISON = 14529;
        const uint _INVINCIBILITY = 10372;
        const uint _FORTRESS = 23589;

        // Occultism Okkultismus
        const uint _MANA_STARS = 14810;
        const uint _CRIPPLING_MIRE = 10201;
        const uint _HELL_SPEAR = 10135;
        const uint _ABSORB_LIFEFORCE = 11441;
        const uint _SUMMON_CROWS = 11395;
        const uint _MANA_FORCE = 12759;
        const uint _TELEKINESIS = 11504;
        const uint _RETRIBUTION = 10655;
        const uint _STILLNESS = 10665;
        const uint _URGENCY = 11442;
        const uint _SUMMON_WRAITH = 10434;
        const uint _DEAHTS_VENGEANCE = 23591;

        // Songcraft Bardenkunst
        const uint _CRITICAL_DISCORD = 11973;
        const uint _STARTLING_STRAIN = 11934;
        const uint _QUICKSTEP = 10723;
        const uint _DISSONANT = 11943;
        const uint _HEALING_HYMN = 17413;
        const uint _HUMMINGBIRD_DITTY = 11377;
        const uint _ODE_TO_RECOVERY = 10724;
        const uint _RHYTHMIC_RENEWAL = 11948;
        const uint _BULLWARK_BALLAD = 11396;
        const uint _BLOODY_CHANTEY = 10727;
        const uint _ALARM_CALL = 11961;
        const uint _GRIEFS_CADENCE = 23595;

        // Vitalism Vitalismus
        const uint _ANTITHESIS = 10534;
        const uint _MIRROR_LIGHT = 11379;
        const uint _RESURGENCE = 10547;
        const uint _REVIVE = 10546;
        const uint _SKEWER = 13284;
        const uint _MEND = 10720;
        const uint _INFUSE = 16783;
        const uint _ARANZEB_BOON = 16004;
        const uint _RENEWAL = 17412;
        const uint _FERVENT_HEALING = 14929;
        const uint _TWILIGHT = 10721;
        const uint _WHIRLWINDS_BLESSING = 23596;

        // Tyrenos's Index (Library Use Only Item)
        uint _Buff_Tyrenos_Index = 8240;
        uint _Item_Tyrenos_Index = 34242;

        // Golden Library Index (Library Use Only Item only 3min)
        uint _Buff_Golden_Library_Index = 7479;
        uint _Item_Golden_Library_Index = 31778;

        // Greedy Dwarven Elixir (+10% loot drop rate)
        uint _Buff_Greedy_Dwaren_Elixir = 8000022;
        uint _Item_Greedy_Dwaren_Elixir = 8000082;

        // Studious Dwarven Elixir (+10% XP gain)
        uint _Buff_Studious_Dwarven_Elixir = 8000023;
        uint _Item_Studious_Dwarven_Elixir = 8000083;

        // Brisk Dwarven Elixir (+3% movment speed)
        uint _Buff_Brisk_Dwarven_Elixir = 8000024;
        uint _Item_Brisk_Dwarven_Elixir = 8000084;

        // Honor Boost Tonic (Increas Honor Points from hunting, Arena, Conflicts, Sieges, War)
        uint _Buff_Honor_Boost_Tonic = 8000007;
        uint _Item_Honor_Boost_Tonic = 8000017;

        // Vocation Expertise Tonic (Decrase production time -10%, increases Vocation badges earned +10%)
        uint _Buff_Vocation_Expertise_Tonic = 8000010;
        uint _Item_Vocation_Expertise_Tonic = 8000020;

        // XP Boost Potion (+20% XP gain)
        uint _Buff_XP_Boost_Potion = 8000034;
        uint _Item_XP_Boost_Potion = 8000151;

        // Immortal XP Tonic (Prevents XP and durability loss)
        uint _Buff_Immortal_XP_Tonic = 8000008;
        uint _Item_Immortal_XP_Tonic = 8000018;

        // Lucky Quicksilver Tonic (+10% loot drop rate)
        uint _Buff_Lucky_Quicksilver_Tonic = 8000009;
        uint _Item_Lucky_Quicksilver_Tonic = 8000019;

        // Spellbook: Brick Wall
        uint _Buff_Brick_Wall = 7477;
        uint _Item_Brick_Wall = 31776;

        // Spellbook: Unstoppable Force
        uint _Buff_Unstoppable_Force = 7478;
        uint _Item_Unstoppable_Force = 31777;

        // Kingdom's Heart
        uint _Buff_Kingdom_Heart = 674;
        uint _Item_Kingdom_Heart = 8529;

        // Ancient Library Relic
        uint _Buff_Ancient_Library_Relic = 2354;
        uint _Item_Ancient_Library_Relic = 18970;

        // Grimoire's
        uint _Buff_Experience_Grimoire = 14778;
        uint _Item_Experience_Griomire = 8000338;

        uint _Buff_Frenzy_Grimoire = 14778;
        uint _Item_Frenzy_Grimoire = 8000342;

        uint _Buff_Haste_Grimoire = 14784;
        uint _Item_Haste_Grimoire = 8000339;

        uint _Buff_Promise_Grimoire = 14785;
        uint _Item_Promise_Grimoire = 8000340;

        uint _Buff_Ward_Grimoire = 14788;
        uint _Item_Ward_Grimoire = 8000343;

        uint _Buff_Zeal_Grimoire = 14786;
        uint _Item_Zeal_Grimoire = 8000341;

        uint _Buff_Greedy_Grimoire = 14783;
        uint _Item_Greedy_Grimoire = 8000344;

        bool WeakendBody = false;

        public string Time()
        {
            string A = DateTime.Now.ToString("[hh:mm:ss] ");
            return A;
        }

        // Basic
        const uint _MELEE_ATTACK = 2;
        const uint _RANGED_ATTACK = 16064;
        const uint _PLAY_INSTRUMENT = 14900;
        const uint _RAISE_BACK_UP = 13719;
        #endregion


        #region Buffs
        const uint _BF_BUBBLE_TRAP = 96;
        const uint _BF_BUBBLE_TRAP2 = 2286; // combo with flamebolt
        const uint _BF_FOCAL_CONCUSSION = 449;
        const uint _BF_EARTHEN_GRIP = 82;
        const uint _BF_METEOR_IMPACT = 13;
        const uint _BF_TRIPPED = 1579;
        const uint _BF_PATRON = 8000011;
        const uint _BF_RETURNING = 550;

        const uint _BF_HEALTH_LIFT_R3 = 796;
        const uint _BF_INSULATING_LENSE_R5 = 429;
        const uint _Buff_Purge = 375;
        #endregion

        private List<Creature> team;
        private List<uint> skills;
        private List<uint> mobBlacklist;
        private int playerDeaths;

        private RoundZone zone;
        private double startX, startY, startZ;
        private bool hasShield;
        private List<uint> controlDebuffs, debugBuffId;
        private uint failedPetRez;
        private bool usePotionHP, usePotionMP, useFoodHP, useFoodMP;
        private DateTime lastPotionUsed, lastFoodUsed;
        private Gps gps;

        public Combat(Core c, Preferences p)
        {
            prefs = p;
            core = c;

            controlDebuffs = new List<uint>() { _BF_BUBBLE_TRAP, _BF_BUBBLE_TRAP2, _BF_FOCAL_CONCUSSION,
                _BF_EARTHEN_GRIP, _BF_METEOR_IMPACT };
            debugBuffId = new List<uint>();

            thread = new Thread(CombatThread);
            thread.Start();
        }
        public void Stop()
        {
            try
            {
                core.CancelSkill();
                core.CancelMoveTo();

                core.onPartyMemberLeaves -= core_onPartyMemberLeaves;
                core.onNewPartyMember -= core_onPartyMemberLeaves;
                core.onCreatureDied -= core_onCreatureDied;

                thread.Abort();
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                core.Log("Couldn't abort combat thread: " + ex.Message);
            }
        }

        #region Mob targeting
        private bool IsValidTarget(Creature obj, bool fightPlayersBack = false)
        {
            try
            {
                bool isNpc = obj != null && obj.type == BotTypes.Npc;
                //core.Log("isNpc: " + isNpc);
                bool shouldFightBackPlayer = isNpc && obj != null && core != null && (!fightPlayersBack || (obj.type == BotTypes.Player && obj.target == core.me));
                //core.Log("shouldFightBackPlayer: " + shouldFightBackPlayer);
                bool isEnemy = shouldFightBackPlayer && core != null && obj != null && !core.isAlly(obj);
                //core.Log("isEnemy: " + isNpc);
                bool isAlive = isEnemy && core != null && obj != null && core.isAlive(obj);
                //core.Log("isAlive: " + isAlive);
                bool isOurs = isAlive && core != null && obj != null && (obj.firstHitter == null || obj.firstHitter == core.me || team.Contains(obj.firstHitter));
                //core.Log("isOurs: " + isNpc);
                bool isFreshOrAggro = isOurs && core != null && obj != null && (core.hpp(obj) == 100 || obj.aggroTarget == core.me || team.Contains(obj.aggroTarget));
                //core.Log("isFreshOrAggro: " + isNpc);
                bool isNotReturning = isFreshOrAggro && core != null && obj != null && core.getBuff(obj, _BF_RETURNING) == null;
                //core.Log("isNotReturning: " + isNotReturning);

                return isNotReturning && !prefs.ignoredMobs.Contains(obj.name);
            }
            catch (Exception e)
            {
                core.Log("Error while validating target: " + e.Message);
                return false;
            }
        }
        private Creature GetBestNearestMob(bool assistPriority = false, bool fightPlayersBack = false)
        {
            Creature mob = null;
            double smallestDist = double.MaxValue;
            bool isBestMobFresh = false;
            try
            {
                List<Creature> nearbyMobs = core.getCreatures();
                foreach (Creature obj in nearbyMobs)
                {
                    bool isFresh = (core.hpp(obj) == 100);
                    double dist = core.me.dist(obj);
                    bool isOnPlayer = obj.target == core.me;

                    if (IsValidTarget(obj, fightPlayersBack)
                        && (zone.ObjInZone(obj) || (isOnPlayer && dist < prefs.combatRange - 1))
                        && (dist < smallestDist || (assistPriority && !isBestMobFresh && isFresh))
                        && (!assistPriority || isFresh || isOnPlayer || (assistPriority && !isBestMobFresh && dist < prefs.combatRange))
                    )
                    {
                        // just return directly if we find something attacking us - we want to take that down for sure
                        if (obj.target == core.me)
                            return obj;

                        // second check to see if the creatures target is one of our party members (do we have healers/assistants?)
                        if (core.me.isPartyMember)
                        {
                            List<Creature> PartyMems = core.getVisiblePartyMembers();
                            if (PartyMems.Contains(obj.target)) { return obj; }
                        }

                        mob = obj;
                        smallestDist = dist;
                        isBestMobFresh = isFresh;
                    }
                }
            }
            catch (Exception ex)
            {
                core.Log("Error while looking for a new target: " + ex.Message);
            }

            return mob;
        
    }
        private Creature AssistPartyLeader(bool assistPriority = false, bool fightPlayersBack = false)
        {
            Creature leader = core.getPartyLeaderObj();
            if (core.me == leader)
            { core.Log("[ALERT] You need to pass lead if running in Assist Mode!"); Thread.Sleep(1000); return null; }
            if (core.isInParty() == false)
            { core.Log("[ALERT] "+core.me.name+ " need to be in party to use the Assist Mode!"); Thread.Sleep(1000); return null; }
            List<String> mems = new List<String>();
            foreach (PartyMember mem in core.getPartyMembers())
            {
                mems.Add(mem.nick);
            }
          
            if (leader.target != null)
            {
                
                if (mems.Contains(leader.target.name) || !leader.target.isAlive()) { Thread.Sleep(1000); core.Log("[Temporary Logging] Party Member is target or target is dead, waiting"); return null; }
                else { return leader.target; }
            }
            return GetBestNearestMob(assistPriority, fightPlayersBack);             
        }
        private bool IsControlled(Creature mob)
        {
            if (mob == null)
                return true;

            List<Buff> buffs = mob.getBuffs();
            foreach (Buff b in buffs)
            {
                if (controlDebuffs.Contains(b.id))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Skills/Items usage
        private bool UseSkillAndWait(uint skillId, Creature target)
        {
            if (target == null)
                return false;

            core.SetTarget(target);
            return UseSkillAndWait(skillId);
        }

        
        private bool UseSkillAndWait(uint skillId, bool selfTarget = false)
        {
            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(50);

            Creature target = core.me.target;

            if (!core.UseSkill(skillId, true, selfTarget))
            {
                if (target != null && core.GetLastError() == LastError.NoLineOfSight)
                {
                    if (core.dist(target) <= 5)
                        core.ComeTo(target, 2);
                    else if (core.dist(core.me.target) <= 10)
                        core.ComeTo(target, 3);
                    else if (core.dist(target) < 20)
                        core.ComeTo(target, 8);
                    else
                        core.ComeTo(target, 8);
                }

                return false;
            }

            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(10);

            return true;
        }
        private bool UseItemAndWait(uint itemId, bool selfTarget = false)
        {
            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(50);

            core.UseItem(itemId, false);

            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(10);

            return true;
        }
        private bool CastSkillAtNoCD (uint skillId, Creature target)
        {
            

            double x = target.X;
            double z = target.Z;
            double y = target.Y;

            if (!core.UseSkill(skillId, x, y, z))
            {
                if (target != null && core.GetLastError() == LastError.NoLineOfSight)
                {
                    if (core.dist(target) <= 5)
                        core.ComeTo(target, 2);
                    else if (core.dist(core.me.target) <= 10)
                        core.ComeTo(target, 3);
                    else if (core.dist(target) < 20)
                        core.ComeTo(target, 8);
                    else
                        core.ComeTo(target, 8);
                }
            }

            while ((core.me.isCasting && target.dist(x, y, z) < 1) || core.me.isGlobalCooldown)
                Thread.Sleep(10);

            return true;
        }

        private bool CastSkillAt(uint skillId, Creature target)
        {
            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(50);

            double x = target.X;
            double z = target.Z;
            double y = target.Y;

            if (!core.UseSkill(skillId, x, y, z))
            {
                if (target != null && core.GetLastError() == LastError.NoLineOfSight)
                {
                    if (core.dist(target) <= 5)
                        core.ComeTo(target, 2);
                    else if (core.dist(core.me.target) <= 10)
                        core.ComeTo(target, 3);
                    else if (core.dist(target) < 20)
                        core.ComeTo(target, 8);
                    else
                        core.ComeTo(target, 8);
                }
            }

            while ((core.me.isCasting && target.dist(x, y, z) < 1) || core.me.isGlobalCooldown)
                Thread.Sleep(10);

            return true;
        }
        private bool ChannelSkill(uint skillId, bool selfTarget = true, bool interruptOnAggro = true)
        {
            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(50);

            if (!core.UseSkill(skillId, false, selfTarget))
                return false;
            Thread.Sleep(1000);

            while (core.me.isCasting && (!interruptOnAggro || GetRealAggroCount() == 0))
                Thread.Sleep(50);

            if (core.me.isCasting)
            {
                core.CancelSkill();
                Thread.Sleep(50);
                core.CancelSkill();
            }

            return true;
        }
        private bool CanCast(uint skillId, bool selfTarget = false)
        {
            if (!skills.Contains(skillId)) return false;

            Skill s = core.getSkill(skillId);

            if (s == null) return false;
            if (core.skillCooldown(s) > 0) return false;
            if (s.db.cost > core.mp())
            {
                core.Log("OOM?");
                return false;
            }

            return true;
        }
        private void InitSkills()
        {
            skills = new List<uint>();

            List<Skill> availableSkills = core.me.getSkills();
            foreach (Skill s in availableSkills)
                skills.Add(s.id);
        }
        private void CheckBuffs()
        {
            /*
            List<Creature> targets = team;
            
            if (team.Count == 0)
                team.Add(core.me);
            */
            List<Creature> targets = new List<Creature> { core.me };

            try
            {
                foreach (Creature teamMember in targets)
                {
                    bool self = teamMember == core.me;
                    if (core.dist(teamMember) > 15 || (!self && core.mpp() < 50))
                        continue;

                    List<Buff> buffs = teamMember.getBuffs();

                    if (self && CanCast(_INSULATING_LENS) && !buffs.Any(b => b.id == _BF_INSULATING_LENSE_R5 || b.name.StartsWith("Insulating Lens")))
                        UseSkillAndWait(_INSULATING_LENS, true);

                    bool hasRefreshment = buffs.Any(b => b.name.StartsWith("Refreshment"));

                    if (self && CanCast(_REFRESHMENT))
                    {
                        if (!hasRefreshment)
                            UseSkillAndWait(_REFRESHMENT, true);
                    }
                    else if (CanCast(_HEALTH_LIFT) && !hasRefreshment && !buffs.Any(b => b.id == _BF_HEALTH_LIFT_R3 || b.name.StartsWith("Health Lift")))
                    {
                        UseSkillAndWait(_HEALTH_LIFT, teamMember);
                    }
                    
                    if (CanCast(_PURGE) && !buffs.Any(b => b.id == _Buff_Purge))
                    {
                        UseSkillAndWait(_PURGE, teamMember);
                    }

                    if (self && CanCast(_TOUGHEN) && !buffs.Any(b => b.name.StartsWith("Toughened")))
                        UseSkillAndWait(_TOUGHEN, true);

                    if (self && CanCast(_DOUBLE_RECURVE) && !buffs.Any(b => b.name.StartsWith("Double Recurve")))
                        UseSkillAndWait(_DOUBLE_RECURVE, true);

                    if (CanCast(_HUMMINGBIRD_DITTY) && !buffs.Any(b => b.name.StartsWith("Hummingbird Ditty")))
                        UseSkillAndWait(_HUMMINGBIRD_DITTY, teamMember);
                }
            }
            catch (Exception e)
            {
                core.Log("Buffing exception: " + e.ToString());
            }

            if (prefs.debugBuffs)
            {
                List<Buff> buffs = core.me.getBuffs();
                foreach (Buff buff in buffs)
                {
                    if (!debugBuffId.Contains(buff.id))
                    {
                        core.Log("[DEBUG] " + buff.name + " => " + buff.id);
                        debugBuffId.Add(buff.id);
                    }
                }
            }
        }
        private void CheckRegen()
        {

            

            CheckWeakendBody();
            if (WeakendBody == true && GetAggroCount() == 0 && core.gameState == GameState.Ingame)
            {
                WeakendBody = false;
                core.Log("Weakend Body debuff detected, let us sleep untill the buff is removed", System.Drawing.Color.Orange);
                List<Buff> buffs3 = core.me.getBuffs();
                foreach (Buff buff in buffs3)
                {
                    if (buff.id == 1128)
                    {
                        long WeakendBodyTimer;
                        WeakendBodyTimer = core.buffTime(1128);
                        int WeakendBodyTimerInt = unchecked((int)WeakendBodyTimer);
                        core.Log("We need more time untill we can Ressurect: " + WeakendBodyTimerInt + "ms");
                        Thread.Sleep(WeakendBodyTimerInt);
                    }
                }
                    Thread.Sleep(10);
             }

            if ((GetAggroCount() == 0 || core.mpp() < 15) && core.mpp() < 59 && CanCast(_MEDIDATE))
            { 
                ChannelSkill(_MEDIDATE, true, true);
            }
            
            if (GetAggroCount() == 0 && prefs.PlayDeadRegMana && core.mpp() < prefs.MinMPplayDead)
            {
                PlayDead();
            }

            if (GetAggroCount() == 0 && prefs.PlayDeadRegHP && core.hpp() < prefs.MinHPplayDead)
        
            
            // check food
            if (useFoodHP && core.hpp() < Math.Max(20, prefs.minHP - 5))
            {
                Item foodHP = core.getInvItem(prefs.foodHP);
                if (foodHP == null)
                {
                    useFoodHP = false;
                    core.Log("WARNING: You ran out of HP food!", System.Drawing.Color.Red);
                }
                else if ((DateTime.UtcNow - lastFoodUsed).TotalSeconds >= prefs.foodCooldown)
                {
                    UseItemAndWait(foodHP.id);
                    core.Log("Using some " + foodHP.name + " to regain health", System.Drawing.Color.Green);
                    lastFoodUsed = DateTime.UtcNow;
                }
            }
            if (useFoodMP && core.mpp() < Math.Max(20, prefs.minMP - 5))
            {
                Item foodMP = core.getInvItem(prefs.foodMP);
                if (foodMP == null)
                {
                    useFoodMP = false;
                    core.Log("WARNING: You ran out of Mana food!", System.Drawing.Color.Red);
                }
                else if ((DateTime.UtcNow - lastFoodUsed).TotalSeconds >= prefs.foodCooldown)
                {
                    UseItemAndWait(foodMP.id);
                    core.Log("Using some " + foodMP.name + " to regain mana");
                    lastFoodUsed = DateTime.UtcNow;
                }
            }

            // play instruments if we're still too low on HP/Mana
            if (GetAggroCount() == 0 && prefs.luteId > 0 && core.itemCooldown(prefs.luteId) == 0 && core.hpp() < prefs.minHP)
            {
                core.Log("Using Lute");
                bool playLute = true;
                if (!core.isEquiped((int)prefs.luteId))
                    if (!core.Equip(prefs.luteId))
                    {
                        core.Log("Failed at equipping lute: " + core.GetLastError());
                        playLute = false;
                    }

                if (playLute)
                    ChannelSkill(_PLAY_INSTRUMENT);
            }

            if (GetAggroCount() == 0 && prefs.fluteId > 0 && core.itemCooldown(prefs.fluteId) == 0 && core.mpp() < prefs.minMP)
            {
                bool playFlute = true;
                core.Log("Using Flute");
                if (!core.isEquiped((int)prefs.fluteId))
                    if (!core.Equip(prefs.fluteId))
                    {
                        core.Log("Failed at equipping flute: " + core.GetLastError());
                        playFlute = false;
                    }

                if (playFlute)
                    ChannelSkill(_PLAY_INSTRUMENT);
            }

            // return to safe anchor if we need to regen a lot - need to check that nothing is in the way also
           // if (GetAggroCount() == 0 && (core.hpp() < prefs.minHP - 20 || core.mpp() < prefs.minMP - 15) && core.dist(startX, startY, startZ) > 3)
           // {
           //     core.ComeTo(startX, startY, startZ, 2, 2);
           // }
        }
        private void CheckBuffItems()
        {
            // Lets get our buffs we need, while we don't fight :P

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Experience_Grimoire) == 0 && core.itemCount(_Item_Experience_Griomire) >= 1 && prefs.UseExperienceGR)
            {
                core.Log(Time() + "Using ExperienceGR");
                core.UseItem(_Item_Experience_Griomire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Frenzy_Grimoire) == 0 && core.itemCount(_Item_Frenzy_Grimoire) >= 1 && prefs.UseFrenzyGR)
            {
                core.Log(Time() + "Using FrenzyGR");
                core.UseItem(_Item_Frenzy_Grimoire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Haste_Grimoire) == 0 && core.itemCount(_Item_Haste_Grimoire) >= 1 && prefs.UseHasteGR)
            {
                core.Log(Time() + "Using HasteGR");
                core.UseItem(_Item_Haste_Grimoire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Promise_Grimoire) == 0 && core.itemCount(_Item_Promise_Grimoire) >= 1 && prefs.UsePromiesGR)
            {
                core.Log(Time() + "Using PromiseGR");
                core.UseItem(_Item_Promise_Grimoire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Ward_Grimoire) == 0 && core.itemCount(_Item_Ward_Grimoire) >= 1 && prefs.UseWardGR)
            {
                core.Log(Time() + "Using WardGR");
                core.UseItem(_Item_Ward_Grimoire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Zeal_Grimoire) == 0 && core.itemCount(_Item_Zeal_Grimoire) >= 1 && prefs.UseZealGR)
            {
                core.Log(Time() + "Using ZealGR");
                core.UseItem(_Item_Zeal_Grimoire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Greedy_Grimoire) == 0 && core.itemCount(_Item_Greedy_Grimoire) >= 1 && prefs.UseGreedyGR)
            {
                core.Log(Time() + "Using GreedyGR");
                core.UseItem(_Item_Greedy_Grimoire);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }




            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Tyrenos_Index) == 0 && core.itemCount(_Item_Tyrenos_Index) >= 1 && prefs.UseTyrenosIndex)
            {
                core.Log(Time() + "Using Tyrenos's Index");
                core.UseItem(_Item_Tyrenos_Index);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Golden_Library_Index) == 0 && core.itemCount(_Item_Golden_Library_Index) >= 1 && prefs.UseGoldenLibraryIndex)
            {
                core.Log(Time() + "Using Golden Library Index");
                core.UseItem(_Item_Golden_Library_Index);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }



            if (GetAggroCount() == 0 && core.buffTime(_Buff_Greedy_Dwaren_Elixir) == 0 && core.itemCount(_Item_Greedy_Dwaren_Elixir) >= 1 && prefs.UseGreedyDwarvenElixir)
            {
                core.Log(Time() + "Greedy Dwarven Elixir");
                core.UseItem(_Item_Greedy_Dwaren_Elixir);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Studious_Dwarven_Elixir) == 0 && core.itemCount(_Item_Studious_Dwarven_Elixir) >= 1 && prefs.UseStudiousDwarvenElixir)
            {
                core.Log(Time() + "Studious Dwarven Elixir");
                core.UseItem(_Item_Studious_Dwarven_Elixir);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Brisk_Dwarven_Elixir) == 0 && core.itemCount(_Item_Brisk_Dwarven_Elixir) >= 1 && prefs.UseBriskDwarvenElixir)
            {
                core.Log(Time() + "Brisk Dwarven Elixir");
                core.UseItem(_Item_Brisk_Dwarven_Elixir);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }



            if (GetAggroCount() == 0 && core.buffTime(_Buff_Honor_Boost_Tonic) == 0 && core.itemCount(_Item_Honor_Boost_Tonic) >= 1 && prefs.UseHonorBoostTonic)
            {
                core.Log(Time() + "Honor Boost Tonic");
                core.UseItem(_Item_Honor_Boost_Tonic);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Vocation_Expertise_Tonic) == 0 && core.itemCount(_Item_Vocation_Expertise_Tonic) >= 1 && prefs.UseVocationExpertiseTonic)
            {
                core.Log(Time() + "Vocation Expertise Tonic");
                core.UseItem(_Item_Vocation_Expertise_Tonic);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_XP_Boost_Potion) == 0 && core.itemCount(_Item_XP_Boost_Potion) >= 1 && prefs.UseXPBoostPotion)
            {
                core.Log(Time() + "XP Boost Potion");
                core.UseItem(_Item_XP_Boost_Potion);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Immortal_XP_Tonic) == 0 && core.itemCount(_Item_Immortal_XP_Tonic) >= 1 && prefs.UseImmortalXPTonic)
            {
                core.Log(Time() + "Immortal XP Tonic");
                core.UseItem(_Item_Immortal_XP_Tonic);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Lucky_Quicksilver_Tonic) == 0 && core.itemCount(_Item_Lucky_Quicksilver_Tonic) >= 1 && prefs.UseQuicksilverTonic)
            {
                core.Log(Time() + "Using Lucky Quicksilver Tonic");
                core.UseItem(_Item_Lucky_Quicksilver_Tonic);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }




            if (GetAggroCount() == 0 && core.buffTime(_Buff_Unstoppable_Force) == 0 && core.itemCount(_Item_Unstoppable_Force) >= 1 && prefs.UseSpellbookUnstoppableForce)
            {
                core.Log(Time() + "Using Spellbook: Unstoppable Force");
                core.UseItem(_Item_Unstoppable_Force);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Brick_Wall) == 0 && core.itemCount(_Item_Brick_Wall) >= 1 && prefs.UseSpellbookBrickWall)
            {
                core.Log(Time() + "Using Spellbook: Brick Wall");
                core.UseItem(_Item_Brick_Wall);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }



            if (GetAggroCount() == 0 && core.buffTime(_Buff_Kingdom_Heart) == 0 && core.itemCount(_Item_Kingdom_Heart) >= 1 && prefs.UseKingdomHeart)
            {
                core.Log(Time() + "Using Kingdom's Heart");
                core.UseItem(_Item_Kingdom_Heart);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            if (GetAggroCount() == 0 && core.buffTime(_Buff_Ancient_Library_Relic) == 0 && core.itemCount(_Item_Ancient_Library_Relic) >= 1 && prefs.UseAncientLibraryRelic)
            {
                core.Log(Time() + "Using Ancient Library Relic");
                core.UseItem(_Item_Ancient_Library_Relic);
                Thread.Sleep(2500); // Rest for 2.5 seconds ( little over the global cooldown )
            }

            var Tyrian = (core.buffTime(_Buff_Tyrenos_Index) / 1000 / 60);
            var GoldenLibrary = (core.buffTime(_Buff_Golden_Library_Index) / 1000 / 60);

            var GreedyDwarvenElixir = (core.buffTime(8000022) / 1000 / 60);
            var StudiousDwarvenElixir = (core.buffTime(_Buff_Studious_Dwarven_Elixir) / 1000 / 60);
            var BriskDwarvenElixir = (core.buffTime(_Buff_Brisk_Dwarven_Elixir) / 1000 / 60);

            var HonorBoostTonic = (core.buffTime(_Buff_Honor_Boost_Tonic) / 1000 / 60);
            var VocationExpertiseTonic = (core.buffTime(_Buff_Vocation_Expertise_Tonic) / 1000 / 60);
            var XPBoostPotion = (core.buffTime(_Buff_XP_Boost_Potion) / 1000 / 60);
            var ImmortalXPTonic = (core.buffTime(_Buff_Immortal_XP_Tonic) / 1000 / 60);
            var QuickSilverTonic = (core.buffTime(8000009) / 1000 / 60);

            var Force = (core.buffTime(7478) / 1000 / 60);
            var Wall = (core.buffTime(7477) / 1000 / 60);

            var KingodmHeart = (core.buffTime(_Buff_Kingdom_Heart) / 1000 / 60);
            var AncientLibraryRelic = (core.buffTime(_Buff_Ancient_Library_Relic) / 1000 / 60);


           /* if (prefs.UseTyrenosIndex) { core.Log(Time() + "Tyrenos's Index Timer:  " + Tyrian + " Minutes"); }
            if (prefs.UseGoldenLibraryIndex) { core.Log(Time() + "Golden Library Index Timer:  " + GoldenLibrary + " Minutes"); }

            if (prefs.UseGreedyDwarvenElixir) { core.Log(Time() + "Greedy Dwarven Elixir Timer:  " + GreedyDwarvenElixir + " Minutes"); }
            if (prefs.UseStudiousDwarvenElixir) { core.Log(Time() + "Studious Dwarven Elixir Timer:  " + StudiousDwarvenElixir + " Minutes"); }
            if (prefs.UseBriskDwarvenElixir) { core.Log(Time() + "Brisk Dwarven Elixir Timer:  " + BriskDwarvenElixir + " Minutes"); }

            if (prefs.UseHonorBoostTonic) { core.Log(Time() + "Honor Boost Tonic Timer:  " + HonorBoostTonic + " Minutes"); }
            if (prefs.UseVocationExpertiseTonic) { core.Log(Time() + "Vocation Expertise Tonic Timer:  " + VocationExpertiseTonic + " Minutes"); }
            if (prefs.UseXPBoostPotion) { core.Log(Time() + "XP Boost Potion Timer:  " + XPBoostPotion + " Minutes"); }
            if (prefs.UseImmortalXPTonic) { core.Log(Time() + "Immortal XP Tonic Timer:  " + ImmortalXPTonic + " Minutes"); }
            if (prefs.UseQuicksilverTonic) { core.Log(Time() + "Quick Silver Tonic Timer:  " + QuickSilverTonic + " Minutes"); }

            if (prefs.UseSpellbookUnstoppableForce) { core.Log(Time() + "Spellbook: Unstoppable Force Timer:  " + Force + " Minutes"); }
            if (prefs.UseSpellbookBrickWall) { core.Log(Time() + "Spellbook: Brick Wall Timer:  " + Wall + " Minutes"); }

            if (prefs.UseKingdomHeart) { core.Log(Time() + "Kingdom's Heart Timer:  " + KingodmHeart + " Minutes"); }
            if (prefs.UseAncientLibraryRelic) { core.Log(Time() + "Ancient Library Relic Timer:  " + AncientLibraryRelic + " Minutes"); } */

            
            

        }
        private void CheckPet()
        {
            if (prefs.petId > 0 && failedPetRez < 5)
            {
                Creature mount = core.getMount();
                if (mount == null)
                {
                    // (re)spawn it
                    core.Log("Summoning pet");
                    UseItemAndWait(prefs.petId);
                    Thread.Sleep(500);
                    mount = core.getMount();
                }

                if (mount != null && !core.isAlive(mount) && mount.getBuffs().Any(b => b.id == _BF_TRIPPED))
                {
                    // rez it
                    core.Log("Looks Like Our Pet is Dead.", System.Drawing.Color.Orange);
                    core.Log("<CrashProtection> Attempting to Wait That 3min CoolDown before we resurrect pet.", System.Drawing.Color.Red);
                    core.Log("<CrashProtection> This is a Temp fix WTFAB will introduce a proper fix soon.", System.Drawing.Color.Red);
                    Thread.Sleep(18000);
                    //core.Log("Trying to resurrect pet.", System.Drawing.Color.PowderBlue);
                    // clear mobs within 7m of pet to get a clean resurrect
                    List<Creature> mobs = core.getCreatures();
                    try
                    {
                        foreach (Creature mob in mobs)
                        {
                            CheckWeakendBody();
                            while ((GetAggroCount() == 0 && (core.hpp() < prefs.minHP || core.mpp() < prefs.minMP)) || (WeakendBody == true))
                            {
                                CheckBuffs();
                                CheckRegen();
                                Thread.Sleep(10);
                            }

                            if (IsValidTarget(mob) && (mount.dist(mob) <= 7 || core.dist(mob) <= 7 || mob.target == core.me))
                            {
                                KillMob(mob);
                            }
                        }

                        core.ComeTo(mount, 1, 1);

                        if (!UseSkillAndWait(_RAISE_BACK_UP, true))
                        {
                            core.Log("Execption: " + core.GetLastError());
                            core.DespawnMount();
                            failedPetRez++;
                        }
                    }
                    catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                        core.Log("<CrashProtection> _____________Exception Detected_____________", System.Drawing.Color.Red);
                        core.Log("<CrashProtection>" + " Exception: " + ex.Message + "\n" + ex.StackTrace, System.Drawing.Color.Red);
                        core.Log("<CrashProtection> ____exception will be patched soon. For now we should keep Grinding____", System.Drawing.Color.Red);

                    }
                }
            }
        }
        public bool PlayDead()
        {
            while (core.me.isCasting || core.me.isGlobalCooldown)
                Thread.Sleep(50);

            core.Log("Using Play Dead", System.Drawing.Color.PowderBlue);
            core.UseSkill(_PLAY_DEAD, false, true);
            Thread.Sleep(1000);

            while (core.me.isCasting && GetAggroCount() == 0 && (core.mpp() < 95 || core.hpp() < 95))
                Thread.Sleep(50);

            core.Log("Finished using Play Dead", System.Drawing.Color.PowderBlue);

            core.CancelSkill();
            Thread.Sleep(50);
            core.CancelSkill();

            return true;
        }
        #endregion
        #region Aggro counts
        private int GetAggroCount(bool includeTeam = false)
        {
            int total = 0;

            try
            {
                total = core.getAggroMobs().Count + (int)core.getAggroMobsCount(core.getMount());

                if (includeTeam)
                    foreach (Creature teammember in team)
                        total += (int)core.getAggroMobsCount(teammember);

                return total;
            }
            catch (Exception e) { core.Log("Exception during GetAggroCount: " + e.Message); }

            return total;
        }
        private int GetRealAggroCount()
        {
            int total = 0;
            try
            {
                List<Creature> aggroMobs = core.getAggroMobs();
                foreach (Creature mob in aggroMobs)
                    if (core.isAlive(mob) && !IsControlled(mob))
                    {
                        total++;
                    }

                aggroMobs = core.getAggroMobs(core.getMount());
                foreach (Creature mob in aggroMobs)
                    if (core.isAlive(mob) && !IsControlled(mob))
                    {
                        total++;
                    }
            }
            catch (Exception e) { core.Log("Exception during GetRealAggroCount: " + e.Message); }

            return total;
        }
        #endregion
        #region On-Death run-back routine
        private void core_onCreatureDied(Creature obj)
        {
            if (obj == core.me)
            {
                core.Log(core.me.name + " Was Slain Triggering death routine to run back to farm spot", System.Drawing.Color.Red);

                string[] paths = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Plugins\\ArcheGrinder\\DeathRoutes", core.me.name + "*.db3");
                if (paths.Length == 0)
                {
                   
                    core.Log(core.me.name + " You don't have any routes in the DeathRoutes folder");
                    return;
                }

                if (!core.me.isAlive())
                {
                    if (thread.IsAlive)
                        thread.Abort();
                    

                   //  Random r = new Random();
                   //  Thread.Sleep(r.Next(5000, 10000));
                    core.ResToRespoint();
                    core.GetLastError();
                    if (core.GetLastError() == LastError.RessurectNeedWaitMoreTime) //vielleicht hilft das ja gegen den Unmarshall error
                    {
                        long ResurrectionTimer;
                        ResurrectionTimer = core.me.resurrectionWaitingTime;
                        int ResurrectionTimerInt = unchecked((int)ResurrectionTimer);
                        core.Log("We need more time untill we can Ressurect: "+ ResurrectionTimerInt+ "ms");
                        Thread.Sleep(ResurrectionTimerInt);
                        
                    }
                    core.WaitTeleportCompleted(60000);
                }

                paths = ArrayTools.RandomizeStrings(paths); 
                bool foundPath = false;
                foreach (string path in paths)
                {
                    if (gps.LoadDataBase(path))
                    {
                        GpsPoint respawn = gps.GetPoint("Respawn");
                        if (respawn == null)
                        {
                            core.Log("Warning: " + path + " doesn't have a 'Respawn' point");
                            continue;
                        }

                        if (core.dist(respawn.x, respawn.y, respawn.z) < 30)
                        {
                            GpsPoint pew = gps.GetPoint("Pew");
                            if (pew == null)
                            {
                                core.Log("Warning: " + path + " doesn't have a 'Pew' point");
                                continue;
                            }

                            foundPath = true;
                            core.Log(core.me.name + " Loading path to run back: " + path, System.Drawing.Color.Green);

                            Thread gpsAttack = new Thread(gps_AttackThread);
                            gpsAttack.Start();

                            while (core.dist(pew.x, pew.y, pew.z) > 10)
                            {
                                if (!gps.GpsMove("Pew"))
                                {
                                    if (core.GetLastError() != LastError.MoveCanceled)
                                        core.Log(core.me.name + " GPS Error: " + core.GetLastError() + " (trying again)");

                                    Thread.Sleep(1000);
                                }
                            }

                            gpsAttack.Abort();

                            if (!thread.IsAlive)
                            {
                                core.Log(core.me.name + " Restarting combat");
                                thread.Abort();

                                thread = new Thread(CombatThread);
                                thread.Start();
                            }
                            else
                                core.Log(core.me.name + " Starting combat");
                            break;
                        }
                    }
                }

                if (!foundPath && core.GetLastError() != LastError.RessurectNeedWaitMoreTime)
                    core.Log(core.me.name + " No path in the DeathRoutes folder matched your current respawn point");
            }
        }
        private void gps_AttackThread()
        {
            while (true)
            {
                if (!core.isInPeaceZone())
                {
                   

                    // Scan path to next gps point to clear the path
                    List<Creature> mobs = core.getCreatures();
                    foreach (Creature mob in mobs)
                    {
                        
                        if (core.dist(mob) > 20)
                            continue;

                        if (GetAggroCount() == 0 && (core.hpp() < prefs.minHP || core.mpp() < prefs.minMP))
                        {
                            core.Log(core.me.name + " Pausing run-back routine to rebuff/regen");
                            gps.SuspendGpsMove();
                            

                            while (GetAggroCount() == 0 && (core.hpp() < prefs.minHP || core.mpp() < prefs.minMP))
                            {
                                gps.SuspendGpsMove();
                                CheckBuffs();
                                CheckRegen();
                                Thread.Sleep(10);
                            }

                            gps.ResumeGpsMove();
                        }

                        int angle = core.angle(mob);
                        double dist = core.dist(mob);

                        if (IsValidTarget(mob)
                            && (
                                dist <= 5
                                || (dist <= 10 && (angle < 75 || angle > 285))
                                || (dist <= 15 && (angle < 45 || angle > 315))
                            )

                        )
                        {
                            core.Log(core.me.name + " Pausing run-back routine to kill " + mob.name);
                            gps.SuspendGpsMove();
                            KillMob(mob);
                            gps.ResumeGpsMove();
                            break;
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }
        private void gps_onGpsPreMove(GpsPoint point)
        {
            // nothing left to do in here for now - probably mount support later on
        }

        private void CheckWeakendBody() //überprüft ob der Buff WeakendBody vorhanden ist, falls ja wird WeakendBody auf true gesetzt.
        {
            List<Buff> buffs2 = core.me.getBuffs();
            foreach (Buff buff in buffs2)
            {
                if (buff.id == 1128)
                {
                    WeakendBody = true;
                    break;
                }
                Thread.Sleep(10);
            }
        }
        #endregion
        #region Party/Raid stuff
        private void RefreshTeam()
        {
            team = new List<Creature>();
            List<PartyMember> partyMembers = core.getPartyMembers();
            foreach (PartyMember t in partyMembers)
                team.Add(t.obj);

            core.Log("Team size: " + team.Count);
        }
        private void core_onPartyMemberLeaves(PartyMember member)
        {
            core.Log(member.nick + " left the group");
            RefreshTeam();
        }
        void core_onNewPartyMember(PartyMember member)
        {
            core.Log(member.nick + " joined the group");
            RefreshTeam();
        }
        #endregion

        private void CombatThread()
        {
            #region init
            RefreshTeam();
            InitSkills();

            gps = new Gps(core);
            gps.onGpsPreMove += gps_onGpsPreMove;

            core.onPartyMemberLeaves += core_onPartyMemberLeaves;
            core.onNewPartyMember += core_onNewPartyMember;
            core.onCreatureDied += core_onCreatureDied;

            mobBlacklist = new List<uint>();

            // Get flute & lute
            Item lute = core.getInvItem(prefs.luteId);
            if (lute == null)
                lute = core.me.getEquipedItem(prefs.luteId);
            if (lute != null)
                core.Log(core.me.name + " Lute: " + lute.name);
            Item flute = core.getInvItem(prefs.fluteId);
            if (flute == null)
                flute = core.me.getEquipedItem(prefs.fluteId);
            if (flute != null)
                core.Log(core.me.name + " Flute: " + flute.name);

            // Check if we have potions
            usePotionHP = usePotionMP = useFoodMP = useFoodHP = false;
            List<Item> inventory = core.getAllInvItems();
            foreach (Item item in inventory)
            {
                if (!usePotionHP && item.name.Equals(prefs.potionHP, StringComparison.CurrentCultureIgnoreCase))
                {
                    usePotionHP = true;
                    core.Log(core.me.name + " Using HP Potion for this session: " + item.name);
                }
                else if (!usePotionMP && item.name.Equals(prefs.potionMP, StringComparison.CurrentCultureIgnoreCase))
                {
                    usePotionMP = true;
                    core.Log(core.me.name + " Using MP Potion for this session: " + item.name);
                }
                else if (!useFoodHP && item.name.Equals(prefs.foodHP, StringComparison.CurrentCultureIgnoreCase))
                {
                    useFoodHP = true;
                    core.Log(core.me.name + " Using HP Food for this session: " + item.name);
                }
                else if (!useFoodMP && item.name.Equals(prefs.foodMP, StringComparison.CurrentCultureIgnoreCase))
                {
                    useFoodMP = true;
                    core.Log(core.me.name + " Using MP Food for this session: " + item.name);
                }
            }

            Creature bestMob = null;
            bool noTargets = false;
            failedPetRez = 0;
            DateTime lastPurseOpened = DateTime.UtcNow.AddSeconds(-300);
            DateTime lastStolenBagOpened = DateTime.UtcNow.AddSeconds(-300);
            DateTime lastScratchedSafeOpened = DateTime.UtcNow.AddSeconds(-300);
            List<uint> purses = new List<uint>() { 29203, 29204, 29205, 29206, 29207, 32059, 34915, 34916, 35461 };
            List<uint> StolenBag = new List<uint>() { 34281, 34853, 35462, 35463, 35464, 35465, 35466, 35467, 35468 };
            List<uint> ScratchedSafe = new List<uint>() { 35469, 35470, 35471, 35472, 35473, 35474, 35475, 35476, 35477 };
            int laborCap = core.me.getBuffs().Any(b => b.id == _BF_PATRON) ? 5000 : 2000;

            hasShield = core.me.getAllEquipedItems().Any(item => item.weaponType == WeaponType.Shield);
            //core.Log("Shield equipped: " + hasShield);
            //core.Log("Labor cap: " + laborCap);

            if (core.isInPeaceZone())
            {
                // near a rez statue, trigger runback
                core.Log(core.me.name + " Near a statue, triggering runback routine before starting combat");
                core_onCreatureDied(core.me);
            }

            startX = core.me.X;
            startY = core.me.Y;
            startZ = core.me.Z;
            zone = new RoundZone(startX, startY, prefs.zoneRadius);
            #endregion

            while (true)
            {
                if (!core.isAlive())
                {
                    core.Log(core.me.name + " You died...");
                    break;
                }

                // out of combat stuff
                if (GetAggroCount() == 0)
                {
                    CheckBuffItems();
                    CheckBuffs();
                    CheckRegen();
                    CheckPet();
                }


                #region mob targeting
                if ((core.mpp() >= prefs.minMP && core.hpp() > prefs.minHP) || GetAggroCount() > 0)
                {
                    if (prefs.assistMode && GetRealAggroCount() == 0)
                        bestMob = AssistPartyLeader(true);
                    else
                        bestMob = GetBestNearestMob(true);


                    if (bestMob == null)
                    {
                        if (!noTargets)
                            core.Log(core.me.name + " No targets available");
                        noTargets = true;
                    }
                    else
                    {
                        core.Log(core.me.name + " new target: " + bestMob.name + " (" + Math.Round(bestMob.dist(startX, startY, startZ)) + "m from anchor)");
                        noTargets = false;
                    }
                }
                #endregion

                #region purse opening
                if ((bestMob == null || laborCap >= laborCap - 50) && prefs.openPurses && core.maxInventoryItemsCount() - core.inventoryItemsCount() > 3)
                {
                    int purseCount = 0;
                    foreach (var item in core.getAllInvItems())
                        if (purses.Contains(item.id))
                            purseCount += item.count;

                    int labor = core.me.opPoints;
                    if (purseCount >= 5 && labor >= 25 && (labor >= laborCap - 50 || (DateTime.UtcNow - lastPurseOpened).TotalSeconds > 300))
                    {
                        core.Log(core.me.name + " Trying to open up to " + purseCount + " coinpurses");
                        while (purseCount > 0 && core.me.opPoints >= 5 && GetAggroCount() == 0 && (noTargets || core.hpp() + core.mpp() < 190))
                        {
                            inventory = core.getAllInvItems();
                            foreach (Item item in inventory)
                            {
                                if (purses.Contains(item.id))
                                {
                                    UseItemAndWait(item.id);
                                    purseCount--;
                                    break;
                                }
                            }
                        }
                    }
                }

                //Open Stolen Bag
                if ((bestMob == null || laborCap >= laborCap - 150) && prefs.OpenStolenBag && core.maxInventoryItemsCount() - core.inventoryItemsCount() > 3)
                {
                    int StolenBagCount = 0;
                    foreach (var item in core.getAllInvItems())
                        if (StolenBag.Contains(item.id))
                            StolenBagCount += item.count;

                    int labor = core.me.opPoints;
                    if (StolenBagCount >= 5 && labor >= 25 && (labor <= laborCap - 150 || (DateTime.UtcNow - lastStolenBagOpened).TotalSeconds > 300))
                    {
                        core.Log(core.me.name + " Trying to open up to " + StolenBagCount + " Stolen Bag's");
                        while (StolenBagCount > 0 && core.me.opPoints >= 5 && GetAggroCount() == 0 && (noTargets || core.hpp() + core.mpp() < 190))
                        {
                            inventory = core.getAllInvItems();
                            foreach (Item item in inventory)
                            {
                                if (StolenBag.Contains(item.id))
                                {

                                    UseItemAndWait(item.id);
                                    StolenBagCount--;
                                    break;

                                }
                            }
                        }
                    }
                }

                //Open Scratched Safe
                if ((bestMob == null || laborCap >= laborCap - 150) && prefs.OpenScratchedSafe && core.maxInventoryItemsCount() - core.inventoryItemsCount() > 3)
                {
                    int ScratchedSafeCount = 0;
                    foreach (var item in core.getAllInvItems())
                        if (ScratchedSafe.Contains(item.id))
                            ScratchedSafeCount += item.count;

                    int labor = core.me.opPoints;
                    if (ScratchedSafeCount >= 5 && labor >= 25 && (labor <= laborCap - 150 || (DateTime.UtcNow - lastScratchedSafeOpened).TotalSeconds > 300))
                    {
                        core.Log(core.me.name + " Trying to open up to " + ScratchedSafeCount + " Stolen Bag's");
                        while (ScratchedSafeCount > 0 && core.me.opPoints >= 5 && GetAggroCount() == 0 && (noTargets || core.hpp() + core.mpp() < 190))
                        {
                            inventory = core.getAllInvItems();
                            foreach (Item item in inventory)
                            {
                                if (ScratchedSafe.Contains(item.id))
                                {

                                    UseItemAndWait(item.id);
                                    ScratchedSafeCount--;
                                    break;

                                }
                            }
                        }
                    }
                }


                #endregion


                if (bestMob != null)
                {
                    KillMob(bestMob);
                    bestMob = null;
                }

                Thread.Sleep(10);
            }
        }
        private void KillMob(Creature bestMob)
        {
            try
            {
                while (bestMob != null && core.isExists(bestMob) && core.isAlive(bestMob) && core.isAlive())
                {
                    if (bestMob.firstHitter != null && bestMob.firstHitter != core.me && !team.Contains(bestMob.firstHitter))
                    {
                        // someone else/some other raid tagged it, find a new one
                        // core.Log("Another raid got that mob, skipping");
                        // bestMob = null;
                        // break;
                    }

                    if (core.getBuff(bestMob, _BF_RETURNING) != null)
                    {
                        core.Log(core.me.name + " Mob is running away, skipping");
                        bestMob = null;
                        break;
                    }

                    // set target and rotate toward it
                    if (core.me.target != bestMob)
                        core.SetTarget(bestMob);

                    // check if we need to save pet (disabled while plugin doesn't wait for pet to regen or can re-enable skills)
                    
                    if (prefs.petId > 0)
                    {
                        Creature pet = core.getMount();
                        if (pet != null && pet.hp > 1 && pet.hpp < 15)
                        {
                            core.DespawnMount();
                            core.Log("Pet is about to die, despawning it");
                        }
                    }
                     

                    SelectAndUseHealingSkill();
                    if (prefs.useCC && GetAggroCount() >= 2 && GetRealAggroCount() >= 2)
                        SelectAndUseControlSkill(bestMob);

                    var distToMob = core.me.dist(bestMob);
                    if (distToMob > prefs.combatRange)
                    {
                        core.ComeTo(core.me.target, prefs.combatRange - 1);
                    }
                    else if (core.isAttackable(bestMob))
                    {
                        if (core.angle(bestMob, core.me) > 45 && core.angle(bestMob, core.me) < 315 && core.isAttackable(bestMob))
                            core.TurnDirectly(bestMob);

                        SelectAndUseCombatSkill();
                    }
                    else
                    {
                        SelectAndUsePreCombatSkill();
                    }
                }

                if (bestMob != null)
                    core.Log(core.me.name + " Killed " + bestMob.name + "?");

                #region looting
                if (team.Count <= 50 && prefs.lootCorpses)
                {
                    DateTime startLoot = DateTime.UtcNow;
                    while (bestMob != null && !core.isAlive(bestMob) && core.isExists(bestMob) && bestMob.type == BotTypes.Npc
                        && ((Npc)bestMob).dropAvailable && core.isAlive())
                    {
                        if (core.me.dist(bestMob) > 2)
                            core.ComeTo(bestMob, 1);
                        core.PickupAllDrop(bestMob);

                        double diff = (DateTime.UtcNow - startLoot).TotalSeconds;
                        if (diff > 5 || (GetAggroCount() > 0 && diff > 3))
                        {
                            core.Log(core.me.name + " Spent too long trying to loot, giving up");
                            break;
                        }
                    }
                }

                //  if (prefs.lootCorpses && bestMob != null && core.me.dist(bestMob) < 10
                //  && !core.isAlive(bestMob) && core.isExists(bestMob) && bestMob.type == BotTypes.Npc
                //  && ((Npc)bestMob).dropAvailable && core.isAlive())
                //  {
                // in group, just try once to loot if the corpse is near - hopefully humans do it otherwise!
                //      core.ComeTo(bestMob, 1);
                //      core.PickupAllDrop(bestMob);
                //  }
                #endregion
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                core.Log(core.me.name + " Combat exception: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        #region Combat Routine
        private void SelectAndUseHealingSkill()
        {
            if (core.hpp() < 75 && CanCast(_REVITALIZING_CHEER))
            {
                Buff mettle = core.getBuff("Mettle");
                if (mettle != null && mettle.charge > 2000)
                    UseSkillAndWait(_REVITALIZING_CHEER);
            }

            if (usePotionHP && core.hpp() < 15)
            {
                Item potionHP = core.getInvItem(prefs.potionHP);
                if (potionHP == null)
                {
                    usePotionHP = false;
                    core.Log(core.me.name + " WARNING: You ran out of HP potions!");
                }
                else if (core.itemCooldown(potionHP) == 0) // überprüft die Cooldown der Potion
                {
                    UseItemAndWait(potionHP.id);
                    core.Log(core.me.name + " Using some " + potionHP.name + " to regain health in combat");
                    lastPotionUsed = DateTime.UtcNow;
                }
            }

            if (usePotionMP && core.mpp() < 10)
            {
                Item potionMP = core.getInvItem(prefs.potionMP);
                if (potionMP == null)
                {
                    usePotionMP = false;
                    core.Log(core.me.name+" WARNING: You ran out of Mana potions!");
                }
                else if (core.itemCooldown(potionMP) == 0) // überprüft die Cooldown der Potion
                {
                    UseItemAndWait(potionMP.id);
                    core.Log(core.me.name + " Using some " + potionMP.name + " to regain mana in combat");
                    lastPotionUsed = DateTime.UtcNow;
                }
            }

            // combat escape if everything else failed
            if (core.hpp() < 15 && !prefs.healerMode && CanCast(_PLAY_DEAD, true))
                PlayDead();
            else if (core.hpp() < 10 && !prefs.healerMode && CanCast(_INVINCIBILITY, true))
                ChannelSkill(_INVINCIBILITY, true, false);
            else if (core.hpp() < 10 && !prefs.healerMode && CanCast(_STEALTH, true))
                UseSkillAndWait(_STEALTH);
        }
        private void SelectAndUseControlSkill(Creature combatTarget)
        {
            core.Log(core.me.name+" is trying to control an extra mob!", System.Drawing.Color.Orange);
            foreach (Creature mob in core.getAggroMobs())
            {
                if (mob != combatTarget && !IsControlled(mob) && core.isAttackable(combatTarget))
                {
                    if (CanCast(_BUBBLE_TRAP) && core.dist(mob) < 20)
                    {
                        core.SetTarget(mob);
                        Thread.Sleep(20);
                        core.TurnDirectly(mob);
                        if (CanCast(_FLAMEBOLT))
                            UseSkillAndWait(_FLAMEBOLT);
                        UseSkillAndWait(_BUBBLE_TRAP);
                        core.SetTarget(combatTarget);
                        break;
                    }
                }
            }
        }
        private void SelectAndUsePreCombatSkill()
        {
            Creature target = core.me.target;
            bool didCast = false;

            double dist = core.dist(core.me.target);
            double hppT = core.hpp(core.me.target);
            double hpT = core.hp(core.me.target);
            double mpp = core.mpp();
            double hpp = core.hpp();

            int aggroCount = GetAggroCount();

            if (CanCast(_MAGIC_CIRCLE, true) && dist <= 20 && ((hppT == 100 && hpT >= 10000) || aggroCount >= 2))
                didCast = UseSkillAndWait(_MAGIC_CIRCLE);
            else if (CanCast(_DEADEYE, true) && dist <= 23 && hppT == 100 && hpT >= 10000)
                didCast = UseSkillAndWait(_DEADEYE);
        }

        

        private void SelectAndUseCombatSkill()
        {
            
            Creature target = core.me.target;
            bool didCast = false;

            double dist = core.dist(core.me.target);
            double hppT = core.hpp(core.me.target);
            double hpT = core.hp(core.me.target);
            double mpp = core.mpp();
            double hpp = core.hpp();

            int aggroCount = GetAggroCount();

            bool doFastTag = prefs.fastTagging && hppT == 100;
            bool isControlled = IsControlled(target);

            //enigmatist combo
            /* if (!didCast && prefs.EnigmatistCombo)
             {
                 if (CanCast(_SHADOW_STEP) && CanCast(_OVERWHELM) && CanCast(_SHADOWSMITE) && CanCast(_Freezing_Earth) && CanCast(_GODS_WHIP) && CanCast(_FREEZING_ARROW) && CanCast(_CHAIN_LIGHTNING) && dist < 25)
                 {
                     if (hppT == 100 && dist < 25)
                     {
                         CastSkillAt(_SHADOW_STEP, target);
                         UseSkillAndWait(_LEECH);
                         UseSkillAndWait(_FRIGID_TRACKS);
                     }



                     UseSkillAndWait(_OVERWHELM);
                     UseSkillAndWait(_SHADOWSMITE);
                     CastSkillAt(_GODS_WHIP, target);
                     core.UseSkill(_Freezing_Earth);
                     while (core.UseSkill(_Freezing_Earth))
                     {
                         CastSkillAtNoCD(_GODS_WHIP, target);
                     }
                     if (hppT != 0)
                         {
                             core.UseSkill(_FREEZING_ARROW);
                             if (hppT != 0)
                             {
                             while (core.UseSkill(_FREEZING_ARROW))
                             {
                                 CastSkillAtNoCD(_GODS_WHIP, target);
                             }
                             if (hppT != 0)
                                 {
                                     core.UseSkill(_CHAIN_LIGHTNING);
                                     if (hppT != 0)
                                     {
                                     while (core.UseSkill(_CHAIN_LIGHTNING))
                                     {
                                         CastSkillAtNoCD(_GODS_WHIP, target);
                                     }
                                     CastSkillAtNoCD(_GODS_WHIP, target);
                                 }
                                 }
                             }
                         }



                 }
             } */
            
                if (target != null && (core.isSpellImmune(target) || core.isMeleeImmune(target) || core.isRangedImmune(target)))
                {
                    if (core.dist(target) <= 5)
                        core.ComeTo(target, 2);
                    else if (core.dist(core.me.target) <= 10)
                        core.ComeTo(target, 3);
                    else if (core.dist(target) < 20)
                        core.ComeTo(target, 8);
                    else
                        core.ComeTo(target, 8);
                }
                
               
            

            // fast tagging
            if (!didCast && doFastTag)
            {
                if (CanCast(_ENDLESS_ARROWS))
                    didCast = UseSkillAndWait(_ENDLESS_ARROWS);
                else if (CanCast(_MANA_STARS))
                    didCast = UseSkillAndWait(_MANA_STARS);
                else if (CanCast(_ENDLESS_ARROWS))
                    didCast = UseSkillAndWait(_ENDLESS_ARROWS);
                else if (CanCast(_CHAIN_LIGHTNING))
                    didCast = UseSkillAndWait(_CHAIN_LIGHTNING);
                else if (CanCast(_RANGED_ATTACK))
                    didCast = UseSkillAndWait(_RANGED_ATTACK);
            }

            // pre-pull buffs
            if (!didCast && (hppT == 100 || aggroCount >= 2))
            {
                if (CanCast(_MAGIC_CIRCLE, true) && dist <= 18)
                    didCast = UseSkillAndWait(_MAGIC_CIRCLE);
                else if (CanCast(_DEADEYE, true) && dist <= 23)
                    didCast = UseSkillAndWait(_DEADEYE);
                else if (CanCast(_BATTLE_FOCUS, true))
                    didCast = UseSkillAndWait(_BATTLE_FOCUS);
                else if (hasShield && CanCast(_REDOUBT, true) && hpp < 85)
                {
                    didCast = UseSkillAndWait(_REDOUBT);
                    // combo with liberation if available
                    if (CanCast(_LIBERATION))
                        UseSkillAndWait(_LIBERATION);
                }
                else if (dist <= 4 && CanCast(_RETRIBUTION))
                    didCast = UseSkillAndWait(_RETRIBUTION);

            }

            // debuffs
            if (!didCast && hpT >= 10000)
            {
                if (CanCast(_STALKERS_MARK))
                    didCast = UseSkillAndWait(_STALKERS_MARK);
                else if (CanCast(_THWART) && dist < 6)
                    didCast = UseSkillAndWait(_THWART);
                else if (CanCast(_MIRROR_LIGHT) && hppT < 100)
                    didCast = UseSkillAndWait(_MIRROR_LIGHT);
            }

            //maybe I will add it as an opt in
            //use some instant skills as finishers if not fast tagging
             if (!didCast && !prefs.fastTagging && hppT < 20)
            {
                if (CanCast(_CHAIN_LIGHTNING) && mpp > 50)
                     didCast = UseSkillAndWait(_CHAIN_LIGHTNING);
            }

            // stuns, don't use on almost dead targets
            if (!didCast && !isControlled && hppT >= 20)
            {
                if (CanCast(_FOCAL_CONCUSSION) && dist <= 8)
                    didCast = UseSkillAndWait(_FOCAL_CONCUSSION);
                else if (CanCast(_DAHUTAS_BREATH) && dist <= 8)
                    didCast = UseSkillAndWait(_DAHUTAS_BREATH);
                else if (CanCast(_MANA_FORCE) && dist <= 8)
                    didCast = UseSkillAndWait(_MANA_FORCE);
            }

            // AoE skills
            if (!didCast && aggroCount >= 2)
            {
                if (CanCast(_OLLOS_HAMMER) && dist <= 8)
                    didCast = UseSkillAndWait(_OLLOS_HAMMER);
                else if (CanCast(_SEARING_RAIN) && dist <= 12)
                    didCast = UseSkillAndWait(_SEARING_RAIN);
                else if (CanCast(_FLAME_BARRIER) && dist <= 12)
                    didCast = UseSkillAndWait(_FLAME_BARRIER);
                else if (CanCast(_BANSHE_WAIL) && dist <= 5)
                    didCast = UseSkillAndWait(_BANSHE_WAIL);
                else if (CanCast(_HELL_SPEAR) && dist <= 4)
                    didCast = CastSkillAt(_HELL_SPEAR, core.me);
                else if (CanCast(_SUMMON_CROWS) && dist <= 4)
                    didCast = UseSkillAndWait(_SUMMON_CROWS);
                else if (CanCast(_SUMMON_WRAITH) && dist <= 4)
                    didCast = ChannelSkill(_SUMMON_WRAITH);
                else if (CanCast(_FORTRESS) && dist <= 5)
                    didCast = UseSkillAndWait(_FORTRESS);
                else if (CanCast(_THROW_DAGGER) && dist <= 10)
                    didCast = UseSkillAndWait(_THROW_DAGGER);
            }

            // Arc Lighning and Chain Lightning combo
            if (!didCast)
            {
                if (CanCast(_ARC_LIGHTNING) && CanCast(_CHAIN_LIGHTNING) && dist <= 25 && hppT > 60)
                {
                    UseSkillAndWait(_CHAIN_LIGHTNING);
                    UseSkillAndWait(_ARC_LIGHTNING);
                    
                        }
            }

            if (!didCast )
            {
                if (CanCast(_CONVERSION_SHIELD) && core.hpp() < 70)
                    UseSkillAndWait(_CONVERSION_SHIELD);
                else if (CanCast(_BOASTFUL_ROAR) && dist <= 10)
                    UseSkillAndWait(_BOASTFUL_ROAR);
            }



            uint TargetAggroCounter = core.getAggroMobsCount(target);

            if ((CanCast(_METEOR_STRIKE)) && !didCast && hppT > 70 && mpp > 50 && GetAggroCount() == 0 && TargetAggroCounter == 0)
            {
               CastSkillAt(_METEOR_STRIKE, target);
            }

            // skill combo: Enervate>Earthen Grip + Meteor/Arc Lightning
            if (!didCast && !isControlled && dist <= 19 && hppT > 30
                && CanCast(_ENERVATE) && CanCast(_EARTHEN_GRIP)
                && (hpp < 80 || dist >= 10))
            {
                if (CanCast(_MAGIC_CIRCLE) && (hppT > 50 || aggroCount >= 2))
                { 
                    UseSkillAndWait(_MAGIC_CIRCLE);
                }

                didCast = UseSkillAndWait(_ENERVATE);
                UseSkillAndWait(_EARTHEN_GRIP);
                // mana whoring skill, only use if we're good on mana and target will eat all the damage 
                if (team.Count == 0 && CanCast(_ARC_LIGHTNING) && mpp > 70 && hpT > 7000)
                {
                    CastSkillAt(_ARC_LIGHTNING, target);
                }
                else if (CanCast(_FIEND_KNELL) && mpp > 60 && hppT > 70)
                {
                    CastSkillAt(_FIEND_KNELL, target);
                }
                else if (CanCast(_GODS_WHIP) && mpp > 40 && hppT > 60)
                {
                  
                    CastSkillAt(_GODS_WHIP, target);
                    if (core.me.target.hpp > 5)
                    { 
                    CastSkillAt(_GODS_WHIP, target);
                    }
                    if (core.me.target.hpp > 5)
                    {
                        CastSkillAt(_GODS_WHIP, target);
                    }
                    if (core.me.target.hpp > 5)
                    {
                        CastSkillAt(_GODS_WHIP, target);
                    }
                    if (core.me.target.hpp > 5)
                    {
                        CastSkillAt(_GODS_WHIP, target);
                    }
                }
            }

            // skill combo: Overwhelm>ShadowStep>PrecisionStrike>Shadowsmite
            if (!didCast && dist < 10 && hppT > 50 && CanCast(_OVERWHELM) && CanCast(_SHADOW_STEP) && CanCast(_PRECISION_STRIKE))
            {
                didCast = UseSkillAndWait(_OVERWHELM);
                UseSkillAndWait(_SHADOW_STEP);
                UseSkillAndWait(_PRECISION_STRIKE);
                if (CanCast(_SHADOWSMITE))
                    UseSkillAndWait(_SHADOWSMITE);
            }

            // melee skills
            if (!didCast && dist <= 4)
            {
                if (CanCast(_PRECISION_STRIKE))
                    didCast = UseSkillAndWait(_PRECISION_STRIKE);
                else if (CanCast(_SUNDER_EARTH))
                    didCast = UseSkillAndWait(_SUNDER_EARTH);
                else if (CanCast(_WHIRLWIND_SLASH))
                    didCast = UseSkillAndWait(_WHIRLWIND_SLASH);
                else if (hasShield && CanCast(_SHIELD_SLAM))
                {
                    // try to combo shield slam + bull rush
                    didCast = UseSkillAndWait(_SHIELD_SLAM);
                    if (CanCast(_BULL_RUSH))
                        UseSkillAndWait(_BULL_RUSH);
                }
                else if (CanCast(_PIN_DOWN))
                    didCast = UseSkillAndWait(_PIN_DOWN);
                else if (CanCast(_OVERWHELM))
                    didCast = UseSkillAndWait(_OVERWHELM);
                else if (CanCast(_SHADOWSMITE))
                    didCast = UseSkillAndWait(_SHADOWSMITE);
                else if (CanCast(_WALLOP))
                    didCast = UseSkillAndWait(_WALLOP);
                else if (CanCast(_BEHIND_ENEMY_LINES))
                    didCast = UseSkillAndWait(_BEHIND_ENEMY_LINES);
            }

            // gap closers
            if (!didCast && dist > 4)
            {
                if ((hppT == 100 || aggroCount >= 2) && CanCast(_TIGER_STRIKE))
                    didCast = UseSkillAndWait(_TIGER_STRIKE);
                else if (dist <= 12 && CanCast(_CHARGE))
                    didCast = UseSkillAndWait(_CHARGE);
            }

            // cooldown big hitting skills
            if (!didCast)
            {
                if (dist > 15 && dist <= 19 && CanCast(_CRIPPLING_MIRE))
                    didCast = UseSkillAndWait(_CRIPPLING_MIRE);
                else if (dist <= 19 && hpp < 80 && hppT > 20 && CanCast(_ABSORB_LIFEFORCE))
                    didCast = UseSkillAndWait(_ABSORB_LIFEFORCE);
                else if (CanCast(_CHARGED_BOLT))
                    didCast = UseSkillAndWait(_CHARGED_BOLT);
                else if (CanCast(_TOXIC_SHOT))
                    didCast = UseSkillAndWait(_TOXIC_SHOT);
                else if (CanCast(_PIERCING_SHOT))
                    didCast = UseSkillAndWait(_PIERCING_SHOT);
                else if (CanCast(_FREEZING_ARROW) && hppT > 60)
                {
                    didCast = UseSkillAndWait(_FREEZING_ARROW);
                        }
                else if (dist <= 40 && CanCast(_SNIPE))
                    didCast = UseSkillAndWait(_SNIPE);
            }

            // spam skills
            if (!didCast)
            {
                if (CanCast(_FLAMEBOLT))
                {
                    // group them by 3 to make good use of the proc
                    didCast = UseSkillAndWait(_FLAMEBOLT);
                    UseSkillAndWait(_FLAMEBOLT);
                    UseSkillAndWait(_FLAMEBOLT);
                }
                else if (CanCast(_ENDLESS_ARROWS))
                    didCast = UseSkillAndWait(_ENDLESS_ARROWS);

                else if (dist <= 4 && CanCast(_RAPID_STRIKE))
                    didCast = UseSkillAndWait(_RAPID_STRIKE);

                else if (dist <= 4 && CanCast(_TRIPLE_SLASH))
                    didCast = UseSkillAndWait(_TRIPLE_SLASH);

                else if (CanCast(_MANA_STARS))
                    didCast = UseSkillAndWait(_MANA_STARS);

                // melee spam
                else if (CanCast(_TRIPLE_SLASH))
                    didCast = UseSkillAndWait(_TRIPLE_SLASH);

                else if (CanCast(_RAPID_STRIKE))
                    didCast = UseSkillAndWait(_RAPID_STRIKE);

                // that's all your got?
                else if (CanCast(_CRITICAL_DISCORD))
                    didCast = UseSkillAndWait(_CRITICAL_DISCORD);

                else
                {
                    core.Log("Something went wrong, will use bow or melee basic attack! ! !");
                    // Something went wrong, use bow or melee basic attack depending on distance
                    if (dist > 4 && CanCast(_RANGED_ATTACK))
                        UseSkillAndWait(_RANGED_ATTACK);
                    else if (dist <= 4 && CanCast(_MELEE_ATTACK))
                        UseSkillAndWait(_MELEE_ATTACK);
                }
            }
        }
        #endregion

    }
}