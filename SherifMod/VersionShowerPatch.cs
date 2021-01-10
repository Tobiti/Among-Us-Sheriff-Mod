using System;
using HarmonyLib;

namespace SheriffMod
{
	[HarmonyPatch(typeof(BOCOFLHKCOJ), "Start")]
	public static class VersionShowerPatch
	{
		public static void Postfix(BOCOFLHKCOJ __instance)
		{
			AELDHKGBIFD text = __instance.text;
			text.Text += "\nloaded Sheriff Mod v1.0 rebuild by Tobiti ";
		}
	}
}
