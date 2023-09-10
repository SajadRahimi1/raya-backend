public class KavehnegarRespository : IKavehnegarRespository
{

    HttpClient _httpClient;

    public KavehnegarRespository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CustomActionResult> sendHiringNurseSms(string phoneNumber, string name)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(string.Format("https://api.kavenegar.com/v1/76526D486C52682F413330784E3575344E664B58714B5261593175776A5A56564C7A576D4A3168314C78633D/verify/lookup.json?receptor={0}&token={1}&template=hiringnurse", phoneNumber, name));        // if (response.IsSuccessStatusCode)
        // {
        return new CustomActionResult(new Result { statusCodes = 200 });
    }

    public async Task<CustomActionResult> sendLoginSms(string phoneNumber, string code)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(string.Format("https://api.kavenegar.com/v1/76526D486C52682F413330784E3575344E664B58714B5261593175776A5A56564C7A576D4A3168314C78633D/verify/lookup.json?receptor={0}&token={1}&template=verify", phoneNumber, code));
        // if (response.IsSuccessStatusCode)
        // {
        return new CustomActionResult(new Result { statusCodes = 200 });
        // }
        // return new CustomActionResult(new Result { statusCodes = ((int)response.StatusCode), ErrorMessage = new ErrorModel { ErrorMessage = await response.Content.ReadAsStringAsync() } });

    }

    public async Task<CustomActionResult> sendNurseReserveSms(string phoneNumber, string name)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(string.Format("https://api.kavenegar.com/v1/76526D486C52682F413330784E3575344E664B58714B5261593175776A5A56564C7A576D4A3168314C78633D/verify/lookup.json?receptor={0}&token={1}&template=nursereserve", phoneNumber, name));        // if (response.IsSuccessStatusCode)        // if (response.IsSuccessStatusCode)
        // {
        return new CustomActionResult(new Result { statusCodes = 200 });
    }
}