﻿// winifix@gmail.com
// ReSharper disable UnusedMember.Global

using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using CloudMagic.Helpers;

namespace CloudMagic.Rotation
{
    public class DemonHunterHavoc : CombatRoutine
    {
        private readonly Stopwatch interruptwatch = new Stopwatch();
		
        public override string Name 
		{
			get
			{
				return "CloudMagic DemonHunter";
			}
		}

        public override string Class 
		{
			get
			{
				return "DemonHunter";
			}
		}
        public override int SINGLE 
		{
			get 
			{ 
				return 1; 
			} 
		}
		public override int CLEAVE 
		{ 
			get 
			{ 
				return 99;
			} 
		}
        public override int AOE 
		{ 
			get 
			{ 
				return 99; 
			} 
		}
		public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.DrawHorizontalLine();            
            Log.WriteCloudMagic("Welcome to CloudMagic Demon Hunter", Color.Black);
            Log.Write("Spec: " + WoW.PlayerSpec);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {			
			if (WoW.HealthPercent == 0 || WoW.IsMounted) return;
            if (!WoW.HasTarget || !WoW.TargetIsEnemy ) return;
			if (WoW.PlayerIsChanneling) return;
			
			if (!WoW.IsSpellInRange("Felblade")) 
			{
				if (WoW.CanCast("ThrowGlaives"))
				{
					WoW.CastSpell("ThrowGlaives");
				}
				return;
			}			
			
            // Cast Nemesis on your primary target, synchronise with Metamorphosis and Chaos Blades if possible.
            if (WoW.HasBossTarget || UseCooldowns)
            {
                if (WoW.CanCast("Nemesis"))
                {
                    WoW.CastSpell("Nemesis"); // Off the GCD no return
                }
                if (WoW.CanCast("Metamorphosis"))  // This requires macro to "/cast [@player] Metamorphosis"
                {
                    WoW.CastSpell("Metamorphosis"); // Off the GCD no return
                }
                if (WoW.CanCast("ChaosBlades") && 
                    WoW.Talent(7) == 1) // If we have taken Chaos Blades Talent
                {
                    WoW.CastSpell("ChaosBlades"); // Off the GCD no return                    
                }
            }

            // Cast Fel Rush if about to hit 2 charges.
            //if (WoW.CanCast("FelRush") && WoW.PlayerSpellCharges("FelRush") == 2)
            //{                
            //    WoW.CastSpell("FelRush");             
            //    return;
            //}

            // Cast Fel Barrage at 5 charges.
            if (WoW.CanCast("FelBarrage") && WoW.PlayerSpellCharges("FelBarrage") == 5 && 
                WoW.Talent(7) == 2)  // If we have taken Fel Barrage Talent
            {
                WoW.CastSpell("FelBarrage");
                return;
            }

            // Cast Fury of the Illidari.
            if (WoW.CanCast("FOTI"))
            {
                WoW.CastSpell("FOTI");
                return;
            }

            // Cast Eye Beam to trigger Demonic.
            if (WoW.CanCast("EyeBeam") &&
                WoW.Talent(7) == 3 && // If we have taken Demonic Talent
				WoW.Fury > 50) 
            {
                WoW.CastSpell("EyeBeam");
                return;
            }

            // Cast Metamorphosis if available and not active. - this is done above to ensure its timed with other CD's
            // Cast Chaos Blades. - this is done above to ensure its timed with Meta and other CD's
            // Cast Felblade if available and more than 30 Fury from your cap.
            if (WoW.CanCast("Felblade") &&
                WoW.Fury <= 100) // ToDo: Here we assume Fury Cap is 130, no clue if something changes that.               
            {
                WoW.CastSpell("Felblade");
                return;
            }

            // Cast Blade Dance / Death Sweep with First Blood.
            if (WoW.CanCast("BladeDance") && 
                WoW.Fury >= 15 && 
                WoW.Talent(3) == 2) // If we have taken First Blood Talent
            {
                WoW.CastSpell("BladeDance");
                return;
            }

            // Cast Chaos Strike / Annihilation.
            if (!WoW.PlayerHasBuff("Metamorphosis"))
            {
                if (WoW.CanCast("ChaosStrike") && WoW.Fury >= 40)
                {
                    WoW.CastSpell("ChaosStrike");
                    return;
                }
            }
            else
            {
                if (WoW.CanCast("Annihilation") && WoW.Fury >= 40)
                {
                    WoW.CastSpell("Annihilation");
                    return;
                }
            }

            // Cast Demon's Bite if Demon Blades is not taken.
            if (WoW.CanCast("DemonsBite") && WoW.Talent(2) != 2)
            {
                WoW.CastSpell("DemonsBite");
                return;
            }

            // Cast Throw Glaive if nothing else is available during empty Globals with Demon Blades.
            if (WoW.CanCast("ThrowGlaives") && WoW.Talent(2) == 2)
            {
                WoW.CastSpell("ThrowGlaives");
                return;
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=WiNiFiX
AddonName=CloudMagic
WoWVersion=Legion - 70200
[SpellBook.db]
Spell,206491,Nemesis,D1
Spell,191427,Metamorphosis,A
Spell,211048,ChaosBlades,D7
Spell,195072,FelRush,U
Spell,211053,FelBarrage,D3
Spell,201467,FOTI,D5
Spell,162243,DemonsBite,T
Spell,162794,ChaosStrike,Y
Spell,201427,Annihilation,Y
Spell,185123,ThrowGlaives,S
Spell,188499,BladeDance,D8
Spell,232893,Felblade,E
Spell,198013,EyeBeam,H
Spell,183752,ConsumeMagic,B
Aura,162264,Metamorphosis
*/
