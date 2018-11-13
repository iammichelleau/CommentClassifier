using System;
using System.Collections.Generic;
using System.Linq;
using CommentClassifierApi.Models;
using CommentClassifierApi.NeuralNetwork.Helpers;
using CommentClassifierApi.NeuralNetwork.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CommentClassifierApi.Controllers
{
    [Route("api/comments")]
    [EnableCors("AllowAllOrigins")]
    public class CommentController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]Comment comment)
        {
            Network network = ImportHelper.ImportNetwork();

            var inputs = ImportHelper.ConvertToNetworkInput(comment);
            double[] outputs = network.Compute(inputs);

            var outputsList = new List<double>(outputs);

            var outputValue = outputsList.OrderBy(x => Math.Abs(x - 1.0)).First();
            var outputDecision = 0;
            var tol = 0.01;
            for (int i = 0; i < outputs.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine($"\t{(Comment.Output)i}: {outputs[i]}");
                if (Math.Abs(outputs[i] - outputValue) < tol)
                    outputDecision = i;
            }
            System.Diagnostics.Debug.WriteLine($"\tOutput: {(Comment.Output)outputDecision}");

            return ((Comment.Output)outputDecision).ToString();
        }
    }
}
