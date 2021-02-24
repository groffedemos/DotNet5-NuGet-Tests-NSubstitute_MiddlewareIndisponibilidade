using Microsoft.AspNetCore.Builder;
using Groffe.AspNetCore.Indisponibilidade;

namespace ExemploAPIMiddleware
{
    public class IndisponibilidadePipeline
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseChecagemIndisponibilidade();
        }
    }
}