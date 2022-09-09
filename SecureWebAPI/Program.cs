using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Audience = builder.Configuration.GetSection("AAD:ResourceId").Value; //  audience is the API
                    opt.Authority = $"{builder.Configuration.GetSection("AAD:Instance").Value}{builder.Configuration.GetSection("AAD:TenantId").Value}"; //The authority to issue web tokens on our behalf. In this case it's the Azure AD
                    opt.RequireHttpsMetadata = false;
                });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
