using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;




/* This file is intended to be used as a library and only uses c# 2.0/.net 2.0 features for the widest compatiblity.
 *
 * http://code.google.com/p/projecteqemu/source/browse/trunk/EQEmuServer/zone/spdat.h
 * http://eqitems.13th-floor.org/phpBB2/viewtopic.php?t=23
 * http://forums.station.sony.com/eqold/posts/list.m?start=150&topic_id=165000 - resists
 *
 * 
 */

namespace Everquest
{
    #region Enums

    public enum SpellClasses
    {
        WAR = 1, CLR, PAL, RNG, SHD, DRU, MNK, BRD, ROG, SHM, NEC, WIZ, MAG, ENC, BST, BER
    }

    public enum SpellClassesLong
    {
        Warrior = 1, Cleric, Paladin, Ranger, ShadowKnight, Druid, Monk, Bard, Rogue, Shaman,
        Necromancer, Wizard, Magician, Enchanter, Beastlord //, Berserker
    }

    [Flags]
    public enum SpellClassesMask
    {
        WAR = 1, CLR = 2, PAL = 4, RNG = 8, SHD = 16, DRU = 32, MNK = 64, BRD = 128, ROG = 256,
        SHM = 512, NEC = 1024, WIZ = 2048, MAG = 4096, ENC = 8192, BST = 16384, BER = 32768
    }

    public enum SpellEffect
    {
        Current_HP = 0,
        AC = 1,
        ATK = 2,
        Movement_Speed = 3,
        STR = 4,
        DEX = 5,
        AGI = 6,
        STA = 7,
        WIS = 8,
        INT = 9,
        CHA = 10,
        Melee_Haste = 11,
        Invisibility = 12,
        See_Invisibile = 13,
        Enduring_Breath = 14,
        Current_Mana = 15,
        Pacify = 18,
        Faction = 19,
        Blind = 20,
        Stun = 21,
        Charm = 22,
        Fear = 23,
        Stamina_Loss = 24,
        Bind = 25,
        Gate = 26,
        Dispel = 27,
        Invisibility_to_Undead = 28,
        Invisibility_to_Animals = 29,
        Mesmerize = 31,
        Summon_Item = 32,
        Summon_Pet = 33,
        Disease_Counter = 35,
        Poison_Counter = 36,
        Invulnerability = 40,
        Shadowstep = 42,
        Delayed_Heal_Marker = 44,
        Fire_Resist = 46,
        Cold_Resist = 47,
        Poison_Resist = 48,
        Disease_Resist = 49,
        Magic_Resist = 50,
        Rune = 55,
        Levitate = 57,
        Illusion = 58,
        Damage_Shield = 59,
        Memory_Blur = 63,
        Stun_Spin = 64,
        Summon_Skeleton_Pet = 71,
        Feign_Death = 74,
        Current_HP_Non_Repeating = 79,
        Resurrect = 81,
        Summon_Player = 82,
        Teleport = 83,
        Melee_Proc = 85,
        Assist_Radius = 86,
        Evacuate = 88,
        Max_HP = 69,
        Summon_Corpse = 91,
        Hate = 92,
        Max_Mana = 97,
        Melee_Haste_v2 = 98,
        Root = 99,
        Current_HP_Repeating = 100,
        Donals_Heal = 101,
        Translocate = 104,
        All_Resists = 111,
        Aggro_Mult = 114,
        Curse_Counter = 116,
        Melee_Haste_v3 = 119,
        Spell_Damage_Focus = 124,
        Healing_Focus = 125,
        Haste_Focus = 127,
        Duration_Focus = 128,
        Ability_Aggro_Mult = 130,
        Mana_Cost_Focus = 132,
        Current_HP_Percent = 147,
        Cure_Detrimental = 154,
        Spell_Rune = 161,
        Melee_Rune = 162,
        Absorb_Hits = 163,
        Melee_Mitigation = 168,
        Critical_Hit_Chance = 169,
        Crippling_Blow_Chance = 171,
        Avoid_Melee_Chance = 172, // combat agility AA?
        Riposte_Chance = 173,
        Dodge_Chance = 174,
        Parry_Chance = 175,
        Lifetap_From_Weapon = 178,
        Weapon_Delay = 182,
        Hit_Chance = 184,
        Block_Chance = 188,
        Endurance_Repeating = 189,
        Hate_Repeating = 192,
        Skill_Attack = 193,
        Cancel_All_Aggro = 194,
        Stun_Resist_Chance = 195,
        Taunt = 199
    }

    public enum SpellSkill
    {
        Hit = -1, // weapons/archery/backstab/frenzy/kick/etc..
        _1H_Blunt = 0,
        _1H_Slash = 1,
        _2H_Blunt = 2,
        _2H_Slash = 3,
        Abjuration = 4,
        Alteration = 5,
        Apply_Poison = 6,
        Archery = 7,
        Backstab = 8,
        Bind_Wound = 9,
        Bash = 10,
        Block = 11,
        Brass = 12,
        Channeling = 13,
        Conjuration = 14,
        Defense = 15,
        Disarm = 16,
        Disarm_Traps = 17,
        Divination = 18,
        Dodge = 19,
        Double_Attack = 20,
        Dragon_Punch = 21,
        Dual_Wield = 22,
        Eagle_Strike = 23,
        Evocation = 24,
        Feign_Death = 25,
        Flying_Kick = 26,
        Forage = 27,
        Hand_to_Hand = 28,
        Hide = 29,
        Kick = 30,
        Meditate = 31,
        Mend = 32,
        Offense = 33,
        Parry = 34,
        Pick_Lock = 35,
        _1H_Pierce = 36,
        Riposte = 37,
        Round_Kick = 38,
        Safe_Fall = 39,
        Sense_Heading = 40,
        Singing = 41,
        Sneak = 42,
        Specialize_Abjure = 43,
        Specialize_Alteration = 44,
        Specialize_Conjuration = 45,
        Specialize_Divination = 46,
        Specialize_Evocation = 47,
        Pick_Pockets = 48,
        Stringed = 49,
        Swimming = 50,
        Throwing = 51,
        Tiger_Claw__Instantaneous = 52,
        Tracking = 53,
        Wind = 54,
        Fishing = 55,
        Make_Poison = 56,
        Tinkering = 57,
        Research = 58,
        Alchemy = 59,
        Baking = 60,
        Tailoring = 61,
        Sense_Traps = 62,
        Blacksmithing = 63,
        Fletching = 64,
        Brewing = 65,
        Alcohol_Tolerance = 66,
        Begging = 67,
        Jewelry_Making = 68,
        Pottery = 69,
        Percusion = 70,
        Intimidation = 71,
        Berserking = 72,
        Taunt = 73,
        Frenzy = 74,
        Remove_Trap = 75,
        Triple_Attack = 76,
        _2H_Pierce = 77,
        Harm_Touch = 105,
        Lay_Hands = 107,
        Slam = 111,
        Inspect_Chest = 114,
        Open_Chest = 115,
        Reveal_Trap_Chest = 116
    }

    public enum SpellSkillCap
    {
        STR = 0,
        STA = 1,
        AGI = 2,
        DEX = 3,
        WIS = 4,
        INT = 5,
        CHA = 6,
        Magic_Resist = 7,
        Cold_Resist = 8,
        Fire_Resist = 9,
        Poison_Resist = 10,
        Disease_Resist = 11
    }

    public enum SpellResist
    {
        Unresistable = 0, // only for detrimental spells
        Magic = 1,
        Fire = 2,
        Cold = 3,
        Poison = 4,
        Disease = 5,
        Lowest = 6, // Chromatic/lowest
        Average = 7, // Prismatic/average
        Physical = 8,
        Corruption = 9
    }

    public enum SpellBodyType
    {
        Humanoid = 1,
        Werewolf = 2,
        Undead = 3,
        Giant = 4,
        Golem = 5,
        Extraplanar = 6,
        UndeadPet = 8,
        Vampyre = 12,
        Atenha_Ra = 13,
        Greater_Akheva = 14,
        Khati_Sha = 15,
        Seru = 16,
        Draz_Nurakk = 18,
        Zek = 19,
        Luggald = 20,
        Animal = 21,
        Insect = 22,
        Elemental = 24,
        Plant = 25,
        Dragonkin = 26,
        Summoned = 28,
        Dragon = 29,
        Familiar = 31,
        Muramite = 34
    }

    public enum SpellTarget
    {
        Line_of_Sight = 1,
        Caster_AE = 2,
        Caster_Group = 3,
        Caster_PB = 4,
        Single = 5,
        Self = 6,
        Target_AE = 8,
        Animal = 9,
        Undead = 10,
        Summoned = 11,
        Lifetap = 13,
        Pet = 14,
        Corpse = 15,
        Plant = 16,
        Old_Giants = 17,
        Old_Dragons = 18,
        Target_AE_Lifetap = 20,
        Target_AE_Undead = 24,
        Target_AE_Summoned = 25,
        Hatelist = 32,
        Hatelist2 = 33,
        Chest = 34,
        Special_Muramites = 35, // bane for Ingenuity group trial in MPG
        Caster_PB_Players = 36,
        Caster_PB_NPC = 37,
        Pet2 = 38,
        No_Pets = 39, // single/group/ae ?
        Caster_AE_Players = 40, // bard AE hits all players
        Target_Group = 41,
        Directional_AE = 42,
        Frontal_AE = 44,
        Single_In_Group = 43,
        Self_and_Pet = 45,
        Construct = 46,
        Pet_Owner = 47,
        Target_AE_No_Players_Pets = 50 // blanket of forgetfullness. beneficial, AE mem blur, with max targets
    }

    public enum SpellTargetRestrict
    {
        Caster = 3, // (any NPC with mana) guess
        Not_On_Horse = 5, // guess
        Animal_or_Humanoid = 100,
        Dragon = 101,
        Animal_or_Insect = 102,
        Animal = 104,
        Plant = 105,
        Giant = 106,
        Not_Animal_or_Humanoid = 108,
        Bixie = 109,
        Harpy = 110,
        Gnoll = 111,
        Sporali = 112,
        Kobald = 113,
        Shade = 114,
        Drakkin = 115,
        Animal_or_Plant = 117,
        Summoned = 118,
        Fire_Pet = 119,
        Undead = 120,
        Living = 121,
        Fairy = 122,
        Humanoid = 123,
        Undead_HP_Less_Than_10_Percent = 124,
        Clockwork_HP_Less_Than_45_Percent = 125,
        Wisp_HP_Less_Than_10_Percent = 126,
        Not_Raid_Boss = 190,
        Raid_Boss = 191,
        HP_Above_75_Percent = 201,
        HP_Less_Than_20_Percent = 203, // dupe of 504
        HP_Less_Than_50_Percent = 204,
        Not_In_Combat = 216,
        At_Least_1_Pet_On_Hatelist = 221,
        At_Least_2_Pets_On_Hatelist = 222,
        At_Least_3_Pets_On_Hatelist = 223,
        At_Least_4_Pets_On_Hatelist = 224,
        At_Least_5_Pets_On_Hatelist = 225,
        At_Least_6_Pets_On_Hatelist = 226,
        At_Least_7_Pets_On_Hatelist = 227,
        At_Least_8_Pets_On_Hatelist = 228,
        At_Least_9_Pets_On_Hatelist = 229,
        At_Least_10_Pets_On_Hatelist = 230,
        At_Least_11_Pets_On_Hatelist = 231,
        At_Least_12_Pets_On_Hatelist = 232,
        At_Least_13_Pets_On_Hatelist = 233,
        At_Least_14_Pets_On_Hatelist = 234,
        At_Least_15_Pets_On_Hatelist = 235,
        At_Least_16_Pets_On_Hatelist = 236,
        At_Least_17_Pets_On_Hatelist = 237,
        At_Least_18_Pets_On_Hatelist = 238,
        At_Least_19_Pets_On_Hatelist = 239,
        At_Least_20_Pets_On_Hatelist = 240,
        HP_Less_Than_35_Percent = 250, // duple of 507
        Chain_Plate_Classes = 304,
        HP_Between_15_and_25_Percent = 399,
        HP_Between_1_and_25_Percent = 400,
        HP_Between_25_and_35_Percent = 401,
        HP_Between_35_and_45_Percent = 402,
        HP_Between_45_and_55_Percent = 403,
        HP_Between_55_and_65_Percent = 404,
        HP_Above_99_Percent = 412,
        //Has_Mana = 412, // guess based on Suppressive Strike
        HP_Below_5_Percent = 501,
        HP_Below_10_Percent = 502,
        HP_Below_15_Percent = 503,
        HP_Below_20_Percent = 504,
        HP_Below_25_Percent = 505,
        HP_Below_30_Percent = 506,
        HP_Below_35_Percent = 507,
        HP_Below_40_Percent = 508,
        HP_Below_45_Percent = 509,
        HP_Below_50_Percent = 510,
        HP_Below_55_Percent = 511,
        HP_Below_60_Percent = 512,
        HP_Below_65_Percent = 513,
        HP_Below_70_Percent = 514,
        HP_Below_75_Percent = 515,
        HP_Below_80_Percent = 516,
        HP_Below_85_Percent = 517,
        HP_Below_90_Percent = 518,
        HP_Below_95_Percent = 519,
        Mana_Below_X_Percent = 521, // 5?
        End_Below_40_Percent = 522,
        Mana_Below_40_Percent = 523,

        Undead2 = 603, // vampiric too? Celestial Contravention Strike
        Undead3 = 608,
        Summoned2 = 624,
        Not_Pet = 701,
        Undead4 = 818,
        Not_Undead4 = 819,
        End_Below_21_Percent = 825,
        End_Below_25_Percent = 826,
        End_Below_29_Percent = 827,
        Regular_Server = 836,
        Progression_Server = 837,
        Humanoid_Level_84_Max = 842,
        Humanoid_Level_86_Max = 843,
        Humanoid_Level_88_Max = 844,

        HP_Less_Than_80_Percent = 1004
    }

    public enum SpellZoneRestrict
    {
        None = 0,
        Outdoors = 1,
        Indoors = 2
    }

