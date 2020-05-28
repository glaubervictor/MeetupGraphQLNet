using GraphQL;
using GraphQL.Types;
using MeetupGraphQLNet.Mutations;
using MeetupGraphQLNet.Queries;

namespace MeetupGraphQLNet.Schemas
{
    public class MeetupGraphQLNetSchema : Schema
    {
        public MeetupGraphQLNetSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<Query>();
            Mutation = resolver.Resolve<Mutation>();
        }
    }
}
