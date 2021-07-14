using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {            
            List<LocationData> locationDatas = new List<LocationData>();
            
            Random random = new Random();
            //var value = lower + (random.NextDouble() * (upper - lower))
            for (int i = 0; i < 10; i++)
            {
                var randDouble = random.NextDouble();
                var latitude = -35.12 + (randDouble * -35.99 - (-35.12));
                var longitude = 149.01 + (randDouble * 149.9999 - (149.01));                
                locationDatas.Add(new LocationData(Guid.NewGuid(), latitude, longitude));
            }

            Console.WriteLine(JsonSerializer.Serialize(locationDatas));

            var recordsInserted = Insert(locationDatas, data => new {
                    data.Latitude,
                    data.Longitude
                });

            Console.WriteLine("Records inserted: " + recordsInserted);
            

            Console.ReadLine();
        }

        private static int Insert(List<LocationData> items, Expression<Func<LocationData, object>> expression)
        {

            var context = new AppDbContext();
            var resultInfo = new Z.BulkOperations.ResultInfo();
            try
            {
                context.BulkInsert(items, options =>
                {
                    options.InsertIfNotExists = true;
                    options.ColumnPrimaryKeyExpression = expression;
                    options.UseRowsAffected = true;
                    options.ResultInfo = resultInfo;
                });
                var rowsInserted = resultInfo.RowsAffectedInserted;
                return rowsInserted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}
