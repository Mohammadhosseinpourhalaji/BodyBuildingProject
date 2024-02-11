using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using BodyBuildingProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyBuildingProject.Controllers
{
    public class PaymentController : Controller
    {
        DBBodyBuilding db { get; }
        UserManager<ApplicationUser> userManager;
        public PaymentController(DBBodyBuilding _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

        public async Task<IActionResult> RequestPayment(int trainScheludeId)
        {
            var tshId = db.Tbl_TrainSchedule.Where(x=>x.Id== trainScheludeId).Include(x=>x.UserTrainInfo).ThenInclude(x=>x.TrainType).FirstOrDefault();
            if (tshId == null)
                return NotFound();

            System.Net.ServicePointManager.Expect100Continue = false;
            Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;
            string href = Url.Action("OnlinePayment", "Payment", new { id = tshId.Id }, "https");
            var res = await zp.PaymentRequestAsync("9d2e55e2-c95b-11e9-b012-000c295eb8fc", (tshId.UserTrainInfo.TrainType.Price / 10), tshId.UserTrainInfo.TrainType.TypeName.ToString(), "", "", href);
            if (res.Body.Status == 100)
            {
                return Redirect("https://zarinpal.com/pg/StartPay/" + res.Body.Authority);
            }
            else
            {
                //return BadRequest();
                TempData["msg"] = "بانک پاسخگو نمیباشد، لطفاً بعداً مجدداً امتحان کنید.";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();

                var tsh = db.Tbl_TrainSchedule.Where(x => x.Id == id).Include(x => x.UserTrainInfo).ThenInclude(x => x.TrainType).FirstOrDefault();
                if (tsh == null)
                    return NotFound();

                int Amout = tsh.UserTrainInfo.TrainType.Price / 10;

                System.Net.ServicePointManager.Expect100Continue = false;
                Zarinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarinpal.PaymentGatewayImplementationServicePortTypeClient();

                var status = zp.PaymentVerificationAsync("9d2e55e2-c95b-11e9-b012-000c295eb8fc", authority, Amout);
                if (status.Result.Body.Status == 100)
                {
                    tsh.isPaid = true;
                    db.Update(tsh);
                    db.SaveChanges();

                    ViewBag.code = status.Result.Body.RefID;
                    return View();
                }
            }
            return NotFound();
        }

    }
}