    public enum SpellIllusion
    {
                    
        Leather = (-2 << 16) + 1,
        Chainmail = (-2 << 16) + 2,
        Platemail = (-2 << 16) + 3,
        Void_Touched = (-1 << 16) + 2,
        Gender_Change = -1,
        Human = 1,
        Barbarian = 2,
        Erudite = 3,
        Wood_Elf = 4,
        High_Elf = 5,
        Dark_Elf = 6,
        Half_Elf = 7,
        Dwarf = 8,
        Troll = 9,
        Ogre = 10,
        Halfling = 11,
        Gnome = 12,
        Old_Aviak = 13,
        Werewolf = 14,
        Old_Brownie = 15,
        Old_Centaur = 16,
        Trakanon = 19,
        Venril_Sathir = 20,
        Froglok = 27,
        Werefrog = (27 << 16) + 1,
        Old_Gargoyle = 29,
        Gelatinous_Cube = 31,
        Goblin = 40,
        Wolf = 42,
        Black_Spirit_Wolf = (42 << 16) + 1,
        White_Spirit_Wolf = (42 << 16) + 2,
        Bear = 43,
        Polar_Bear = (43 << 16) + 2,
        Freeport_Militia = 44,
        Imp = 46,
        Dragon = 49,
        Lizard_Man = 51,
        Fae = 56,
        Old_Drachnid = 57,
        Solusek_Ro = 58,
        Skeleton = 60,
        Tunare = 62,
        Tiger = 63,
        Form_of_the_Gladiator = (66 << 16) + 2,
        Form_of_the_Frozen_Monolith = (66 << 16) + 1,
        Elemental = 75,
        Earth_Elemental = 75 << 16,
        Fire_Elemental = (75 << 16) + 1,
        Water_Elemental = (75 << 16) + 2,
        Air_Elemental = (75 << 16) + 3,
        Scarecrow = 82,
        Spectre = 85,
        Dragonkin = 89,
        Old_Alligator = 91,
        Oldest_Fear = 95,
        Old_Dervish = 100,
        Tadpole = 102,
        Old_Kedge = 103,
        Mammoth = 107,
        Eyesore = 108,
        Wasp = 109,
        Mermaid = 110,
        Seahorse = 116,
        Ghost = 118,
        Sabertooth = 119,
        Spirit_Wolf = 120,
        Gorgon = 121,
        Old_Dragon = 122,
        Hated_One = 123,
        Unicorn = 124,
        Pegasus = 125,
        Djinn = 126,
        Conceal_Strikes = 127,
        Avatar_of_Destruction = (127 << 16) + 2,
        Somatic_Bond = (127 << 16) + 1,
        Iksar = 128,
        Vah = 130,
        Mosquito = 134,
        Yiv_Goblin = 137,
        Minor_Illusion = 142,
        Treeform = 143,
        Nightmarr = 150,
        Iksar_Skeleton = 161,
        Snow_Rabbit = 176,
        Walrus = 177,
        Geonid = 178,
        Coldain = 183,
        Hag = 185,
        Othmir = 190,
        Ulthork = 191,
        Sea_Turtle = 194,
        Shik_Nar = 199,
        Rockhopper = 200,
        Underbulk = 201,
        Grimling = 202,
        Worm = 203,
        Shadel = 205,
        Owlbear = 206,
        Rhino_Beetle = 207,
        Earth_Elemental2 = 209,
        Air_Elemental2 = 210,
        Water_Elemental2 = 211,
        Fire_Elemental2 = 212,
        Thought_Horror = 214,
        Shissar = 217,
        Fungal_Fiend = 218,
        Stonegrabber = 220,
        Zelniak = 222,
        Lightcrawler = 223,
        Sunflower = 225,
        Sun_Revenant = 226,
        Shrieker = 227,
        Galorian = 228,
        Netherbian = 229,
        Portal_Passage = 230,
        Plaguebringer = 235,
        Guard = 239,
        Moon = 241,
        Banshee = 250,
        Mad_Fever = 258,
        Treefrog = 316,
        Arachnid = 326,
        Taldorian = 330,
        Gnome_Pirate = 338,
        Dark_Elf_Pirate = 339,
        Ogre_Pirate = 340,
        Human_Pirate = 341,
        Erudite_Pirate = 342,
        Demon_Beast = 348,
        Froglok_Skeleton = 349,
        Undead_Froglok = 350,
        Hound = 356,
        Vampire = 360,
        Nightrage_Orphan = (360 << 16) + 1,
        Master_of_Death = 367,
        Scion = (367 << 16) + 1,
        Call_to_Nemesis = (367 << 16) + 2,
        Jyre = (367 << 16) + 3,
        Scorched_Skeleton = (367 << 16) + 4,
        Mummy = 368,
        Froglok_Ghost = 371,
        Shade = 373,
        Golem = 374,
        Ice_Golem = (374 << 16) + 1,
        Crystal_Golem = (374 << 16) + 3,
        Crate = 376,
        Burial = 382,
        King = 384,
        Nihil = 385,
        Trusik = 386,
        Hynid = 388,
        Turepta = 389,
        Cragbeast = 390,
        Stonemite = 391,
        Ukun = 392,
        Ikaav = 394,
        Aneuk = 395,
        Kyv = 396,
        Noc = 397,
        Ra_tuk = 398,
        Taneth = 399,
        Huvul = 400,
        Mutna = 401,
        Mastruq = 402,
        Taelosian = 403,
        Mata_Muram = 406,
        Lightning_Warrior = 407,
        Feran = 410,
        Pyrilen = 411,
        Chimera = 412,
        Dragorn = 413,
        Murkglider = 414,
        Rat = 415,
        Bat = 416,
        Gelidran = 417,
        Girplan = 419,
        Crystal_Shard = 425,
        Dervish = 431,
        Drake = 432,
        New_Goblin = 433,
        Solusek_Goblin = (433 << 16) + 1,
        Dagnor_Goblin = (433 << 16) + 2,
        Valley_Goblin = (433 << 16) + 3,
        Aqua_Goblin = (433 << 16) + 7,
        Goblin_King = (433 << 16) + 8,
        Rallosian_Goblin = (433 << 16) + 11,
        Frost_Goblin = (433 << 16) + 12,
        Kirin = 434,
        Basilisk = 436,
        Puma = 439,
        Domain_Prowler = (439 << 16) + 9,
        Spider = 440,
        Spider_Queen = 441,
        Animated_Statue = 442,
        Lava_Spider = 450,
        Lava_Spider_Queen = 451,
        Dragon_Egg = 445,
        New_Werewolf = 454,
        White_Werewolf = (454 << 16) + 2,
        Kobold = 455,
        Kobold_King = (455 << 16) + 2,
        Sporali = 456,
        Violet_Sporali = (456 << 16) + 2,
        Azure_Sporali = (456 << 16) + 11,
        Gnomework = 457,
        Orc = 458,
        Bloodmoon_Orc = (458 << 16) + 4,
        Drachnid = 461,
        Drachnid_Cocoon = 462,
        Fungus_Patch = 463,
        Gargoyle = 464,
        Runed_Gargoyle = (464 << 16) + 1,
        Undead_Shiliskin = 467,
        Armored_Shiliskin = (467 << 16) + 5,
        Snake = 468,
        Evil_Eye = 469,
        Minotaur = 470,
        Zombie = 471,
        Clockwork_Boar = 472,
        Fairy = 473,
        Tree_Fairy = (473 << 16) + 1,
        Witheran = 474,
        Air_Elemental3 = 475,
        Earth_Elemental3 = 476,
        Fire_Elemental3 = 477,
        Water_Elemental3 = 478,
        Alligator = 479,
        Bear_ = 480,
        Wolf_ = 482,
        Spectre2 = 485,
        Banshee3 = 487,
        Banshee2 = 488,
        Bone_Golem = 491,
        Scrykin = 495,
        Treant = 496, // or izon
        Regal_Vampire = 497,
        Floating_Skull = 512,
        Totem = 514,
        Bixie_Drone = 520,
        Bixie_Queen = (520 << 16) + 2,
        Centaur = 521,
        Centaur_Warrior = (521 << 16) + 3,
        Drakkin = 522,
        Gnoll = 524,
        Undead_Gnoll = (524 << 16) + 1,
        Satyr = 529,
        New_Dragon = 530,
        Hideous_Harpy = 527,
        Goo = 549,
        Aviak = 558,
        Beetle = 559,
        Death_Beetle = (559 << 16) + 1,
        Kedge = 561,
        Kerran = 562,
        Shissar2 = 563,
        Siren = 564,
        Siren_Sorceress = (564 << 16) + 1,
        Plaguebringer2 = 566,
        Hooded_Plaguebringer = (566 << 16) + 7,
        Brownie = 568,
        Brownie_Noble = (568 << 16) + 2,
        Steam_Suit = 570,
        Embattled_Minotaur = 574,
        Scarecrow2 = 575,
        Shade2 = 576,
        Steamwork = 577,
        Tyranont = 578,
        Worg = 580,
        Wyvern = 581,
        Elven_Ghost = 587,
        Burynai = 602,
        Dracolich = 604,
        Iksar_Ghost = 605,
        Iksar_Skeleton2 = 606,
        Mephit = 607,
        Muddite = 608,
        Raptor = 609,
        Sarnak = 610,
        Scorpion = 611,
        Plague_Fly = 612,
        Burning_Nekhon = 614,
        Shadow_Nekhon = (614 << 16) + 1,
        Crystal_Hydra = 615,
        Crystal_Sphere = 616,
        Vitrik = 620,
        Bellikos = 638,
        Cliknar = 643,
        Ant = 644,
        Crystal_Sessiloid = 647,
        Telmira = 653,
        Flood_Telmira = (653 << 16) + 2,
        Morell_Thule = 658,
        Marionette = 659,
        Book_Dervish = 660,
        Topiary_Lion = 661,
        Rotdog = 662,
        Amygdalan = 663,
        Sandman = 664,
        Grandfather_Clock = 665,
        Gingerbread_Man = 666,
        Royal_Guardian = 667,
        Rabbit = 668,
        Gouzah_Rabbit = (668 << 16) + 3,
        Polka_Dot_Rabbit = (668 << 16) + 5,
        Cazic_Thule = 670,
        Selyrah = 686,
        Goral = 687,
        Braxi = 688,
        Kangon = 689,
        Undead_Thelasa = 695,
        Thel_Ereth_Ril = (695 << 16) + 21,
        Swinetor = 696,
        Swinetor_Necro = (696 << 16) + 1,
        Triumvirate = 697,
        Hadal = 698,
        Hadal_Templar = (698 << 16) + 2,
        Alaran_Ghost = 708,
        Holgresh = 715,
        Ratman = 718,
        Fallen_Knight = 719,
        Akhevan = 722
    }

    public enum SpellFaction
    {
        SHIP_Workshop = 1178,
        Kithicor_Good = 1204, // army of light
        Kithicor_Evil = 1205, // army of obliteration
        Ancient_Iksar = 1229
    }

    public enum SpellMaxHits
    {
        None = 0,
        Incoming_Hit_Attempts = 1, // incoming melee attempts (prior to success checks)
        Outgoing_Hit_Attempts = 2, // outgoing melee attempts of Skill type (prior to success checks)
        Incoming_Spells = 3,
        Outgoing_Spells = 4,
        Outgoing_Hit_Successes = 5,
        Incoming_Hit_Successes = 6,
        Matching_Spells = 7, // mostly outgoing, sometimes incoming (puratus) matching limits
        Incoming_Hits_Or_Spells = 8,
        Reflected_Spells = 9,
        Defensive_Proc_Casts = 10,
        Offensive_Proc_Casts = 11
    }

    public enum SpellTeleport
    {
        Primary_Anchor = 52584,
        Secondary_Anchor = 52585,
        Guild_Anchor = 50874
    }

    // it's hard to stick many spells into a single category, but i think this is only used by SPA 403
    public enum SpellCategory
    {
        Cures = 2,
        Offensive_Damage = 3, // nukes, DoT, AA discs, and spells that cast nukes as a side effect
        Heals = 5,
        Lifetap = 6,
        Transport = 8
    }
    // For items with spell effects associated
    // put spell in format __Spell_###___
    // __ is replaced by " (effect: ["
    // ___ is replaced by "]) "
    // Resulting in [Spell ###]
    // "www" = (worn)
    // "ccc" = (combat)
    // 
    // The [Spell ###] will parse out as a link to the associated spell.
    // replace _s_ with 's_

