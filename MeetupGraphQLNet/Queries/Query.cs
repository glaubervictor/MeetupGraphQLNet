using GraphQL.Types;

namespace MeetupGraphQLNet.Queries
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Name = "Queries";
            Field<PessoaQuery>("pessoa", resolve: context => new { });
        }
    }
}
