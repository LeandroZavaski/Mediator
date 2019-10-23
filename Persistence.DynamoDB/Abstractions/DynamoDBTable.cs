using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.DynamoDB.Abstractions
{
    public class DynamoDBTable
    {
        private readonly AwsCredentials _awsCredentials;
        protected IAmazonDynamoDB Database { get; }

        protected DynamoDBTable(IAmazonDynamoDB database, IOptions<AwsCredentials> awsCredentials)
        {
            //Database = database;
            _awsCredentials = awsCredentials.Value;
            Database = new AmazonDynamoDBClient(_awsCredentials.AccessKey, _awsCredentials.SecretKey, _awsCredentials.TokenKey, RegionEndpoint.USEast1);
        }

        private readonly string TableName = "TempDynamoDbTable";
        private Table _table;

        protected Table Table
        {
            get
            {
                if (_table != null) return _table;
                _table = Table.LoadTable(Database, TableName);
                return _table;
            }
        }
    }
}
