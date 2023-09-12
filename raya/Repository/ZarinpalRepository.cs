using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestSharp;

public class ZarinpalRepository : IZarinpalRepository
{
    private readonly AppDbContext appDbContext;
    private readonly string baseUrl = "https://api.zarinpal.com/pg/v4/payment/request.json";
    private readonly string merchant_id = "1d3b480c-178d-4aa7-8f07-e129e9d05690";


    public ZarinpalRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<CustomActionResult> checkPayement(string authority)
    {
        var body = new VerifyPaymentModel
        {
            authority = authority
        };
        var client = new RestClient("https://api.zarinpal.com/pg/v4/payment/verify.json");
        var request = new RestRequest("", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddJsonBody(body);
        var requestresponse = await client.ExecuteAsync(request);

        if (requestresponse.IsSuccessStatusCode)
        {
            JObject response = JObject.Parse(requestresponse.Content);
            if (response["data"].ToString() != "[]")
            {


                if (response["data"]["code"].ToString() == "100" || response["data"]["code"].ToString() == "101")
                {

                    return new CustomActionResult(new Result { });
                }
                else
                {
                    return new CustomActionResult(new Result { statusCodes = StatusCodes.Status400BadRequest, ErrorMessage = new ErrorModel { ErrorMessage = response["data"]["message"].ToString() } });

                }


            }
        }
        return new CustomActionResult(new Result { statusCodes = StatusCodes.Status400BadRequest, ErrorMessage = new ErrorModel { ErrorMessage = requestresponse.Content } });
    }

    public async Task<CustomActionResult> payCourse(string classCatgoryId, string userId, bool IsInstallment)
    {
        throw new NotImplementedException();

    }

    public async Task<CustomActionResult> payHiringNurse(string nurseId)
    {
        var nurseModel = await appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id.ToString() == nurseId);
        if (nurseModel == null)
        {
            return new CustomActionResult(new Result { statusCodes = 404, ErrorMessage = new ErrorModel { ErrorMessage = "پرستاری با این ایدی یافت نشد" } });
        }
        var body = new ZarinpalRequestModel
        {
            callback_url = "http://185.110.188.141/uploads/" + nurseModel.pdfLink
        };
        var client = new RestClient(baseUrl);
        var request = new RestRequest("", Method.Post);
        request.AddHeader("accept", "application/json");

        request.AddHeader("content-type", "application/json");
        request.AddJsonBody(body);
        var requestresponse = await client.ExecuteAsync(request);

        if (requestresponse.IsSuccessStatusCode)
        {
            JObject response = JObject.Parse(requestresponse.Content);
            if (response["data"].ToString() != "[]")
            {


                var authority = response["data"]["authority"].ToString();

                return new CustomActionResult(new Result { Data = "https://www.zarinpal.com/pg/StartPay/" + authority });

            }
        }
        return new CustomActionResult(new Result { statusCodes = StatusCodes.Status400BadRequest, ErrorMessage = new ErrorModel { ErrorMessage = requestresponse.Content } });

    }
}