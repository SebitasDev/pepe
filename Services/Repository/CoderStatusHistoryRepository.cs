using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;
using RiwiTalent.Models.Enums;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class CoderStatusHistoryRepository : ICoderStatusHistoryRepository
    {
        private readonly IMongoCollection<CoderStatusHistory> _mongoCollection;
        private readonly IMapper _mapper;

        private string Error = "The group not found";
        public CoderStatusHistoryRepository(MongoDbContext context, IMapper mapper)
        {
            _mongoCollection = context.CoderStatusHistories;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CoderStatusHistory>> GetCodersStatus()
        {
          List<CoderStatusHistory> coders = await _mongoCollection.Find(_ => true)
            .ToListAsync();

          return coders;
        }

        // public async Task<IEnumerable<CoderStatusHistory>> GetCompanyGroupedCoders(string companyId)
        // {
        //   List<CoderStatusHistory> coders = await _mongoCollection.Find(x => x.IdGroup == companyId && x.Status == Status.Grouped.ToString())
        //     .ToListAsync();

        //   // List<CoderStatusHistory> coders2 = await _mongoCollection.Distinct(nameof(CoderStatusHistory.IdCoder), x => x.IdGroup == companyId && x.Status == Status.Grouped.ToString());
        //   var builder = Builders<CoderStatusHistory>.Filter;
        //   var filter = builder.Eq(x => x.IdGroup, companyId)
        //     & builder.Eq(x => x.Status, Status.Grouped.ToString());

        //   var coders2 = await _mongoCollection.Distinct<ObjectId>(nameof(CoderStatusHistory.IdCoder), filter)
        //     .ToListAsync();


        //   return coders;
        // }Hello World!
        public async Task<IEnumerable<CoderStatusHistory>> GetCompanyGroupedCoders(string companyId)
        {
          var builder = Builders<CoderStatusHistory>.Filter;

          var filterGroup = builder.Eq(x => x.IdGroup, companyId);
          var filterStatus = builder.Eq(x => x.Status, Status.Grouped.ToString());

          var query = _mongoCollection.Aggregate()
            .Match(filterGroup & filterStatus)
            .SortByDescending(p => p.Date)
            .Group(p => p.IdCoder, g => new
            { 
              Data = g.First()
            })
            .ToList();

          var data = query.Select(r => r.ToBsonDocument()).ToList();
          
          return null;
        }

        // public async Task<IEnumerable<CoderStatusHistory>> GetCompanyGroupedCoders(string companyId)
        // {
        //   var query = new BsonDocument[]
        //   {
        //     // new BsonDocument
        //     // {
        //       // {"$match": new}
        //     // }
        //     // new BsonDocument("$match", new BsonDocument("companyId", new ObjectId(companyId))), // Example companyId = 1
        //     // new BsonDocument("$match", new BsonDocument("companyId", new ObjectId("60d5ec1ad47e3c001c4f010a"))), // Example companyId = 1
        //     new BsonDocument("$sort", new BsonDocument
        //     {
        //       // {"IdCoder", 1},
        //       {"status", 1}
        //     })
        //   };

        //   var result = _mongoCollection.Aggregate<CoderStatusHistory>(query)
        //     .ToList();

        //   return result;
        // }

        /*
        using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

public class PaymentService
{
    private readonly IMongoDatabase _database;

    public PaymentService()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        _database = client.GetDatabase("yourDatabaseName");
    }

    public List<BsonDocument> GetFilteredCoders()
    {
        var paymentsCollection = _database.GetCollection<BsonDocument>("companiesCoderPayment");

        // Define the aggregation pipeline
        var pipeline = new BsonDocument[]
        {
            // Step 1: Filter payments for the specific company (companyId = 1)
            new BsonDocument("$match", new BsonDocument("companyId", new ObjectId("60d5ec1ad47e3c001c4f010a"))), // Example companyId = 1

            // Step 2: Join with the coder collection to get coder information
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "coder" }, // The collection to join
                { "localField", "coderId" }, // Field in companiesCoderPayment
                { "foreignField", "_id" }, // Field in coder collection (the _id field)
                { "as", "coderInfo" } // Output field
            }),

            // Step 3: Unwind the coderInfo array to get a single coder object
            new BsonDocument("$unwind", "$coderInfo"),

            // Step 4: Filter coders that are alive and have two kids
            new BsonDocument("$match", new BsonDocument
            {
                { "coderInfo.isAlive", true },
                { "coderInfo.numberOfKids", 2 }
            }),

            // Step 5: Group by coderId to ensure each coder appears only once
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$coderId" }, // Group by coderId
                { "coderName", new BsonDocument("$first", "$coderInfo.name") }, // Get the first coder name
                { "totalPayments", new BsonDocument("$sum", 1) }, // Count the total number of payments for this coder (optional)
                { "coderDetails", new BsonDocument("$first", "$coderInfo") } // Get the first document of coder info
            })
        };

        // Execute the aggregation pipeline
        var results = paymentsCollection.Aggregate<BsonDocument>(pipeline).ToList();

        return results;
    }
}

        */
    }
}