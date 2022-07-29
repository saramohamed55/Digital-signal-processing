using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        // this function to calculate the normalization term of the auto_correlation
        public float get_normalized_term_auto_correlation()
        {
            // the correlation is givin by the formula of -> sqr(for all components i -> x1(i) * x2(i))
            // the normalized corrleation is the (corr / (1 / N) * sqr(summition of all components (xi)^2))
            float sum_values = 0; // make variable sum to add the summition of all compoents (x(i) ^ 2)

            // here we get the square summition of all the components 
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                sum_values += InputSignal1.Samples[i] * InputSignal1.Samples[i];

            // now get the normalized term (1/N * sqr(sum values * sumvlaues -> summition all values square))
            float Normalized = (float)1 / InputSignal1.Samples.Count * (float)Math.Sqrt(sum_values * sum_values);
            // return the normalization term
            return Normalized;
        }

        // this function for padding the singal 1 and 2 in case of crros correlation and periodic the first one 
        // the padding according to the size of each one 
        public void padding_periodic()
        {
            // here we will pad the signal one with the rest of singal 2 and via verse 
            // the padding with zero length is (N1 - N2 + 1) 
            int size1 = InputSignal1.Samples.Count;
            for (int i = 0; i < InputSignal2.Samples.Count - 1; i++)
                // pad singal one with zeros according to singal 2 
                InputSignal1.Samples.Add(0);
            for (int i = 0; i < size1 - 1; i++)
                // pad singal two with zeros according to singal 1
                InputSignal2.Samples.Add(0);
        }

        // this function for padding the singal 1 and 2 in case of crros correlation and non periodic the first one 
        // the padding according to the size of the largest one  
        public void padding_non_periodic()
        {
            // first get the size of each one of those singals 
            int N_1 = InputSignal1.Samples.Count;
            int N_2 = InputSignal2.Samples.Count;
            // if the size of the second singal is largest than the first signal 
            if (N_1 < N_2)
            {
                // we will pad the first singal with zeros according to (size_2 - size_1) + 1
                for (int i = 0; i < (N_2 - N_1); i++)
                    // add zeros to the first singal
                    InputSignal1.Samples.Add(0);
            }
            else
            {
                // we will pad the second singal with zeros according to (size_1 - size_2) + 1
                for (int i = 0; i < (N_1 - N_2); i++)
                    // add zeros to the second singal
                    InputSignal2.Samples.Add(0);
            }
        }

        public float get_normalized_term_cross_correlation()
        {
            // the correlation is givin by the formula of -> sqr(for all components i -> x1(i) * x2(i))
            // the normalized corrleation is the (corr / (1 / N) * sqr(summition of all components (xi)^2))
            float sum_values_1 = 0, sum_values_2 = 0;
            // here we get the square summition of all the components of the first singal 
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                sum_values_1 += InputSignal1.Samples[i] * InputSignal1.Samples[i];
            // here we get the square summition of all the components of the Second singal 
            for (int i = 0; i < InputSignal2.Samples.Count; i++)
                sum_values_2 += InputSignal2.Samples[i] * InputSignal2.Samples[i];
            // now get the normalized term (1/N * sqr(sum values_1 * sumvlaues_2 -> summition all values square))
            float Normalized = (float)1 / InputSignal1.Samples.Count * (float)Math.Sqrt(sum_values_1 * sum_values_2);

            return Normalized;// return the normalization term
        }
        public override void Run()
        {
            OutputNormalizedCorrelation = new List<float>();
            OutputNonNormalizedCorrelation = new List<float>();


            //AUTO CORRELATION SECTION ////////////////////////////////////////////////

            // in case of the auto correlation the secind singal will be null 
            // now check if the singal number one is peroidic or not 

            // in case non periodic signal (with delay)
            if (InputSignal1.Periodic == false && InputSignal2 == null)
            {

                // this function to get the normalized term in case of auto_correlation(the same signal)
                float Normalized = get_normalized_term_auto_correlation();

                // in case of the peridic singal we will make the terms with the number of components 
                // in  the input signal 

                // each time with delay (each time increament the delay by one)
                int delay = 0;
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    // make the correlation value with 0 initially 
                    float corrleation_value = 0;
                    // each time we loop in the components of the second signal (the singal it self)
                    // and we make the delay as the equation is :  
                    // Equation : j(delay) = (1/N)sum(x1(i) * x2(i + delay)) as the j(delay) is the correlation with delay
                    for (int j = 0; j < InputSignal1.Samples.Count - delay; j++)
                        corrleation_value += (InputSignal1.Samples[j] * InputSignal1.Samples[j + delay]);
                    // multiply the result correlation in this delay with 1/N as N is the number of samples 
                    corrleation_value /= InputSignal1.Samples.Count;

                    // now add the value one to the normalized list (after dividing by the normalized term we calculated) 
                    // and add the correlation value it self to the list that non normalized
                    OutputNonNormalizedCorrelation.Add(corrleation_value);
                    OutputNormalizedCorrelation.Add(corrleation_value / Normalized);
                    delay++; // increament the delay
                }
            }

            // in case a periodic signal (with no delay)
            else if (InputSignal1.Periodic == true && InputSignal2 == null)
            {
                // this function to get the normalized term in case of auto_correlation(the same signal)
                float Normalized = get_normalized_term_auto_correlation();

                // here we will make the same signal as two signals (to avoid the errors while doing the rotation)
                // make a new reference of the singal (list of samples , indices and the periodic of course wiht flase)
                Signal same_signal_one = new Signal(new List<float>(), new List<int>(), false);
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                    same_signal_one.Samples.Add(InputSignal1.Samples[i]); // add the samples of singal one to the samples of singla 2 

                // here we will make the delay for the second signal we just added as shift left that we will 
                // each time remove the value in the first index and add it in the end of the list 

                // number of terms equal to the number of components in the sinal
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    float corrleation_value = 0; // initially the corrleation value wiht zero
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                        // the correlation value is the summiton of the product of the two singals 
                        corrleation_value += (InputSignal1.Samples[j] * same_signal_one.Samples[j]);

                    // multiply the result correlation in this delay with 1 / N as N is the number of samples
                    corrleation_value /= InputSignal1.Samples.Count;

                    // now add the value one to the normalized list (after dividing by the normalized term we calculated) 
                    // and add the correlation value it self to the list that non normalized
                    OutputNonNormalizedCorrelation.Add(corrleation_value);
                    OutputNormalizedCorrelation.Add(corrleation_value / Normalized);

                    // do the rotaion of the second singal (same_signal_one) 
                    // get the first value (as moving left will make it the last value)
                    float rotated_value = same_signal_one.Samples[0];
                    // remove the rotated value form the list (singal)
                    same_signal_one.Samples.Remove(same_signal_one.Samples[0]);
                    // add the value to the end of the list (signal)
                    same_signal_one.Samples.Add(rotated_value);
                }
            }

            //CROSS CORRELATION SECTION ////////////////////////////////////////////////
            // if the seond signal is not null and the first one is perodic (with no delay)
            else if (InputSignal1.Periodic == true && InputSignal2 != null)
            {
                // we call this function for the padding of the singal
                padding_periodic();
                // we call this function to calculate the normalized term in case of cross correlation  
                float Normalized = get_normalized_term_cross_correlation();
                // number of terms equal to the number of components in the sinal
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    float corrleation_value = 0; // initially the corrleation value wiht zero
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                        // the correlation value is the summiton of the product of the two singals 
                        corrleation_value += (InputSignal1.Samples[j] * InputSignal2.Samples[j]);

                    // multiply the result correlation in this delay with 1 / N as N is the number of samples
                    corrleation_value /= InputSignal1.Samples.Count;

                    // now add the value one to the normalized list (after dividing by the normalized term we calculated) 
                    // and add the correlation value it self to the list that non normalized
                    OutputNonNormalizedCorrelation.Add(corrleation_value);
                    OutputNormalizedCorrelation.Add(corrleation_value / Normalized);

                    // do the rotaion of the second singal 
                    // get the first value (as moving left will make it the last value)
                    float rotated_value = InputSignal2.Samples[0];
                    // remove the rotated value form the list (singal)
                    InputSignal2.Samples.Remove(InputSignal2.Samples[0]);
                    // add the value to the end of the list (signal)
                    InputSignal2.Samples.Add(rotated_value);
                }
            }

            // in case of second singal is not null (cross correlation) but the first singla is non periodic 
            // this case with the delay 
            else if (InputSignal1.Periodic == false && InputSignal2 != null)
            {
                // we call this function for the padding of the singal
                padding_non_periodic();
                // we call this function to calculate the normalized term in case of cross correlation  
                float Normalized = get_normalized_term_cross_correlation();
                // each time with delay (each time increament the delay by one)
                int delay = 0;
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    // make the correlation value with 0 initially 
                    float corrleation_value = 0;
                    // each time we loop in the components of the second signal (the singal it self)
                    // and we make the delay as the equation is :  
                    // Equation : j(delay) = (1/N)sum(x1(i) * x2(i + delay)) as the j(delay) is the correlation with delay
                    for (int j = 0; j < InputSignal1.Samples.Count - delay; j++)
                        corrleation_value += (InputSignal1.Samples[j] * InputSignal2.Samples[j + delay]);
                    // multiply the result correlation in this delay with 1/N as N is the number of samples 
                    // any size of the two signals as we made them both the same size
                    corrleation_value /= InputSignal1.Samples.Count;

                    // now add the value one to the normalized list (after dividing by the normalized term we calculated) 
                    // and add the correlation value it self to the list that non normalized
                    OutputNormalizedCorrelation.Add(corrleation_value / Normalized);
                    OutputNonNormalizedCorrelation.Add(corrleation_value);
                    delay++; // increament the delay
                }
            }
        }
    }
}