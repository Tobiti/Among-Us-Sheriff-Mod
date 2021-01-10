using System;
using System.Reflection;
using HarmonyLib;
using Hazel;

namespace SheriffMod
{
	[HarmonyPatch]
	public static class KillButtonManager
	{
		[HarmonyPatch(typeof(MLPJGKEACMM), "PerformKill")]
		private static bool Prefix(MethodBase __originalMethod)
		{
			if (PlayerControlPatch.isSheriff(FFGALNAPKCD.LocalPlayer))
			{
				if (PlayerControlPatch.SheriffKillTimer() <= 0f)
				{
					double distBetweenPlayers = PlayerControlPatch.getDistBetweenPlayers(FFGALNAPKCD.LocalPlayer, PlayerControlPatch.closestPlayer);
					if (distBetweenPlayers < 1.2000000476837158)
					{
						bool flag4 = !PlayerControlPatch.closestPlayer.NDGFFHMFGIG.DAPKNDBLKIA;
						if (flag4)
						{
							MessageWriter messageWriter = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, 0, -1);
							messageWriter.Write(FFGALNAPKCD.LocalPlayer.PlayerId);
							messageWriter.Write(FFGALNAPKCD.LocalPlayer.PlayerId);
							FMLLKEACGIO.Instance.FinishRpcImmediately(messageWriter);
							FFGALNAPKCD.LocalPlayer.MurderPlayer(FFGALNAPKCD.LocalPlayer);
						}
						else
						{
							MessageWriter messageWriter2 = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, 0, -1);
							messageWriter2.Write(FFGALNAPKCD.LocalPlayer.PlayerId);
							messageWriter2.Write(PlayerControlPatch.closestPlayer.PlayerId);
							FMLLKEACGIO.Instance.FinishRpcImmediately(messageWriter2);
							FFGALNAPKCD.LocalPlayer.MurderPlayer(PlayerControlPatch.closestPlayer);
						}
						PlayerControlPatch.lastKilled = DateTime.UtcNow;
					}
				}
				return false;
			}
			return true;
		}
	}
}
