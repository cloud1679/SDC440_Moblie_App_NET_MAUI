using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using Web_Service_Authentication_and_Storage.Authentication;
using Web_Service_Authentication_and_Storage.DataAccess;
using Web_Service_Authentication_and_Storage.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi(options => options.AddDocumentTransformer((document, context, cancellationToken) =>
{
    document.Components ??= new OpenApiComponents();
    document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
    document.Components.SecuritySchemes["Basic"] = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Enter your assignment user name and password."
    };
    // Only login requires credentials; item storage and retrieval remain anonymous.
    if (document.Paths.TryGetValue("/api/auth/login", out var loginPath)
        && loginPath.Operations is not null)
    {
        foreach (var operation in loginPath.Operations.Values)
            operation.Security = [new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Basic", document)] = []
            }];
    }
    return Task.CompletedTask;
}));
builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
builder.Services.AddAuthorization();
builder.Services.AddSingleton<ItemRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint(
        "/openapi/v1.json", "Week 4 Authentication and Storage API"));
}
// Local emulator development uses HTTP. Deployed Basic authentication must use HTTPS.
else app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/auth/login", () => Results.Ok(new { userName = "Burns01" }))
    .RequireAuthorization();

app.MapGet("/api/items", (ItemRepository repository) => Results.Ok(repository.GetAll()))
    .AllowAnonymous();

app.MapPost("/api/items", (StoredItem item, ItemRepository repository) =>
{
    if (string.IsNullOrWhiteSpace(item.ItemId) || string.IsNullOrWhiteSpace(item.ItemName)
        || string.IsNullOrWhiteSpace(item.ItemDescription))
        return Results.BadRequest(new { message = "Item ID, Item Name, and Item Description are required." });

    item.ItemId = item.ItemId.Trim();
    item.ItemName = item.ItemName.Trim();
    item.ItemDescription = item.ItemDescription.Trim();
    return repository.TryAdd(item)
        ? Results.Json(item, statusCode: StatusCodes.Status201Created)
        : Results.Conflict(new { message = "An item with this Item ID already exists." });
}).AllowAnonymous();

app.Run();