    public enum Items
    {
        Fish_Scales = 1352,
        Pixie_Stix = 1976,
        Chunk_of_Meat = 3418,
        Imbued_Resurrection_Urn__Spell_926___ = 3500,
        Burning_Rock__Spell_91___ = 3714,
        Throwing_Rock = 3870,
        Sensei_s_Belt_Pouch = 3901,
        Leonin_Bola__Spell_2855___ccc = 4456,
        Summoned_Hammer_of_Judgement__Spell_2___ccc = 5800,
        Summoned_Hammer_of_Divinity__Spell_2___ccc = 5801,
        Summoned_Hammer_of_Wrath__Spell_2___ccc = 5802,
        Summoned_Hammer_of_the_Gods__Spell_2___ccc = 5803,
        Bandages = 5926,
        Bat_Wing = 6002,
        Water_Flask = 6011,
        Summoned_Bread = 7001,
        Summoned_Water_Flask = 7002,
        Summoned_Dagger = 7003,
        Summoned_Bandages = 7004,
        Summoned_Dimensional_Pocket = 7005,
        Summoned_Staff_of_Tracing = 7006,
        Summoned_Fang = 7007,
        Summoned_Heatstone__Spell_679___www = 7008,
        Summoned_Staff_of_Warding = 7009,
        Summoned_Spear_of_Warding = 7010,
        Summoned_Arrow = 7011,
        Summoned_Miraculous_Water = 7012,
        Summoned_Miraculous_Food = 7013,
        Summoned_Staff_of_Runes = 7014,
        Summoned_Coldstone__Spell_539___www = 7015,
        Summoned_Sword_of_Runes__Spell_2688___ccc = 7016,
        Summoned_Dimensional_Hole = 7017,
        Summoned_Staff_of_Symbols = 7018,
        Summoned_Shard_of_the_Core__Spell_539___www = 7019,
        Summoned_Ring_of_Velocity = 7020,
        Summoned_Ring_of_Flight__Spell_261___ = 7021,
        Summoned_Modulating_Rod__Spell_1024___ = 7022,
        Summoned_Throwing_Dagger = 7025,
        Summoned_Staff_of_the_Magi = 7024,
        Summoned_Throwing_Knife = 7025,
        Summoned_Dagger_of_Symbols__Spell_2688___ccc = 7026,
        Summoned_Coldlight = 7027,
        Summoned_Wisp = 7028,
        Summoned_Globe_of_Fireflies = 7031,
        Summoned_Halo_of_Light = 7032,
        Summoned_Sphere_of_Light = 7033,
        Summoned_Bauble_of_Battle = 7034,
        Summoned_Globe_of_Searfire__Spell_2856___www = 7035,
        Very_Easy_Treasure_Map = 9585,
        Easy_Treasure_Map = 9586,
        Moderate_Treasure_Map = 9587,
        Difficult_Treasure_Map = 9588,
        Very_Difficult_Treasure_Map = 9589,
        Card_of_Wonder = 9999,
        Bloodstone = 10019,
        Jasper = 10020,
        Amber = 10022,
        Pearl = 10024,
        Cat_s_Eye_Agate = 10026,
        Peridot = 10028,
        Opal = 10030,
        Fire_Opal = 10031,
        Ruby = 10035,
        Summoned_Starshine = 10095,
        Simple_Treasure_Map = 10174,
        Squirrel = 10577,
        Extremely_Difficult_Treasure_Map = 10582,
        Summoned_Razor_Spine_Shuriken = 10584,
        Summoned_Dimensional_Fold = 10586,
        Sky_Darkener = 12065,
        Elemental_Augmentor = 12157,
        Elemental_Augmentor_ = 12158,
        Seeker_Dart = 12202,
        Skysplitter = 12221,
        Summoned_Nightmare_Arrow = 12292,
        Everfrozen_Shard = 13079,
        Dagger_of_Spiritual_Emancipation = 13232,
        Summoned_Spectral_Cap = 13663,
        Summoned_Spectral_Robe = 13664,
        Summoned_Spectral_Sleeves = 13665,
        Summoned_Spectral_Bracelet = 13666,
        Summoned_Spectral_Mitts = 13667,
        Summoned_Spectral_Pantaloons = 13668,
        Summoned_Spectral_Slippers = 13669,
        Stormkeep_Blasting_Powder = 14291,
        Heavy_Mallet = 14817,
        Silver_Bar = 16500,
        Electrum_Bar = 16501,
        Gold_Bar = 16502,
        Platinum_Bar = 16503,
        Enchanted_Silver_Bar = 16504,
        Enchanted_Electrum_Bar = 16505,
        Enchanted_Gold_Bar = 16506,
        Enchanted_Platinum_Bar = 16507,
        Summoned_Gruplok_Collar = 19002,
        Summoned_Murk_Beads__Spell_4022___ = 19030,
        Soulfire = 21401,
        Soulfire_ = 21402,
        Burning_Arrow = 21505,
        Summoned_Gruplok_Attuning_Rod = 21767,
        Summoned_Cat_s_Eye_Agate = 31288,
        Summoned_Bloodstone = 31289,
        Summoned_Jasper = 31290,
        Summoned_Peridot = 31291,
        Summoned_Deepshard__Spell_86___ = 31292,
        Summoned_Enduring_Bread = 31293,
        Summoned_Water_of_Knowledge = 31294,
        Summoned_Seeking_Arrow = 31295,
        Summoned_Dart_of_Doom = 31296,
        Summoned_Heartseeker_Arrow = 31297,
        Summoned_Blade_of_Power__Spell_2688___ccc = 31298,
        Summoned_Spear_of_Power__Spell_2688___ccc = 31299,
        Spiderkin_Silk__Spell_303___ = 32008,
        Tiny_Tin_Whistle = 32501,
        Dwarven_Ale = 32593
    }

    #endregion

    public sealed class Spell
    {
        public int ID;
        public int GroupID;
        public string Name;
        public int Icon;
        public int Mana;
        public int Endurance;
        public int EnduranceUpkeep;
        public int DurationTicks;
        public int DurationCalc;
        public int DurationBase;
        public bool DurationExtendable;
        public string[] Slots;
        public string[] AdvSlots;
        public int[] SlotEffects;
        public int[] Base1s;
        //public byte Level;
        public byte[] Levels;
        public byte[] ExtLevels; // similar to levels but assigns levels for side effect spells that don't have levels defined (e.g. a proc effect will get the level of it's proc buff)
        public string ClassesLevels;
        public SpellClassesMask ClassesMask;
        public SpellSkill Skill;
        public bool Beneficial;
        public bool BeneficialBlockable;
        public SpellTarget Target;
        public SpellResist ResistType;
        public int ResistMod;
        public bool PartialResist;
        public int MinResist;
        public int MaxResist;
        public string Extra;
        public string ExtraBase;
        public int HateOverride;
        public int HateMod;
        public int Range;
        public int AERange;
        public int AEDuration; // rain spells
        public float CastingTime;
        public float RestTime; // refresh time
        public float RecastTime;
        public float PushBack;
        public float PushUp;
        public int DescID;
        public string Desc;
        public int MaxHits;
        public SpellMaxHits MaxHitsType;
        public int MaxTargets;
        public int RecourseID;
        public string Recourse;
        public int TimerID;
        public int ViralRange;
        public int MinViralTime;
        public int MaxViralTime;
        public SpellTargetRestrict TargetRestrict;
        public SpellTargetRestrict CasterRestrict;
        public int[] ConsumeItemID;
        public int[] ConsumeItemCount;
        public int[] FocusID;
        public string YouCast;
        public string OtherCast;
        public string LandOnSelf;
        public string LandOnOther;
        public string WearOffMessage;
        public int ConeStartAngle;
        public int ConeEndAngle;
        public bool MGBable;
        public int Rank;
        public bool CastOutOfCombat;
        public SpellZoneRestrict Zone;
        public bool DurationFrozen; // in guildhall/lobby
        public bool Dispelable;
        public bool PersistAfterDeath;
        public bool ShortDuration; // song window
        public bool CancelOnSit;
        public int SPAIdx;
        public bool Sneaking;
        public int[] CategoryDescID; // most AAs don't have these set
        public string CategoryIDs;
        public string Deity;
        public int SongCap;
        public int MinRange;
        public int RangeModCloseDist;
        public int RangeModCloseMult;
        public int RangeModFarDist;
        public int RangeModFarMult;
        public bool Interruptable;
        public bool Reflectable;
        public int SpellClass;
        public int SpellSubclass;
        public bool CastInFastRegen;
        public bool AllowFastRegen;
        public bool BetaOnly;
        public bool CannotRemove;


        public int[] LinksTo;
        public int RefCount; // number of spells that link to this

#if !LimitMemoryUse
        public string[] Categories;
#endif

        public float Unknown;


        /// Effects can reference other spells or items via square bracket notation. e.g.
        /// [Spell 123]    is a reference to spell 123
        /// [Group 123]    is a reference to spell group 123
        /// [Item 123]     is a reference to item 123
        static public readonly Regex SpellRefExpr = new Regex(@"\[Spell\s(\d+)\]");
        static public readonly Regex GroupRefExpr = new Regex(@"\[Group\s(\d+)\]");
        static public readonly Regex ItemRefExpr = new Regex(@"\[Item\s(\d+)\]");

        public Spell()
        {
            Slots = new string[12];
            AdvSlots = new string[12];
            SlotEffects = new int[12];
            Base1s = new int[12];
            Levels = new byte[16];
            ExtLevels = new byte[16];
            ConsumeItemID = new int[4];
            ConsumeItemCount = new int[4];
            FocusID = new int[4];
            CategoryDescID = new int[3];
        }

        public override string ToString()
        {
            if (GroupID <= 0)
                return String.Format("[{0}] {1}", ID, Name);
            return String.Format("[{0}/{2}] {1}", ID, Name, GroupID);
        }

        /// <summary>
        /// Get a full description of the spell. This is mostly useful as a debug dump.
        /// </summary>
        public string[] Details()
        {
            List<string> result = new List<string>(20);
            //Action<string> Add = delegate(string s) { result.Add(s); };


            if (!String.IsNullOrEmpty(ClassesLevels))
            {
                result.Add("Classes: " + ClassesLevels);
                if (SongCap > 0)
                    result.Add("Skill: " + FormatEnum(Skill) + ", Cap: " + SongCap);
                else if ((int)Skill != 98) // 98 might be for AA only
                    result.Add("Skill: " + FormatEnum(Skill));
            }
            if (Categories != null && Categories.GetLength(0) > 0)
                result.Add("Category: " + Categories[0].ToString());

            if (!String.IsNullOrEmpty(Deity))
                result.Add("Deity: " + Deity);

            if (Mana > 0)
                result.Add("Mana: " + Mana);

            if (EnduranceUpkeep > 0)
                result.Add("Endurance: " + Endurance + ", Upkeep: " + EnduranceUpkeep + " per tick");
            else if (Endurance > 0)
                result.Add("Endurance: " + Endurance);

            for (int i = 0; i < ConsumeItemID.Length; i++)
                if (ConsumeItemID[i] > 0)
                {
                    if (ConsumeItemCount[i] > 1)
                        result.Add("Consumes: " + Spell.FormatEnumItem((Items)ConsumeItemID[i]) + " x " + ConsumeItemCount[i]);
                    else
                        result.Add("Consumes: " + Spell.FormatEnumItem((Items)ConsumeItemID[i]));
                }

            //for (int i = 0; i < FocusID.Length; i++)
            //    if (FocusID[i] > 0)
            //        result.Add("Focus: [Item " + FocusID[i] + "]");

            //if (BetaOnly)
            //    result.Add("Restriction: Beta Only");

            //if (CannotRemove)
             //   result.Add("Restriction: Cannot Remove");

            //if (CastOutOfCombat)
            //    result.Add("Restriction: Out of Combat"); // i.e. no aggro

            //if (CastInFastRegen)
            //    result.Add("Restriction: In Fast Regen");

            if (Zone != SpellZoneRestrict.None)
                result.Add("Restriction: " + Zone + " Only");

            //if (Sneaking)
            //    result.Add("Restriction: Sneaking");

            //if (CancelOnSit)
            //    result.Add("Restriction: Cancel on Sit");

            //if ((int)CasterRestrict > 100)
            //    result.Add("Restriction: " + FormatEnum(CasterRestrict));

            if (Target == SpellTarget.Directional_AE)
                result.Add("Target: " + FormatEnum(Target) + " (" + ConeStartAngle + " to " + ConeEndAngle + " Degrees)");
            else if (TargetRestrict > 0)
                result.Add("Target: " + FormatEnum(Target) + " (if " + FormatEnum(TargetRestrict) + ")");
            //else if ((Target == SpellTarget.Caster_Group || Target == SpellTarget.Target_Group) && (ClassesMask != 0 && ClassesMask != SpellClassesMask.BRD) && DurationTicks > 0)
            //    result.Add("Target: " + FormatEnum(Target) + ", MGB: " + (MGBable ? "Yes" : "No"));
            else
                result.Add("Target: " + FormatEnum(Target));

            if (AERange > 0 && Range == 0)
                result.Add("AE Range: " + (MinRange > 0 ? MinRange + "' to " : "") + AERange + "'");
            else if (AERange > 0)
                result.Add("Range: " + Range + "', AE Range: " + (MinRange > 0 ? MinRange + "' to " : "") + AERange + "'"); // unsure where the min range should be applied
            else if (Range > 0)
                result.Add("Range: " + (MinRange > 0 ? MinRange + "' to " : "") + Range + "'");

            //if (RangeModFarDist != 0)
            //    result.Add("Range Mod: " + (RangeModCloseMult * 100) + "% at " + RangeModCloseDist + "' to " + (RangeModFarMult * 100) + "% at " + RangeModFarDist + "'");

            //if (ViralRange > 0)
            //    result.Add("Viral Range: " + ViralRange + "', Recast: " + MinViralTime + "s to " + MaxViralTime + "s");

            if (!Beneficial)
                result.Add("Resist: " + ResistType + (ResistMod != 0 ? " " + ResistMod : "") + (MinResist > 0 ? ", Min: " + MinResist / 2f + "%" : "") + (MaxResist > 0 ? ", Max: " + MaxResist / 2f + "%" : "")); // + (!PartialResist ? ", No Partials" : ""));
            //else 
            //    result.Add("Beneficial: " + (BeneficialBlockable ? "Blockable" : "Not Blockable"));

            string rest = ClassesMask == 0 || ClassesMask == SpellClassesMask.BRD || RestTime == 0 ? "" : ", Recharge Time: " + RestTime.ToString() + "s";
            if (TimerID > 0)
                result.Add("Cast Time: " + (CastingTime == 0 ? "Instant" : CastingTime.ToString() + "s") + ", Recast: " + FormatTime(RecastTime) + ", Timer: " + TimerID + rest);
            else if (RecastTime > 0)
                result.Add("Cast Time: " + (CastingTime == 0 ? "Instant" : CastingTime.ToString() + "s") + ", Recast: " + FormatTime(RecastTime) + rest);
            else
                result.Add("Cast Time: " + (CastingTime == 0 ? "Instant" : CastingTime.ToString() + "s") + rest);
            
            if (Recourse != null)
                result.Add("Recourse: " + Recourse);
            
            if (DurationTicks > 0)
                result.Add("Duration: " + FormatTime(DurationTicks * 6) + " (" + DurationTicks + " ticks)" 
             //       + (Beneficial && ClassesMask != SpellClassesMask.BRD ? ", Extendable: " + (DurationExtendable ? "Yes" : "No") : "")
             //       + ", Dispelable: " + (Dispelable ? "Yes" : "No")
             //       + (!Beneficial && DurationTicks > 10 ? ", Allow Fast Regen: " + (AllowFastRegen ? "Yes" : "No") : "")  // it applies on <10 ticks, but there really is no need to show it
                    + (PersistAfterDeath ? ", Persist After Death" : "")); // pretty rare, so only shown when it's used
            else if (AEDuration >= 2500)
                result.Add("AE Waves: " + AEDuration / 2500);

            //if (PushUp != 0)
            //    result.Add("Push: " + PushBack + "' Up: " + PushUp + "'");
            //else if (PushBack != 0)
            //    result.Add("Push: " + PushBack + "'");

            if (HateMod != 0)
                result.Add("Hate Mod: " + (HateMod > 0 ? "+" : "") + HateMod);

            if (HateOverride != 0)
                result.Add("Hate: " + HateOverride);

            if (MaxHits > 0)
                result.Add("Max Hits: " + MaxHits + " " + FormatEnum((SpellMaxHits)MaxHitsType));

            if (MaxTargets > 0)
                result.Add("Max Targets: " + MaxTargets);

            //if (Unknown != 0)
            //    result.Add("Unknown: " + Unknown);

            //for (int i = 0; i < Slots.Length; i++)
            //    if (Slots[i] != null)
            //        result.Add(String.Format("Slot {0}: {1}", i + 1, Slots[i]));

            return result.ToArray();
        }

