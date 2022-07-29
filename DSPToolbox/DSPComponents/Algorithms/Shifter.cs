using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            //  throw new NotImplementedException();
           
           int min= InputSignal.SamplesIndices.Min();
            int max = InputSignal.SamplesIndices.Max();
            List<int> indeces = new List <int>();
            List<float> value = new List<float>();
            if (ShiftingValue > 0)
            {//shift left                                             //-3 -2 -1 0 1 2 3
                                                        //shift +2      -5 -4  -3  -2  -1  0 1    
                for(int i = 0; i <InputSignal.Samples.Count; i++)                        //   5  6 10  0   3  2 6

                {
                    indeces.Add(InputSignal.SamplesIndices[i]-ShiftingValue);
                    

                    value.Add(InputSignal.Samples[i]);

                }

            }
          else if (ShiftingValue < 0)
            {//shift right               /* -3 -2 -1 0 1 2 3
                                       //   -1  0  1 2 3 4 5        
                                         //  4  5  7 0 4 2 6
                for (int i = 0; i < InputSignal.Samples.Count(); i++)
                {
                    indeces.Add(InputSignal.SamplesIndices[i] +Math.Abs(ShiftingValue));

                    value.Add(InputSignal.Samples[i]);

                }

            }
            else
            {
                OutputShiftedSignal = InputSignal;


            }
            OutputShiftedSignal = new Signal(value, indeces, false);
        }
    }
}
