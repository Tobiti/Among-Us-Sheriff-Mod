using System;
using System.Linq;
using HarmonyLib;
using UnhollowerBaseLib;
using UnityEngine;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class GameOptionMenuPatchStart
	{

        [HarmonyPatch(typeof(PHCKLDDNJNP), "Start")]
		public static void Postfix(PHCKLDDNJNP __instance)
		{
			bool flag = UnityEngine.Object.FindObjectsOfType<BCLDBBKFJPK>().Count == 4;
			if (flag)
			{
				BCLDBBKFJPK bcldbbkfjpk = (from x in UnityEngine.Object.FindObjectsOfType<BCLDBBKFJPK>().ToList<BCLDBBKFJPK>()
				where x.TitleText.Text == "Anonymous Votes"
				select x).First<BCLDBBKFJPK>();
				// Create show sheriff label
				GameOptionMenuPatchUpdate.showSheriff = UnityEngine.Object.Instantiate<BCLDBBKFJPK>(bcldbbkfjpk);
				GameOptionMenuPatchUpdate.showSheriff.TitleText.Text = "Show Sheriff";
				GameOptionMenuPatchUpdate.showSheriff.NHLMDAOEOAE = CustomGameOptions.showSheriff;
				GameOptionMenuPatchUpdate.showSheriff.CheckMark.enabled = CustomGameOptions.showSheriff;

				// Create sheriff count label
				PCGDGFIAJJI emergencyMeetingBox = (from x in UnityEngine.Object.FindObjectsOfType<PCGDGFIAJJI>().ToList<PCGDGFIAJJI>()
										   where x.TitleText.Text == "# Emergency Meetings"
										   select x).First<PCGDGFIAJJI>();
				GameOptionMenuPatchUpdate.sheriffCount = UnityEngine.Object.Instantiate<PCGDGFIAJJI>(emergencyMeetingBox);
				GameOptionMenuPatchUpdate.sheriffCount.TitleText.Text = "Sheriffs";
				GameOptionMenuPatchUpdate.sheriffCount.NHLMDAOEOAE = CustomGameOptions.sheriffCount;
				GameOptionMenuPatchUpdate.sheriffCount.Value = CustomGameOptions.sheriffCount;
				GameOptionMenuPatchUpdate.sheriffCount.ValueText.Text = CustomGameOptions.sheriffCount.ToString();

				// Create sheriff cooldown label
				PCGDGFIAJJI impostorCooldownBox = (from x in UnityEngine.Object.FindObjectsOfType<PCGDGFIAJJI>().ToList<PCGDGFIAJJI>()
										   where x.TitleText.Text == "Emergency Cooldown"
										   select x).First<PCGDGFIAJJI>();
				GameOptionMenuPatchUpdate.sheriffCooldown = UnityEngine.Object.Instantiate<PCGDGFIAJJI>(impostorCooldownBox);
				GameOptionMenuPatchUpdate.sheriffCooldown.TitleText.Text = "Sheriff Cooldown";
				GameOptionMenuPatchUpdate.sheriffCooldown.NHLMDAOEOAE = CustomGameOptions.sheriffCooldown;
				GameOptionMenuPatchUpdate.sheriffCooldown.Value = CustomGameOptions.sheriffCooldown;
				GameOptionMenuPatchUpdate.sheriffCooldown.ValueText.Text = CustomGameOptions.sheriffCooldown.ToString();

				LLKOLCLGCBD[] array = new LLKOLCLGCBD[__instance.KJFHAPEDEBH.Count + 3];
				__instance.KJFHAPEDEBH.ToArray<LLKOLCLGCBD>().CopyTo(array, 0);
				array[array.Length - 3] = GameOptionMenuPatchUpdate.showSheriff;
				array[array.Length - 2] = GameOptionMenuPatchUpdate.sheriffCount;
				array[array.Length - 1] = GameOptionMenuPatchUpdate.sheriffCooldown;
				__instance.KJFHAPEDEBH = new Il2CppReferenceArray<LLKOLCLGCBD>(array);
			}
		}
	}
}
