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
			text.Text += "\nSheriff Mod v1.2.5 by Tobiti/Woodi-dev";
		}
	}
}
