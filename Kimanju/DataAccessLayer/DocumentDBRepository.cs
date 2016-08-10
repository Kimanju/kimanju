using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Kimanju.DataAccess
{
  public class DocumentDBRepository
  {
    public DocumentClient Client { get; set; }

    public DocumentDBRepository(Uri endPoint, String primaryKey)
    {
      Client = new DocumentClient(endPoint, primaryKey);
    }

    public async Task CreateDatabaseIfNotExists(String databaseName)
    {
      try
      {
        await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
      }
      catch (DocumentClientException de)
      {
        // If the database does not exist, create a new database
        if (de.StatusCode == HttpStatusCode.NotFound)
        {
          await Client.CreateDatabaseAsync(new Database { Id = databaseName });
        }
        else
        {
          throw;
        }
      }
    }

    public async Task CreateDocumentCollectionIfNotExists(String databaseName, String collectionName)
    {
      try
      {
        await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
      }
      catch (DocumentClientException de)
      {
        // If the document collection does not exist, create a new collection
        if (de.StatusCode == HttpStatusCode.NotFound)
        {
          var collectionInfo = new DocumentCollection
          {
            Id = collectionName,
            // Configure collections for maximum query flexibility including string range queries.
            IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) {Precision = -1})
          };

          // Here we create a collection with 400 RU/s.
          await Client.CreateDocumentCollectionAsync(
              UriFactory.CreateDatabaseUri(databaseName),
              collectionInfo,
              new RequestOptions { OfferThroughput = 400 });
        }
        else
        {
          throw;
        }
      }
    }

    public async Task CreateKimanjuDocumentIfNotExists(String databaseName, String collectionName, IKimanjuDocument document)
    {
      try
      {
        await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, document.Id));
      }
      catch (DocumentClientException de)
      {
        if (de.StatusCode == HttpStatusCode.NotFound)
        {
          await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), document);
        }
        else
        {
          throw;
        }
      }
    }

    public async Task<IKimanjuDocument> GetItemAsync(String databaseName, String collectionName, String id)
    {
      try
      {
        Document document = await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, id));
        return (IKimanjuDocument)document;
      }
      catch (DocumentClientException e)
      {
        if (e.StatusCode == HttpStatusCode.NotFound)
        {
          return null;
        }
        else
        {
          throw;
        }
      }
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(String databaseName, String collectionName, Expression<Func<T, Boolean>> predicate)
      where T : class
    {
      var query =
        Client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName))
          .Where(predicate)
          .AsDocumentQuery();

      var results = new List<T>();
      while (query.HasMoreResults)
      {
        results.AddRange(await query.ExecuteNextAsync<T>());
      }

      return results;
    }
  }
}