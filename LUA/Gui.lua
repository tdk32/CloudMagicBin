-- Configurable Variables
local size = 1	-- this is the size of the "pixels" at the top of the screen that will show stuff, currently 5x5 because its easier to see and debug with

local GuiBaseFrame = CreateFrame("frame", "RecountGui", UIParent)
local GuiStuff = {On = 1, Rotation = 0,CoolDown = 0,Mode = .02, Auto = 0}
local GuiControlFrame = {}
for i=1, 2 do
	GuiControlFrame[i] = CreateFrame("frame","Control",ParentFrame)
	GuiControlFrame[i]:SetSize(size, size);
	
	GuiControlFrame[i]:SetPoint("TOPLEFT", size*(5+i), -size *12)                                         -- row 7, column 1 [Player Is Moving
	GuiControlFrame[i].t = GuiControlFrame[i]:CreateTexture()
	GuiControlFrame[i].t:SetColorTexture(1, 1, 1, 1)
	GuiControlFrame[i].t:SetAllPoints(GuiControlFrame[i])
	GuiControlFrame[i]:Show()
end
Global_Npc_Nameplate = 0
GuiControlFrame[1].t:SetColorTexture(GuiStuff.Rotation, GuiStuff.CoolDown, GuiStuff.Mode, 1)
GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, 1)
GuiBaseFrame.On = "|cFF00FF00"
GuiBaseFrame.Off = "|cFFFF0000"
GuiBaseFrame.Cleave = "|c008C8000"
GuiBaseFrame.RotationOn = false
GuiBaseFrame.Rotation = GuiBaseFrame.Off.."Off|r"
GuiBaseFrame.CoolDownOn = false
GuiBaseFrame.CoolDown = GuiBaseFrame.Off.."Off|r"
GuiBaseFrame.ModeOn = "Cleave"
GuiBaseFrame.Mode = GuiBaseFrame.Cleave .."Cleave|r"
GuiBaseFrame.Auto = false
GuiBaseFrame.AutoOn = GuiBaseFrame.Off .. "Off|r"
GuiBaseFrame:SetMovable(true)
GuiBaseFrame:EnableMouse(true)
GuiBaseFrame:SetSize(300, 64); 
GuiBaseFrame:SetPoint("CENTER", 0, 0)
GuiBaseFrame:RegisterEvent("ADDON_LOADED")
GuiBaseFrame:RegisterEvent("OnMouseDown")
GuiBaseFrame:RegisterEvent("OnMoauseUP")
GuiBaseFrame:RegisterEvent("OnHide")
GuiBaseFrame:SetFrameStrata("TOOLTIP");
GuiBaseFrame:SetScript("OnMouseDown", function(self, button)
  if button == "LeftButton" and not self.isMoving then
   self:StartMoving();
   self.isMoving = true;
  end
end)
GuiBaseFrame:SetScript("OnMouseUp", function(self, button)
  if self.isMoving then
   self:StopMovingOrSizing();
   self.isMoving = false;
  end
end)
GuiBaseFrame:SetScript("OnHide", function(self)
  if ( self.isMoving ) then
   self:StopMovingOrSizing();
   self.isMoving = false;
  end
end)
GuiBaseFrame.tex = GuiBaseFrame:CreateTexture("ARTWORK");
GuiBaseFrame.tex:SetAllPoints();
GuiBaseFrame.tex:SetColorTexture(0, 0, 0, .5)
GuiBaseFrame:Hide()
do
	if GuiBaseFrame:IsVisible() == false then
		GuiStuff.On = 0
		GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, 1)
	elseif GuiBaseFrame:IsVisible() then
		GuiStuff.On = 1
		GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, 1)
	end		
end

local CloseButton = CreateFrame("Button", "CloseButton", GuiBaseFrame, "UIPanelCloseButton") -- Parent the button to the main frame
CloseButton:SetPoint("TOPRIGHT",GuiBaseFrame, -5, -5)
CloseButton:SetWidth(10)
CloseButton:SetHeight(10)
CloseButton:Show()
CloseButton:RegisterEvent("OnClick")
CloseButton:SetScript("OnClick", function(self, arg1)
	GuiStuff.On = 0
  GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, alphaColor)
  GuiBaseFrame:Hide()
 end)


local RotationText = GuiBaseFrame:CreateFontString("RotationTxt") -- Parent the button to the main frame
RotationText:SetPoint("TopLeft",GuiBaseFrame, 10, -20)
RotationText:SetFont("Fonts\\FRIZQT__.TTF",16)
RotationText:SetText(format("Rotation :        %3s",GuiBaseFrame.Rotation))
RotationText:SetTextColor(1, 1,1, .5)
RotationText:Show()

local CoolDownText = GuiBaseFrame:CreateFontString("CoolDownTxt") -- Parent the button to the main frame
CoolDownText:SetPoint("TopLeft",GuiBaseFrame, 160, -20)
CoolDownText:SetFont("Fonts\\FRIZQT__.TTF",16)
CoolDownText:SetText(format("Cool Down : %3s ",GuiBaseFrame.CoolDown ))
CoolDownText:SetTextColor(1, 1,1, .5)
CoolDownText:Show()

