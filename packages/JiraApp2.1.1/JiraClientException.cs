﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace JiraAddin
{
    [Serializable]
    public class JiraClientException : Exception
    {
        private readonly string response;
        public JiraClientException() { }
        public JiraClientException(string message) : base(message) { }
        public JiraClientException(string message, string response) : base(message) { this.response = response; }
        public JiraClientException(string message, Exception inner) : base(message, inner) { }
        protected JiraClientException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public string ErrorResponse { get { return response; } }
    }
}
