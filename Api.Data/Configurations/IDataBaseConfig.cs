using System;
namespace Api.Data.Configurations
{
    public interface IDataBaseConfig
    {
        string DataBaseName { get; set; }

        string ConnectionString { get; set; }
    }
}

