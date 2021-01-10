using System;
using HarmonyLib;
using UnityEngine;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class IntroCutScenePatch
	{
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
