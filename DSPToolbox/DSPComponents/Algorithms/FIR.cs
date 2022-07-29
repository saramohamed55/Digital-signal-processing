using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
           
            OutputYn = new Signal(new List<float>(), new List<int>(), false);
            //throw new NotImplementedException();
            int coeff=0;
           
            List<double> outputhd = new List<double>();
            List<double> outputwn = new List<double>();
            //get w(n)
            int temp;
            if (InputStopBandAttenuation<=21)
            {
                
                temp = (int)(( 0.9 *InputFS) / InputTransitionBand);
                //make temp symmetric
                if (temp % 2 == 0)
                    temp =(int)temp+ 1;
                coeff = (int)((temp )/ 2);
                //get w(n)
                for (int i = -coeff; i <= coeff; i++)
                {
                    outputwn.Add(1);

                }


            }
            else if (InputStopBandAttenuation <= 44 && InputStopBandAttenuation>21)
            {
                temp = (int)((3.1 *InputFS) / InputTransitionBand);
                if (temp % 2 == 0)
                    temp += 1;
                coeff = (int)((temp) / 2);
                //get w(n)
                for(int i = -1*coeff; i <= coeff; i++)
                {
                    outputwn.Add((float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / temp)));
                }

            }
            else if(InputStopBandAttenuation<=53 && InputStopBandAttenuation > 44)
            {
                temp = (int)((3.3*InputFS ) / InputTransitionBand);
                if (temp % 2 == 0)
                    temp += 1;
                coeff = (int)((temp) / 2);
                //get w(n)
                for(int i = -1*coeff; i <= coeff; i++)
                {
                   outputwn.Add((float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * i) / temp)));
                }

            }
            else if(InputStopBandAttenuation<=74 && InputStopBandAttenuation > 53)
            {
                temp = (int)((5.5 *InputFS)/ InputTransitionBand);
                if (temp % 2 == 0)
                    temp += 1;
                coeff = (int)((temp ) / 2);
                //get w(n)
                for(int i = -1*coeff; i <= coeff; i++)
                {
                    outputwn.Add(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / (temp - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / (temp - 1))));
                }

            }
            //types of filters LOW, HIGH, BAND_PASS, BAND_STOP
            //get hd(w)
            float? fc;
            
            if (InputFilterType == FILTER_TYPES.LOW)
            {
                //sampling frequancy

                //because the smearing//normalized
                fc = (float?)((InputCutOffFrequency + (InputTransitionBand / 2.0)));
                fc = fc / InputFS;
               float? wc = (float?)(2 * Math.PI * fc);
                for (int i = -coeff; i <=coeff; i++)
                {
                    if (i== 0)
                    {
                        outputhd.Add((float)(2 * fc));
                    }
                    else
                    {
                        outputhd.Add((float)(2 * fc * (Math.Sin((double)(i *wc)) / (i * wc))));

                    }
                }

            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                //sampling frequancy
               
                //because the smearing
                fc = (float?)((InputCutOffFrequency + (InputTransitionBand / 2.0)) / InputFS);
                //normalized
              float?  wc = (float)(2 * Math.PI * fc);
                
                for (int i = -1*coeff; i <= coeff; i++)
                {
                    if (i == 0)
                    {
                        outputhd.Add((float)(1-(2 * fc)));
                    }
                    else
                    {
                        outputhd.Add((float)(-2 * fc * (Math.Sin((double)(i * wc)) / (i * wc))));
                    }
                }

            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {//sampling frequancy
             
                float? f1 = ((float?)InputF1 - (InputTransitionBand / 2f)) / InputFS;
                float? f2 = ((float?)InputF2 + (InputTransitionBand / 2f)) / InputFS;
                float?  wc = (float?)( 2 * Math.PI * f1);
              float?  wc2 = (float?)(2 * Math.PI * f2);
                for(int i = -coeff; i <= coeff; i++)
                {
                    if (i == 0)
                    {
                        outputhd.Add((float)( 2*(f2-f1)));
                    }
                    else
                    {
                        outputhd.Add((float)((2 * f2 * (Math.Sin((double)(i * wc2)) / (i * wc2))) - (2 * f1 * (Math.Sin((double)(i * wc)) / (i * wc)))));
                    }

                }


            }
            else if(InputFilterType == FILTER_TYPES.BAND_STOP)
            {//sampling frequancy
                
                float? f1 = ((float?)InputF1 - (InputTransitionBand / 2f)) / InputFS;
                float? f2 = ((float?)InputF2 + (InputTransitionBand / 2f)) / InputFS;
                float? wc = (float?)(2 * Math.PI * f1);
                float? wc2 = (float?)(2 * Math.PI * f2);
                for (int i = -coeff; i <= coeff; i++)
                {
                    if (i == 0)
                    {
                        outputhd.Add((float)(1-(2 * (f2 - f1))));
                    }
                    else
                    {
                        outputhd.Add((float)((2 * f1 * (Math.Sin((double)(i * wc)) / (i * wc))) - (2 * f2 * (Math.Sin((double)(i * wc2)) / (i * wc2)))));

                    }

                }
            }
            //mirroring
            List<float> hn_samples = new List<float>();
            List<int> hn_indx = new List<int>();
            for (int i =-coeff; i <= coeff; i++)
            {
                hn_indx.Add (i);//-26 to 26
                
            }
            for(int i = 0; i <outputwn.Count; i++)
            {
                hn_samples.Add((float)(outputhd[i] * outputwn[i]));
            }
            OutputHn = new Signal(hn_samples, hn_indx, false);
            
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = InputTimeDomainSignal; 
            conv.InputSignal2 = OutputHn; 
            conv.Run(); 
            OutputYn = conv.OutputConvolvedSignal;

        }
    }
}
