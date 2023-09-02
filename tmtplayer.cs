using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace tmt
{
    public class tmtplayer : ModPlayer
    {
        public bool CatRose;
        public override void ResetEffects()
        {
            CatRose = false;
        }

    }
}
