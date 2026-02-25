using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var postgresdb = postgres.AddDatabase("postgresdb");


var apiService = builder.AddProject<Projects.AspireApp_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WaitFor(postgresdb)
    .WithReference(postgresdb);

builder.AddProject<Projects.AspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddViteApp(name: "frontend", workingDirectory: "../AspireApp.Frontend")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithNpmPackageInstallation();


var scalar = builder.AddScalarApiReference();

scalar
    .WithApiReference(apiService);


builder.Build().Run();
