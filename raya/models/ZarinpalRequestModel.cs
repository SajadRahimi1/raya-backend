public class ZarinpalRequestModel
{
    public string merchant_id { get; set; }="1d3b480c-178d-4aa7-8f07-e129e9d05690";
    public string currency { get; set; }="IRT";
    public int amount { get; set; }=20000;
    public string description { get; set; }="هزینه استخدام پرستار";
    public string callback_url { get; set; }
    
}