using Newtonsoft.Json;
using System.Collections.Generic;

namespace HangfireApp.Models
{
    public class JobModel
    {
        public long Id { get; set; }

        public string InvocationData { get; set; }

        public string Arguments { get; set; }
    }

    public class Root
    {
        [JsonProperty("t")]
        public string t { get; set; }

        [JsonProperty("m")]
        public string Method { get; set; }

        [JsonProperty("p")]
        public List<string> Parameters { get; set; }
    }
}