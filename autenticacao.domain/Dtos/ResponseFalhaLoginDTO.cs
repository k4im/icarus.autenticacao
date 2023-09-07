using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autenticacao.domain.Dtos
{
    public class ResponseFalhaLoginDTO
    {
        public string Message { get; }

        public ResponseFalhaLoginDTO(string message)
        {
            Message = message;
        }
    }
}