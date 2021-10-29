using System;
namespace Api.Data.Configurations
{
    public class DataBaseConfig : IDataBaseConfig
    {
        public DataBaseConfig()
        {
        }

        public string DataBaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}

