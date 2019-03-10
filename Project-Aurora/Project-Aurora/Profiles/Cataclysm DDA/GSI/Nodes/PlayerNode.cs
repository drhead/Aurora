﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA.GSI.Nodes
{
    public class PlayerNode : Node<PlayerNode>
    {
        public bool selfAware;
        public int hunger;
        public int thirst;
        public int fatigue;
        public int temp_level;
        public int temp_change;
        public int[] hp_cur = new int[6];
        public int[] hp_max = new int[6];
        public double[] splints = new double[6];
        public int[] limbs = new int[6];
        public int stamina;
        public int stamina_max;
        public int power_level;
        public int max_power_level;
        public int pain;
        public int morale;
        public int safe_mode;
    }
}