local RotationMode = GuiBaseFrame:CreateFontString("AOETxt") -- Parent the button to the main frame
RotationMode:SetPoint("BottomLeft",GuiBaseFrame, 10, 10)
RotationMode:SetFont("Fonts\\FRIZQT__.TTF",16)
RotationMode:SetText(format("Mode: %-5s",GuiBaseFrame.Mode))
RotationMode:SetTextColor(1, 1, 1, .5)

local RotationAuto = GuiBaseFrame:CreateFontString("AOEAuto") -- Parent the button to the main frame
 RotationAuto:SetPoint("BottomLeft",GuiBaseFrame, 130, 10)
 RotationAuto:SetFont("Fonts\\FRIZQT__.TTF",16)
 RotationAuto:SetText(format("Auto: %-3s",GuiBaseFrame.AutoOn))
 RotationAuto:SetTextColor(1, 1, 1, .5)


local RotationNpc = GuiBaseFrame:CreateFontString("NPCcount") -- Parent the button to the main frame
RotationNpc:SetPoint("BottomLeft",GuiBaseFrame, 210, 10)
RotationNpc:SetFont("Fonts\\FRIZQT__.TTF",16)
RotationNpc:SetText(format("Targets : %2d",Global_Npc_Nameplate))
RotationNpc:SetTextColor(1, 1, 1, .5)
local function rotationNPCUdate()
		RotationNpc:SetText(format("Targets : %2d",Global_Npc_Nameplate))
	if GuiStuff.Auto == 0 then return end 
    if Global_Npc_Nameplate >= Auto.Cleave and Global_Npc_Nameplate < Auto.AOE then 
		GuiBaseFrame.ModeOn = "Cleave"	
		GuiStuff.Mode = .02
		GuiBaseFrame.Mode = GuiBaseFrame.Cleave.."Cleave|r"
	end
	if Global_Npc_Nameplate <= Auto.Single or Global_Npc_Nameplate < Auto.Cleave and Auto.Cleave ~= 99 or Auto.Cleave == 99 and Global_Npc_Nameplate < Auto.AOE  then
		GuiBaseFrame.ModeOn = "Single"	
		GuiStuff.Mode = .01
		GuiBaseFrame.Mode = GuiBaseFrame.On.."Single|r"
	end
	if Global_Npc_Nameplate >= Auto.AOE then 
		GuiBaseFrame.ModeOn = "AOE"	
		GuiStuff.Mode = .03
		GuiBaseFrame.Mode = GuiBaseFrame.Off.."AOE|r"
	end
	RotationMode:SetText(format("Mode : %-5s",GuiBaseFrame.Mode))
	GuiControlFrame[1].t:SetColorTexture(GuiStuff.Rotation, GuiStuff.CoolDown, GuiStuff.Mode, 1)
end
GuiBaseFrame:HookScript("OnUpdate", rotationNPCUdate)

local RotationModeButton = CreateFrame("Button", "ModeButton", GuiBaseFrame) -- Parent the button to the main frame
RotationModeButton:SetPoint("BottomLeft",GuiBaseFrame, 10, 10)
RotationModeButton.tex = RotationModeButton:CreateTexture("ARTWORK");
RotationModeButton.tex:SetAllPoints();
RotationModeButton.tex:SetColorTexture(1, 1, 1, 0)
RotationModeButton:SetWidth(130)
RotationModeButton:SetHeight(16)
RotationModeButton:Show()
RotationModeButton:RegisterEvent("OnClick")
RotationModeButton:SetScript("OnClick", function(self, arg1)
    if GuiBaseFrame.ModeOn == "AOE" then 
		GuiBaseFrame.ModeOn = "Cleave"	
		GuiStuff.Mode = .02
		GuiBaseFrame.Mode = GuiBaseFrame.Cleave.."Cleave|r"
	elseif GuiBaseFrame.ModeOn == "Cleave" then
		GuiBaseFrame.ModeOn = "Single"	
		GuiStuff.Mode = .01
		GuiBaseFrame.Mode = GuiBaseFrame.On.."Single|r"
	elseif GuiBaseFrame.ModeOn == "Single" then 
		GuiBaseFrame.ModeOn = "AOE"	
		GuiStuff.Mode = .03
		GuiBaseFrame.Mode = GuiBaseFrame.Off.."AOE|r"
	end
	RotationMode:SetText(format("Mode : %-5s",GuiBaseFrame.Mode))
	GuiControlFrame[1].t:SetColorTexture(GuiStuff.Rotation, GuiStuff.CoolDown, GuiStuff.Mode, 1)
end)
SetBindingClick(CMD.WoWGuiMode,RotationModeButton:GetName())



