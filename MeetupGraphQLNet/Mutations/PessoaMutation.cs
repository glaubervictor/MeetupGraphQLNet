using GraphQL.Types;
using MeetupGraphQLNet.Models;
using MeetupGraphQLNet.Types;
using System;

namespace MeetupGraphQLNet.Mutations
{
    public class PessoaMutation : ObjectGraphType
    {
        public PessoaMutation(MeetupGraphQLNetContext context)
        {
            Name = nameof(PessoaMutation);

            Field<PessoaType>(
                "create",
                description: "Criar um novo registro de pessoa",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PessoaInputType>> { Name = "pessoa" }
                ),
                resolve: ctx =>
                {
                    var pessoa = ctx.GetArgument<Pessoa>("pessoa");

                    context.Pessoas.Add(pessoa);
                    context.SaveChanges();

                    return pessoa;
                });

            Field<PessoaType>(
                "update",
                description: "Atualizar um registro de pessoa",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PessoaInputType>> { Name = "pessoa" }
                ),
                resolve: ctx =>
                {
                    var pessoa = ctx.GetArgument<Pessoa>("pessoa");

                    context.Pessoas.Update(pessoa);
                    context.SaveChanges();

                    return pessoa;
                });

            Field<StringGraphType>(
                "delete",
                description: "Excluir um registro de pessoa",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<int>("id");
                    var pessoa = context.Pessoas.Find(id);

                    context.Pessoas.Remove(pessoa);
                    context.SaveChanges();

                    return $"O registro {id} foi excluído com sucesso!.";
                });
        }
    }
}
