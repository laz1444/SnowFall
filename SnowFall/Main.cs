using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using log4net;
using fNbt;
using MiNET.Net;
using MiNET;
using MiNET.Utils;
using MiNET.Utils.Nbt;

namespace SnowFall
{
    [Plugin]
    public class Main : Plugin
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Main));
        protected override void OnEnable()
        {
            var server = Context.Server;
            float snowIntensity = (float) Convert.ToDouble(Config.GetProperty("SnowIntensity", "0.5"));

            server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                Player player = args.Player;

                player.PlayerJoin += (o, eventArgs) =>
                {
                    McpeLevelEvent levelEvent = McpeLevelEvent.CreateObject();
                    levelEvent.eventId = (int)LevelEventType.StartRaining;
                    levelEvent.data = getStrength(snowIntensity);
                    player.SendPacket(levelEvent);
                };
            };

            Log.Warn("SnowFall plugin Enabled.");
        }

        private int getStrength(float downfallStrength)
        {
            var Strength = Math.Clamp(downfallStrength, 0.0f, 1.0f);
            return (int)(Strength * 65535);
        }

        [PacketHandler, Send]
        public void HandlePacket(McpeBiomeDefinitionList packet, Player source)
        {
            var nbt = new Nbt
            {
                NbtFile = new NbtFile
                {
                    BigEndian = false,
                    UseVarInt = true,
                    RootTag = customBiomeUtils.DefinitionList(),
                }
            };

            packet.namedtag = nbt;
        }
    }
}
