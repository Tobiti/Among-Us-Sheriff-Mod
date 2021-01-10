using System;
using HarmonyLib;
using UnityEngine;

namespace SheriffMod
{
	// Token: 0x0200000B RID: 11
	[HarmonyPatch]
	public static class IntroCutScenePatch
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002D48 File Offset: 0x00000F48
		[HarmonyPatch(typeof(PENEIDJGGAF.CKACLKCOJFO), "MoveNext")]
		public static void Postfix(PENEIDJGGAF.CKACLKCOJFO __instance)
		{
			bool flag = PlayerControlPatch.isSheriff(FFGALNAPKCD.LocalPlayer);
			if (flag)
			{
				__instance.field_Public_PENEIDJGGAF_0.Title.Text = "Sheriff";
				__instance.field_Public_PENEIDJGGAF_0.Title.Color = new Color(1f, 0.8f, 0f, 1f);
				__instance.field_Public_PENEIDJGGAF_0.ImpostorText.Text = "Shoot the [FF0000FF]Impostor";
				__instance.field_Public_PENEIDJGGAF_0.BackgroundBar.material.color = new Color(1f, 0.8f, 0f, 1f);
			}
		}
	}
}
