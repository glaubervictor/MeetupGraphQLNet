using GraphQL.Types;
using MeetupGraphQLNet.Models;

namespace MeetupGraphQLNet.Types
{
    public class PessoaType : ObjectGraphType<Pessoa>
    {
        public PessoaType()
        {
            Name = nameof(PessoaType);

            Field(x => x.Id, type: typeof(IntGraphType));
            Field(x => x.Nome, type: typeof(StringGraphType));
            Field(x => x.Email, type: typeof(StringGraphType));
        }
    }
}
