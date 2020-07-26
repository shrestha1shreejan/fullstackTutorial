using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace DatingApp.Helpers
{
    // custom extension class
    public static class Extensions
    {
        /// <summary>
        /// Adds response header for the error case
        /// </summary>
        /// <param name="response"></param>
        /// <param name="message"></param>
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("application-error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "application-error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        /// <summary>
        /// Adds response header for Pagination
        /// </summary>
        /// <param name="response"></param>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="totalItems"></param>
        /// <param name="totalPages"></param>
       public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
       {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theDateTime"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}
