using System;
using System.Collections.Generic;

namespace Workflowapi.util
{

    public class RetornoREST<E>
    {
        public List<E>? Dados { get; set; }
        public Int16 Error { get; set; }
        public Int64 Total_registro { get; set; }
        public String? Mensagem { get; set; }

    }


    public class RetornoSingleREST<E>
    {
        public E? Dados { get; set; }
        public Int16 Error { get; set; }
        public Int64 Total_registro { get; set; }
        public String? Mensagem { get; set; }

    }

}