using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {  
            int  count = InputSignals.Count();//get the count of inputSignals
            OutputSignal = new Signal(new List <float>(),false) ;//initialize the outputsignal
            float max = 0;
            for(int i = 0; i < count; i++)//get the maximum size of inputsignals
            {
                if(max < InputSignals[i].Samples.Count)
                {
                    max = InputSignals[i].Samples.Count;
                }
            }
            
            for (int i = 0; i < max; i++)
            { 
                float sum = 0;
                for(int j=0;j<count; j++)
                {
                    
                  
                    sum= InputSignals[j].Samples[i]+sum;

                }
                 OutputSignal.Samples.Add(sum);

            }
             
         
            
            
          

        }
    }
}