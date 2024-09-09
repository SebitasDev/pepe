using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RiwiTalent.Utils.Exceptions
{
    public class StatusError
    {
        //404
        public static ProblemDetails CreateNotFound()
        {
            return new ProblemDetails
            {
                Title = "Error 404 - Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = "The content you are trying to access is not available. Please check the URL to ensure it is correct."
            };
        }

        //400
        public static ProblemDetails CreateBadRequest()
        {
            return new ProblemDetails
            {
                Title = "Error 400 - Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = "The request you are trying to make is not valid. Please check the data sent and try again."
            };
        }


        //500
        public static ProblemDetails CreateInternalServerError()
        {
            return new ProblemDetails
            {
                Title = "Error 500 - Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An unexpected error occurred on the server. Please try again later or contact support if the problem persists."
            };
        }
    }
}
