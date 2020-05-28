using GraphQL.Types;

namespace MeetupGraphQLNet.Mutations
{
    public class Mutation : ObjectGraphType
    {
        public Mutation()
        {
            Name = "Mutations";

            Field<PessoaMutation>("pessoa", resolve: context => new { });
        }
    }
}
