using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }

        public override void Run()
        {
            List<float> mmm = new List<float>();

            int sizee = InputSignal.Samples.Count();
            for (int i = InputWindowSize / 2; i < sizee - (InputWindowSize / 2); i++)
            {
                float c = 0;
                for (int j = i - (InputWindowSize / 2); j <= i + (InputWindowSize / 2); j++)
                {
                    c += InputSignal.Samples[j];
                }
                mmm.Add(c / InputWindowSize);
            }


            OutputAverageSignal = new Signal(mmm, false);




        }
    }
}