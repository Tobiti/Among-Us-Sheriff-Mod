using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SheriffMod
{
    [HarmonyPatch]
    class ChatPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch(typeof(FFGALNAPKCD), "RpcSendChat")]
        private static void sendChat(string PGIBDIEPGIC)
        {
            if (isHost())
            {
                String chatMessage = PGIBDIEPGIC;
                if (chatMessage.StartsWith("/sheriffs"))
                {
                    String[] split = chatMessage.Split(' ');
                    if (split.Length == 2)
                    {
                        int result;
                        if (int.TryParse(split[1], out result))
                        {
                            CustomGameOptions.sheriffCount = Mathf.Max(result, 0);
                            FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                            SheriffMod.log.LogMessage($"Changed sheriff count to {CustomGameOptions.sheriffCooldown}");
                        }
                    }
                }
            }
        }
        
        //TODO: -Get a right check
        private static bool isHost()
        {
            if (FFGALNAPKCD.LocalPlayer != null)
            {
                return true;
            }
            return false;
        }
    }
}
