﻿/*
Copyright 2011 MCForge
Dual-licensed under the Educational Community License, Version 2.0 and
the GNU General Public License, Version 3 (the "Licenses"); you may
not use this file except in compliance with the Licenses. You may
obtain a copy of the Licenses at
http://www.opensource.org/licenses/ecl2.php
http://www.gnu.org/licenses/gpl-3.0.html
Unless required by applicable law or agreed to in writing,
software distributed under the Licenses are distributed on an "AS IS"
BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
or implied. See the Licenses for the specific language governing
permissions and limitations under the Licenses.
*/
using MCForge.Utils;
using MCForge.Entity;
using MCForge.Interface.Command;
using MCForge;

namespace MCForge.Commands
{
    public class CmdNotify: ICommand
    {
        public string Name { get { return "Notify"; } }
        public CommandTypes Type { get { return CommandTypes.Misc; } }
        public string Author { get { return "givo"; } }
        public int Version { get { return 1; } }
        public string CUD { get { return ""; } }
        public byte Permission { get { return 0; } }

        public void Use(Player p, string[] args)
        {
            string tosend = null;
            foreach (string str in args)
            {
                tosend += str + " ";
            }
            if (tosend.Length >= 44) { p.SendMessage("You can only send messages with 44 characters or less"); return; } //gah ill make it send the rest below another time

            MCForge.Core.Server.ForeachPlayer(pl =>
            {
                if ((bool)(pl.ExtraData.GetIfExist<object, object>("UsingWoM") ?? false))
                {
                    p.SendMessage("^detail.user.alert=" + tosend);
                }
            });
        }

        public void Help(Player p)
        {
            p.SendMessage("/notify - Sends a message up the top right for wom users.");
            p.SendMessage("/alert may also be used.");
        }

        public void Initialize()
        {
            Command.AddReference(this, new string[2] {"notify", "alert"});
        }
    }
}
