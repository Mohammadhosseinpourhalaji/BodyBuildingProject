using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using BodyBuildingProject.Models;
using BodyBuildingProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyBuildingProject.Controllers
{
    [Authorize(Policy = "PolicyTrainers")]
    public class AdminController : Controller
    {
        DBBodyBuilding db { get; }
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public AdminController(DBBodyBuilding _db, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewTrainRequestList()
        {
            ViewData["RequestList"] = db.Tbl_TrainSchedule.Where(x => x.Status == false).Include(x => x.UserTrainInfo).ThenInclude(x=>x.TrainType).Include(x => x.User).ToList();
            return View();
        }

        public IActionResult TrainRequestList()
        {
            ViewData["RequestList"] = db.Tbl_TrainSchedule.Include(x => x.UserTrainInfo).ThenInclude(x=>x.TrainType).Include(x => x.User).ToList();
            return View("~/Views/Admin/NewTrainRequestList.cshtml");
        }

        [Route("{controller}/{action}/{trainId}")]
        public IActionResult TrainSetSchedule(int trainId)
        {
            var trainapp = db.Tbl_TrainSchedule.Where(x => x.Id == trainId).FirstOrDefault();
            if (trainapp == null)
                return NotFound();

            ViewData["RequestApp"] = db.Tbl_TrainSchedule.Where(x => x.Id == trainId).Include(x => x.UserTrainInfo).Include(x => x.User).FirstOrDefault();
            ViewData["UserTrainScheduleItems"] = db.Tbl_UserTrainSchedule.Where(x => x.TrainScheduleId == trainId).Include(x => x.DaysOfWeek).OrderBy(x => x.DayId).ToList();
            ViewData["Days"] = db.Tbl_DaysOfWeek.ToList();
            return View();
        }

        public IActionResult AddSchedulePart()
        {
            //ViewData["Days"] = db.Tbl_DaysOfWeek.ToList();
            return PartialView();
        }

        public IActionResult ConfirmAddTrainSchedule(UserTrainScheduleViewModel model, int scheduleId)
        {
            var count = model.TrainText.Count;
            List<UserTrainSchedule> userTrainSch = new List<UserTrainSchedule>();
            for (int i = 0; i < count; i++)
            {
                if (model.DayId[i] == 0 || string.IsNullOrWhiteSpace(model.TrainText[i]))
                    continue;

                UserTrainSchedule sch = new UserTrainSchedule
                {
                    TrainText = model.TrainText[i],
                    Time = model.Time[i],
                    DayId = model.DayId[i]
                };
                userTrainSch.Add(sch);
            }

            try
            {
                foreach (var item in userTrainSch)
                {
                    UserTrainSchedule tr = new UserTrainSchedule
                    {
                        TrainScheduleId = scheduleId,
                        TrainText = item.TrainText,
                        Time = item.Time,
                        DayId = item.DayId
                    };

                    db.Tbl_UserTrainSchedule.Add(tr);
                    db.SaveChanges();
                }

                var schedule = db.Find<TrainSchedule>(scheduleId);
                if (schedule.Status == false)
                {
                    schedule.Status = true;
                    db.SaveChanges();
                }

                //Message Successfully
                return Redirect("/Admin/TrainSetSchedule/" + scheduleId);
            }
            catch
            {
                //Message
                return null;
            }
        }

        [Route("{controller}/{action}/{scheduleId}/{trainScheduleId}")]
        public IActionResult ConfirmDeleteUserTrainSchedule(int trainScheduleId, int scheduleId)
        {
            var trainSchedule = db.Tbl_UserTrainSchedule.Where(x => x.Id == trainScheduleId).FirstOrDefault();
            if (trainSchedule == null)
                return NotFound();

            try
            {
                db.Tbl_UserTrainSchedule.Remove(trainSchedule);
                db.SaveChanges();
                return Redirect("/Admin/TrainSetSchedule/" + scheduleId);
            }
            catch
            {
                return NotFound();
            }

        }

        [Authorize(Policy = "PolicyAdmins")]
        public IActionResult ScheduleTypePriceManagement()
        {
            ViewData["TrainTypes"] = db.Tbl_TrainType.ToList();
            return View();
        }

        [Authorize(Policy = "PolicyAdmins")]
        public IActionResult EditTrainType(int trainTypeId)
        {
            var trainType = db.Find<TrainType>(trainTypeId);
            if (trainType == null)
                return NotFound();

            ViewData["TrainTypeSingle"] = trainType;
            return View();
        }

        public IActionResult AddNewTrainType(TrainTypeViewModel model)
        {
            try
            {
                var traintype = new TrainType()
                {
                    TypeName = model.TypeName,
                    Price = model.Price
                };

                db.Tbl_TrainType.Add(traintype);
                db.SaveChanges();

                //Message OK
                return RedirectToAction(nameof(ScheduleTypePriceManagement));
            }
            catch
            {
                //Error
                return RedirectToAction(nameof(ScheduleTypePriceManagement));
            }
        }

        [Route("{controller}/{action}/{trainTypeId}")]
        public IActionResult ConfirmEditTrainType(int trainTypeId, TrainTypeViewModel model)
        {
            var trainType = db.Find<TrainType>(trainTypeId);
            if (trainType == null)
                return NotFound();

            try
            {
                trainType.TypeName = model.TypeName;
                trainType.Price = model.Price;

                db.Update(trainType);
                db.SaveChanges();

                //Message OK
                return RedirectToAction(nameof(ScheduleTypePriceManagement));
            }
            catch
            {
                //Error
                return RedirectToAction(nameof(EditTrainType));
            }
        }
    }
}