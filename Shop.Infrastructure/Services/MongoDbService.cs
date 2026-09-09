using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Shop.Application.DTOs.ProductFeedbackDTOs;
using Shop.Application.Interfaces.Services;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services;


public class MongoDbService : IMongoDbService
{
    private readonly IMongoCollection<ProductFeedback> _collection;

    public MongoDbService(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        _collection = database.GetCollection<ProductFeedback>("ProductFeedback");
    }
    public async Task AddFeedbackAsync(ProductFeedbackDTO feedback, CancellationToken cancellationToken)
    {
        var productFeedback = new ProductFeedback
        {
            ProductId = feedback.ProductId,
            Type = feedback.Type,
            UserEmail = feedback.UserEmail,
            Message = feedback.Message,
            Rating = feedback.Rating
        };

        await _collection.InsertOneAsync(productFeedback, cancellationToken);
   
    }

}
