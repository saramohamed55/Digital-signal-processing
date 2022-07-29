using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            OutputFreqDomainSignal = new Signal(new List<float>(), false);

            List<float> amplitute = new List<float>();
            List<float> phaseshift = new List<float>();
            int size = InputTimeDomainSignal.Samples.Count();
            for(int i = 0; i < size; i++)
            {
              
                Complex dft =new Complex(0,0) ;
                for(int j = 0; j < size; j++)
                {
                    float seta =(float)( 2 * Math.PI/size) * i * j;
                   
                    Complex temp = new Complex(0, Math.Sin(seta));//new complex(real,imaginary)
                    dft+= InputTimeDomainSignal.Samples[j]*((Math.Cos(seta))-temp);
                   
                }
                amplitute.Add((float)dft.Magnitude);
                phaseshift.Add((float)dft.Phase);
               
               
            }
           


            OutputFreqDomainSignal.FrequenciesAmplitudes =  amplitute; 
            
            OutputFreqDomainSignal.FrequenciesPhaseShifts = phaseshift;
          

        }
    }
}
