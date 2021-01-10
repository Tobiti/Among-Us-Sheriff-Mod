using System;
using System.Reflection;
using HarmonyLib;
using Hazel;

namespace SheriffMod
{
	// Token: 0x02000007 RID: 7
	[HarmonyPatch]
	public static class KillButtonManager
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000026B0 File Offset: 0x000008B0
		[HarmonyPatch(typeof(MLPJGKEACMM), "PerformKill")]
		private static bool Prefix(MethodBase __originalMethod)
		{
			bool flag = PlayerControlPatch.isSheriff(FFGALNAPKCD.LocalPlayer);
			bool result;
			if (flag)
			{
				bool flag2 = PlayerControlPatch.SheriffKillTimer() == 0f;
				if (flag2)
				{
					double distBetweenPlayers = PlayerControlPatch.getDistBetweenPlayers(FFGALNAPKCD.LocalPlayer, PlayerControlPatch.closestPlayer);
					bool flag3 = distBetweenPlayers < 1.2000000476837158;
					if (flag3)
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
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
	}
}
