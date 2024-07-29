using TackleWiki.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IArticleRepository, ArticleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var articleRepository = app.Services.GetRequiredService<IArticleRepository>();
await articleRepository.RegisterFakeArticlesAsync();

app.MapGet("/articles", async () =>
{
    var articles = await articleRepository.GetArticlesAsync();
    return Results.Ok(articles);
});

app.Run();