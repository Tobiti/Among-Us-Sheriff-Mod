using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class GameOptionMenuPatchUpdate
	{
		public static BCLDBBKFJPK showSheriff;
		public static PCGDGFIAJJI sheriffCount;
		public static PCGDGFIAJJI sheriffCooldown;

		[HarmonyPatch(typeof(PHCKLDDNJNP), "Update")]
		public static void Postfix(PHCKLDDNJNP __instance)
		{

			BCLDBBKFJPK bcldbbkfjpk = (from x in UnityEngine.Object.FindObjectsOfType<BCLDBBKFJPK>().ToList<BCLDBBKFJPK>()
			where x.TitleText.Text == "Anonymous Votes"
			select x).First<BCLDBBKFJPK>();
			GameOptionMenuPatchUpdate.showSheriff.transform.position = bcldbbkfjpk.transform.position - new Vector3(0f, 5.5f, 0f);
			GameOptionMenuPatchUpdate.sheriffCooldown.transform.position = GameOptionMenuPatchUpdate.showSheriff.transform.position - new Vector3(0f, 0.5f, 0f);
			GameOptionMenuPatchUpdate.sheriffCount.transform.position = GameOptionMenuPatchUpdate.sheriffCooldown.transform.position - new Vector3(0f, 0.5f, 0f);
		}

        internal static void AddToSheriffCooldown(float addValue)
		{
			CustomGameOptions.sheriffCooldown = Mathf.Max(CustomGameOptions.sheriffCooldown + addValue, 10);
			FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
			GameOptionMenuPatchUpdate.sheriffCooldown.NHLMDAOEOAE = CustomGameOptions.sheriffCooldown;
			GameOptionMenuPatchUpdate.sheriffCooldown.Value = CustomGameOptions.sheriffCooldown;
			GameOptionMenuPatchUpdate.sheriffCooldown.ValueText.Text = CustomGameOptions.sheriffCooldown.ToString();
		}

        public static void AddToSheriffCount(int addValue)
		{
			CustomGameOptions.sheriffCount = Mathf.Max(CustomGameOptions.sheriffCount + addValue, 0);
			FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
			GameOptionMenuPatchUpdate.sheriffCount.NHLMDAOEOAE = CustomGameOptions.sheriffCount;
			GameOptionMenuPatchUpdate.sheriffCount.Value = CustomGameOptions.sheriffCount;
			GameOptionMenuPatchUpdate.sheriffCount.ValueText.Text = CustomGameOptions.sheriffCount.ToString();
		}
	}
}
