using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Yo_Yo_App.Controllers
{
    public class ErrorController : Controller
    {
        #region Declarations
        private readonly ILogger<ErrorController> _ilogger;
        #endregion

        #region Constructor
        public ErrorController(ILogger<ErrorController> ilogger)
        {
            _ilogger = ilogger;
        }
        #endregion

        #region Error
        /// <summary>
        /// Write Exceptions and redirect user to Error page. IMP-For now added the exception details.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            _ilogger.LogError($"Exception occured:ExceptionPath:{exceptionHandlerPathFeature.Path}, Message: {exceptionHandlerPathFeature.Error.Message}, StackTrace:{exceptionHandlerPathFeature.Error.StackTrace} ");
            return View("Error");
        }
        #endregion
    }
}