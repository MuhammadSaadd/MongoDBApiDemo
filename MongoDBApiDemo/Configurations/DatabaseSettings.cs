﻿namespace MongoDBApiDemo.Configurations;

public class DatabaseSettings
{
    public string MongoDbConnection { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;

}
