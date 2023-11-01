using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> checkPayement(string authority, string id)
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
                    var nurse = await appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id.ToString() == id);
                    return new RedirectResult("http://185.110.188.141/uploads/success.html?link=" + nurse.pdfLink);
                }
                else
                {
                    return new RedirectResult("http://185.110.188.141/uploads/error.html");

                }


            }
        }
        return new RedirectResult("http://185.110.188.141/uploads/error.html");

    }

    public async Task<CustomActionResult> payCourse(ReserveClass reserveClass)
    {
        var classCategory = await appDbContext.ClassCategories.SingleOrDefaultAsync(_ => _.Id == reserveClass.ClassCategoryId);
        int amount = int.Parse(reserveClass.IsInstallment ? classCategory.InstallmentPrice.Replace(",", null) : classCategory.PrePaid.Replace(",", null));
        var body = new ZarinpalRequestModel
        {
            callback_url = "asia://salamat",
            amount = amount,

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

                reserveClass.authority = authority;
                await appDbContext.ReserveClasses.AddAsync(reserveClass);
                await appDbContext.SaveChangesAsync();

                return new CustomActionResult(new Result { Data = "https://www.zarinpal.com/pg/StartPay/" + authority });

            }
        }
        return new CustomActionResult(new Result { statusCodes = StatusCodes.Status400BadRequest, ErrorMessage = new ErrorModel { ErrorMessage = requestresponse.Content } });

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
            callback_url = string.Format("http://185.110.188.141/Nurse/verify?id={0}", nurseId),
            description = string.Format("هزینه استخدام پرستار ${0}", nurseModel.Name)
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
                nurseModel.authority = authority;
                appDbContext.Nurses.Update(nurseModel);
                await appDbContext.SaveChangesAsync();

                return new CustomActionResult(new Result { Data = "https://www.zarinpal.com/pg/StartPay/" + authority });

            }
        }
        return new CustomActionResult(new Result { statusCodes = StatusCodes.Status400BadRequest, ErrorMessage = new ErrorModel { ErrorMessage = requestresponse.Content } });

    }

    public async Task<CustomActionResult> checkPayementApi(string authority, string id)
    {
        var nurseModel = await appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        if (nurseModel == null)
        {
            return new CustomActionResult(new Result { statusCodes = 404, ErrorMessage = new ErrorModel { ErrorMessage = "پرستار یافت نشد" } });
        }
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
                    var nurse = await appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id.ToString() == id);
                    if (nurseModel.authority == null)
                    {
                        nurseModel.authority = authority;
                        appDbContext.Nurses.Update(nurseModel);
                        await appDbContext.SaveChangesAsync();
                    }
                    return new CustomActionResult(new Result { Data = response });
                }
                else
                {
                    return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = response }, statusCodes = StatusCodes.Status400BadRequest });

                }


            }
        }
        return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = requestresponse.Content }, statusCodes = StatusCodes.Status400BadRequest });

    }

        public async Task<CustomActionResult> checkNursePayement(string id)
    {
        Nurse? nurseModel = await appDbContext.Nurses.SingleOrDefaultAsync(_ => _.Id.ToString() == id);
        if (nurseModel == null)
        {
            return new CustomActionResult(new Result { statusCodes = 404, ErrorMessage = new ErrorModel { ErrorMessage = "پرستار یافت نشد" } });
        }
        
        var body = new VerifyPaymentModel
        {
            authority = nurseModel.authority
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
                    
                    return new CustomActionResult(new Result { Data = response });
                }
                else
                {
                    return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = response }, statusCodes = StatusCodes.Status400BadRequest });

                }


            }
        }
        return new CustomActionResult(new Result { ErrorMessage = new ErrorModel { ErrorMessage = requestresponse.Content }, statusCodes = StatusCodes.Status400BadRequest });

    }

}