using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            OutputIntervalIndices = new List<int>() ;
           if (InputLevel == 0)
            {
                InputLevel =(int)Math.Ceiling(Math.Pow (2 ,InputNumBits));//(base,pow)

            }
           else if (InputNumBits == 0)
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);


            }

            float delta =( InputSignal.Samples.Max() - InputSignal.Samples.Min()) / InputLevel;
            List<float> arr = new List<float>();
                
            //intialize first element in 
            arr.Add(InputSignal.Samples.Min()) ;
            for(int i = 1; i <InputLevel; i++)
            {
                arr.Add(arr[i-1] + delta);
            }arr.Add(InputSignal.Samples.Max());//arr[inputlevel+1]from [0 to 4]
            //make array of midpoint of array
            List <float> midpoints = new List<float>();//with size from[0 to 3]
            for (int i = 0; i < InputLevel; i++) 
            {
                midpoints.Add( (arr[i] + arr[i + 1])/2);
            }
          
            //make list of OutputQuantizedSignal
            List<float> OutputQuantized = new List<float>();
            //make indecise
            int size = InputSignal.Samples.Count();
            for (int i = 0; i < size; i++)
            {
               
                for(int j = 0; j <InputLevel; j++)
                {
                    if (j+1 == InputLevel && InputSignal.Samples[i] <= arr[j+1]) //b3mel check on last elemnt yaaaaaaaaaaaayy
                    {
                        OutputIntervalIndices.Add(j+1);
                        OutputQuantized.Add(midpoints[j]);
                        break;
                    }
                    else if(InputSignal.Samples[i]>= arr[j] && InputSignal.Samples[i] < arr[j+1])
                    {
                        OutputIntervalIndices.Add(j+1);
                        OutputQuantized.Add(midpoints[j]);
                        break;

                    }

                }

            }
            
            OutputQuantizedSignal = new Signal(OutputQuantized, false);
            OutputSamplesError = new List<float>();
            //error
            for (int i = 0; i < size; i++)
            {
                 
                OutputSamplesError.Add(OutputQuantizedSignal.Samples[i] - InputSignal.Samples[i]);

            }
            //convert to string
            OutputEncodedSignal = new List<string>();

        
            for(int i = 0; i < InputSignal.Samples.Count; i++)
            {
                string s = Convert.ToString((OutputIntervalIndices[i]-1),2);
                
            
                s=s.PadLeft(InputNumBits, '0');


                OutputEncodedSignal.Add(s);
                  

            }


        }
    }
}