        /// <summary>
        /// Finalize spell data after all the attributes have been loaded.
        /// </summary>
        public void Prepare()
        {
            ClassesLevels = String.Empty;
            ClassesMask = 0;
            bool All254 = true;
            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i] == 255)
                    Levels[i] = 0;
                if (Levels[i] != 0)
                {
                    ClassesMask |= (SpellClassesMask)(1 << i);
                    ClassesLevels += " " + (SpellClasses)(i + 1) + "/" + Levels[i];
                }
                // bard AA i=7 are marked as 255 even though are usable
                if (Levels[i] != 254 && i != 7)
                    All254 = false;
            }
            Array.Copy(Levels, ExtLevels, Levels.Length);
            ClassesLevels = ClassesLevels.TrimStart();
            if (All254)
                ClassesLevels = "ALL/254";

            if (MaxHitsType == SpellMaxHits.None || (DurationTicks == 0 && !Name.Contains("Aura")))
                MaxHits = 0;

            if (Target == SpellTarget.Caster_PB)
            {
                Range = 0;
            }

            if (Target == SpellTarget.Self)
            {
                Range = 0;
                AERange = 0;
                MaxTargets = 0;
            }

            if (Target == SpellTarget.Single)
            {
                AERange = 0;
                MaxTargets = 0;
            }

            if (ResistType == SpellResist.Unresistable)
                ResistMod = 0;

            if (Zone != SpellZoneRestrict.Indoors && Zone != SpellZoneRestrict.Outdoors)
                Zone = SpellZoneRestrict.None;

            if (RangeModCloseDist == RangeModFarDist)
            { 
                RangeModCloseDist = RangeModFarDist = 0;
                RangeModCloseMult = RangeModFarMult = 0;
            }
        }

        /// <summary>
        /// Search all spell slots for a certain effect using SPA number.
        /// </summary>
        public bool HasEffect(int spa, int slot)
        {
            if (slot > 0)
                return SlotEffects[slot - 1] == spa;
            return Array.IndexOf(SlotEffects, spa) >= 0;
        }

        /// <summary>
        /// Search all spell slots for a certain effect using a text match.
        /// </summary>
        /// <param name="text">Effect to search for. Can be text or a integer representing an SPA.</param>
        /// <param name="slot">0 to check or slots, or a value between 1 and 12.</param>
        public bool HasEffect(string text, int slot)
        {
            int spa;
            if (Int32.TryParse(text, out spa))
                return HasEffect(spa, slot);

            if (slot > 0)
                return Slots[slot - 1] != null && Slots[slot - 1].IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0;

            for (int i = 0; i < Slots.Length; i++)
                if (Slots[i] != null && Slots[i].IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    return true;

            return false;
        }

        public bool HasEffect(string text)
        {
            return HasEffect(text, 0);
        }

        /// <summary>
        /// Search all spell slots for a certain effect using a RegEx.
        /// </summary>
        /// <param name="slot">0 to check or slots, or a value between 1 and 12.</param>
        public bool HasEffect(Regex re, int slot)
        {
            if (slot > 0)
                return Slots[slot - 1] != null && re.IsMatch(Slots[slot - 1]);

            for (int i = 0; i < Slots.Length; i++)
                if (Slots[i] != null && re.IsMatch(Slots[i]))
                    return true;

            return false;
        }

        /// <summary>
        /// Sum all the spell slots effects that match a regex. The regex must have a capturing group for an integer value.
        /// e.g. Increase Current HP by (\d+)
        /// </summary>
        public int ScoreEffect(Regex re)
        {
            int score = 0;
            for (int i = 0; i < Slots.Length; i++)
                if (Slots[i] != null)
                {
                    Match m = re.Match(Slots[i]);
                    if (m.Success)
                        score += Int32.Parse(m.Groups[1].Value);
                }

            return score;
        }

        /// <summary>
        /// Parse a spell effect. Each spell has 12 effect slots. Devs refer to these as SPAs.
        /// Attributes like ID, Skill, Extra, DurationTicks, SPAIdx are referenced and should be set before
        /// calling this function.
        /// </summary>
        public string ParseEffect(int spa, int base1, int base2, int max, int calc, int level, int minlevel)
        {
            // type 254 indicates an unused slot
            if (spa == 254)
                return null;

            // unused slots
            if (base1 == 0 && base2 == 0 && max == 0 && (spa == 0 || spa == 1 || spa == 2 || spa == 5 || spa == 6 || spa == 10))
                return null;

            // many SPAs use a scaled value based on either current tick or caster level
            int value = CalcValue(calc, base1, max, 1, level);
            int minvalue = CalcValue(calc, base1, max, 1, minlevel);
            string range = CalcValueRange(calc, base1, max, DurationTicks, level);
            //Func<int> base1_or_value = delegate() { Debug.WriteLineIf(base1 != value, "SPA " + spa + " value uncertain"); return base1; };

            // some hp/mana/end/hate effects repeat for each tick of the duration
            string repeating = (DurationTicks > 0) ? " per tick" : null;

            string spaString = String.Empty;
            switch (SPAIdx)
            {
                case 922:
                    spaString = ", healing you.";
                    break;
                case 923:
                    spaString = ", healing your pet.";
                    break;
                case 924:
                    spaString = ", healing your group.";
                    break;
            };

            // some effects are capped at a max level
            string maxlevel = (max > 0) ? String.Format(" up to level {0}", max) : null;

            //Func<string, string> FormatCount = delegate(string name) { return ((value > 0) ? "Increase " : "Decrease ") + name + " by " + Math.Abs(value); };

            switch (spa)
            {
                case 0:
                    if (base2 > 0)
                        return Spell.FormatCount("Hit Points", value) + repeating + range + " (if " + Spell.FormatEnum((SpellTargetRestrict)base2) + ")";

                    return Spell.FormatCount("Hit Points", value, minvalue, level, minlevel) + repeating + spaString + range;
                case 1:
                    if (value > 0)
                        return Spell.FormatCount("AC", value * 300 / 1000, minvalue * 300 / 1000, level, minlevel);
                    else
                        return Spell.FormatCount("AC", value, minvalue, level, minlevel);
                case 2:
                    return Spell.FormatCount("ATK", (int)value,(int) minvalue, level, minlevel) + range;
                case 3:
                    return Spell.FormatPercent("Movement Speed", value, minvalue, minlevel, level);
                case 4:
                    return Spell.FormatCount("STR", value, minvalue, level, minlevel) + range;
                case 5:
                    return Spell.FormatCount("DEX", value, minvalue, level, minlevel) + range;
                case 6:
                    return Spell.FormatCount("AGI", value, minvalue, level, minlevel) + range;
                case 7:
                    return Spell.FormatCount("STA", value, minvalue, level, minlevel) + range;
                case 8:
                    return Spell.FormatCount("INT", value, minvalue, level, minlevel) + range;
                case 9:
                    return Spell.FormatCount("WIS", value, minvalue, level, minlevel) + range;
                case 10:
                    // 10 is often used as a filler or to trigger server side scripts
                    return Spell.FormatCount("CHA", value, minvalue, level, minlevel) + range;
                case 11:
                    // base attack speed is 100. so 85 = 15% slow, 130 = 30% haste
                    if (value > 100)
                        return Spell.FormatPercent("Melee Haste", value - 100, minvalue - 100, minlevel, level);
                    else
                        return Spell.FormatPercent("Attack Speed", value - 100);
                case 12:
                    if (base1 > 1)
                        return String.Format("Invisibility (Enhanced {0})", base1);
                    return "Invisibility (Unstable)";
                case 13:
                    if (base1 > 1)
                        return String.Format("See Invisible (Enhanced {0})", base1);
                    return "See Invisible";
                case 14:
                    return "Enduring Breath";
                case 15:
                    return Spell.FormatCount("Current Mana", value) + repeating + range;
                case 18:
                    return "Pacify";
                case 19:
                    return Spell.FormatCount("Faction", value);
                case 20:
                    return "Blind";
                case 21:
                    //if (base2 != base1 && base2 != 0)
                    //    return String.Format("Stun NPC for {0:0.##}s (PC for {1:0.##}s)", base1 / 1000f, base2 / 1000f) + maxlevel;
                    return String.Format("Stun for {0:0.##}s", base1 / 1000f) + maxlevel;
                case 22:
                    maxlevel = (base1 > 0) ? String.Format(" up to level {0}", base1) : null;
                    return "Charm" + maxlevel;
                case 23:
                    return "Fear" + maxlevel;
                case 24:
                    return Spell.FormatPercent("Endurance Loss", -value);
                case 25:
                    return "Bind";
                case 26:
                    if (base1 > 1)
                        return "Gate to Secondary Bind";
                    return "Gate";
                case 27:
                    return String.Format("Dispel ({0})", value);
                case 28:
                    return "Invisibility to Undead";
                case 29:
                    return "Invisibility to Animals";
                case 30:
                    return String.Format("Decrease Aggro Radius to {0}", value) + maxlevel;
                case 31:
                    if (base1 > 1)
                        maxlevel = String.Format(" up to level {0}", base1);
                    return "Mesmerize" + maxlevel;
                case 32:
                    // calc 100 = summon a stack? (based on item stack size) Pouch of Quellious, Quiver of Marr
                    //return String.Format("Summon: [Item {0}] x {1} {2} {3}", base1, calc, max, base2);
                    if (max > 1)
                        return String.Format("Summon x {1}: {0}", Spell.FormatEnumItem((Items)base1), max);
                    else
                        return String.Format("Summon: {0}", Spell.FormatEnumItem((Items)base1));
                case 33:
                    return String.Format("Summon Pet: {0}", Extra);
                case 35:
                    return Spell.FormatCount("Disease Counter", value);
                case 36:
                    return Spell.FormatCount("Poison Counter", value);
                case 40:
                    return "Invulnerability";
                case 41:
                    maxlevel = (base1 > 0) ? String.Format(" (Maximum level {0})", base1) : null;
                    return "Destroy's Target" + maxlevel;
                case 42:
                    // TODO: does shadowstep always gate an NPC? e.g. highsun
                    return "Shadowstep";
                case 44:
                    return String.Format("Stacking: Delayed Heal Marker ({0})", value);
                case 46:
                    return Spell.FormatCount("Fire Resist", value, minvalue, level, minlevel);
                case 47:
                    return Spell.FormatCount("Cold Resist", value, minvalue, level, minlevel);
                case 48:
                    return Spell.FormatCount("Poison Resist", value, minvalue, level, minlevel);
                case 49:
                    return Spell.FormatCount("Disease Resist", value, minvalue, level, minlevel);
                case 50:
                    return Spell.FormatCount("Magic Resist", value, minvalue, level, minlevel);
                case 52:
                    return "Sense Undead";
                case 53:
                    return "Sense Summoned";
                case 54:
                    return "Sense Animal";
                case 55:
                    return String.Format("Absorb Damage: {0} points", value);
                case 56:
                    return "True North";
                case 57:
                    return "Levitate";
                case 58:
                    value = (base1 << 16) + max;
                    if (Enum.IsDefined(typeof(SpellIllusion), value))
                        return String.Format("Illusion: {0}", Spell.FormatEnum((SpellIllusion)value));
                    if (base2 > 0)
                        return String.Format("Illusion: {0} ({1})", Spell.FormatEnum((SpellIllusion)base1), max);
                    return String.Format("Illusion: {0}", Spell.FormatEnum((SpellIllusion)base1));
                case 59:
                    return Spell.FormatCount("Damage Shield", -value, -minvalue, level, minlevel);
                case 61:
                    return "Identify Item";
                case 63:
                    // +25 if over level 53, +(cha - 150)/10 max:15. so base is 40 + whatever the value is
                    //return String.Format("Memory Blur ({0})", value);
                    if (value + 40 < 100)
                        return String.Format("Memory Blur ({0}% Chance)", value + 40);
                    return "Memory Blur";
                case 64:
                    //if (base2 != base1 && base2 != 0)
                    //    return String.Format("Stun and Spin NPC for {0:0.##}s (PC for {1:0.##}s)", base1 / 1000f, base2 / 1000f) + maxlevel;
                    return String.Format("Stun and Spin for {0:0.##}s", base1 / 1000f) + maxlevel;
                case 65:
                    return "Infravision";
                case 66:
                    return "Ultravision";
                case 67:
                    return "Eye of Zomm";
                case 68:
                    return "Reclaim Pet";
                case 69:
                    return Spell.FormatCount("Max Hit Points", value, minvalue, level, minlevel) + range;
                case 71:
                    return String.Format("Summon Pet: {0}", Extra);
                case 73:
                    return "Bind Sight";
                case 74:
                    if (value < 100)
                        return String.Format("Feign Death ({0}% Chance)", value);
                    return "Feign Death";
                case 75:
                    return "Voice Graft";
                case 76:
                    return "Sentinel";
                case 77:
                    return "Locate Corpse";
                case 78:
                    return String.Format("Absorb Magical Damage: {0} hit points", value);
                case 79:
                    // delta hp for heal/nuke, non repeating
                    if (base2 > 0)
                        return Spell.FormatCount("Current Hit Points", value) + range + " (if " + Spell.FormatEnum((SpellTargetRestrict)base2) + ")";
                    return Spell.FormatCount("Current Hit Points", value) + range;
                case 81:
                    return String.Format("Resurrect with {0}% XP", value);
                case 82:
                    // call of the hero
                    return "Summon Player";
                case 83:
                    return String.Format("Teleport to {0}", Extra);
                case 84:
                    return "Gravity Flux";
                case 85:
                    if (base2 != 0)
                        return String.Format("Add Proc: [Spell {0}] with {1}% Rate Mod", base1, base2);
                    return String.Format("Add Proc: [Spell {0}]", base1);
                case 86:
                    return String.Format("Decrease Social Radius to {0}", value) + maxlevel;
                case 87:
                    return Spell.FormatPercent("Magnification", value);
                case 88:
                    return String.Format("Evacuate to {0}", Extra);
                case 89:
                    return Spell.FormatPercent("Player Size", base1 - 100);
                case 91:
                    return String.Format("Summon Corpse up to level {0}", base1);
                case 92:
                    // calming strike spells are all capped at 100. so base1 would be more appropriate for those
                    // but most other hate spells seem to imply scaled value is used
                    return Spell.FormatCount("Hate", value);
                case 93:
                    return "Stop Rain";
                case 94:
                    return "Cancel if Combat Initiated";
                case 95:
                    return "Sacrifice";
                case 96:
                    // aka silence, but named this way to match melee version
                    return "Silence";
                case 97:
                    return Spell.FormatCount("Max Mana", value);
                case 98:
                    // yet another super turbo haste. only on 3 bard songs
                    return Spell.FormatPercent("Melee Haste v2", value - 100);
                case 99:
                    return "Root";
                case 100:
                    // heal over time
                    if (base2 > 0)
                        return Spell.FormatCount("Current HP", value) + repeating + range + " (if " + Spell.FormatEnum((SpellTargetRestrict)base2) + ")";
                    return Spell.FormatCount("Current HP", value) + repeating + range;
                case 101:
                    // only castable via Donal's BP. creates a buf that blocks recasting
                    return "Increase Current HP by 7500";
                case 102:
                    return "Fear Immunity";
                case 103:
                    if (!String.IsNullOrEmpty(Extra))
                        return String.Format("Summon Pet: {0}", Extra);
                    else
                        return "Summon Pet";
                case 104:
                    return String.Format("Translocate to {0}", Extra);
                case 105:
                    return "Inhibit Gate";
                case 106:
                    return String.Format("Summon Warder: {0}", Extra);
                case 107:
                    switch (max)
                    {
                        case 0:
                            return Spell.FormatPercent("Maximum Pet Hit Points", base1);
                        case 1:
                            return Spell.FormatPercent("Maximum Pet Damage", base1 / 2);
                        default:
                            return String.Format("Unknown Pet Enhancement: {0}", max);
                    }
                case 108:
                    return String.Format("Summon Familiar: {0}", Extra);
                case 109:
                    return String.Format("Summon: [Item {0}]", base1);
                case 110:
                    if (calc == 200)
                        return Spell.FormatPercent("Archery Accuracy", base1);
                    else if (calc == 400)
                        return Spell.FormatPercent("Archery Damage", base1);
                    else
                        return String.Format("Unknown Archery Modifier: {0}", calc);
                case 111:
                    return Spell.FormatCount("All Resists", value);
                case 112:
                    return Spell.FormatCount("Effective Casting Level", value);
                case 113:
                    return String.Format("Summon Mount: {0}", Extra);
                case 114:
                    return Spell.FormatPercent("Agro", value - 100);
                case 115:
                    return "Reset Hunger Counter";
                case 116:
                    return Spell.FormatCount("Curse Counter", value);
                case 117:
                    return "Scrambles Chat";
                case 118:
                    return Spell.FormatCount("Singing Skill", value);
                case 119:
                    return Spell.FormatPercent("Melee Haste v3 (Above Cap)", value - 100);
                case 120:
                    return Spell.FormatPercent("Spell Damage Taken", base1); // not range
                case 121:
                    // damages the target whenever it hits something
                    return Spell.FormatCount("Reverse Damage Shield", -value);
                case 123:
                    //for (int j = 0; j < 12; j++)
                    //{
                    //    if (base1 == SlotEffects[j] && SlotEffects[j] > -1)
                    //        return String.Format("Forced Spell Stacking:  Slot {0} ", j + 1) + Spell.FormatEnum((SpellEffect)SlotEffects[j]);
                    //}
                    return "Forced Spell Stacking: Unknown";
                case 124:
                    switch (max)
                    {
                        case -5:
                            return Spell.FormatPercent("Disease Elemental DD and DoT Damage", base1);
                        case -4:
                            return Spell.FormatPercent("Poison Elemental DD and DoT Damage", base1);
                        case -3:
                            return Spell.FormatPercent("Cold Elemental DD and DoT Damage", base1);
                        case -2:
                            return Spell.FormatPercent("Fire Elemental DD and DoT Damage", base1);
                        case -1:
                            return Spell.FormatPercent("Magic Elemental DD and DoT Damage", base1);
                        case 0:
                            return Spell.FormatPercent("DD Damage", base1);
                        case 1:
                            return Spell.FormatPercent("DD and DoT Crit Rate", base1);
                        case 2:
                            return Spell.FormatPercent("Bane DD and DoT Damage", base1);
                        case 3:
                            return Spell.FormatPercent("DoT Damage", base1);
                        case 10:
                            return Spell.FormatPercent("Slow Percentage", base1);
                        default:
                            return String.Format("Unknown Effect Boost: {0}", max);
                    }
                case 125:
                    switch (max)
                    {
                        case 0:
                            return Spell.FormatPercent("Strength of Healing Spells", base1);
                        case 1:
                            return Spell.FormatPercent("Chance of Critical Heals", base1);
                        default:
                            return String.Format("Unknown Healing spell Boost: {0}", max);
                    }
                case 126:
                    return Spell.FormatCount("Charm Duration", base1) + String.Format(" tick{0}", (base1 > 1 ? "s":"")) + " and " + Spell.FormatPercent("Mez Duration", base1 * 10);
                case 127:
                    return Spell.FormatPercent("Casting Time", -base1 ).Replace("Increase", "Slow").Replace("Decrease", "Improve");//- 100);
                case 128:
                    return Spell.FormatPercent("Spell Duration", base1);
                case 129:
                    return Spell.FormatPercent("Spell Range", base1);
                case 130:
                    // i think this affects all special attacks. bash/kick/frenzy/etc...
                    return Spell.FormatPercent("Spell and Bash Hate", base1, base2);
                case 131:
                    return Spell.FormatPercent("Chance of Using Reagent", -base1);
                case 132:
                    return Spell.FormatPercent("Mana Cost", -value);
                case 134:
                    if (base2 == 0)
                        base2 = 100; // just to make it obvious that 0 means the focus stops functioning
                    return String.Format("Limit Max Level: {0} (lose {1}% per level)", base1, base2);
                case 135:
                    return String.Format("Limit Resist: {1}{0}", (SpellResist)Math.Abs(base1), base1 >= 0 ? "" : "Exclude ");
                case 136:
                    return String.Format("Limit Target: {1}{0}", Spell.FormatEnum((SpellTarget)Math.Abs(base1)), base1 >= 0 ? "" : "Exclude ");
                case 137:
                    return String.Format("Limit Effect: {1}{0}", Spell.FormatEnum((SpellEffect)Math.Abs(base1)), base1 >= 0 ? "" : "Exclude ");
                case 138:
                    return String.Format("Limit Type: {0}", base1 == 0 ? "Detrimental" : "Beneficial");
                case 139:
                    return String.Format("Excludes Spell: {1}[Spell {0}]", Math.Abs(base1), base1 >= 0 ? "" : "Exclude ");
                case 140:
                    return String.Format("Limit Min Duration: {0}s", base1 * 6);
                case 141:
                    return String.Format("Limit Max Duration: {0}s", 0);
                case 142:
                    return String.Format("Limit Min Level: {0}", base1);
                case 143:
                    return String.Format("Limit Min Casting Time: {0}s", base1 / 1000f);
                case 144:
                    return String.Format("Damage Shield: Causes [Spell {0}]", base1);
                case 145:
                    return String.Format("Fly Mode");
                case 146:
                    // has not been implemented in the game
                    return Spell.FormatCount("Electricity Resist", value);
                case 147:
                    return Spell.FormatCount("Current HP", base1);
                case 148:
                    //if (max > 1000) max -= 1000;
                    return String.Format("Stacking: Block new spell if slot {0} is '{1}' and < {2}", calc % 100, Spell.FormatEnum((SpellEffect)base1), max);
                case 149:
                    //if (max > 1000) max -= 1000;
                    return String.Format("Stacking: Overwrite existing spell if slot {0} is '{1}' and < {2}", calc % 100, Spell.FormatEnum((SpellEffect)base1), max);
                case 150:
                    return String.Format("Death Save ({0}%)", base1) + (max > 0 ? String.Format(" with {0} Heal", max): "");
                case 151:
                    switch (base1)
                    {
                        case 1:
                            return "Melee Automatically Critical Hits";
                        case 2:
                            return "Reduce Spell Damage Coming from Target by 15%";
                        case 3:
                            return "Spells Automatically Critical Hit";
                        case 4:
                            return "Melee Automatically Critical Hits (constructs only)";
                        default:
                            return String.Format("Unknown Curse: {0}", base1);
                    }
                case 152:
                    if (base1 > 1)
                        return String.Format("Swarm Pet (x{0}) -- {1}", base1, Extra);
                    else
                        return String.Format("Swarm Pet -- {0}", Extra);
                case 153:
                    // AUTOCAST for SOD - need to add spell id, and links same as recourse
                    return String.Format("Autocast: [Spell {0}]", base1);
                case 154:
                    return String.Format("Dispell Effect: [Spell {0}]", base1);
                case 199:
                    return Spell.FormatCount("Hate Transfer", base1);
                case 200:
                    return Spell.FormatCount("Hate", base1) + " per tick";
            }

            return String.Format("Unknown Effect: {0} Base1={1} Base2={2} Max={3} Calc={4} Value={5}", spa, base1, base2, max, calc, value);
        }

        static string Choose(int index, params string[] text)
        {
            if (index >= text.Length)
                return null;
            return text[index];
        }

        /// <summary>
        /// Calculate a duration.
        /// </summary>
        /// <returns>Numbers of ticks (6 second units)</returns>
        static public int CalcDuration(int calc, int max, int level)
        {
            int value = 0;

            switch (calc)
            {
                case 0:
                    value = 0;
                    break;
                case 1:
                    value = level / 2;
                    if (value < 1)
                        value = 1;
                    break;
                case 2:
                    value = (level / 2) + 5;
                    if (value < 6)
                        value = 6;
                    break;
                case 3:
                    value = level * 30;
                    break;
                case 4:
                    value = 50;
                    break;
                case 5:
                    value = 2;
                    break;
                case 6:
                    value = level / 2;
                    break;
                case 7:
                    value = level;
                    break;
                case 8:
                    value = level + 10;
                    break;
                case 9:
                    value = level * 2 + 10;
                    break;
                case 10:
                    value = level * 30 + 10;
                    break;
                case 11:
                    value = (level + 3) * 30;
                    break;
                case 12:
                    value = level / 2;
                    if (value < 1)
                        value = 1;
                    break;
                case 13:
                    value = level * 3 + 10;
                    break;
                case 14:
                    value = (level + 2) * 5;
                    break;
                case 15:
                    value = (level + 10) * 10;
                    break;
                case 50:
                    value = 72000;
                    break;
                case 3600:
                    value = 3600;
                    break;
                default:
                    value = max;
                    break;
            }

            if (max > 0 && value > max)
                value = max;

            return value;
        }

        /// <summary>
        /// Calculate a level/tick scaled value.
        /// </summary>
        static public int CalcValue(int calc, int base1, int max, int tick, int level)
        {
            if (calc == 0)
                return base1;

            if (calc == 100 || calc == 200 || calc == 400)
            {
                if (max > 0 && base1 > max)
                    return max;
                return base1;
            }

            int updownsign = 1;
            int ubase = Math.Abs(base1);

            if (max < 1 && max != 0)
            {
                // values are calculated down
                updownsign = -1;
            }
            int value = ubase;
            switch (calc)
            {
                case 100:
                    break;
                case 101:
                    value = updownsign * (ubase + (level / 2));
                    break;
                case 102:
                    value = updownsign * (ubase + level);
                    break;
                case 103:
                    value = updownsign * (ubase + (level * 2));
                    break;
                case 104:
                    value = updownsign * (ubase + (level * 3));
                    break;
                case 105:
                    value = updownsign * (ubase + (level * 4));
                    break;
                case 107:
                    value = updownsign * (ubase + tick);
                    break;
                case 108:
                    value = updownsign * (ubase + (2 * tick));
                    break;
                case 109:
                    value = updownsign * (ubase +  (level / 4));
                    break;
                case 110:
                    value = ubase + (level / 6);
                    break;
                case 111:
                    if (level > 16) value = updownsign * (ubase + ((level - 16) * 6));
                    break;
                case 112:
                    if (level > 24) value = updownsign * (ubase + ((level - 24) * 8));
                    break;
                case 113:
                    if (level > 34) value = updownsign * (ubase + ((level - 34) * 10));
                    break;
                case 114:
                    if (level > 44) value = updownsign * (ubase + ((level - 44) * 15));
                    break;
                case 115:
                    if (level > 15) value = updownsign * (ubase + ((level - 15) * 7));
                    break;
                case 116:
                    if (level > 24) value = ubase + ((level - 24) * 10);
                    break;
                case 117:
                    if (level > 34) value = ubase + ((level - 34) * 13);
                    break;
                case 118:
                    if (level > 44) value = ubase + ((level - 44) * 20);
                    break;
                case 119:
                    value = ubase + (level / 8);
                    break;
                case 120:
                    value = updownsign * (ubase + (5 * tick));
                    break;
                case 121:
                    value = ubase + (level / 3);
                    break;
                case 122:
                    int tick_remain = tick - 1;
                    if (tick_remain < 0)
                        tick_remain = 0;
                    value = updownsign * (base1 - (12 * tick_remain));
                    break;
                case 123:
                    // random in range
                    value = (Math.Abs(max) - ubase) / 2;
                    break;
                case 124:
                    if (level > 50) value = updownsign * (ubase + ((level - 50)));
                    break;
                case 125:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 2));
                    break;
                case 126:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 3));
                    break;
                case 127:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 4));
                    break;
                case 128:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 5));
                    break;
                case 129:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 10));
                    break;
                case 130:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 15));
                    break;
                case 131:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 20));
                    break;
                case 132:
                    if (level > 50) value = updownsign * (ubase + ((level - 50) * 25));
                    break;
                case 139:
                    if (level > 30) value = updownsign * (ubase + ((level - 30) / 2));
                    break;
                case 140:
                    if (level > 30) value = updownsign * (ubase + (level - 30));
                    break;
                case 141:
                    if (level > 30) value = updownsign * (ubase + (3 * (level - 30) / 2));
                    break;
                case 142:
                    if (level > 30) value = updownsign * (ubase + (2 * (level - 60)));
                    break;
                case 143:
                    value = updownsign * (ubase + (3 * level / 4));
                    break;

                default:
                    if (calc > 0 && calc < 1000)
                        value = ubase + (level * calc);

                    // 1000..1999 variable by tick
                    // e.g. splort (growing): Effect=0 Base1=1 Base2=0 Max=0 Calc=1035
                    //      34 - 69 - 104 - 139 - 174 - 209 - 244 - 279 - 314 - 349 - 384 - 419 - 454 - 489 - 524 - 559 - 594 - 629 - 664 - 699 - 699
                    // e.g. venonscale (decaying): Effect=0 Base1=-822 Base2=0 Max=822 Calc=1018
                    //
                    // e.g. Deathcloth Spore: Base1=-1000 Base2=0 Max=0 Calc=1999
                    // e.g. Bleeding Bite: Base1=-1000 Base2=0 Max=0 Calc=1100 (The damage done will decrease in severity over time.)
                    // e.g. Blood Rites: Base1=-1500 Base2=0 Max=0 Calc=1999
                    if (calc >= 1000 && calc < 2000)
                        value = updownsign * (ubase - (tick * (calc - 1000)));
                    // 2000..2999 variable by level
                    if (calc >= 2000)
                        value = ubase + (level * (calc - 2000));
                    break;
            }


            // now check result against the allowed maximum
            if (max != 0)
            {
                    if (updownsign == 1)
                    {
                            if (value > max)
                                    value = max;
                    }
                    else
                    {
                            if (value < max)
                                    value = max;
                    }
            }

            
            if (base1 < 0 && value > 0)
                value = -value;

            return value;
        }

        /// <summary>
        /// Calculate the min/max values for a scaled value.
        /// </summary>
        static public string CalcValueRange(int calc, int base1, int max, int duration, int level)
        {
            int start = CalcValue(calc, base1, max, 1, level);
            int finish = Math.Abs(CalcValue(calc, base1, max, duration, level));

            string type = Math.Abs(start) < Math.Abs(finish) ? "Growing" : "Decaying";

            if (calc == 123)
                return String.Format(" (Random: {0} to {1})", base1, max * ((base1 >= 0) ? 1 : -1));

            if (calc == 107)
                return String.Format(" ({0} to {1} @ 1/tick)", type, finish);

            if (calc == 108)
                return String.Format(" ({0} to {1} @ 2/tick)", type, finish);

            if (calc == 120)
                return String.Format(" ({0} to {1} @ 5/tick)", type, finish);

            if (calc == 122)
                return String.Format(" ({0} to {1} @ 12/tick)", type, finish);

            if (calc > 1000 && calc < 2000)
                return String.Format(" ({0} to {1} @ {2}/tick)", type, finish, calc - 1000);

            return null;
        }

        static public string FormatEnum(Enum e)
        {
            string type = e.ToString().Replace("__"," / ").Replace("_", " ").Trim();
            if (Regex.IsMatch(type, @"^-?\d+$"))
                type = "Type " + type; // undefined numeric enum
            else
                type = Regex.Replace(type, @"\d+$", ""); // remove numeric suffix on duplicate enums undead3/summoned3/etc
            return type;
        }
        static public string FormatEnumItem(Enum e)
        {
            string type = e.ToString().Replace("Summoned", "Summoned:").Replace("_s_", "'s_").Replace("___", "] ").Replace("__", " [").Replace("ccc", " (combat)").Replace("www", " (worn)").Replace("_", " ").Trim();
            if (Regex.IsMatch(type, @"^-?\d+$"))
                return "[Item " + type + "]"; // undefined numeric enum
            return type;
        }

        static private string FormatTime(float seconds)
        {
            if (seconds < 120)
                return seconds.ToString("0.##") + "s";

            if (seconds < 7200)
                return (seconds / 60f).ToString("0.#") + "m";

            return new TimeSpan(0, 0, (int)seconds).ToString();
        }

        static private string FormatCount(string name, int value)
        {
            return String.Format("{0} {1} by {2}", value < 0 ? "Decrease" : "Increase", name, Math.Abs(value));
        }

        static private string FormatCount(string name, int value, int minvalue, int level, int minlevel)
        {
            if (value != minvalue)
                return String.Format("{0} {1} by {2} (L{3}) to {4} (L{5})", value < 0 ? "Decrease" : "Increase", name, Math.Abs(minvalue), minlevel, Math.Abs(value), level);
            else
                return String.Format("{0} {1} by {2}", value < 0 ? "Decrease" : "Increase", name, Math.Abs(value));
        }

        static private string FormatPercent(string name, float value)
        {
            return Spell.FormatPercent(name, value, value);
        }

        static private string FormatPercent(string name, float min, float max)
        {
            if (Math.Abs(min) > Math.Abs(max))
            {
                float temp = min;
                min = max;
                max = temp;
            }

            if (min == 0 && max != 0)
                min = 1;

            if (min == max)
                return String.Format("{0} {1} by {2}%", max < 0 ? "Decrease" : "Increase", name, Math.Abs(max));

            return String.Format("{0} {1} by {2}% to {3}%", max < 0 ? "Decrease" : "Increase", name, Math.Abs(min), Math.Abs(max));
        }

        static private string FormatPercent(string name, float min, float max, int level, int minlevel)
        {
            if (Math.Abs(min) > Math.Abs(max))
            {
                float temp = min;
                min = max;
                max = temp;
            }

            if (min == 0 && max != 0)
                min = 1;

            if (min == max)
                return String.Format("{0} {1} by {2}%", max < 0 ? "Decrease" : "Increase", name, Math.Abs(max));

            return String.Format("{0} {1} by {2}% (L{4}) to {3}% (L{5})", max < 0 ? "Decrease" : "Increase", name, Math.Abs(min), Math.Abs(max), level, minlevel);
        }

        /*
        static private string FormatDesc()
        {
            // Spell descriptions include references to 12 spell attributes. e.g.
            // #7 - base1 for slot 7
            // @7 - calc(base1) for slot 7
            // $7 - base2 for slot 7
            return null;
        }
        */
    }

    public static class SpellParser
    {
        // the spell file is in US culture (dots are used for decimals)
        private static readonly CultureInfo culture = new CultureInfo("en-US", false);
        private static Dictionary<string, string> friendlynames = new Dictionary<string, string>(200);
        /// <summary>
        /// Load spell list from the EQ spell definition files.
        /// </summary>
        static public List<Spell> LoadFromFile(string spellPath, string descPath)
        {
            List<Spell> list = new List<Spell>(50000);
            Dictionary<int, Spell> listById = new Dictionary<int, Spell>(50000);

            //Dictionary<int, Spell> listByGroup = new Dictionary<int, Spell>(30000);

            // load description text file
            bool dbfile = descPath.Contains("dbstr_us");
            Dictionary<string, string> desc = new Dictionary<string, string>(50000);
            if (File.Exists(descPath))
                using (StreamReader text = File.OpenText(descPath))
                    while (!text.EndOfStream)
                    {
                        string line = text.ReadLine();
                        if (dbfile)
                        {
                            string[] fields = line.Split('^');
                            if (fields.Length < 3)
                                continue;
                            // 0 = id in type
                            // 1 = type
                            // 2 = description
                            // type 1 = AA names
                            // type 4 = AA desc
                            // type 5 = spell categories
                            // type 6 = spell desc
                            // type 7 = lore groups
                            // type 11 = illusions
                            // type 12 = body type
                            // type 16 = aug slot desc
                            // type 18 = currency
                            desc[fields[1] + "/" + fields[0]] = fields[2].Trim();
                        }
                        else
                        {
                            line = line.Replace("^", " ");
                            int index_start = line.IndexOf(" ");
                            if (index_start < 2)
                                continue;
                            int type_val = ParseInt(line.Substring(0, index_start));
                            if (type_val > 36000 && type_val < 40000)
                                desc["5/" + line.Substring(0, index_start)] = line.Substring(index_start).Trim();
                            else if (type_val > 39999)
                                desc["6/" + line.Substring(0, index_start)] = line.Substring(index_start).Trim();
                        }
                    }






            // load spell definition file
            if (File.Exists(spellPath))
                using (StreamReader text = File.OpenText(spellPath))
                    while (!text.EndOfStream)
                    {
                        string line = text.ReadLine();
                        string[] fields = line.Split('^');
                        Spell spell = LoadSpell(fields, dbfile);

#if !LimitMemoryUse
                        // all spells can be grouped into up to 2 categories (type 5 in db_str)
                        // ignore the "timer" categories because they are frequently wrong
                        List<string> cat = new List<string>();
                        string c1;
                        string c2;
                        if (desc.TryGetValue("5/" + spell.CategoryDescID[0], out c1))
                        {
                            // sub category 1
                            if (desc.TryGetValue("5/" + spell.CategoryDescID[1], out c2) && !c2.StartsWith("Timer"))
                                cat.Add(c1 + "/" + c2);

                            // sub category 2
                            if (desc.TryGetValue("5/" + spell.CategoryDescID[2], out c2) && !c2.StartsWith("Timer"))
                                cat.Add(c1 + "/" + c2);

                            // general category if no subcategories are defined
                            if (cat.Count == 0)
                                cat.Add(c1);
                        }

                        // add a timer category 
                        if (spell.TimerID > 0)
                            cat.Add("Timer " + spell.TimerID.ToString("D2"));
                        spell.Categories = cat.ToArray();
#endif

                        if (!desc.TryGetValue("6/" + spell.DescID, out spell.Desc))
                            spell.Desc = null;

                        list.Add(spell);
                        listById[spell.ID] = spell;
                        //if (spell.GroupID > 0)
                        //    listByGroup[spell.GroupID] = spell;
                    }

            // second pass fixes that require the entire spell list to be loaded already
            List<Spell> groups = list.FindAll(delegate(Spell x) { return x.GroupID > 0; });
            foreach (Spell spell in list)
            {
                // get list of linked spells
                List<int> linked = new List<int>(10);
                if (spell.RecourseID != 0)
                    linked.Add(spell.RecourseID);

                foreach (string s in spell.Slots)
                    if (s != null)
                    {
                        // match spell refs
                        //Match match = Spell.SpellRefExpr.Match(s);
                        //if (match.Success)
                        //    linked.Add(Int32.Parse(match.Groups[1].Value));
                        // effect 443 will references 2 spells. all other effects reference 1 at most
                        MatchCollection matches = Spell.SpellRefExpr.Matches(s);
                        foreach (Match m in matches)
                            if (m.Success)
                                linked.Add(Int32.Parse(m.Groups[1].Value));

                        // match spell group refs
                        Match match = Spell.GroupRefExpr.Match(s);
                        if (match.Success)
                        {
                            int id = Int32.Parse(match.Groups[1].Value);
                            // negate id on excluded spells so that we don't set extlevels on them
                            //if (s.Contains("Exclude"))
                            //    id = -id;
                            groups.FindAll(delegate(Spell x) { return x.GroupID == id; }).ForEach(delegate(Spell x) { linked.Add(x.ID); });
                        }
                    }

                // process each of the linked spells
                foreach (int id in linked)
                {
                    Spell target = null;
                    if (listById.TryGetValue(id, out target))
                    {
                        target.RefCount++;

                        // a lot of side effect spells do not have a level on them. this will copy the level of the referring spell
                        // onto the side effect spell so that the spell will be searchable when filtering by class.
                        // e.g. Jolting Swings Strike has no level so it won't show up in a ranger search even though Jolting Swings will show up
                        // we create this separate array and never display it because modifying the levels array would imply different functionality
                        // e.g. some spells purposely don't have levels assigned so that they are not affected by focus spells
                        for (int i = 0; i < spell.Levels.Length; i++)
                        {
                            if (target.ExtLevels[i] == 0 && spell.Levels[i] != 0)
                                target.ExtLevels[i] = spell.Levels[i];

                            // apply in the reverse direction too. this will probably only be useful for including type3 augs
                            // todo: check if this includes too many focus spells
                            //if (spell.ExtLevels[i] == 0 && target.Levels[i] != 0)
                            //    spell.ExtLevels[i] = target.Levels[i];
                        }
                    }
                }

                // revert negated IDs on excluded spells/groups
                for (int i = 0; i < linked.Count; i++)
                    if (linked[i] < 0)
                        linked[i] = -linked[i];

                spell.LinksTo = linked.ToArray();
            }

            return list;
        }

        static public void loadFriendlyNames()
        {
            friendlynames.Add("SumEarthR4", "Earth Elemental");
            friendlynames.Add("SpiritWolf30", "Spirit Wolf");
            friendlynames.Add("SpiritWolf38", "Spirit Wolf");
            friendlynames.Add("SpiritWolf42", "Spirit Wolf");
            friendlynames.Add("Animation1", "Animation");
            friendlynames.Add("Animation8", "Animation");
            friendlynames.Add("SumWaterR4", "Water Elemental");
            friendlynames.Add("SumFireR4", "Fire Elemental");
            friendlynames.Add("SumAirR4", "Air Elemental");
            friendlynames.Add("SumEarthR15", "Earth Elemental");
            friendlynames.Add("SumWaterR15", "Water Elemental");
            friendlynames.Add("Skeleton1", "Skeleton");
            friendlynames.Add("Skeleton8", "Skeleton");
            friendlynames.Add("Skeleton11", "Skeleton");
            friendlynames.Add("SumFireR15", "Fire Elemental");
            friendlynames.Add("SumAirR15", "Air Elemental");
            friendlynames.Add("SumEarthR8", "Earth Elemental");
            friendlynames.Add("SumWaterR8", "Water Elemental");
            friendlynames.Add("SumFireR8", "Fire Elemental");
            friendlynames.Add("SumAirR8", "Air Elemental");
            friendlynames.Add("SumEarthR11", "Earth Elemental");
            friendlynames.Add("SumWaterR11", "Water Elemental");
            friendlynames.Add("SumFireR11", "Fire Elemental");
            friendlynames.Add("SumAirR11", "Air Elemental");
            friendlynames.Add("Skeleton19", "Skeleton");
            friendlynames.Add("Skeleton27", "Skeleton");
            friendlynames.Add("Skeleton36", "Skeleton");
            friendlynames.Add("Skeleton44", "Skeleton");
            friendlynames.Add("Skeleton4", "Skeleton");
            friendlynames.Add("Skeleton15", "Skeleton");
            friendlynames.Add("Skeleton22", "Skeleton");
            friendlynames.Add("Skeleton31", "Skeleton");
            friendlynames.Add("Skeleton40", "Skeleton");
            friendlynames.Add("SumEarthR19", "Earth Elemental");
            friendlynames.Add("SumWaterR19", "Water Elemental");
            friendlynames.Add("SumFireR19", "Fire Elemental");
            friendlynames.Add("SumAirR19", "Air Elemental");
            friendlynames.Add("mistwoods", "Mistwoods");
            friendlynames.Add("SumEarthR23", "Earth Elemental");
            friendlynames.Add("SumWaterR23", "Water Elemental");
            friendlynames.Add("SumFireR23", "Fire Elemental");
            friendlynames.Add("SumAirR23", "Air Elemental");
            friendlynames.Add("SumEarthR27", "Earth Elemental");
            friendlynames.Add("SumWaterR27", "Water Elemental");
            friendlynames.Add("SumFireR27", "Fire  Elemental");
            friendlynames.Add("SumAirR27", "Air Elemental");
            friendlynames.Add("SpiritWolf34", "Spirit Wolf");
            friendlynames.Add("SumEarthR32", "Earth Elemental");
            friendlynames.Add("SumWaterR32", "Water Elemental");
            friendlynames.Add("SumFireR32", "Fire Elemental");
            friendlynames.Add("SumAirR32", "Air Elemental");
            friendlynames.Add("SumEarthR36", "Earth Elemental");
            friendlynames.Add("SumWaterR36", "Water Elemental");
            friendlynames.Add("SumFireR36", "Fire Elemental");
            friendlynames.Add("SumAirR36", "Air Elemental");
            friendlynames.Add("SumEarthR40", "Earth Elemental");
            friendlynames.Add("SumWaterR40", "Water Elemental");
            friendlynames.Add("SumFireR40", "Fire Elemental");
            friendlynames.Add("SumAirR40", "Air Elemental");
            friendlynames.Add("SumEarthR44", "Earth Elemental");
            friendlynames.Add("SumWaterR44", "Water Elemental");
            friendlynames.Add("SumFireR44", "Fire Elemental");
            friendlynames.Add("SumAirR44", "Air Elemental");
            friendlynames.Add("Animation4", "Animation");
            friendlynames.Add("Animation11", "Animation");
            friendlynames.Add("Animation15", "Animation");
            friendlynames.Add("Animation18", "Animation");
            friendlynames.Add("Animation22", "Animation");
            friendlynames.Add("Animation26", "Animation");
            friendlynames.Add("Animation31", "Animation");
            friendlynames.Add("Animation35", "Animation");
            friendlynames.Add("Animation39", "Animation");
            friendlynames.Add("Animation44", "Animation");
            friendlynames.Add("Mistwalker", "Mistwalker");
            friendlynames.Add("towertarhyl", "Tower of Tarhyl");
            friendlynames.Add("grobb", "Grobb");
            friendlynames.Add("southwaste", "Southern Waste of Tarhyl");
            friendlynames.Add("greenmist", "Greenmist");
            friendlynames.Add("shardmtns", "Obsidian Shard Mountains");
            friendlynames.Add("sunpeaks", "Red Sun Peaks");
            friendlynames.Add("nbadlands", "Northern Badlands");
            friendlynames.Add("surefall", "Surefall Glade");
            friendlynames.Add("everfrost", "Everfrost Peaks");
            friendlynames.Add("gfaydark", "Greater Faydark");
            friendlynames.Add("goblinskull", "Goblinskull Mountains");
            friendlynames.Add("steamfont", "Steamfont Mountains");
            friendlynames.Add("Familiar5", "Familiar");
            friendlynames.Add("seaswords", "Sea of Swords");
            friendlynames.Add("burningwood", "Burning Wood");
            friendlynames.Add("southnport", "South Newport");
            friendlynames.Add("ElementalHost", "Elemental Host");
            friendlynames.Add("SpiritHost", "Spirit Host");
            friendlynames.Add("CallOfXuzl", "Sword of Xuzl");
            friendlynames.Add("CrownStorms0", "Crown of Storms");
            friendlynames.Add("CrownStorms1", "Crown of Storms");
            friendlynames.Add("CrownStorms2", "Crown of Storms");
            friendlynames.Add("powater", "Plane of Water");
            friendlynames.Add("prison", "Prison of Admyrrza");
            friendlynames.Add("Skeleton58", "Skeleton");
            friendlynames.Add("Familiar8", "Familiar");
            friendlynames.Add("Familiar9", "Familiar");
            friendlynames.Add("pocketplane", "Pocket Plane");
            friendlynames.Add("centaur", "Centaur Hills");
            friendlynames.Add("rivervale", "Rivervale");
            friendlynames.Add("Familiar10", "Familiar");
            friendlynames.Add("overthere", "Overthere");
            friendlynames.Add("thurgadina", "Thurgadin");
            friendlynames.Add("westwastes", "Western Wastes");
            friendlynames.Add("poair", "Plane of Air");
            friendlynames.Add("same", "Same Zone");
            friendlynames.Add("faentharc", "Faentharc");
            friendlynames.Add("MonsterSum32", "Monster");
            friendlynames.Add("MonsterSum45", "Monster");
            friendlynames.Add("MonsterSum52", "Monster");
            friendlynames.Add("frosthorn", "Frosthorn");
            friendlynames.Add("greatdivide", "Great Divide");
            friendlynames.Add("wyvernfang", "Wyvernfang");
            friendlynames.Add("DruidPet", "Druid Pet");
            friendlynames.Add("cmalath", "Cyrtho Malath");
            friendlynames.Add("SpiritWolf48", "Spirit Wolf");
            friendlynames.Add("Skeleton48", "Skeleton");
            friendlynames.Add("Skeleton50", "Skeleton");
            friendlynames.Add("Skeleton52", "Skeleton");
            friendlynames.Add("SumEarthR49", "Earth Elemental");
            friendlynames.Add("SumWaterR49", "Water Elemental");
            friendlynames.Add("SumFireR49", "Fire Elemental");
            friendlynames.Add("SumAirR49", "Air Elemental");
            friendlynames.Add("SumEarthR52", "Earth Elemental");
            friendlynames.Add("SumWaterR52", "Water Elemental");
            friendlynames.Add("SumFireR52", "Fire Elemental");
            friendlynames.Add("SumAirR52", "Air Elemental");
            friendlynames.Add("SumHammer", "Summoned Hammer");
            friendlynames.Add("SumSword", "Summoned Sword");
            friendlynames.Add("Animation48", "Animation");
            friendlynames.Add("dreadfang", "Dreadfang Spire");
            friendlynames.Add("CrysSpider", "Crystal Spider");
            friendlynames.Add("heartland", "Heartland Plateau");
            friendlynames.Add("highkeep", "High Keep");
            friendlynames.Add("athicaa", "Athica");
            friendlynames.Add("Animation52", "Animation");
            friendlynames.Add("SumMageMultiElement", "Multi-Elemental");
            friendlynames.Add("Halflings", "Halflings");
            friendlynames.Add("SkeleHost", "Skeleton Host");
            friendlynames.Add("ncat", "Newport Sewers");
            friendlynames.Add("feerrott", "Greenmist");
            friendlynames.Add("trap", "Trap");
            friendlynames.Add("murk", "The Murk");
            friendlynames.Add("dreadlands", "Dreadlands");
            friendlynames.Add("potorment", "Plane of Torment");
            friendlynames.Add("misery", "Halls of Misery");
            friendlynames.Add("oggok", "Oggok");
            friendlynames.Add("ClayGolem", "Clay Golem");
            friendlynames.Add("LampSpirit", "Lamp Spirit");
            friendlynames.Add("ycrat", "Yclist Rat");
            friendlynames.Add("valor", "Plane of Valor");
            friendlynames.Add("seastorm", "Storm Sea");
            friendlynames.Add("smalath", "Sadri Malath");
            friendlynames.Add("ponightmare", "Plane of Nightmare");
            friendlynames.Add("rust", "The Rust");
            friendlynames.Add("SumFireR75", "Fire Elemental");
            friendlynames.Add("Familiar1", "Familiar");
            friendlynames.Add("Familiar2", "Familiar");
            friendlynames.Add("Familiar3", "Familiar");
            friendlynames.Add("Familiar4", "Familiar");
            friendlynames.Add("BLpet09", "Beastlord Pet");
            friendlynames.Add("BLpet22", "Beastlord Pet");
            friendlynames.Add("BLpet29", "Beastlord Pet");
            friendlynames.Add("BLpet37", "Beastlord Pet");
            friendlynames.Add("BLpet45", "Beastlord Pet");
            friendlynames.Add("BLpet47", "Beastlord Pet");
            friendlynames.Add("BLpet49", "Beastlord Pet");
            friendlynames.Add("BLpet51", "Beastlord Pet");
            friendlynames.Add("BLpet53", "Beastlord Pet");
            friendlynames.Add("BLpet14", "Beastlord Pet");
            friendlynames.Add("shadowhaven", "Shadowhaven");
            friendlynames.Add("TestHorseA", "Horse");
            friendlynames.Add("Familiar6", "Familiar");
            friendlynames.Add("Familiar7", "Familiar");
            friendlynames.Add("thaztower", "Thazeran's Tower");
            friendlynames.Add("sseru", "Sanctus Seru");
            friendlynames.Add("dragonhorn", "Dragonhorn Keep");
            friendlynames.Add("stormkeep", "Stormkeep");
            friendlynames.Add("Skeleton55", "Skeleton");
            friendlynames.Add("SumFireR55", "Fire Elemental");
            friendlynames.Add("SumAirR55", "Air Elemental");
            friendlynames.Add("SumWaterR55", "Water Elemental");
            friendlynames.Add("SumEarthR55", "Earth Elemental");
            friendlynames.Add("MonsterSum55", "Monster");
            friendlynames.Add("ServantRo", "Servant of Ro");
            friendlynames.Add("SumSword2", "Summoned Sword");
            friendlynames.Add("SpiritWolf52", "Spirit Wolf");
            friendlynames.Add("BLpet55", "Beastlord Pet");
            friendlynames.Add("pofire", "Azzerach, the Forsaken Realm");
            friendlynames.Add("Familiar11", "Familiar");
            friendlynames.Add("comercy", "City of Mercy");
            friendlynames.Add("night", "Plane of Nightmare");
            friendlynames.Add("poearth", "Plane of Earth");
            friendlynames.Add("ranger1", "Rangefinder");
            friendlynames.Add("ranger2", "Rangefinder");
            friendlynames.Add("ranger3", "Rangefinder");
            friendlynames.Add("poentropy", "Plane of Entropy");
            friendlynames.Add("prophets", "Prophet's Landing");
            friendlynames.Add("Skeleton65", "Skeleton");
            friendlynames.Add("MonsterSum66", "Monster");
            friendlynames.Add("BeastHost", "Beast Host");
            friendlynames.Add("BLpet66", "Beastlord Pet");
            friendlynames.Add("SumMageFusion", "Fusion Pet");
            friendlynames.Add("Celestwell", "Celestial Well");
            friendlynames.Add("ranger4", "Rangefinder");
            friendlynames.Add("magicdog", "Magic Dog");
            friendlynames.Add("DruidPet2", "Druid Pet");
            friendlynames.Add("underhill", "Underhill");
        }

        /// <summary>
        /// Parse a spell from a set of spell fields.
        /// </summary>
        static Spell LoadSpell(string[] fields, bool dbfile = false)
        {
            int MaxLevel = 65;
            int MinLevel = 65;

            Spell spell = new Spell();

            spell.ID = Convert.ToInt32(fields[0]);
            spell.Name = fields[1];
            //Target = fields[2];
            spell.Extra = fields[3];
            spell.ExtraBase = spell.Extra;

            if (spell.Extra.Length > 0 && friendlynames.ContainsKey(spell.Extra.ToString()))
                spell.Extra = friendlynames[spell.Extra.ToString()];
            spell.YouCast = fields[4];
            spell.OtherCast = fields[5];
            spell.LandOnSelf = fields[6];
            spell.LandOnOther = fields[7];
            spell.WearOffMessage = fields[8];
            spell.Range = ParseInt(fields[9]);
            spell.AERange = ParseInt(fields[10]);
            spell.PushBack = ParseFloat(fields[11]);
            spell.PushUp = ParseFloat(fields[12]);
            spell.CastingTime = ParseFloat(fields[13]) / 1000f;
            spell.RestTime = ParseFloat(fields[14]) / 1000f;
            spell.RecastTime = ParseFloat(fields[15]) / 1000f;
            spell.DurationCalc = ParseInt(fields[16]);
            spell.DurationBase = ParseInt(fields[17]);
            spell.DurationTicks = Spell.CalcDuration(ParseInt(fields[16]), ParseInt(fields[17]), MaxLevel);
            spell.AEDuration = ParseInt(fields[18]);
            spell.Mana = ParseInt(fields[19]);

            // 56 = icon
            // 57 = icon

            for (int i = 0; i < 3; i++)
            {
                spell.ConsumeItemID[i] = ParseInt(fields[58 + i]);
                spell.ConsumeItemCount[i] = ParseInt(fields[62 + i]);
                spell.FocusID[i] = ParseInt(fields[66 + i]);
            }

            //Light_Type = fields[82];
            spell.Beneficial = ParseBool(fields[83]);
            //Activated (AAs?) = fields[84];
            spell.ResistType = (SpellResist)ParseInt(fields[85]);
            spell.Target = (SpellTarget)ParseInt(fields[98]);
            // 99 =  base difficulty fizzle adjustment?
            spell.Skill = (SpellSkill)ParseInt(fields[100]);
            spell.Zone = (SpellZoneRestrict)ParseInt(fields[101]);
            //Environment Type = fields[102];
            //Time of Day = fields[103]; Day, Night, Both


            // each spell has a different casting level for all 16 classes
            for (int i = 0; i < spell.Levels.Length; i++)
            {
                spell.Levels[i] = (byte)ParseInt(fields[104 + i]);
                if (spell.Levels[i] < MinLevel)
                    MinLevel = spell.Levels[i];
            }

            //Casting Animation = fields[120];
            //Target Animation = fields[121];
            //Travel Type = fields[122];
            spell.SPAIdx = ParseInt(fields[123]);
            spell.CancelOnSit = ParseBool(fields[124]);

            // 125..141 deity casting restrictions
            // string[] gods = new string[] { "Agnostic", "Bertox", "Brell", "Cazic", "Erollisi", "Bristlebane", "Innoruuk", "Karana", "Mithanial", "Prexus", "Quellious", "Rallos", "Rodcet", "Solusek", "Tribunal", "Tunare", "Veeshan" };
            string[] gods = new string[] { "Unsworn", "Shiritri", "Tarhyl", "Sihala", "Shojar", "Gradalsh", "Tarhansar", "Sivyana", "Althuna", "Marlow", "Jayla", "Enthann", "Unknown", "Pariah", "Divine Light", "Malath", "Unknown" };
            for (int i = 0; i < gods.Length; i++)
                if (ParseBool(fields[125 + i]))
                    spell.Deity += gods[i] + " ";

            //NPC Do Not Cast = fields[142];
            //AI PT Bonus = fields[143];
            spell.Icon = ParseInt(fields[144]);
            //Spell Effect ID
            spell.Interruptable = !ParseBool(fields[146]);
            spell.ResistMod = ParseInt(fields[147]);
            // 148 = non stackable DoT
            // 149 = deletable
            spell.RecourseID = ParseInt(fields[150]);
            // 151 = used to prevent a nuke from being partially resisted.
            // also, it prevents or allows a player to resist a spell fully if they resist "part" of its components.
            spell.PartialResist = ParseBool(fields[151]);
            if (spell.RecourseID != 0)
                spell.Recourse = String.Format("[Spell {0}]", spell.RecourseID);
            //Small Targets Only = fields[152];
            //Persistent particle effects = fields [153];

            spell.ShortDuration = ParseBool(fields[154]);

            spell.DescID = ParseInt(fields[155]);

            spell.CategoryDescID[0] = ParseInt(fields[156]);
            spell.CategoryDescID[1] = ParseInt(fields[157]);
            spell.CategoryDescID[2] = ParseInt(fields[158]);
            if (spell.CategoryDescID[0] > 0)
                spell.CategoryIDs = spell.CategoryDescID[0].ToString();
            if (spell.CategoryDescID[1] > 0)
                spell.CategoryIDs = spell.CategoryIDs + "/" + spell.CategoryDescID[1].ToString();
            if (spell.CategoryDescID[2] > 0)
                spell.CategoryIDs = spell.CategoryIDs + "/" + spell.CategoryDescID[2].ToString();

            if (spell.CategoryDescID[0] == 0 && spell.CategoryDescID[1] == 0 && spell.CategoryDescID[2] == 0)
            {
                // If all 3 are set to 0, then likely an AA.  
                // Set the level field for useable classes to 254, if set to 1, to match AA ident.
                for (int i = 0; i < spell.Levels.Length; i++)
                {
                    if (spell.Levels[i] == 1)
                        spell.Levels[i] = 254;
                }
            }
            // 159 NPC Does not Require LoS
            // 160 Feedbackable (Triggers spell damage shield)
            spell.Reflectable = ParseBool(fields[161]);
            spell.HateMod = ParseInt(fields[162]);
            // 163 Resist Level = 19 values.  looks similar to calc values
            // 164 Resist Cap = 147 values. mostly negative
            // 165 Useable On Objects Boolean
            spell.Endurance = ParseInt(fields[166]);
            spell.TimerID = ParseInt(fields[167]);
            // 168 Skill = 3 values. 0, -1, 1
            // 169 Attack Open = all 0
            // 170 Defense Open = all 0
            // 171 Skill Open = all 0
            // 172 NPC Error Open= all 0

            // Dalaya stops at 173

            //spell.HateOverride = ParseInt(fields[173]);
            //spell.EnduranceUpkeep = ParseInt(fields[174]);
            //spell.MaxHitsType = (SpellMaxHits)ParseInt(fields[175]);
            //spell.MaxHits = ParseInt(fields[176]);
            // 177 PVP Resist Mod = 197 values.
            // 178 PVP Resist Level = 20 values. looks similar to calc values
            // 179 PVP Resist Cap = 266 values.
            // 180 Spell Category = 185 values.
            // 181 PVP Duration= 19 values. looks similar to duration calc values
            // 182 PVP Duration = 115 values.
            // 183 No Pet = 3 values. 0, 1, 2
            // 184 Cast While Sitting Boolean
            //spell.MGBable = ParseBool(fields[185]);
            //spell.Dispelable = !ParseBool(fields[186]);
            // 187 NPC Mem Category = npc stuff
            // 188 NPC Usefulness = 192 values.
            //spell.MinResist = ParseInt(fields[189]);
            //spell.MaxResist = ParseInt(fields[190]);
            //spell.MinViralTime = ParseInt(fields[191]);
            //spell.MaxViralTime = ParseInt(fields[192]);
            // 193 Particle Duration Time = 124 values. nimbus type effects
            //spell.ConeStartAngle = ParseInt(fields[194]);
            //spell.ConeEndAngle = ParseInt(fields[195]);
            //spell.Sneaking = ParseBool(fields[196]);
            //spell.DurationExtendable = !ParseBool(fields[197]);
            // 198 No Detrimental Spell Aggro Boolean
            // 199 Show Wear Off Message Boolean
            //spell.DurationFrozen = ParseBool(fields[200]);
            //spell.ViralRange = ParseInt(fields[201]);
            //spell.SongCap = ParseInt(fields[202]);
            // 203 Stacks With Self = melee specials
            // 204 Not Shown To Player Boolean
            //spell.BeneficialBlockable = !ParseBool(fields[205]); // for beneficial spells
            //Animation Variation = fields[206];
            //spell.GroupID = ParseInt(fields[207]);
            //spell.Rank = ParseInt(fields[208]); // rank 1/5/10. a few auras do not have this set properly
            //if (spell.Rank == 5 || spell.Name.EndsWith("II"))
             //   spell.Rank = 2;
            //if (spell.Rank == 10 || spell.Name.EndsWith("III"))
            //    spell.Rank = 3;
            // 209 No Resist Boolean = ignore SPA 180 resist?
            // 210 SpellBook Scribable Boolean
            //spell.TargetRestrict = (SpellTargetRestrict)ParseInt(fields[211]);
            //spell.AllowFastRegen = ParseBool(fields[212]);
            //spell.CastOutOfCombat = !ParseBool(fields[213]); //Cast in Combat
            // 214 Cast Out of Combat Boolean
            // 215 Show DoT Message Boolean
            // 216 Invalid Boolean
            // 217 Override Crit chance Boolean
            //spell.MaxTargets = ParseInt(fields[218]);
            // 219 No Effect from Spell Damage / Heal Amount on Items Boolean
            //spell.CasterRestrict = (SpellTargetRestrict)ParseInt(fields[220]);
            // 221 = spell class. 13 sequential values.
            // 222 = spell subclass. 57 sequential values.
            // 223 AI Valid Targets = 9 values. looks like a character class mask? 2013-3-13 Hand of Piety can now crit again.
            //spell.PersistAfterDeath = ParseBool(fields[224]);
            // 225 = song slope?
            // 226 = song offset?
            // range multiplier seems to be an integer so far
            //spell.RangeModCloseDist = ParseInt(fields[227]);
            //spell.RangeModCloseMult = ParseInt(fields[228]);
            //spell.RangeModFarDist = ParseInt(fields[229]);
            //spell.RangeModFarMult = ParseInt(fields[230]);
            //spell.MinRange = ParseInt(fields[231]);
            //spell.CannotRemove = ParseBool(fields[232]);
            //spell.Recourse Type = fields[233];
            //spell.CastInFastRegen = ParseBool(fields[234]);
            //spell.BetaOnly = ParseBool(fields[235]);
            //Spell Subgoup = fields[236];


            // debug stuff
            //spell.Unknown = ParseFloat(fields[217]);




            // each spell has 12 effect slots which have 5 attributes each
            // 20..31 - slot 1..12 base1 effect
            // 32..43 - slot 1..12 base2 effect
            // 44..55 - slot 1..12 max effect
            // 70..81 - slot 1..12 calc forumla data
            // 86..97 - slot 1..12 spa/type
            for (int i = 0; i < spell.Slots.Length; i++)
            {
                int spa = ParseInt(fields[86 + i]);
                int calc = ParseInt(fields[70 + i]);
                int max = ParseInt(fields[44 + i]);
                int base1 = ParseInt(fields[20 + i]);
                int base2 = ParseInt(fields[32 + i]);

                spell.SlotEffects[i] = spa;
                spell.Base1s[i] = base1;
                spell.Slots[i] = spell.ParseEffect(spa, base1, base2, max, calc, MaxLevel, MinLevel);
                

                if (spell.Slots[i] != null) 
                {
                    spell.AdvSlots[i] = String.Format("SPA={0} Base1={1} Base2={2} Max={3} Calc={4} --- ", spa, base1, base2, max, calc);
                }

                // debug stuff: detect difference in value/base1 for spells where i'm not sure which one should be used and have chosen one arbitrarily
                //int[] uses_value = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 21, 24, 35, 36, 46, 47, 48, 49, 50, 55, 58, 59, 69, 79, 92, 97, 100, 111, 116, 158, 159, 164, 165, 166, 169, 184, 189, 190, 192, 262, 334, 417};
                //int[] uses_base1 = new int[] { 32, 64, 109, 148, 149, 193, 254, 323, 360, 374, 414 };
                //int value = Spell.CalcValue(calc, base1, max, 0, 90);
                //if (value != base1 && Array.IndexOf(uses_value, spa) < 0 && Array.IndexOf(uses_base1, spa) < 0)
                //    Console.Error.WriteLine(String.Format("SPA {1} {0} has diff value/base1: {2}/{3} calc: {4}", spell.Name, spa, value, base1, calc));
            }

            // Spell stacking checks after all slots filled in with spell effect and base1 values, to get any missed matches
            for (int j = 0; j < 12; j++)
            {
                if (spell.SlotEffects[j] == 123)
                    for (int p = 0; p < 12; p++)
                    {
                        if (spell.Base1s[j] == spell.SlotEffects[p] && spell.SlotEffects[p] > -1)
                            spell.Slots[j] = String.Format("Forced Spell Stacking:  Slot {0} ", p + 1) + Spell.FormatEnum((SpellEffect)spell.SlotEffects[p]);
                    }
            }


            // debug stuff
            //if (spell.ID == 36293) for (int i = 0; i < fields.Length; i++) Console.WriteLine("{0}: {1}", i, fields[i]);

            spell.Prepare();
            return spell;
        }

        static float ParseFloat(string s)
        {
            if (String.IsNullOrEmpty(s))
                return 0f;
            return Single.Parse(s, culture);
        }

        static int ParseInt(string s)
        {
            if (s == "" || s == "0" || s[0] == '.')
                return 0;
            return (int)Single.Parse(s, culture);
        }

        static bool ParseBool(string s)
        {
            return !String.IsNullOrEmpty(s) && (s != "0");
        }

        public static int ParseClass(string text)
        {
            if (String.IsNullOrEmpty(text))
                return 0;

            if (text.Length == 3 && Enum.IsDefined(typeof(SpellClasses), text.ToUpper()))
                return (int)Enum.Parse(typeof(SpellClasses), text.ToUpper());

            string[] names = Enum.GetNames(typeof(SpellClassesLong));
            for (int i = 0; i < names.Length; i++)
                if (String.Compare(names[i], text, true) == 0)
                    return (int)Enum.Parse(typeof(SpellClassesLong), names[i]);

            return 0;
        }

    }

}
