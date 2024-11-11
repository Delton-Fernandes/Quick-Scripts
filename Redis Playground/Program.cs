using StackExchange.Redis;


ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();
db.StringSet("first key", "first value");
redis.Close();