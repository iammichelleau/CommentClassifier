using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommentClassifierApi.Models;
using CommentClassifierApi.NeuralNetwork.Models;
using Newtonsoft.Json;

namespace CommentClassifierApi.NeuralNetwork.Helpers
{
    public class ImportHelper
	{
        
        public static Network ImportNetwork()
		{
			var dn = GetHelperNetwork();
			if (dn == null) return null;

			var network = new Network();
			var allNeurons = new List<Neuron>();

			network.LearnRate = dn.LearnRate;
			network.Momentum = dn.Momentum;

			//Input Layer
			foreach (var n in dn.InputLayer)
			{
				var neuron = new Neuron
				{
					Id = n.Id,
					Bias = n.Bias,
					BiasDelta = n.BiasDelta,
					Gradient = n.Gradient,
					Value = n.Value
				};

				network.InputLayer.Add(neuron);
				allNeurons.Add(neuron);
			}

			//Hidden Layers
			foreach (var layer in dn.HiddenLayers)
			{
				var neurons = new List<Neuron>();
				foreach (var n in layer)
				{
					var neuron = new Neuron
					{
						Id = n.Id,
						Bias = n.Bias,
						BiasDelta = n.BiasDelta,
						Gradient = n.Gradient,
						Value = n.Value
					};

					neurons.Add(neuron);
					allNeurons.Add(neuron);
				}

				network.HiddenLayers.Add(neurons);
			}

			//Export Layer
			foreach (var n in dn.OutputLayer)
			{
				var neuron = new Neuron
				{
					Id = n.Id,
					Bias = n.Bias,
					BiasDelta = n.BiasDelta,
					Gradient = n.Gradient,
					Value = n.Value
				};

				network.OutputLayer.Add(neuron);
				allNeurons.Add(neuron);
			}

			//Synapses
			foreach (var syn in dn.Synapses)
			{
				var synapse = new Synapse { Id = syn.Id };
				var inputNeuron = allNeurons.First(x => x.Id == syn.InputNeuronId);
				var outputNeuron = allNeurons.First(x => x.Id == syn.OutputNeuronId);
				synapse.InputNeuron = inputNeuron;
				synapse.OutputNeuron = outputNeuron;
				synapse.Weight = syn.Weight;
				synapse.WeightDelta = syn.WeightDelta;

				inputNeuron.OutputSynapses.Add(synapse);
				outputNeuron.InputSynapses.Add(synapse);
			}

			return network;
		}

		private static HelperNetwork GetHelperNetwork()
		{
			try
			{
			    var FileName = Directory.GetCurrentDirectory() + @"\NeuralNetwork\TrainingData\trainedNetwork.txt";

			    using (var file = File.OpenText(FileName))
			    {
			        return JsonConvert.DeserializeObject<HelperNetwork>(file.ReadToEnd());
			    }
            }
			catch (Exception)
			{
				return null;
			}
		}

        public static double[] ConvertToNetworkInput(Comment comment)
        {
            var punctuation = comment.Content.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = comment.Content.Replace("\n", "").ToLower().Split().Select(x => x.Trim(punctuation)).ToList();

            var keywords =  Comment.PositiveKeywords.Concat(Comment.NegativeKeywords).Concat(Comment.NeutralKeywords).ToList();

            var inputs = new double[keywords.Count()];
            for (int i = 0; i < keywords.Count(); i++)
            {
                if (words.Contains(keywords[i]))
                    inputs[i] = 1.0;
            }

            return inputs;
        }

    }
}
