using GraphQL.Types;
using MeetupGraphQLNet.Models;

namespace MeetupGraphQLNet.Types
{
    public class PessoaInputType : InputObjectGraphType<Pessoa>
    {
        public PessoaInputType()
        {
            Name = nameof(PessoaInputType);

            Field(x => x.Id, type: typeof(IntGraphType), nullable: false);
            Field(x => x.Nome, type: typeof(StringGraphType), nullable: false);
            Field(x => x.Email, type: typeof(StringGraphType), nullable: false);
        }
    }
}
