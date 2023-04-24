using System;
using Oxide.Core;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("Invisibility", "Olegeekk", "1.0.1")]
    [Description("Allows players with permission to become invisible")]

    public class Invisibility : RustPlugin
    {
        private const string invisibilityPermission = "invisibility.use";

        private void Init()
        {
            permission.RegisterPermission(invisibilityPermission, this);
        }

        [ChatCommand("invisible")]
        private void InvisibleCommand(BasePlayer player, string command, string[] args)
        {
            if (!player.IPlayer.HasPermission(invisibilityPermission))
            {
                player.ChatMessage("You did not have permissions to use this.");
                return;
            }

            if (IsVisible(player))
            {
                MakeInvisible(player);
                player.ChatMessage("You are Invisible now .");
            }
            else
            {
                MakeVisible(player);
                player.ChatMessage("You are Visible for now ");
            }
        }

        private bool IsVisible(BasePlayer player)
        {
            return !player.HasPlayerFlag(BasePlayer.PlayerFlags.NoSprint);
        }

        private void MakeInvisible(BasePlayer player)
        {
            player.SetPlayerFlag(BasePlayer.PlayerFlags.NoSprint, true);
            player.UpdateNetworkGroup();
            player.SendNetworkUpdate();
            player.GetActiveItem()?.GetHeldEntity()?.SendNetworkUpdate();
        }

        private void MakeVisible(BasePlayer player)
        {
            player.SetPlayerFlag(BasePlayer.PlayerFlags.NoSprint, false);
            player.UpdateNetworkGroup();
            player.SendNetworkUpdate();
            player.GetActiveItem()?.GetHeldEntity()?.SendNetworkUpdate();
        }
    }
}
