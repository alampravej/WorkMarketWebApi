using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WorkMarketWebAPI.Models
{
    public class AssignmentHelper
    {
        public static async Task<ResponseCommon> CreateAssignment(Assignment assignmentModel )
        {
            AccessTokenHelper accessTokenHelper = new AccessTokenHelper();

            WorkMarketApiInterface workMarketApiInterface = WorkMarketApiInterface.GetInstance();
            workMarketApiInterface.AccessToken = await accessTokenHelper.GetToken();

            ResponseCommon responseCommon = await workMarketApiInterface.CreateAssignment(assignmentModel);

            //Handle error if access token is expired

            return responseCommon;
        }
    }


    public partial class Assignment
    {
        [JsonProperty("title")]
        [JsonRequired]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("industry_id")]
        [JsonRequired]
        public long IndustryId { get; set; }

        [JsonProperty("pricing_type")]
        [JsonRequired]
        public string PricingType { get; set; }

        [JsonProperty("location_offsite")]
        public bool LocationOffsite { get; set; }

        [JsonProperty("location_name")]
        public string LocationName { get; set; }

        [JsonProperty("location_number")]
        public string LocationNumber { get; set; }

        [JsonProperty("location_address1")]
        public string LocationAddress1 { get; set; }

        [JsonProperty("location_address2")]
        public string LocationAddress2 { get; set; }

        [JsonProperty("location_city")]
        public string LocationCity { get; set; }

        [JsonProperty("location_state")]
        public string LocationState { get; set; }

        [JsonProperty("location_zip")]
        public string LocationZip { get; set; }

        [JsonProperty("location_country")]
        public string LocationCountry { get; set; }

        /// <summary>
        /// Start time of an assignment in yyyy/MM/dd hh:mm a z or MM/dd/yyyy hh:mm a z format. 
        /// </summary>
        [JsonProperty("scheduled_start_date")]
        public string ScheduledStartDate { get; set; }

        /// <summary>
        /// Start time of an assignment in yyyy/MM/dd hh:mm a z or MM/dd/yyyy hh:mm a z format. 
        /// </summary>
        [JsonProperty("scheduled_end_date")]
        public string ScheduledEndDate { get; set; }

        [JsonProperty("pricing_flat_price")]
        public float PricingFlatPrice { get; set; }

        [JsonProperty("template_id")]
        public long TemplateId { get; set; }
    }
}