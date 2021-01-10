using System;
using HarmonyLib;

namespace SheriffMod
{
	// Token: 0x0200000A RID: 10
	[HarmonyPatch]
	public static class MeetingHUDPatchClose
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002D20 File Offset: 0x00000F20
		[HarmonyPatch(typeof(OOCJALPKPEP), "Close")]
		public static void Postfix(OOCJALPKPEP __instance)
		{
			PlayerControlPatch.lastKilled = DateTime.UtcNow;
			PlayerControlPatch.lastKilled = PlayerControlPatch.lastKilled.AddSeconds(8.0);
		}
	}
}
