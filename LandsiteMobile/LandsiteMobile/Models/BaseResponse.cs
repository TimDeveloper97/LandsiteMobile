using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandsiteMobile.Models
{
    public class Errors
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Error
    {
        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class BaseResponse
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
