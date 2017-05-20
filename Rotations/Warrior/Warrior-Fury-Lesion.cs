// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using CloudMagic.Helpers;
using CloudMagic.GUI;


namespace CloudMagic.Rotation
{
    public class FuryWarrior : CombatRoutine
    {
		//will check a list of spells and interrupt ones deemed important by wowhead guides
        private CheckBox mplusint;		
		private CheckBox TrueFrothingZerkerFalseMassacre;
		
		
		 	
		public override string Name
        {
            get { return "Fury Warrior"; }
        }

        
		 public override string Class
        {
            get { return "Warrior"; }
        }
		        
		private static bool TFZFMint
        {
            get
            {
                var TFZFMint = ConfigFile.ReadValue("FuryWarrior", "TFZFMint").Trim();

                return TFZFMint != "" && Convert.ToBoolean(TFZFMint);
            }
            set { ConfigFile.WriteValue("FuryWarrior", "TFZFMint", value.ToString()); }
        }
		
		
        private static bool mythicplusinterrupts
        {
            get
            {
                var mythicplusinterrupts = ConfigFile.ReadValue("ProtectionLesion", "mythicplusinterrupts").Trim();

                return mythicplusinterrupts != "" && Convert.ToBoolean(mythicplusinterrupts);
            }
            set { ConfigFile.WriteValue("ProtectionLesion", "mythicplusinterrupts", value.ToString()); }
        }
		public override Form SettingsForm { get; set; }
		
		
        public override void Initialize()
        {
            Log.Write("Welcome to Fury Warrior", Color.Green);
			Log.Write("Written based on WoWHead Guide ", Color.Green);
			
			
			
			
			SettingsForm = new Form {Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 350, Height = 250, ShowIcon = false};

            //var picBox = new PictureBox {Left = 0, Top = 0, Width = 800, Height = 100, Image = TopLogo};
            //SettingsForm.Controls.Add(picBox);

            var lblTFZFMintText = new Label //12; 114 LEFT is first value, Top is second.
            {
                Text = "Tick for Frothing, Untick for Massacre",
                Size = new Size(188, 13), //81; 13
                Left = 12,
                Top = 14
            };
            SettingsForm.Controls.Add(lblTFZFMintText); //113; 114 

            TrueFrothingZerkerFalseMassacre = new CheckBox {Checked = TFZFMint, TabIndex = 2, Size = new Size(15, 14), Left = 200, Top = 14};
            SettingsForm.Controls.Add(TrueFrothingZerkerFalseMassacre);
			//
						
			var lblMythicPlusText = new Label //12; 129 is first value, Top is second.
            {
                Text = "M+ Interrupts",
                Size = new Size(188, 13), //95; 13
                Left = 12,
                Top = 44
            };
            SettingsForm.Controls.Add(lblMythicPlusText); //113; 114 

            mplusint = new CheckBox {Checked = mythicplusinterrupts, TabIndex = 4, Size = new Size(15, 14), Left = 200, Top = 44};
            SettingsForm.Controls.Add(mplusint);
			
			
			var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 15, Top = 150, Size = new Size(120, 48)};
			
			TrueFrothingZerkerFalseMassacre.Checked = TFZFMint;
			mplusint.Checked = mythicplusinterrupts;
			
			cmdSave.Click += CmdSave_Click;
			TrueFrothingZerkerFalseMassacre.CheckedChanged += TFZFM_Click;
            mplusint.CheckedChanged += MP_Click;
			
			SettingsForm.Controls.Add(cmdSave);
            lblTFZFMintText.BringToFront();
			lblMythicPlusText.BringToFront();
			
