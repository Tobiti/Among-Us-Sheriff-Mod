using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;

namespace SheriffMod
{
	// Token: 0x02000004 RID: 4
	[BepInPlugin("org.bepinex.plugins.SheriffMod", "Sheriff Mod", "1.0.1.0")]
	public class SheriffMod : BasePlugin
	{
		public ConfigEntry<string> Name { get; set; }

		public ConfigEntry<string> Ip { get; set; }

		public ConfigEntry<ushort> Port { get; set; }

		public SheriffMod()
		{
			SheriffMod.log = base.Log;
			this.harmony = new Harmony("Sheriff Mod");
		}

		public override void Load()
		{
			SheriffMod.log.LogMessage("Sheriff Mod loaded");
			this.Name = base.Config.Bind<string>("Custom", "Name", "Custom", new ConfigDescription("Name of the new server connection"));
			this.Ip = base.Config.Bind<string>("Custom", "Ipv4 or Hostname", "127.0.0.1", new ConfigDescription("IP of the new server connection"));
			this.Port = base.Config.Bind<ushort>("Custom", "Port", 22023, new ConfigDescription("Port of the new server connection"));
			List<OIBMKGDLGOG> list = AOBNFCIHAJL.DefaultRegions.ToList<OIBMKGDLGOG>();
			string text = this.Ip.Value;
			bool flag = Uri.CheckHostName(this.Ip.Value).ToString() == "Dns";
			if (flag)
			{
				SheriffMod.log.LogMessage("Resolving " + text + " ...");
				try
				{
					foreach (IPAddress ipaddress in Dns.GetHostAddresses(this.Ip.Value))
					{
						bool flag2 = ipaddress.AddressFamily == AddressFamily.InterNetwork;
						if (flag2)
						{
							text = ipaddress.ToString();
							break;
						}
					}
				}
				catch
				{
					SheriffMod.log.LogMessage("Hostname could not be resolved" + text);
				}
				SheriffMod.log.LogMessage("IP is " + text);
			}
			ushort value = this.Port.Value;
			list.Insert(0, new OIBMKGDLGOG(this.Name.Value, text, new PLFDMKKDEMI[]
			{
				new PLFDMKKDEMI(this.Name.Value + "-Master-1", text, value)
			}));
			AOBNFCIHAJL.DefaultRegions = list.ToArray();
			this.harmony.PatchAll();
		}
		public static ManualLogSource log;

		private readonly Harmony harmony;
	}
}
