using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PetriNets
{
    [Serializable]
    public class Transition
    {
        //порядковый номер в листе переходов
        public static int Id_cntr
        { get; set; }
        public int ID
        { get; set; }
        public int Fieldnumber
        { get; set; }
        public string Type
        { get; set; }
        public double Time
        { get; set; }
        public Dictionary<Position, int> DictOfIn
        { get; set; }

        public List<int> Lin
        { get; set; }

        public System.Drawing.Point Location
        { get; set; }

        public Transition(int fieldnumber)
        {
            ID = Id_cntr++;
            this.Fieldnumber = fieldnumber;
            DictOfIn = new Dictionary<Position, int>();
            //мгновенный
            if (ID % 2 == 1)
            {
                Type = "momentary";
                Time = 0;
            }
                //временной
            else
            {
                Type = "time";
                Time = 1; //потом поменять
            }
            Lin = new List<int>();
        }

        public void addIn(Position o)
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

        public void deleteIn(Position o)
        {
            DictOfIn[o]--;            
        }
    }
}