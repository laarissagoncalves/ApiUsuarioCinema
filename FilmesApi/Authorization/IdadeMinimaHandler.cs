using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmesApi.Authorization
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IdadeMinimaRequirement requirement)
        {
            //Se a gente tiver dentro das claims do usuario uma claim do tipo DateOfBirth segue
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                //senão retorne
                return Task.CompletedTask;
            // pegando do meu contexto, do token do usuário a claim que possui o tipo de DateOfBirth, que é a data de nascimento, e estou pegando o valor dessa claim e convertendo isso para uma data de nascimento, um DateTime.
            DateTime dataNascimento = Convert.ToDateTime(context.User.FindFirst(c =>
                c.Type == ClaimTypes.DateOfBirth
            ).Value);

            int idadeObtida = DateTime.Today.Year - dataNascimento.Year;

            //Se minha data de nascimento, eu estou no dia 01/12/2100, se a data de nascimento da pessoa for 11/12/2100, ela ainda não fez aniversário nesse ano, então não vamos considerar esse ano, é basicamente isso que estamos fazendo. Então vamos subtrair caso a pessoa ainda não tenha feito aniversário.
            if (dataNascimento > DateTime.Today.AddYears(-idadeObtida))
                idadeObtida--;

            if (idadeObtida >= requirement.IdadeMinima) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}