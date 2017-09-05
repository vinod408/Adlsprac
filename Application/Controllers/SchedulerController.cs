using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.ValAutoMapper;
using ValidationDAL;
using System.Web.Script.Serialization;

namespace Application.Controllers
{

    public class SchedulerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetCalendarEvents()
        {
            ValidationMapper<Scheduler, Models.TaskViewModel> mapperObj = new ValidationMapper<Scheduler, Models.TaskViewModel>();
            var dalObj = new DALRepository();
            var testSuiteList = dalObj.GetSchedulerEvents();
            var testSuites = new List<Models.TaskViewModel>();
            if (testSuiteList.Any())
            {
                foreach (var ts in testSuiteList)
                {
                    testSuites.Add(mapperObj.Translate(ts));
                }
                var data = testSuites.ToList();

                return this.Jsonp(data);
            }
            return null;


        }
        public JsonResult UpdateCalendarEvent(Models.Event data)
        {

            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.Event, Event> mapperObj = new ValidationMapper<Models.Event, Event>();
                var dalObj = new DALRepository();
                result = dalObj.SaveEvent(mapperObj.Translate(data));
                if (result == 1)
                {
                    status = true;
                }
                return Json(new { success = status });
            }
            catch
            {
                return Json(new { success = status });
            }
        }
        public JsonResult SaveCalendarEvent(Models.Event eve)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.Event, Event> mapperObj = new ValidationMapper<Models.Event, Event>();
                var dalObj = new DALRepository();
                result = dalObj.SaveEvent(mapperObj.Translate(eve));
                if (result == 1)
                {
                    status = true;
                }
                return Json(new { success = status });
            }
            catch
            {
                return Json(new { success = status });
            }
        }
        public JsonResult DeleteCalendarEvent(Models.Event eve)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.Event, Event> mapperObj = new ValidationMapper<Models.Event, Event>();
                var dalObj = new DALRepository();
                result = dalObj.DeleteEvent(eve.TaskID);
                if (result == 1)
                {
                    status = true;
                }
                return Json(new { success = status });
            }
            catch
            {
                return Json(new { success = status });
            }
        }
        public JsonResult Read()
        {
            IList<Models.TaskViewModel> result = null;

            result = new List<Models.TaskViewModel>() { new Models.TaskViewModel
                {
                    TaskID = 1,
                    Title = "Task 1",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(1),
                    IsAllDay = true
                }
            };

            return this.Jsonp(result);
        }

        public virtual ActionResult Destroy()
        {

            {
                var tasks = this.DeserializeObject<IEnumerable<Models.TaskViewModel>>("models").FirstOrDefault();
                if (tasks != null)
                {
                    int result = 0; bool status = false;
                    try
                    {
                        ValidationMapper<Models.TaskViewModel, Scheduler> mapperObj = new ValidationMapper<Models.TaskViewModel, Scheduler>();
                        var dalObj = new DALRepository();
                        result = dalObj.DeleteSchedularEvent(tasks.TaskID);
                        if (result == 1)
                        {
                            return this.Jsonp(tasks);
                        }
                        else
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert('Error Occured!! Try again later);</script>");
                        }
                    }
                    catch
                    {
                        return Json(new { success = status });
                    }
                }

                return this.Jsonp(tasks);
            }
        }

        public virtual ActionResult Create()
        {
            var tasks = this.DeserializeObject<IEnumerable<Models.TaskViewModel>>("models").FirstOrDefault();

            if (tasks != null)
            {
                int result = 0; bool status = false;
                try
                {
                    ValidationMapper<Models.TaskViewModel, Scheduler> mapperObj = new ValidationMapper<Models.TaskViewModel, Scheduler>();
                    var dalObj = new DALRepository();
                    result = dalObj.SaveSchedularEvent(mapperObj.Translate(tasks));
                    if (result == 1)
                    {
                        return this.Jsonp(tasks);
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Error Occured');</script>");
                    }
                }
                catch
                {
                    return Json(new { success = status });
                }
            }

            return this.Jsonp(tasks);
        }

        public virtual ActionResult Update()
        {
            var tasks = this.DeserializeObject<IEnumerable<Models.TaskViewModel>>("models").FirstOrDefault();
            if (tasks != null)
            {
                int result = 0; bool status = false;
                try
                {
                    ValidationMapper<Models.TaskViewModel, Scheduler> mapperObj = new ValidationMapper<Models.TaskViewModel, Scheduler>();
                    var dalObj = new DALRepository();
                    result = dalObj.UpdateSchedularEvent(mapperObj.Translate(tasks));
                    if (result == 1)
                    {
                        return this.Jsonp(tasks);
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Error Occured');</script>");
                    }
                }
                catch
                {
                    return Json(new { success = status });
                }
            }

            return this.Jsonp(tasks);
        }
    }

    public class JsonpResult : JsonResult
    {
        public JsonpResult(string callbackName)
        {
            CallbackName = callbackName;
        }

        public JsonpResult() : this("jsoncallback")
        {
        }

        public string CallbackName { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            string jsoncallback = ((context.RouteData.Values[CallbackName] as string) ?? request[CallbackName]) ?? CallbackName;

            if (!string.IsNullOrEmpty(jsoncallback))
            {
                if (string.IsNullOrEmpty(base.ContentType))
                {
                    base.ContentType = "application/x-javascript";
                }
                response.Write(string.Format("{0}(", jsoncallback));
            }

            base.ExecuteResult(context);

            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write(")");
            }
        }
    }

    public static class ControllerExtensions
    {
        public static JsonpResult Jsonp(this Controller controller, object data, string callbackName = "callback")
        {
            return new JsonpResult(callbackName)
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public static T DeserializeObject<T>(this Controller controller, string key) where T : class
        {
            var value = controller.HttpContext.Request.QueryString.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<T>(value);
        }
    }

}