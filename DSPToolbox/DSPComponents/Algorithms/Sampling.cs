using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }


        public override void Run()

        {
         
            OutputSignal = new Signal(new List<float>(), false);
            // throw new NotImplementedException();
            //upsampling
            List <float> temp =  new List<float>();
            if (L != 0 && M == 0)
            {
                int num = L - 1;
                for(int i = 0; i < InputSignal.Samples.Count; i++)
                { temp.Add(InputSignal.Samples[i]);
                    for(int j = 0; j < num; j++)
                    {
                        temp.Add(0);
                    }


                }
                InputSignal = new Signal(temp, false);
                FIR f = new FIR();
                f.InputTimeDomainSignal = InputSignal;
                f.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                f.InputFS = 8000;
                f.InputStopBandAttenuation = 50;
                f.InputCutOffFrequency = 1500;
                f.InputTransitionBand = 500;
                f.Run();
                OutputSignal=f.OutputYn;



            }
            //down sampling
            else if (L == 0 && M != 0)
            {
                FIR f = new FIR();
                f.InputTimeDomainSignal = InputSignal;
                f.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                f.InputFS = 8000;
                f.InputStopBandAttenuation = 50;
                f.InputCutOffFrequency = 1500;
                f.InputTransitionBand = 500;
                f.Run();
              
                int num = M - 1;
                for (int i = 0; i <f.OutputYn.Samples.Count();i+=M )
                {
                    temp.Add( f.OutputYn.Samples[i]);
                   
                }
                OutputSignal = new Signal(temp, false);

            }
            else if(L!=0 && M != 0)
            {
                List<float> temp2 = new List<float>();

                int num = L - 1;
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    temp2.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < num; j++)
                    {
                        temp2.Add(0);
                    }


                }
                InputSignal = new Signal(temp2, false);
                FIR f = new FIR();
                f.InputTimeDomainSignal = InputSignal;
                f.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                f.InputFS = 8000;
                f.InputStopBandAttenuation = 50;
                f.InputCutOffFrequency = 1500;
                f.InputTransitionBand = 500;
                f.Run();
                List<float> temp3 = new List<float>();

                for (int i = 0; i < f.OutputYn.Samples.Count(); i += M)
                {
                    temp3.Add(f.OutputYn.Samples[i]);

                }
                OutputSignal = new Signal(temp3, false);




            }
            else
            {
                Console.WriteLine("error");

            }
            
        }
    }
    
}
