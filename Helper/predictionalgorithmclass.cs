using Accord.Neuro;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using Accord.Math;
using System;
using System.Linq;
using System.IO;
namespace webproject2.Helper
{
    public  class predictionalgorithmclass
    {

        public predictionalgorithmclass(){
            
        }
        public void predictionalgorithm(){
            
            // double[,] inputs=new double[10,10];
            // double[,] outputs=new double[10][];
            // double[][] testInputs=new double[10][];
            // double[][] testOutputs=new double[10][];
            // new filemangerhelp().readfiledt(6,6,inputs,outputs);
           
            // inputs = inputs.Take(10).ToArray();
            // outputs = outputs.Take(10).ToArray();
            // testInputs = inputs.Skip(10).ToArray();
            // testOutputs = outputs.Skip(10).ToArray();
            // // Setup the deep belief network and initialize with random weights.
            // DeepBeliefNetwork network = new DeepBeliefNetwork(inputs.First().Length, 10, 10);
            // new GaussianWeights(network, 0.1).Randomize();
            // network.UpdateVisibleWeights();
            
           

            
            // // Supervised learning on entire network, to provide output classification.
            // var teacher2 = new BackPropagationLearning(network)
            // {
            //     LearningRate = 0.1,
            //     Momentum = 0.5
            // };

            // // Run supervised learning.
            // for (int i = 0; i < 500; i++)
            // {
            //     double error = teacher2.RunEpoch(inputs, outputs) / inputs.Length;
            //     if (i % 10 == 0)
            //     {
            //         Console.WriteLine(i + ", Error = " + error);
            //     }
            // }
            // // Test the resulting accuracy.
            // int correct = 0;
            // for (int i = 0; i < inputs.Length; i++)
            // {
            //     double[] outputValues = network.Compute(testInputs[i]);
            //     if (DataManager.FormatOutputResult(outputValues) == DataManager.FormatOutputResult(testOutputs[i]))
            //     {
            //         correct++;
            //     }
            // }


        
        }



        
    }
}