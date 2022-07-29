using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            List<int> indeces = new List<int>();
            List<float> value = new List<float>();
            int size = InputSignal.Samples.Count();
            /*int min = InputSignal.SamplesIndices.Min();
            int max = InputSignal.SamplesIndices.Max();*/

            for (int i =size - 1; i >= 0; i--)
            { 
                indeces.Add(InputSignal.SamplesIndices[i] * (-1));
                value.Add(InputSignal.Samples[i]);
               
            }
            
            OutputFoldedSignal = new Signal(value, indeces, false);
           

        }
    }
}
