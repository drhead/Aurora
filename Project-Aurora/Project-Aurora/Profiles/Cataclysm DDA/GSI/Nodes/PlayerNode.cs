using System;
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
        //public int[] hp_cur = new int[6];
        //public int[] hp_max = new int[6];
        //public double[] splints = new double[6];
        //public int[] limbs = new int[6];
        public BodyPart head = new BodyPart();
        public BodyPart torso = new BodyPart();
        public BodyPart leftarm = new BodyPart();
        public BodyPart leftleg = new BodyPart();
        public BodyPart rightarm = new BodyPart();
        public BodyPart rightleg = new BodyPart();
        public int stamina;
        public int stamina_max;
        public int power_level;
        public int max_power_level;
        public int pain;
        public int morale;
        public int safe_mode;

        private int[] arr_hpcur = new int[6];
        private int[] arr_hpmax = new int[6];
        private double[] arr_splint = new double[6];
        private int[] arr_status = new int[6];

        internal PlayerNode(string json) : base(json)
        {
            try
            {
                selfAware = GetBool("is_self_aware");
                hunger = GetInt("hunger");
                thirst = GetInt("thirst");
                fatigue = GetInt("fatigue");
                temp_level = GetInt("temp_level");
                temp_change = GetInt("temp_change");
                stamina = GetInt("stamina");
                stamina_max = GetInt("stamina_max");
                power_level = GetInt("power_level");
                max_power_level = GetInt("max_power_level");
                pain = GetInt("pain");
                morale = GetInt("morale");
                safe_mode = GetInt("safe_mode");
                arr_hpcur = GetArray<int>("hp_cur");
                arr_hpmax = GetArray<int>("hp_max");
                arr_splint = GetArray<double>("splints");
                arr_status = GetArray<int>("limbs");
                head = new BodyPart(arr_hpcur, arr_hpmax, arr_splint, arr_status, 0);
                torso = new BodyPart(arr_hpcur, arr_hpmax, arr_splint, arr_status, 1);
                leftarm = new BodyPart(arr_hpcur, arr_hpmax, arr_splint, arr_status, 2);
                rightarm = new BodyPart(arr_hpcur, arr_hpmax, arr_splint, arr_status, 3);
                leftleg = new BodyPart(arr_hpcur, arr_hpmax, arr_splint, arr_status, 4);
                rightleg = new BodyPart(arr_hpcur, arr_hpmax, arr_splint, arr_status, 5);
            }
            catch { }
            
        }
    }
    public class BodyPart : Node<BodyPart>
    {
        public int hp_cur;
        public int hp_max;
        public double splint;
        public int status;

        public BodyPart()
        {

        }

        public BodyPart(int[] a_hpcur, int[] a_hpmax, double[] a_splint, int[] a_status, int i)
        {
            hp_cur = a_hpcur[i];
            hp_max = a_hpmax[i];
            splint = a_splint[i];
            status = a_status[i];
        }
    }

}

