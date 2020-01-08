using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Documents;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Operations.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xappido.Authentication;
using xappido.Authentication.Otp;
using xappido.Authentication.Otp.Operations;
using xappido.Authorization;
using xappido.Mvc.Attributes;
using xappido.Operations;
using xappido.Output.Pdf.Operations;
using xappido.Output.Template;
using xappido.Output.Template.Operations;

namespace aiala.Backend.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Pictures")]
    public class DocumentsController : ControllerBase
    {
        private readonly IOperationExecutor _executor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DocumentsController(IOperationExecutor executor, IHostingEnvironment hostingEnvironment)
        {
            _executor = executor;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(AuthenticationSchemes = AuthSchemes.Otp)]
        [HttpGet("day")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetDay()
        {
            var otpContext = OtpContext.FromPrincipal(User);

            IOperationResult result = null;
            if (otpContext.TryParse<DayOtpModel>(out var model))
            {
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = model.Culture.ToCultureInfo();

                result = await _executor
                    .Add<GetDaysOperation, GetDaysOperation.Input>(() => new GetDaysOperation.Input(model.Date, model.Date, User.GetTenantId()))
                    .Add<ParseTemplateOperation, List<Day>, ParseTemplateOperation.Input>(days => new ParseTemplateOperation.Input
                    {
                        Model = days.FirstOrDefault(),
                        TemplateKey = "Day"
                    })
                    .Add<HtmlToPdfOperation, ParseTemplateOperation.Output, HtmlToPdfOperation.Input>(last => new HtmlToPdfOperation.Input(last.Result))
                    .Execute();
            }

            if (result?.ResultType == OperationResultType.Succeeded)
            {
                return new FileContentResult(result.GetResult<HtmlToPdfOperation.Output>().Pdf, "application/pdf");
            }
            else
            {
                string htmlResult;
                if (_hostingEnvironment.IsDevelopment())
                {
                    htmlResult = result.Error?.ToString();
                }
                else
                {
                    htmlResult = "Error";
                }
                return new OkObjectResult(htmlResult);
            }
        }
    }
}
