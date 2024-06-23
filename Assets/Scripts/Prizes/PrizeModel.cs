using System;

namespace Assets.Scripts.Prizes
{
    [Serializable]
    public class PrizeModel
    {
        public PrizeModel(string name)
        {
            this.name = name;
        }
        public string name;
        public int count;
    }
}