using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WorkMarketWebAPI.Models;

namespace WorkMarketWebAPI.Controllers
{
    public class AssignmentController : ApiController
    {
        public async Task<ResponseCommon> CreateAssignment(Assignment assignment)
        {
            /*//Sample data to pass from swagger           
             {
             "title":"test","description":"test",
             "industry_id":"1",
             "pricing_type":"flat",
             "location_offsite":"false",
             "location_name":"asdasdsad",
             "location_number":"100",
             "location_address1":"add1",
             "location_address2":"add2",
             "location_city":"pune",
             "location_state":"mh",
             "location_zip":"411048",
             "location_country":"india",
             "scheduled_start_date":"2020/04/07 10:10 PM",
             "scheduled_end_date":"2020/04/08 12:10 PM",
             "pricing_flat_price":"100",
             "template_id":"5285380806"
           }
           */
            if (assignment == null)
            {
                //for testing purpose
                assignment = new Assignment();

            }

            return await AssignmentHelper.CreateAssignment(assignment);
            //return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
