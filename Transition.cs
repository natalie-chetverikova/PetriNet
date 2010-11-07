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
        private static int index
        { get; set; }
        public int ID
        { get; set; }
        public int fieldnumber
        { get; set; }
        public string Type
        { get; set; }
        public double Time
        { get; set; }
        public Dictionary<Position, int> DictOfIn
        { get; set; }
        

        public Transition(int fieldnumber)
        {
            ID = index;
            index++;
            this.fieldnumber = fieldnumber;
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

        public void setIndex(int iindex)
        {
            index = iindex; 
        }

        public int getIndex()
        {
            return index;
        }

    }
}