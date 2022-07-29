using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            //  throw new NotImplementedException();
            OutputNormalizedSignal = new Signal(new List<float>(), false);
            int size = InputSignal.Samples.Count;
            float max = InputSignal.Samples.Max();
            float min = InputSignal.Samples.Min();
            for (int i = 0; i < size; i++)
            {
                float result=0;
                result = (((InputSignal.Samples[i] - min) / (max - min)) * (InputMaxRange - InputMinRange) )
                    + InputMinRange;
                    
                OutputNormalizedSignal.Samples.Add(result);

            }
        }
    }
}
