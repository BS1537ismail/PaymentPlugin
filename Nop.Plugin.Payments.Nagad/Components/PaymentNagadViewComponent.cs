using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.Nagad.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.Nagad.Components;

public class PaymentNagadViewComponent : NopViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new PaymentInfoModel()
        {
            AccountTypes = new List<SelectListItem>
            {
                new() { Text = "Nagad", Value = "Nagad" },
                new() { Text = "Bkash", Value = "Bkash" },
            }
        };

        //set postback values (we cannot access "Form" with "GET" requests)
        if (!Request.IsGetRequest())
        {
            var form = await Request.ReadFormAsync();

            model.MobileNumber = form["MobileNumber"];
            model.TransactionId = form["TransactionId"];
            model.AccountType = form["AccountType"];
        }

        return View("~/Plugins/Payments.Nagad/Views/PaymentInfo.cshtml", model);
    }
}