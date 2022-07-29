using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> convolvedSamples = new List<float>();
            List<int> indxs = new List<int>();

            int startIndx = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];
            int endIndx = InputSignal1.SamplesIndices[InputSignal1.SamplesIndices.Count - 1] + InputSignal2.SamplesIndices[InputSignal2.SamplesIndices.Count - 1];

            for (int i = startIndx; i <= endIndx; i++)
                indxs.Add(i);

            for (int i = 0; i < indxs.Count; i++)
            {
                float sum = 0;
                int n = indxs[i];

                for (int k = InputSignal1.SamplesIndices[0]; !(n - k < InputSignal2.SamplesIndices[0] || k > InputSignal1.SamplesIndices[InputSignal1.SamplesIndices.Count - 1]); k++)
                {
                    int k_indx = InputSignal1.SamplesIndices.IndexOf(k);
                    int n_k = InputSignal2.SamplesIndices.IndexOf(n - k);

                    if (n - k > InputSignal2.SamplesIndices[InputSignal2.SamplesIndices.Count - 1] || k < InputSignal1.SamplesIndices[0])
                        continue;

                    sum += InputSignal1.Samples[k_indx] * InputSignal2.Samples[n_k];
                }
                convolvedSamples.Add(sum);
            }

            if (convolvedSamples[convolvedSamples.Count - 1] == 0)
            {
                convolvedSamples.RemoveAt(convolvedSamples.Count - 1);
                indxs.RemoveAt(indxs.Count - 1);
            }
            OutputConvolvedSignal = new Signal(convolvedSamples, indxs, false);

        }
    }
}