local RotationOnButton = CreateFrame("Button", "On/OffButton", GuiBaseFrame) -- Parent the button to the main frame
RotationOnButton:SetPoint("TopLeft",GuiBaseFrame, 10, -20)
RotationOnButton.tex = RotationOnButton:CreateTexture("ARTWORK");
RotationOnButton.tex:SetAllPoints();
RotationOnButton.tex:SetColorTexture(1, 1, 1, 0)
RotationOnButton:SetWidth(140)
RotationOnButton:SetHeight(16)
RotationOnButton:Show()
RotationOnButton:RegisterEvent("OnClick")
RotationOnButton:SetScript("OnClick", function(self, arg1)
    if GuiBaseFrame.RotationOn == false then 
		GuiBaseFrame.RotationOn = true	
		GuiBaseFrame.Rotation = GuiBaseFrame.On.."On|r"
		GuiStuff.Rotation = 1
	elseif GuiBaseFrame.RotationOn == true then
		GuiBaseFrame.RotationOn = false
		GuiBaseFrame.Rotation = GuiBaseFrame.Off.."Off|r"
		GuiStuff.Rotation = 0
	end
	RotationText:SetText(format("Rotation :        %3s",GuiBaseFrame.Rotation))
	GuiControlFrame[1].t:SetColorTexture(GuiStuff.Rotation, GuiStuff.CoolDown, GuiStuff.Mode, 1)
end)
SetBindingClick(CMD.WoWGuiStart, RotationOnButton:GetName())

local RotationAutoButton = CreateFrame("Button", "AutoButton", GuiBaseFrame) -- Parent the button to the main frame
RotationAutoButton:SetPoint("BottomLeft",GuiBaseFrame,130, 10)
RotationAutoButton.tex = RotationAutoButton:CreateTexture("ARTWORK");
RotationAutoButton.tex:SetAllPoints();
RotationAutoButton.tex:SetColorTexture(1, 1, 1, 0)
RotationAutoButton:SetWidth(130)
RotationAutoButton:SetHeight(16)
RotationAutoButton:Show()
RotationAutoButton:RegisterEvent("OnClick")
RotationAutoButton:SetScript("OnClick", function(self, arg1)
    if GuiBaseFrame.Auto == false then 
		GuiBaseFrame.Auto = true	
		GuiBaseFrame.AutoOn = GuiBaseFrame.On.."On|r"
		GuiStuff.Auto = 1
	elseif GuiBaseFrame.Auto == true then
		GuiBaseFrame.Auto = false
		GuiBaseFrame.AutoOn = GuiBaseFrame.Off.."Off|r"
		GuiStuff.Auto = 0
	end
	RotationAuto:SetText(format("Auto: %-3s",GuiBaseFrame.AutoOn))
	GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, 1)
end)
SetBindingClick(CMD.WoWGuiAuto,RotationAutoButton:GetName())


local CoolDownOnButton = CreateFrame("Button", "CoolDownButton", GuiBaseFrame) -- Parent the button to the main frame
CoolDownOnButton:SetPoint("TopLeft",GuiBaseFrame, 160, -20)
CoolDownOnButton.tex = CoolDownOnButton:CreateTexture("ARTWORK");
CoolDownOnButton.tex:SetAllPoints();
CoolDownOnButton.tex:SetColorTexture(1, 1, 1, 0)
CoolDownOnButton:SetWidth(140)
CoolDownOnButton:SetHeight(16)
CoolDownOnButton:Show()
CoolDownOnButton:RegisterEvent("OnClick")
CoolDownOnButton:SetScript("OnClick", function(self, arg1)
    if GuiBaseFrame.CoolDownOn == false then 
		GuiBaseFrame.CoolDownOn = true	
		GuiBaseFrame.CoolDown = GuiBaseFrame.On.."On|r"
		GuiStuff.CoolDown = 1
	elseif GuiBaseFrame.CoolDownOn == true then
		GuiBaseFrame.CoolDownOn = false
		GuiBaseFrame.CoolDown = GuiBaseFrame.Off.."Off|r"
		GuiStuff.CoolDown = 0
	end
	CoolDownText:SetText(format("Cool Down : %-3s ", GuiBaseFrame.CoolDown ))
	GuiControlFrame[1].t:SetColorTexture(GuiStuff.Rotation, GuiStuff.CoolDown, GuiStuff.Mode, 1)
end)
SetBindingClick(CMD.WoWGuiCoolDown,CoolDownOnButton:GetName())

local SLASH_COMMAND = "/CM"
local function slashHandler(param,editbox)
	if param == "toggle" then
		if GuiBaseFrame:IsVisible() == false then
			GuiBaseFrame:Show()
			GuiStuff.On = 1
			GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, 1)
		elseif GuiBaseFrame:IsVisible() then
			GuiBaseFrame:Hide()
			GuiStuff.On = 0
			GuiControlFrame[2].t:SetColorTexture(GuiStuff.Auto, 0, GuiStuff.On, 1)
		end		
	end
end

SlashCmdList["COMMAND"] = slashHandler
SLASH_COMMAND1 = "/CM"
