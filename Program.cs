var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Crear Lista Productos
var products = new List<Productos>();

//Obtiene Productos
app.MapGet("/products", ()=>
{
    return products;
});

//Obtener por id
app.MapGet("/products/{id}", (int id)=>{
var product = products.FirstOrDefault(c => c.Id == id);
return product;
});

//Crear Productos
app.MapPost("/products", (Productos producto)=>{
    products.Add(producto);
    return Results.Ok();
});

//Actualiza por id
app.MapPut("/products/{id}", (int id, Productos producto)=>{
    var existingProduct = products.FirstOrDefault(c => c.Id == id);
    if(existingProduct != null)
    {
        existingProduct.Nombre = producto.Nombre;
        existingProduct.Descripcion = producto.Descripcion;
        return Results.Ok();
    }
    else{
        return Results.NotFound();
    }
});

//Eliminar por id
app.MapDelete("/products/{id}", (int id)=>{
    var existingProduct = products.FirstOrDefault(c => c.Id == id);
    if(existingProduct != null)
    {
        products.Remove(existingProduct);
        return Results.Ok();
    }
    else{
        return Results.NotFound();
    }
});

app.Run();

internal class Productos
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}