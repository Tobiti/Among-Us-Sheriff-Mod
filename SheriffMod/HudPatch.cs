using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using MeetingHUD = OOCJALPKPEP;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class HudPatch
	{
		private static int counter = 0;
		private static int countercounter = 0;
		public static MLPJGKEACMM KillButton = null;
		private static string GameSettingsText = null;

		public static void FixDeadBodies()
		{
			System.Collections.Generic.List<DDPGLPLGFOI> list = UnityEngine.Object.FindObjectsOfType<DDPGLPLGFOI>().ToList<DDPGLPLGFOI>();
			for (int i = 0; i < list.Count - 1; i++)
			{
				DDPGLPLGFOI ddpglplgfoi = list[i];
				DDPGLPLGFOI ddpglplgfoi2 = list[i + 1];
				bool flag = Vector3.Distance(ddpglplgfoi.transform.position, ddpglplgfoi2.transform.position) < 0.2f;
				if (flag)
				{
					ddpglplgfoi2.transform.position += new Vector3(0.2f, 0.2f, 0f);
					break;
				}
			}
		}

		public static void UpdateGameSettingsText(PIEFJFEOGOL __instance)
		{
			bool flag = __instance.GameSettings.Text.Split(new char[]
			{
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries).Count<string>() == 19;
			if (flag)
			{
				HudPatch.GameSettingsText = __instance.GameSettings.Text;
			}
			bool flag2 = HudPatch.GameSettingsText != null;
			if (flag2)
			{
				String options = HudPatch.GameSettingsText;
				options += String.Format("Show Sheriff: {0}\n", CustomGameOptions.showSheriff ? "On" : "Off");
				options += $"Sheriffs: {CustomGameOptions.sheriffCount}\n";
				options += $"Sheriff Cooldown: {CustomGameOptions.sheriffCooldown}s";
				__instance.GameSettings.Text = options;
			}
		}

		public static void updateGameOptions(KMOGFLPJLLK options)
		{
			Il2CppSystem.Collections.Generic.List<FFGALNAPKCD> allPlayerControls = FFGALNAPKCD.AllPlayerControls;
			foreach (FFGALNAPKCD ffgalnapkcd in allPlayerControls)
			{
				ffgalnapkcd.RpcSyncSettings(options);
			}
		}

		public static void updateMeetingHUD(OOCJALPKPEP __instance)
		{
			foreach (HDJGDMFCHDN hdjgdmfchdn in __instance.HBDFFAHBIGI)
			{
				bool flag = PlayerControlPatch.sheriffs.Select(player => player.name).Contains(hdjgdmfchdn.NameText.Text);
				if (flag)
				{
					bool flag2 = CustomGameOptions.showSheriff | PlayerControlPatch.isSheriff(FFGALNAPKCD.LocalPlayer);
					if (flag2)
					{
						hdjgdmfchdn.NameText.Color = new Color(1f, 0.8f, 0f, 1f);
					}
				}
			}
		}

		[HarmonyPatch(typeof(PIEFJFEOGOL), "Update")]
		public static void Postfix(PIEFJFEOGOL __instance)
		{
			HudPatch.KillButton = __instance.KillButton;
			if (MeetingHUD.Instance != null)
			{
				HudPatch.updateMeetingHUD(MeetingHUD.Instance);
			}
			HudPatch.UpdateGameSettingsText(__instance);
			if (FFGALNAPKCD.LocalPlayer == null)
			{
				return;
			}

			UpdateKillButton();

			if (HudPatch.counter < 30)
			{
				HudPatch.counter++;
			}
			else
			{
				HudPatch.counter = 0;
				HudPatch.countercounter++;
				if (GameOptionMenuPatchUpdate.showSheriff != null)
				{
					GameOptionMenuPatchUpdate.showSheriff.gameObject.SetActive(UnityEngine.Object.FindObjectsOfType<PHCKLDDNJNP>().Count != 0);
				}
				if (GameOptionMenuPatchUpdate.sheriffCount != null)
				{
					GameOptionMenuPatchUpdate.sheriffCount.gameObject.SetActive(UnityEngine.Object.FindObjectsOfType<PHCKLDDNJNP>().Count != 0);
				}
				if (GameOptionMenuPatchUpdate.sheriffCooldown != null)
				{
					GameOptionMenuPatchUpdate.sheriffCooldown.gameObject.SetActive(UnityEngine.Object.FindObjectsOfType<PHCKLDDNJNP>().Count != 0);
				}
				if (!(HudPatch.countercounter < 5))
				{
					HudPatch.countercounter = 0;
				}
			}
		}

		private static void UpdateKillButton()
		{
			// Check if local player is dead
			if (FFGALNAPKCD.LocalPlayer.NDGFFHMFGIG.DLPCKPBIJOE)
			{
				HudPatch.KillButton.gameObject.SetActive(false);
				HudPatch.KillButton.isActive = false;
				return;
			}
			if (FFGALNAPKCD.AllPlayerControls.Count > 1 && PlayerControlPatch.sheriffs != null && PlayerControlPatch.sheriffs.Count > 0)
			{
				if (PlayerControlPatch.isSheriff(FFGALNAPKCD.LocalPlayer))
				{
					if (MeetingHUD.Instance == null)
					{
						FFGALNAPKCD.LocalPlayer.nameText.Color = new Color(1f, 0.8f, 0f, 1f);
						HudPatch.KillButton.gameObject.SetActive(true);
						HudPatch.KillButton.isActive = true;
						HudPatch.KillButton.SetCoolDown(PlayerControlPatch.SheriffKillTimer(), CustomGameOptions.sheriffCooldown);
						PlayerControlPatch.closestPlayer = PlayerControlPatch.getClosestPlayer(FFGALNAPKCD.LocalPlayer);
						double distBetweenPlayers = PlayerControlPatch.getDistBetweenPlayers(FFGALNAPKCD.LocalPlayer, PlayerControlPatch.closestPlayer);
						if (distBetweenPlayers < 1.2)
						{
							HudPatch.KillButton.SetTarget(PlayerControlPatch.closestPlayer);
						}
					}
					else
					{
						HudPatch.KillButton.isActive = false;
					}
				} 
			}
		}
	}
}
