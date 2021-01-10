using System;
using HarmonyLib;

namespace SheriffMod
{
	// Token: 0x02000009 RID: 9
	[HarmonyPatch]
	public static class ShipStatusPatch
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002CFB File Offset: 0x00000EFB
		[HarmonyPatch(typeof(HLBNNHFCNAJ), "Start")]
		public static void Postfix(HLBNNHFCNAJ __instance)
		{
			PlayerControlPatch.lastKilled = DateTime.UtcNow;
			PlayerControlPatch.lastKilled = PlayerControlPatch.lastKilled.AddSeconds(-15.0);
		}
	}
}
