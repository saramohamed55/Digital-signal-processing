using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos : Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {

            // throw new NotImplementedException();
            //thesampling frequency chosen should obey the sampling theorem
            samples = new List<float>();

            if (SamplingFrequency >= 2 * AnalogFrequency)
            {
                
                    if (type.Equals("sin"))
                    {for (int i = 0; i < SamplingFrequency; i++)
                    {
                        samples.Add((float)(A * Math.Sin(2 * Math.PI * (AnalogFrequency / SamplingFrequency)*i + PhaseShift)));
                    }
                    }
                    else if (type.Equals("cos"))
                    {for (int i = 0; i < SamplingFrequency; i++)
                    {
                        samples.Add((float)(A * Math.Cos(2 * Math.PI * (AnalogFrequency / SamplingFrequency)*i + PhaseShift)));
                    }
                    }
                
            }


        }

    }
}


       
            
          