using GraphQL.Types;
using MeetupGraphQLNet.Models;
using MeetupGraphQLNet.Types;
using System.Linq;

namespace MeetupGraphQLNet.Queries
{
    public class PessoaQuery : ObjectGraphType
    {
        public PessoaQuery(MeetupGraphQLNetContext context)
        {
            Name = nameof(PessoaQuery);

            Field<PessoaType>(
               "byId",
               description: "Obter pessoa pelo id",
               arguments: new QueryArguments(new QueryArgument<IdGraphType>
               {
                   Name = "id",
                   Description = "Id"
               }),
               resolve: ctx => context.Pessoas.Find(ctx.GetArgument<int>("id")));

            Field<ListGraphType<PessoaType>>(
                "all",
                description: "Obter os registros de pessoa",
                 arguments: new QueryArguments(new QueryArgument<IntGraphType>
                 {
                     Name = "index",
                     DefaultValue = 0,
                     Description = "Índice da página"
                 },
                new QueryArgument<IntGraphType>
                {
                    Name = "size",
                    DefaultValue = 30,
                    Description = "Tamanho da página"
                },
                new QueryArgument<StringGraphType>
                {
                    Name = "name",
                    DefaultValue = "",
                    Description = "Palavra de pesquisa"
                }),

                resolve: ctx => context.Pessoas
                    .Take(ctx.GetArgument<int>("size"))
                    .Skip(ctx.GetArgument<int>("index")));
                    
        }
    }
}
