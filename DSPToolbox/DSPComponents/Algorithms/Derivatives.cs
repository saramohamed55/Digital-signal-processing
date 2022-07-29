using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {

            // throw new NotImplementedException();
            FirstDerivative = new Signal(new List<float>(), false);
            SecondDerivative = new Signal(new List<float>(), false);
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {
                FirstDerivative.Samples.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);

            }
            SecondDerivative.Samples.Add(InputSignal.Samples[1] - 2 * InputSignal.Samples[0]);
            for(int i = 1; i < InputSignal.Samples.Count-1; i++)
            {
                SecondDerivative.Samples.Add(InputSignal.Samples[i+1] - 2 * InputSignal.Samples[i ]+InputSignal.Samples[i-1]);
            }

        }
    }
}
