using System;
using System.Collections.Generic;
using HarmonyLib;
using Hazel;
using UnhollowerBaseLib;
using UnityEngine;
using System.Linq;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class PlayerControlPatch
	{

		public static FFGALNAPKCD closestPlayer;
		public static List<FFGALNAPKCD> sheriffs = new List<FFGALNAPKCD>();
		public static DateTime lastKilled;

		[HarmonyPatch(typeof(FFGALNAPKCD), "HandleRpc")]
		public static void Postfix(byte HKHMBLJFLMC, MessageReader ALMCIJKELCP)
		{
			switch (HKHMBLJFLMC)
			{
				case (byte)CustomRPC.SetSheriff:
					{
						byte b = ALMCIJKELCP.ReadByte();
						foreach (FFGALNAPKCD ffgalnapkcd in FFGALNAPKCD.AllPlayerControls)
						{
							bool flag = ffgalnapkcd.PlayerId == b;
							if (flag)
							{
								PlayerControlPatch.sheriffs.Add(ffgalnapkcd);
								bool showSheriff = CustomGameOptions.showSheriff;
								if (showSheriff)
								{
									ffgalnapkcd.nameText.Color = new Color(1f, 0.8f, 0f, 1f);
								}
							}
						}
						break;
					}
				case (byte)CustomRPC.SyncCustomSettingsShowSheriff:
					CustomGameOptions.showSheriff = ALMCIJKELCP.ReadBoolean();
					CustomGameOptions.sheriffCount = ALMCIJKELCP.ReadInt32();
					break;
				case (byte)CustomRPC.SheriffKill:
					{
						FFGALNAPKCD playerById = PlayerControlPatch.getPlayerById(ALMCIJKELCP.ReadByte());
						FFGALNAPKCD playerById2 = PlayerControlPatch.getPlayerById(ALMCIJKELCP.ReadByte());
						bool flag2 = PlayerControlPatch.isSheriff(playerById);
						if (flag2)
						{
							playerById.MurderPlayer(playerById2);
						}
						break;
					}
			}
		}

		public static bool isSheriff(FFGALNAPKCD player)
		{
			return isSheriff(player.PlayerId);
		}
		public static bool isSheriff(byte playerId)
		{
			return PlayerControlPatch.sheriffs.Select(player => player.PlayerId).Contains(playerId);
		}

		public static FFGALNAPKCD getPlayerById(byte id)
		{
			foreach (FFGALNAPKCD ffgalnapkcd in FFGALNAPKCD.AllPlayerControls)
			{
				bool flag = ffgalnapkcd.PlayerId == id;
				if (flag)
				{
					return ffgalnapkcd;
				}
			}
			return null;
		}

		public static float SheriffKillTimer()
		{
			DateTime dateTime = PlayerControlPatch.lastKilled;
			bool flag = false;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				DateTime utcNow = DateTime.UtcNow;
				TimeSpan timeSpan = utcNow - PlayerControlPatch.lastKilled;
				float num = (CustomGameOptions.sheriffCooldown) * 1000f;
				bool flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = (num - (float)timeSpan.TotalMilliseconds) / 1000f;
				}
			}
			return result;
		}

		public static List<FFGALNAPKCD> getCrewMates(Il2CppReferenceArray<EGLJNOMOGNP.DCJMABDDJCF> infection)
		{
			List<FFGALNAPKCD> list = new List<FFGALNAPKCD>();
			foreach (FFGALNAPKCD teammate in FFGALNAPKCD.AllPlayerControls)
			{
				if (!infection.Select(player => player.LAOEJKHLKAI.PlayerId).Contains(teammate.PlayerId)) 
				{
					list.Add(teammate);
				}
			}
			return list;
		}

		public static FFGALNAPKCD getClosestPlayer(FFGALNAPKCD refplayer)
		{
			double num = double.MaxValue;
			FFGALNAPKCD result = null;
			foreach (FFGALNAPKCD ffgalnapkcd in FFGALNAPKCD.AllPlayerControls)
			{
				bool dlpckpbijoe = ffgalnapkcd.NDGFFHMFGIG.DLPCKPBIJOE;
				if (!dlpckpbijoe)
				{
					bool flag = ffgalnapkcd.PlayerId != refplayer.PlayerId;
					if (flag)
					{
						double distBetweenPlayers = PlayerControlPatch.getDistBetweenPlayers(ffgalnapkcd, refplayer);
						bool flag2 = distBetweenPlayers < num;
						if (flag2)
						{
							num = distBetweenPlayers;
							result = ffgalnapkcd;
						}
					}
				}
			}
			return result;
		}

		public static double getDistBetweenPlayers(FFGALNAPKCD player, FFGALNAPKCD refplayer)
		{
			Vector2 truePosition = refplayer.GetTruePosition();
			Vector2 truePosition2 = player.GetTruePosition();
			return Math.Sqrt((double)((truePosition[0] - truePosition2[0]) * (truePosition[0] - truePosition2[0]) + (truePosition[1] - truePosition2[1]) * (truePosition[1] - truePosition2[1])));
		}

		[HarmonyPatch(typeof(FFGALNAPKCD), "RpcSetInfected")]
		public static void Postfix(Il2CppReferenceArray<EGLJNOMOGNP.DCJMABDDJCF> JPGEIBIBJPJ)
		{
			MessageWriter messageWriter = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, 40, 0, -1);
			List<FFGALNAPKCD> crewMates = PlayerControlPatch.getCrewMates(JPGEIBIBJPJ);
			for (int i = 0; i < Mathf.Min(CustomGameOptions.sheriffCount, PlayerControlPatch.getCrewMates(JPGEIBIBJPJ).Count); i++)
			{
				FFGALNAPKCD sheriff = crewMates[UnityEngine.Random.Range(0, crewMates.Count)];
				PlayerControlPatch.sheriffs.Add(sheriff);
				crewMates.Remove(sheriff);

				if (CustomGameOptions.showSheriff)
				{
					sheriff.nameText.Color = new Color(1f, 0.8f, 0f, 1f);
				}
				byte playerId = sheriff.PlayerId;
				messageWriter.Write(playerId);
				FMLLKEACGIO.Instance.FinishRpcImmediately(messageWriter);
			}
		}

		[HarmonyPatch(typeof(FFGALNAPKCD), "MurderPlayer")]
		public static void Prefix(FFGALNAPKCD __instance, FFGALNAPKCD CAKODNGLPDF)
		{
			if (PlayerControlPatch.isSheriff(__instance.PlayerId))
			{
				__instance.NDGFFHMFGIG.DAPKNDBLKIA = true;
			}
		}

		[HarmonyPatch(typeof(FFGALNAPKCD), "MurderPlayer")]
		public static void Postfix(FFGALNAPKCD __instance, FFGALNAPKCD CAKODNGLPDF)
		{
			if (PlayerControlPatch.isSheriff(__instance.PlayerId))
			{
				__instance.NDGFFHMFGIG.DAPKNDBLKIA = false;
			}
		}

		[HarmonyPatch(typeof(FFGALNAPKCD), "RpcSyncSettings")]
		public static void Postfix(KMOGFLPJLLK IOFBPLNIJIC)
		{
			if (FMLLKEACGIO.Instance != null && FFGALNAPKCD.LocalPlayer != null)
			{
				MessageWriter messageWriter = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SyncCustomSettingsShowSheriff, 0, -1);
				messageWriter.Write(CustomGameOptions.showSheriff);
				messageWriter.Write(CustomGameOptions.sheriffCount);
				FMLLKEACGIO.Instance.FinishRpcImmediately(messageWriter);
			}
		}
	}
}
