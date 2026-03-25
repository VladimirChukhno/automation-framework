using NUnit.Framework;
using RestSharp;
using SeleniumAPI.Helpers;
using SeleniumAPI.Models.Client;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace SeleniumAPI.Tests
{
    [TestFixture]
    public class ClientTests
    {
        private ApiClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient();
        }

        // =========================
        // ✅ ПОЗИТИВНИЙ
        // =========================

        [Test]
        public void Create_Client_Positive()
        {
            var request = new ClientRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Balance = 100
            };

            var sw = Stopwatch.StartNew();
            var response = _apiClient.PostClient(request);
            sw.Stop();

            Assert.AreEqual(201, (int)response.StatusCode);
            Assert.Less(sw.ElapsedMilliseconds, 2000);
            Assert.IsTrue(response.ContentType.Contains("application/json"));

            var body = _apiClient.DeserializeResponse<ClientResponse>(response);

            Assert.IsTrue(body.Id > 0);
            Assert.AreEqual(request.Name, body.Name);
            Assert.AreEqual(request.Email, body.Email);
            Assert.AreEqual(request.Balance, body.Balance);
        }

        [Test]
        public void Create_Client_Validate_JsonSchema()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Email = "test@test.com",
                Balance = 100
            };
            var response = _apiClient.PostClient(request);
            var json = JToken.Parse(response.Content);

            var schemaJson = File.ReadAllText("Schemas/client-schema.json");
            var schema = JSchema.Parse(schemaJson);

            bool isValid = json.IsValid(schema, out IList<string> errors);

            Assert.IsTrue(isValid, "Schema errors: " + string.Join(", ", errors));
        }

        // =========================
        // ❌ НЕГАТИВНІ
        // =========================

        [Test]
        public void Create_InvalidRequestMethod()
        {
            var response = _apiClient.ExecuteWrongMethod("/client", Method.Get);
            Assert.AreEqual(405, (int)response.StatusCode);
        }

        [Test]
        public void Create_InvalidFormatType_XML()
        {
            var response = _apiClient.ExecuteInvalidFormat("/client", "application/xml");
            Assert.AreEqual(415, (int)response.StatusCode);
        }

        [Test]
        public void Create_InvalidFormatJson()
        {
            var response = _apiClient.ExecuteInvalidJson("/client");
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        // NAME

        [Test]
        public void Create_MissingRequaredName()
        {
            var request = new ClientRequest
            {
                Email = "test@test.com",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_EmptyRequaredName()
        {
            var request = new ClientRequest
            {
                Name = "",
                Email = "test@test.com",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_NullRequaredName()
        {
            var request = new ClientRequest
            {
                Name = null,
                Email = "test@test.com",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_RequaredNameWithSpaces()
        {
            var request = new ClientRequest
            {
                Name = "     ",
                Email = "test@test.com",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        // EMAIL

        [Test]
        public void Create_MissingRequaredEmail()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_EmptyRequaredEmail()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Email = "",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_NullRequaredEmail()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Email = null,
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_InvalidValueEmail()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Email = "invalid-email",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_InvalidValueWithoutAtEmail()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Email = "invalidemail.com",
                Balance = 100
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        // BALANCE

        [Test]
        public void Create_NegativeValueBalance()
        {
            var request = new ClientRequest
            {
                Name = "Test",
                Email = "test@gmail.com",
                Balance = -1 
            };

            var response = _apiClient.PostClient(request);
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void Create_InvalidBalance_Type()
        {
            var invalidJson = @"{
            ""name"": ""Test"",
            ""email"": ""test@test.com"",
            ""balance"": true
            }";

            var request = new RestRequest("/client", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddStringBody(invalidJson, DataFormat.Json);

            var response = _apiClient.ExecuteRaw(request);

            Assert.AreEqual(400, (int)response.StatusCode);
        }
    }
}