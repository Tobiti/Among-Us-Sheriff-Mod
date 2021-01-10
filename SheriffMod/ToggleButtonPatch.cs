using System;
using HarmonyLib;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class ToggleButtonPatch
	{
		[HarmonyPatch(typeof(BCLDBBKFJPK), "Toggle")]
		public static bool Prefix(BCLDBBKFJPK __instance)
		{
			bool flag = __instance.TitleText.Text == "Show Sheriff";
			bool result;
			if (flag)
			{
				CustomGameOptions.showSheriff = !CustomGameOptions.showSheriff;
				FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
				__instance.NHLMDAOEOAE = CustomGameOptions.showSheriff;
				__instance.CheckMark.enabled = CustomGameOptions.showSheriff;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
	}
}
