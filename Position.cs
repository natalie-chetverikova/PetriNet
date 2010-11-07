using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PetriNets
{
    [Serializable]
    public class Position
    {
        public int ID
        { get; set; }
        public int Tokens
        { get; set; }
        public Dictionary<Transition, int> DictOfIn
        { get; set; }
        
        
        public Position(int ID)
        {
            this.ID = ID;
            Tokens = 0;
            DictOfIn = new Dictionary<Transition, int>();            
        }

        public void addIn(Transition o)
        {
            if (DictOfIn.ContainsKey(o))
            {
                DictOfIn[o]++;            
            }
            else
            {
                DictOfIn.Add(o, 1);
            }
        }

        public void deleteIn(Transition o)
        {
            DictOfIn[o]--;            
        }        
    }
}
