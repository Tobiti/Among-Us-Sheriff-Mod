using System;
using HarmonyLib;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class ShipStatusPatch
	{
		[HarmonyPatch(typeof(HLBNNHFCNAJ), "Start")]
		public static void Postfix(HLBNNHFCNAJ __instance)
		{
			PlayerControlPatch.lastKilled = DateTime.UtcNow;
			PlayerControlPatch.lastKilled = PlayerControlPatch.lastKilled.AddSeconds(-15.0);
		}
	}
}
