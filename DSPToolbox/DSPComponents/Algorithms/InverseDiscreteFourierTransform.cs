using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            OutputTimeDomainSignal = new Signal(new List<float>(), false);

  
            int size = InputFreqDomainSignal.Frequencies.Count;
            for (int i = 0; i < size; i++)
            {
                Complex dft = new Complex(0, 0);
                for (int j = 0; j < size; j++)
                {
                    Complex c = Complex.FromPolarCoordinates(InputFreqDomainSignal.FrequenciesAmplitudes[j], InputFreqDomainSignal.FrequenciesPhaseShifts[j]);
                   
                    float seta = (float)(2 * Math.PI / size) * i * j;

                    Complex temp = new Complex(0, Math.Sin(seta));//new complex(real,imaginary)
                    dft += c* ((Math.Cos(seta)) + temp);

                }
                float x = (float)(dft.Real / size);
                OutputTimeDomainSignal.Samples.Add(x);
               


            }



         
        }
    }
}
