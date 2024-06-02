using Newtonsoft.Json;
using System.Net.Http.Headers;
using T_Shop.Client.MVC.Repository.Interfaces;
using T_Shop.Client.MVC.Services.Services;
using T_Shop.Shared.DTOs.ProductReview.RequestModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;
using T_Shop.Shared.DTOs.Transaction.RequestModel;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;
using T_Shop.Shared.ViewModels.ProductsPage;
using T_Shop.Shared.ViewModels.TransactionPage;

namespace T_Shop.Client.MVC.Repository.Repository;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
    {

    }


    public async Task<TransactionResponseModel> GetTransactionByIdAsync(Guid transactionId)
    {
        var requestUrl = $"api/transaction/{transactionId}";
        HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TransactionResponseModel>(data);
        }
        return null;
    }

    public async Task<TransactionVM> GetTransactionsOfUserAsync(TransactionRequestParam transactionRequestParam, Guid userId)
    {
        var query = new Dictionary<string, string>
        {
            ["pageNumber"] = transactionRequestParam.PageNumber.ToString(),
            ["pageSize"] = transactionRequestParam.PageSize.ToString(),
        };
        var queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));

        var requestUrl = $"api/transaction/user/{userId}?{queryString}";

        HttpResponseMessage response = _httpClient.GetAsync(requestUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            var transactions = JsonConvert.DeserializeObject<List<TransactionResponseModel>>(data);
            TransactionVM transactionVM = new TransactionVM()
            {
                Transactions = transactions
            };

            if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> paginationValues))
            {
                string paginationHeaderValue = paginationValues.FirstOrDefault();
                var pagination = JsonConvert.DeserializeObject<MetaData>(paginationHeaderValue);
                //Set value pagination
                transactionVM.PaginationMetaData = pagination;
            }
            return transactionVM;
        }

        return new TransactionVM();
    }

    public async Task<TransactionResponseModel> UpdateTransactionStatusAsync(TransactionUpdateRequestModel transactionToUpdate)
    {
        var requestUrl = $"api/transaction/{transactionToUpdate.ID}";
        HttpResponseMessage response = _httpClient.PutAsJsonAsync(requestUrl, transactionToUpdate).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TransactionResponseModel>(data); ;
        }
        return null;
    }


    public async Task<ProductReviewResponseModel> CreateReviewForProduct(ProductReviewCreationRequestModel review)
    {
        var requestUrl = $"api/transaction/review";

        var formData = new MultipartFormDataContent();

        // Add the review properties to the formData
        formData.Add(new StringContent(review.UserID.ToString()), "UserID");
        formData.Add(new StringContent(review.ProductID.ToString()), "ProductID");
        formData.Add(new StringContent(review.TransactionID.ToString()), "TransactionID");
        formData.Add(new StringContent(review.OrderID.ToString()), "OrderID");
        formData.Add(new StringContent(review.Title), "Title");
        formData.Add(new StringContent(review.Content), "Content");
        formData.Add(new StringContent(review.Rating.ToString()), "Rating");

        // Add the files to the formData
        foreach (var file in review.ImagesUpload)
        {
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            formData.Add(fileContent, "ImagesUpload", file.FileName);
        }

        HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, formData);

        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductReviewResponseModel>(data); ;
        }
        return null;
    }
}
