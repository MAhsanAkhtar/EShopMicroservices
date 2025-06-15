
using Ordering.Application.Order.Command.DeleteOrder;

namespace Ordering.API.Endpoints;

// Accept the order ID as parameters
// Constructs a DeleteOrderCommand
// Send the command using MediatR
// Returns a success or not found response. 

public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sendeer) =>
        {
            var result = await sendeer.Send(new DeleteOrderCommand(id));

            var response =  result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
            .WithName("DeletOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order"); ;
    }
}
