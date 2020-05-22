using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WorkMarketWebAPI.Models
{
    public class WorkMarketApiInterface
    {
        HttpClientHelper httpClientHelper;
        static WorkMarketApiInterface apiInterface;
        public static string WorkMarketApiUrl { get; set; } = "https://www.workmarket.com/api/v1/";
        private static object thisLock = new object();
        public static bool IsUrlSet { get; set; }
        public string AccessToken { get; set; }

        private WorkMarketApiInterface()
        {
            httpClientHelper = new HttpClientHelper(WorkMarketApiUrl);
        }

        public static WorkMarketApiInterface GetInstance()
        {
            if (apiInterface == null)
                lock (thisLock)
                {
                    if (apiInterface == null)
                    {
                        apiInterface = new WorkMarketApiInterface();
                    }
                }
            return apiInterface;
        }

        public async Task<ResponseCommon> RequestAccessToken(string token, string secret)
        {
            var endPointUrl = $"authorization/request?token={token}&secret={secret}";

            return await httpClientHelper.PostAsync<ResponseCommon>(endPointUrl).ConfigureAwait(false);
        }

        public async Task<ResponseCommon> CreateAssignment(Assignment model)
        {

            string strParams = $"title={model.Title}&description={model.Description}&industry_id={model.IndustryId}&pricing_type={model.PricingType}&location_offsite={model.LocationOffsite}&location_name={model.LocationName}&location_number={model.LocationNumber}" +
                $"&location_address1={model.LocationAddress1}&location_address2={model.LocationAddress2}&location_city={model.LocationCity}&location_state={model.LocationState}&location_zip={model.LocationZip}&location_country={model.LocationCountry}&scheduled_start_date={model.ScheduledStartDate}&scheduled_end_date={model.ScheduledEndDate}&pricing_flat_price={model.PricingFlatPrice}&template_id={model.TemplateId}";

            #region Custom Fields
            int mainId = 489;
            int serviceOrderId = 2987;
            int siteCode = 2988;

            strParams += $"&custom_field_groups[0][id]={mainId}";
            strParams += $"&custom_field_groups[0][fields][0][id]={serviceOrderId}&custom_field_groups[0][fields][0][value]={(string.IsNullOrWhiteSpace(model.P_ServiceOrderCode) ? "0" : model.P_ServiceOrderCode)}";
            strParams += $"&custom_field_groups[0][fields][1][id]={siteCode}&custom_field_groups[0][fields][1][value]={(string.IsNullOrWhiteSpace(model.P_SiteCode) ? "0" : model.P_SiteCode)}";
            #endregion


            var endPointUrl = $"assignments/create?" + strParams;

            httpClientHelper.AccessToken = AccessToken;

            return await httpClientHelper.PostAsync<ResponseCommon>(endPointUrl).ConfigureAwait(false);
        }



        //public async Task<FramesDataApiResposne<FramesDataSignAgreement>> SignReadAgreement(FramesDataAuthorizationDetail framesDataAuthorizationDetail)
        //{
        //    var endPointUrl = $"signreadmeagreement?partnerid={PartnerId}&username={framesDataAuthorizationDetail.Username}";
        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataSignAgreement>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne<FramesDataQuickSearch>> QuickSearch(FramesDataQuickSearchFilter framesDataQuickSearchFilter)
        //{
        //    var endPointUrl = string.Empty;
        //    switch (framesDataQuickSearchFilter.SearchType)
        //    {
        //        case FramesDataSearchType.Quick:
        //            endPointUrl = $"quicksearch?auth={framesDataQuickSearchFilter.AuthorizationTicket}&keyword={framesDataQuickSearchFilter.Keyword}";
        //            break;

        //            //case FramesDataSearchType.Detail:
        //            //    switch (framesDataQuickSearchFilter.SearchCategory?.ToUpper())
        //            //    {
        //            //        case "MANUFACTURER":
        //            //            endPointUrl = $"manufacturers/{framesDataQuickSearchFilter.SearchValue}/styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}";
        //            //            break;
        //            //        case "BRAND":
        //            //            endPointUrl = $"brands/{framesDataQuickSearchFilter.SearchValue}/styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}";
        //            //            break;
        //            //        case "COLLECTION":
        //            //            endPointUrl = $"collections/{framesDataQuickSearchFilter.SearchValue}/styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}";
        //            //            break;
        //            //    }                   
        //            //    break;
        //            //case FramesDataSearchType.UPC:
        //            //    endPointUrl = $"styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}&upc={framesDataQuickSearchFilter.Keyword}";
        //            //    break;
        //    }


        //    if (!string.IsNullOrEmpty(framesDataQuickSearchFilter.Market))
        //        endPointUrl = endPointUrl + $"&mkt={framesDataQuickSearchFilter.Market}";

        //    if (!string.IsNullOrEmpty(framesDataQuickSearchFilter.Status))
        //        endPointUrl = endPointUrl + $"&status={framesDataQuickSearchFilter.Status}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataQuickSearch>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne_V1<FramesDataStyleConfiguration>> DetailSearch(FramesDataQuickSearchFilter framesDataQuickSearchFilter)
        //{
        //    var endPointUrl = string.Empty;
        //    switch (framesDataQuickSearchFilter.SearchType)
        //    {
        //        case FramesDataSearchType.Detail:
        //            switch (framesDataQuickSearchFilter.SearchCategory?.ToUpper())
        //            {
        //                case "MANUFACTURER":
        //                    endPointUrl = $"manufacturers/{framesDataQuickSearchFilter.SearchValue}/styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}";
        //                    break;
        //                case "BRAND":
        //                    endPointUrl = $"brands/{framesDataQuickSearchFilter.SearchValue}/styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}";
        //                    break;
        //                case "COLLECTION":
        //                    endPointUrl = $"collections/{framesDataQuickSearchFilter.SearchValue}/styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}";
        //                    break;
        //            }
        //            break;
        //        case FramesDataSearchType.UPC:
        //            endPointUrl = $"styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}&upc={framesDataQuickSearchFilter.Keyword}";
        //            break;
        //    }


        //    if (!string.IsNullOrEmpty(framesDataQuickSearchFilter.Market))
        //        endPointUrl = endPointUrl + $"&mkt={framesDataQuickSearchFilter.Market}";

        //    if (!string.IsNullOrEmpty(framesDataQuickSearchFilter.Status))
        //        endPointUrl = endPointUrl + $"&status={framesDataQuickSearchFilter.Status}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne_V1<FramesDataStyleConfiguration>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne<FramesDataStyleConfiguration>> UPCSearch(FramesDataQuickSearchFilter framesDataQuickSearchFilter)
        //{
        //    var endPointUrl = $"styleconfigurations?auth={framesDataQuickSearchFilter.AuthorizationTicket}&upc={framesDataQuickSearchFilter.Keyword}";

        //    if (!string.IsNullOrEmpty(framesDataQuickSearchFilter.Market))
        //        endPointUrl = endPointUrl + $"&mkt={framesDataQuickSearchFilter.Market}";

        //    if (!string.IsNullOrEmpty(framesDataQuickSearchFilter.Status))
        //        endPointUrl = endPointUrl + $"&status={framesDataQuickSearchFilter.Status}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataStyleConfiguration>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne<FramesDataStyleConfiguration>> GetStyleConfiguration(string auth, long styleFramesMasterID)
        //{
        //    var endPointUrl = $"styleconfigurations/{styleFramesMasterID}?auth={auth}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataStyleConfiguration>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne<FramesDataManufacturer>> GetManufacturers(string auth, int? manufacturerFramesMasterID = null, string status = "")
        //{
        //    var endPointUrl = manufacturerFramesMasterID.HasValue ? $"manufacturers/{manufacturerFramesMasterID}?auth={auth}" : $"manufacturers?auth={auth}";
        //    if (!string.IsNullOrWhiteSpace(status) && !manufacturerFramesMasterID.HasValue)
        //        endPointUrl = endPointUrl + $"&status={status}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataManufacturer>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne<FramesDataBrand>> GetBrands(string auth, int? brandFramesMasterID = null, int searchByManufacturerId = 0, string status = "")
        //{
        //    var endPointUrl = brandFramesMasterID.HasValue ? $"brands/{brandFramesMasterID}?auth={auth}" : $"brands?auth={auth}";

        //    if (searchByManufacturerId > 0)
        //        endPointUrl = $"manufacturers/{searchByManufacturerId}/brands?auth={auth}";

        //    if (!string.IsNullOrWhiteSpace(status))
        //        endPointUrl = endPointUrl + $"&status={status}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataBrand>>(endPointUrl).ConfigureAwait(false);
        //}

        //public async Task<FramesDataApiResposne<FramesDataCollection>> GetCollections(string auth, int? collectionFramesMasterID = null, FramesDataSearchBy? framesDataSearchBy = null, int searchById = 0, string status = "")
        //{
        //    var endPointUrl = collectionFramesMasterID.HasValue ? $"collections/{collectionFramesMasterID}?auth={auth}" : $"collections?auth={auth}";
        //    switch (framesDataSearchBy)
        //    {
        //        case FramesDataSearchBy.Brand:
        //            endPointUrl = $"brands/{searchById}/collections?auth={auth}";
        //            break;
        //        case FramesDataSearchBy.Manufacturer:
        //            endPointUrl = $"manufacturers/{searchById}/collections?auth={auth}";
        //            break;
        //        default:
        //            break;
        //    }

        //    if (!string.IsNullOrWhiteSpace(status))
        //        endPointUrl = endPointUrl + $"&status={status}";

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne<FramesDataCollection>>(endPointUrl).ConfigureAwait(false);
        //}

        ///// <summary>
        ///// Search frames style configurations by comma separated upcs 
        ///// </summary>
        ///// <param name="auth"></param>
        ///// <param name="upcList"></param>
        ///// <returns></returns>
        //public async Task<FramesDataApiResposne_V1<FramesDataStyleConfiguration>> GetFrameStyleConfigurationsByUPCList(string auth, string upcList)
        //{
        //    var endPointUrl = $"styleconfigurations/upclist?auth={auth}";
        //    return await httpClientHelper.PostAsync<FramesDataApiResposne_V1<FramesDataStyleConfiguration>, string>(upcList, endPointUrl).ConfigureAwait(false);
        //}

        ///// <summary>
        ///// Search dicscontinued frames style configurations
        ///// </summary>
        ///// <param name="auth"></param>
        ///// <param name="upcList"></param>
        ///// <returns></returns>
        //public async Task<FramesDataApiResposne_V1<FramesDataStyleConfiguration>> GetDiscontinuedFrameStyleConfigurations(string auth, FramesDataDicontinuedFilter filterParams)
        //{
        //    var endPointUrl = $"styleconfigurations/discontinued?auth={auth}";
        //    if (filterParams != null)
        //    {
        //        if (filterParams.StartDate.HasValue)
        //            endPointUrl = endPointUrl + $"&datestart={filterParams.StartDate.Value.ToString("yyyy-MM-dd")}";

        //        if (filterParams.EndDate.HasValue)
        //            endPointUrl = endPointUrl + $"&dateend={filterParams.EndDate.Value.ToString("yyyy-MM-dd")}";

        //        if (filterParams.ManufacturerFramesMasterID.HasValue)
        //            endPointUrl = endPointUrl + $"&mid={filterParams.ManufacturerFramesMasterID.Value}";

        //        if (filterParams.BrandFramesMasterID.HasValue)
        //            endPointUrl = endPointUrl + $"&bid={filterParams.BrandFramesMasterID.Value}";

        //        if (filterParams.CollectionFramesMasterID.HasValue)
        //            endPointUrl = endPointUrl + $"&cid={filterParams.CollectionFramesMasterID.Value}";
        //    }

        //    return await httpClientHelper.GetAsync<FramesDataApiResposne_V1<FramesDataStyleConfiguration>>(endPointUrl).ConfigureAwait(false);
        //}
    }

    public class ResponseCommon
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("errors")]
        public object[] Errors { get; set; }

        [JsonProperty("status_code")]
        public long StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("execution_time")]
        public double ExecutionTime { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }

    public class Response
    {
        [JsonProperty("successful")]
        public bool Successful { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }


        [JsonProperty("id")]
        public string Id { get; set; }
    }
}