using System;
using HarmonyLib;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class MeetingHUDPatchClose
	{
		[HarmonyPatch(typeof(OOCJALPKPEP), "Close")]
		public static void Postfix(OOCJALPKPEP __instance)
		{
			PlayerControlPatch.lastKilled = DateTime.UtcNow;
			PlayerControlPatch.lastKilled = PlayerControlPatch.lastKilled.AddSeconds(8.0);
		}
	}
}
