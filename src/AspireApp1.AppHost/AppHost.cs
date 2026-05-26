var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var appConfig = builder.AddAzureAppConfiguration("config")
    .RunAsEmulator(emulator =>
    {
        emulator.WithLifetime(ContainerLifetime.Persistent);
    });

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator(emulator =>
    {
        emulator.WithLifetime(ContainerLifetime.Persistent);
    })
    .AddBlobs("blobs");

var db = sql.AddDatabase("database");

builder.AddProject<Projects.AspireApp1_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(db)
    .WaitFor(db)
    .WithReference(appConfig)
    .WithReference(storage)
    .WaitFor(storage);

await builder.Build().RunAsync();
