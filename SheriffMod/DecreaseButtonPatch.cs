using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheriffMod
{
	[HarmonyPatch]
	class DecreaseButtonPatch
    {
		[HarmonyPatch(typeof(PCGDGFIAJJI), "Decrease")]
		public static bool Prefix(PCGDGFIAJJI __instance)
		{
			if (__instance.TitleText.Text == "Sheriffs")
			{
				GameOptionMenuPatchUpdate.AddToSheriffCount(-1);
				return false;
			}
			if (__instance.TitleText.Text == "Sheriff Cooldown")
			{
				GameOptionMenuPatchUpdate.AddToSheriffCooldown(-2.5f);
				return false;
			}
			return true;
		}
	}
}