			Log.Write("---------------------------------------------------------", Color.Blue);
            Log.Write("IF True = Frothing Zerk, IF False = Massacre = ", Color.Red);
			Log.Write("=" + TFZFMint, Color.Blue);
           	Log.Write("---------------------------------------------------------", Color.Blue);
		}	
			private void CmdSave_Click(object sender, EventArgs e)
        {
            TFZFMint = TrueFrothingZerkerFalseMassacre.Checked;
            mythicplusinterrupts = mplusint.Checked;
            			
            MessageBox.Show("Settings saved", "CloudMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }
			
        private void TFZFM_Click(object sender, EventArgs e)
        {
            TFZFMint = TrueFrothingZerkerFalseMassacre.Checked;
        }
		
				
		private void MP_Click(object sender, EventArgs e)
        {
            mythicplusinterrupts = mplusint.Checked;
        }
		
        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
			/*
			Endless Rage / War Machine
			Double Time
			Avatar
			Warpaint
			Frothing Zerker/Massacre
			Inner Rage
			Reckless Abandon
			*/
				
								
			//AOE on Press
			
			

                //Normal ST rotation
                if (WoW.HasTarget && WoW.IsInCombat && WoW.TargetIsEnemy && WoW.IsSpellInRange("Bloodthirst"))
                {
                    if (WoW.TargetHealthPercent >= 20)
					{
                        //When targets are above 20%. Not in Execute phase. (need to change this to TTExecute)
                        //Single target with Reckless Abandon -  Battle Cry  Avatar  Rampage -  Raging Blow -  Odyn's Fury -  Bloodthirst - Raging Blow -  Furious Slash -  Bloodthirst
						
						if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_KEY_Q) < 0)
							
							{
								
							if (WoW.CanCast("Whirlwind")
								&& TFZFMint
								&& !WoW.PlayerHasBuff("Meat Cleaver")
								&& WoW.Rage <= 99)
							{WoW.CastSpell("Whirlwind"); return;}
								
							if (WoW.CanCast("Whirlwind")
								&& !TFZFMint
								&& !WoW.PlayerHasBuff("Meat Cleaver")
								&& WoW.Rage <= 85)
							{WoW.CastSpell("Whirlwind"); return;}
							
							/*if (WoW.CanCast("Whirlwind")
								&& TFZFMint
								&& WoW.Rage <= 99
								&& !WoW.CanCast("Bloodthirst")
								&& !WoW.CanCast("Raging Blow")
								&& !WoW.CanCast("Rampage")
								)
							{WoW.CastSpell("Whirlwind"); return;}
							
							if (WoW.CanCast("Whirlwind")
								&& !TFZFMint
								&& WoW.Rage <= 85
								&& !WoW.CanCast("Bloodthirst")
								&& !WoW.CanCast("Raging Blow")
								&& !WoW.CanCast("Rampage")
								)
							{WoW.CastSpell("Whirlwind"); return;}
								
								*/
							}
						
						
						
                        if (TFZFMint)
                        {
                            if (WoW.CanCast("Rampage")
                                && WoW.Rage > 99)
                            { WoW.CastSpell("Rampage"); return; }

                            if (WoW.CanCast("Bloodthirst")
                                && !WoW.PlayerHasBuff("Enrage"))
                            { WoW.CastSpell("Bloodthirst"); return; }

                            if (WoW.CanCast("Raging Blow")
								&& WoW.Rage <= 85
                                && !WoW.CanCast("Bloodthirst"))
                            { WoW.CastSpell("Raging Blow"); return; }

                            if (WoW.CanCast("Bloodthirst")
                                && WoW.PlayerHasBuff("Enrage"))
                            { WoW.CastSpell("Bloodthirst"); return; }

                            if (WoW.CanCast("Furious Slash")
                                && !WoW.CanCast("Bloodthirst")
                                && !WoW.CanCast("Raging Blow"))
                            { WoW.CastSpell("Furious Slash"); return; }
                        }
					

                    if (!TFZFMint)
                    {
                        if (WoW.CanCast("Rampage")
							&& WoW.Rage >= 85)
                        { WoW.CastSpell("Rampage"); return; }

                        if (WoW.CanCast("Bloodthirst")
                            && !WoW.PlayerHasBuff("Enrage"))
                        { WoW.CastSpell("Bloodthirst"); return; }

                        //if (WoW.CanCast("Execute")
						//	&& WoW.TargetHealthPercent <= 20
                        //    && WoW.IsSpellOverlayed("Execute") || WoW.Rage >= 25)
                        //{ WoW.CastSpell("Execute"); return; }

                        if (WoW.CanCast("Raging Blow")
							&& WoW.Rage <= 85
                            && !WoW.CanCast("Bloodthirst"))
                        { WoW.CastSpell("Raging Blow"); return; }

                        if (WoW.CanCast("Bloodthirst")
                            && WoW.PlayerHasBuff("Enrage"))
                        { WoW.CastSpell("Bloodthirst"); return; }

                        if (WoW.CanCast("Furious Slash")
                            && !WoW.CanCast("Bloodthirst")
                            && !WoW.CanCast("Raging Blow"))
                        { WoW.CastSpell("Furious Slash"); return; }
                    }
					}

                    if (WoW.TargetHealthPercent <= 20)

                    {
                        if (TFZFMint)
                        {
                            if (WoW.CanCast("Rampage")
                                && WoW.Rage >= 100)
                            { WoW.CastSpell("Rampage"); return; }

                            if (WoW.CanCast("Bloodthirst")
                                && !WoW.PlayerHasBuff("Enrage"))
                            { WoW.CastSpell("Bloodthirst"); return; }

                            if (WoW.CanCast("Execute")
                                && WoW.IsSpellOverlayed("Execute") || WoW.Rage >= 25)
                            { WoW.CastSpell("Execute"); return; }

                            if (WoW.CanCast("Execute")
                                && WoW.Rage > 25)
                            { WoW.CastSpell("Execute"); return; }

                            if (WoW.CanCast("Raging Blow")
                                && !WoW.CanCast("Bloodthirst"))
                            { WoW.CastSpell("Raging Blow"); return; }

                            if (WoW.CanCast("Bloodthirst")
                                && WoW.PlayerHasBuff("Enrage"))
                            { WoW.CastSpell("Bloodthirst"); return; }

                            if (WoW.CanCast("Furious Slash")
                                && !WoW.CanCast("Bloodthirst")
                                && !WoW.CanCast("Raging Blow"))
                            { WoW.CastSpell("Furious Slash"); return; }
                        }

                        if (!TFZFMint)
                        {
                            if (WoW.CanCast("Execute")
                                && !WoW.PlayerHasBuff("Enrage")
                                && !WoW.PlayerHasBuff("Massacre"))
                            { WoW.CastSpell("Execute"); return; }

                            if (WoW.CanCast("Rampage")
                                && WoW.PlayerHasBuff("Massacre"))
                            { WoW.CastSpell("Rampage"); return; }

                            if (WoW.CanCast("Execute")
                                && WoW.PlayerHasBuff("Enrage"))
                            { WoW.CastSpell("Execute"); return; }

                            if (WoW.CanCast("Bloodthirst")
                                && WoW.Rage < 25)
                            { WoW.CastSpell("Bloodthirst"); return; }

                            if (WoW.CanCast("Raging Blow")
                                && WoW.Rage < 25)
                            { WoW.CastSpell("Raging Blow"); return; }

                        }
                    }
				
						if (mythicplusinterrupts)
                    {
                        if (WoW.CanCast("Pummel") && WoW.TargetIsCasting && WoW.TargetIsCastingAndSpellIsInterruptible)
                        {
//int spell list for all important spells in M+                        
                            if (WoW.TargetCastingSpellID == 200248
//Court Of Stars Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 225573 || WoW.TargetCastingSpellID == 208165 || WoW.TargetCastingSpellID == 211401 || WoW.TargetCastingSpellID == 21147 ||
                                WoW.TargetCastingSpellID == 211299 || WoW.TargetCastingSpellID == 2251 || WoW.TargetCastingSpellID == 209413 || WoW.TargetCastingSpellID == 209404 ||
                                WoW.TargetCastingSpellID == 215204 || WoW.TargetCastingSpellID == 210261
//Darkheart Thicket Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 200658 || WoW.TargetCastingSpellID == 200631 || WoW.TargetCastingSpellID == 204246 || WoW.TargetCastingSpellID == 2014
//Eye of Azshara Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 19687 || WoW.TargetCastingSpellID == 218532 || WoW.TargetCastingSpellID == 195129 || WoW.TargetCastingSpellID == 195046 ||
                                WoW.TargetCastingSpellID == 197502 || WoW.TargetCastingSpellID == 196027 || WoW.TargetCastingSpellID == 196175 || WoW.TargetCastingSpellID == 192003 ||
                                WoW.TargetCastingSpellID == 191848
//Halls of Valor Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 198595 || WoW.TargetCastingSpellID == 198962 || WoW.TargetCastingSpellID == 198931 || WoW.TargetCastingSpellID == 192563 ||
                                WoW.TargetCastingSpellID == 192288 || WoW.TargetCastingSpellID == 199726
//Maw of Souls Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 198495 || WoW.TargetCastingSpellID == 195293 || WoW.TargetCastingSpellID == 199589 || WoW.TargetCastingSpellID == 194266 ||
                                WoW.TargetCastingSpellID == 198405 || WoW.TargetCastingSpellID == 199514 || WoW.TargetCastingSpellID == 194657
//Neltharions Lair Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 193585 || WoW.TargetCastingSpellID == 202181
//The Arcway Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 226269 || WoW.TargetCastingSpellID == 211007 || WoW.TargetCastingSpellID == 211757 || WoW.TargetCastingSpellID == 226285 ||
                                WoW.TargetCastingSpellID == 226206 || WoW.TargetCastingSpellID == 211115 || WoW.TargetCastingSpellID == 196392
// Advisor Vandros (Interrupt manually) Spell,203176,Accelerating Blast
                                || WoW.TargetCastingSpellID == 203957
//Vault of the Wardens Mythic+ Interrupt list
                                || WoW.TargetCastingSpellID == 193069 || WoW.TargetCastingSpellID == 191823 || WoW.TargetCastingSpellID == 202661 || WoW.TargetCastingSpellID == 201488 ||
                                WoW.TargetCastingSpellID == 195332
//Raid Interrupts
                                || WoW.TargetCastingSpellID == 209485 || WoW.TargetCastingSpellID == 209410 || WoW.TargetCastingSpellID == 211470 || WoW.TargetCastingSpellID == 225100 ||
                                WoW.TargetCastingSpellID == 207980 || WoW.TargetCastingSpellID == 196870 || WoW.TargetCastingSpellID == 195284 || WoW.TargetCastingSpellID == 192005 ||
                                WoW.TargetCastingSpellID == 228255 || WoW.TargetCastingSpellID == 228239 || WoW.TargetCastingSpellID == 227917 || WoW.TargetCastingSpellID == 228625 ||
                                WoW.TargetCastingSpellID == 228606 || WoW.TargetCastingSpellID == 229714 || WoW.TargetCastingSpellID == 227592 || WoW.TargetCastingSpellID == 229083 ||
                                WoW.TargetCastingSpellID == 228025 || WoW.TargetCastingSpellID == 228019 || WoW.TargetCastingSpellID == 227987 || WoW.TargetCastingSpellID == 227420 ||
                                WoW.TargetCastingSpellID == 200905)

                            {
                                if (!WoW.IsSpellOnCooldown("Pummel") && WoW.TargetPercentCast >= 40)
                                {
                                    WoW.CastSpell("Pummel");
                                    return;
                                }
							}
				
						}
					}
				
				
                }	
			}
				
				
							
			
					
			
            if (combatRoutine.Type == RotationType.AOE)
		    {
				// AOE stuff here
			}
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
            }
			
		}
	}
}

	
	


			
            
    


/*
[AddonDetails.db]
AddonAuthor=Dupe
AddonName=badgggggggggerui
WoWVersion=Legion - 70100
[SpellBook.db]
Spell,205545,Odyn's Fury,D1
Spell,23881,Bloodthirst,D2
Spell,184367,Rampage,D3
Spell,85288,Raging Blow,D4
Spell,100130,Furious Slash,D5
Spell,118000,Dragon Roar,F1
Spell,1719,Battle Cry,F3
Spell,5308,Execute,Z
Spell,190411,Whirlwind,D6
Aura,184362,Enrage
Aura,206316,Massacre
Aura,85739,Meat Cleaver
*/
