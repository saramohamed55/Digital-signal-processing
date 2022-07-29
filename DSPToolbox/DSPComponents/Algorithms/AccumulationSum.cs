using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            int sizee = InputSignal.Samples.Count();

            List<float> mmm = new List<float>();

            for (int i = 0; i < sizee; i++)
            {
                float c = 0;
                for (int j = 0; j <= i; j++)
                {
                    c += InputSignal.Samples[j];
                }

                mmm.Add(c);
            }
            OutputSignal = new Signal(mmm, false);



        }
    }
}
