﻿using Core.Entities;
using Core.Interfaces.Persistence;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CosmosDbData.Repository
{
    public class ApplicantTaskRepository : CosmosDbRepository<ApplicantTask>, IApplicantTaskRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "applicantTasks";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "Id:783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(ApplicantTask entity) => $"{Guid.NewGuid()}";

        public ApplicantTaskRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Applicant is also an example of single partition read, where get by any other collection value will be a cross partition read
        public async Task<IEnumerable<ApplicantTask>> GetItemsAsyncByApplicantId(string applicantId)
        {
            List<ApplicantTask> results = new List<ApplicantTask>();
            string query = @$"SELECT c.* FROM c WHERE c.applicantId = @applicantId";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@applicantId", applicantId);
            string queryString = queryDefinition.ToString();

            var entities = await this.GetItemsAsync(queryString);

            return results;
        }

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Year is also an example of cross partition read, where Get by Category will be single partition read
        //public async Task<IEnumerable<TimeSlot>> GetItemsAsyncByTitle(string otherProperty)
        //{
        //    List<TimeSlot> results = new List<TimeSlot>();
        //    string query = @$"SELECT c.Name FROM c WHERE c.otherProperty = @otherProperty";

        //    QueryDefinition queryDefinition = new QueryDefinition(query)
        //                                            .WithParameter("@otherProperty", otherProperty);
        //    string queryString = queryDefinition.ToString();

        //    var entities = await this.GetItemsAsync(queryString);

        //    return results;
        //}
    }
}

