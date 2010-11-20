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
        public static int Id_cntr
        { get; set; }

        public int ID
        { get; set; }
        public int Tokens
        { get; set; }
        public Dictionary<Transition, int> DictOfIn
        { get; set; }

        public System.Drawing.Point Location
        { get; set; }

        public int Fieldnumber
        { get; set; }

        public List<int> Lin
        { get; set; }

        public Position(int fieldnum)
        {
            this.Fieldnumber = fieldnum;   
            this.ID = Id_cntr++;
            Tokens = 0;
            DictOfIn = new Dictionary<Transition, int>();
            Lin = new List<int>();
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
