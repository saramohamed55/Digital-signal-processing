using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            OutputMultipliedSignal = new Signal(new List<float>(), false);
            int size = InputSignal.Samples.Count();//get the size of signal
            for(int i = 0; i < size; i++)
            {
                float result = 0;
                    result = InputSignal.Samples[i] * InputConstant;
                    OutputMultipliedSignal.Samples.Add(result);
                


            }


        }
    }
}
