using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            //  throw new NotImplementedException();
            //initialize the output signal
            OutputSignal = new Signal(new List<float>() , false);
            //gte size of 2 signals
            int size1 = InputSignal1.Samples.Count;
            int size2 = InputSignal2.Samples.Count;
            for(int i = 0; i < size2; i++)
            {
                InputSignal2.Samples[i] = InputSignal2.Samples[i] / -1;

            }
            for(int i = 0; i < size1; i++)
            {
                float result=0;
                result = InputSignal1.Samples[i] + InputSignal2.Samples[i];
                OutputSignal.Samples.Add(result);

            }

        }
    }
}