using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), false);
            // throw new NotImplementedException();
            float au;
            int N = InputSignal.Samples.Count();
            float sum ;
           for (int i = 0; i < N; i++)
            {
                sum = 0;
                
                if (i == 0)
                {
                    au = (float)(Math.Sqrt(1)) / (float)(Math.Sqrt(N));


                }
                else
                {
                    au = (float)(Math.Sqrt(2) )/ (float)(Math.Sqrt(N));
                }
                
                for(int j = 0; j < N; j++)
                {
                    sum += InputSignal.Samples[j]*(float)Math.Cos((2 * j + 1)*(i * Math.PI) / (2 * N));
                 }
                OutputSignal.Samples.Add(au * sum);

            }
        }
    }
}
