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
    public class ValidationController : Controller
    {
        // GET: Validation
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult LandingPage()
        {
            return View();
        }
        public ActionResult RediLanding()
        {
            return Redirect("/Validation/LandingPage/?#0");
        }
        public ActionResult Dashboard2()
        {
            return View();
        }
        public ActionResult Table()
        {
            GetTestCaseTypeDetails();
            GetPipeLinestageDetails();
            return View();
        }
        public ActionResult gijgopartial()
        {

            return PartialView("_gijgo");
        }
        public ActionResult kendopartial()
        {

            return PartialView("_kendopartial");
        }
        public ActionResult Scheduler()
        {
            return View();
        }
        public ActionResult Schedulerwindow()
        {
            ViewBag.id=Request.QueryString["id"];
            ViewBag.desc = Request.QueryString["desc"];
            return View();
        }
        public ActionResult _schedulerpartial()
        {
            return View();
        }

        public JsonResult RegisterAdmin(Models.Admin admin)
        {
            int status;
            admin.UserName = admin.FullName;
            if (ModelState.IsValid)
            {
                ValidationMapper<Models.Admin, Admin> mapperObj = new ValidationMapper<Models.Admin, Admin>();
                var dalObj = new DALRepository();

                status = dalObj.RegisterAdmin(mapperObj.Translate(admin));
                if (status == 1)
                {
                    Session["userName"] = admin.UserName;
                    Session["Role"] = "Admin";
                    status = 1;
                }
                else
                {
                    status = 0;
                }

            }
            else
            {
                status = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoginAdmin(Models.Admin user)
        {
            int status=0;
            ValidationMapper<Models.Admin, Admin> mapperObj = new ValidationMapper<Models.Admin, Admin>();
            var dalObj = new DALRepository();

            var loginData = dalObj.GetAdminCredentials(mapperObj.Translate(user));
            if(loginData!=null)
            {
                Session["userName"] = loginData.UserName;
                Session["Role"] = "Admin";
                status = 1;   
            }
            return Json(status, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            string loggedOutPageUrl = "Logout.aspx";
            Response.Write("<script language='javascript'>");
            Response.Write("function ClearHistory()");
            Response.Write("{");
            Response.Write(" var backlen=history.length;");
            Response.Write(" history.go(-backlen);");
            Response.Write(" window.location.href='" + loggedOutPageUrl + "'; ");
            Response.Write("}");
            Response.Write("</script>");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1000;
            Response.CacheControl = "no-cache";
            Session["userName"] = null;
            Session.Clear();
            Session.RemoveAll();
            return Redirect("/Validation/LandingPage/?#0");
        }
        public ActionResult GetPipeLineStages()
        {
            return View();
        }
        public JsonResult GetPipeLinestageDetails()
        {
            ValidationMapper<PipelineStage, Models.PipeLineStage> mapperObj = new ValidationMapper<PipelineStage, Models.PipeLineStage>();
            var dalObj = new DALRepository();
            var pipelineStageList = dalObj.GetPipeLineStage();
            var stages = new List<Models.PipeLineStage>();
            if (pipelineStageList.Any())
            {
                foreach (var ts in pipelineStageList)
                {
                    stages.Add(mapperObj.Translate(ts));
                }
                var data = stages.ToList();
                ViewBag.pipelinelist = data;
                var pageSize = data.Count();
                return Json(new { data, pageSize }, JsonRequestBehavior.AllowGet);
            }
            return null;

        }

        public ActionResult GetPipelineStageById(int id)
        {
            ValidationMapper<PipelineStage, Models.PipeLineStage> mapperObj = new ValidationMapper<PipelineStage, Models.PipeLineStage>();
            var dalObj = new DALRepository();
            var pipelist = dalObj.GetPipelineById(id);
            var mpipelist = new List<Models.PipeLineStage>();
            if (pipelist != null)
            {
                mpipelist.Add(mapperObj.Translate(pipelist));
                return Json(mpipelist, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult SavePipeline(Models.PipeLineStage pipeline)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.PipeLineStage, PipelineStage> mapperObj = new ValidationMapper<Models.PipeLineStage, PipelineStage>();
                var dalObj = new DALRepository();
                result = dalObj.SavePipeline(mapperObj.Translate(pipeline));
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
        [HttpPost]
        public JsonResult UpdatePipeline(Models.PipeLineStage pipeline)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.PipeLineStage, PipelineStage> mapperObj = new ValidationMapper<Models.PipeLineStage, PipelineStage>();
                var dalObj = new DALRepository();
                result = dalObj.SavePipeline(mapperObj.Translate(pipeline));
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
        public JsonResult DeletePipeline(Models.PipeLineStage pipeline)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<PipelineStage, Models.PipeLineStage> mapperObj = new ValidationMapper<PipelineStage, Models.PipeLineStage>();
                var dalObj = new DALRepository();
                result = dalObj.DeletePipeline(pipeline.PipelineStageId);
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

        public ActionResult GetTestSuites1()
        {
            return View("GetTestSuites");
        }
        public JsonResult GetTestSuiteDetails()
        {
            ValidationMapper<TestSuite, Models.TestSuites> mapperObj = new ValidationMapper<TestSuite, Models.TestSuites>();
            var dalObj = new DALRepository();
            var testSuiteList = dalObj.GetTestSuites();
            var testSuites = new List<Models.TestSuites>();
            if (testSuiteList.Any())
            {
                foreach (var ts in testSuiteList)
                {
                    testSuites.Add(mapperObj.Translate(ts));
                }
                var data = testSuites.ToList();
                ViewBag.testsuiteslist = data;
                var pageSize = data.Count();
                return Json(new { data, pageSize }, JsonRequestBehavior.AllowGet);
            }
            return null;


        }
        public JsonResult jqueryGettestsuites(int? page, int? limit, string sortBy, string direction, Models.TestSuites testsuit)
        {
            ValidationMapper<TestSuite, Models.TestSuites> mapperObj = new ValidationMapper<TestSuite, Models.TestSuites>();
            var dalObj = new DALRepository();
            var testSuiteList = dalObj.GetTestSuites();
            var query = testSuiteList.Select(p => new Models.TestSuites
            {
                TestSuiteId = p.TestSuiteId,
                TestSuite1 = p.TestSuite1,
                TestSuiteDescription = p.TestSuiteDescription,
                TestSuiteOwner = p.TestSuiteOwner,
                CreatedBy = p.CreatedBy,
                CreatedDate = p.CreatedDate,
                IsActive = p.IsActive,
            });

            if (!string.IsNullOrWhiteSpace(testsuit.TestSuite1))
            {
                query = query.Where(q => q.TestSuite1.Contains(testsuit.TestSuite1));
            }

            if (testsuit.IsActive)
            {
                query = query.Where(q => q.IsActive.Equals(testsuit.IsActive));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "testsuite1":
                            query = query.OrderBy(q => q.TestSuite1);
                            break;
                        case "createddate":
                            query = query.OrderBy(q => q.CreatedDate);
                            break;
                        case "testsuiteid":
                            query = query.OrderBy(q => q.TestSuiteId);
                            break;
                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "testsuite1":
                            query = query.OrderByDescending(q => q.TestSuite1);
                            break;
                        case "createddate":
                            query = query.OrderByDescending(q => q.CreatedDate);
                            break;
                        case "testsuiteid":
                            query = query.OrderByDescending(q => q.TestSuiteId);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.TestSuiteId);
            }
            var records = new List<Models.TestSuites>();
            var total = records.Count();
            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }
            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
            //if (testSuiteList.Any())
            //{
            //    foreach (var ts in testSuiteList)
            //    {
            //        records.Add(mapperObj.Translate(ts));
            //    }
            //}
            //return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult jqueryGettestcases(int Id, int? page, int? limit, string sortBy, string direction, Models.TestCase testcas)
        {
            ValidationMapper<TestCase, Models.TestCase> mapperObj = new ValidationMapper<TestCase, Models.TestCase>();
            var dalObj = new DALRepository();
            var testSuiteList = dalObj.Gettestsuitechilds();
            var records = new List<Models.TestCase>();
            var query = testSuiteList.Select(p => new Models.TestCase
            {
                TestCaseId = p.TestCaseId,
                TestCaseTypeId = p.TestCaseTypeId,
                TestSuiteId = p.TestSuiteId,
                TestCaseName = p.TestCaseName,
                CreatedBy = p.CreatedBy,
                CreatedDate = p.CreatedDate,
                IsActive = p.IsActive,
                PipelineStageId = p.PipelineStageId,
                Description = p.Description,
                IsObsolete = p.IsObsolete,
                ObsoleteReason = p.ObsoleteReason
            });

            if (Id != 0)
            {
                query = query.Where(q => q.TestSuiteId == Id);
            }
            if (!string.IsNullOrWhiteSpace(testcas.TestCaseName))
            {
                query = query.Where(q => q.TestCaseName.Contains(testcas.TestCaseName));
            }

            if (testcas.IsActive)
            {
                query = query.Where(q => q.IsActive.Equals(testcas.IsActive));
            }
            if (testcas.IsObsolete)
            {
                query = query.Where(q => q.IsObsolete.Equals(testcas.IsObsolete));
            }
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "testcasename":
                            query = query.OrderBy(q => q.TestCaseName);
                            break;
                        case "createddate":
                            query = query.OrderBy(q => q.CreatedDate);
                            break;
                        case "testsuiteid":
                            query = query.OrderBy(q => q.TestSuiteId);
                            break;
                        case "testcasetypeid":
                            query = query.OrderBy(q => q.TestCaseTypeId);
                            break;
                        case "pipelinestageid":
                            query = query.OrderBy(q => q.PipelineStageId);
                            break;
                        case "testcaseid":
                            query = query.OrderBy(q => q.TestCaseId);
                            break;
                    }
                }
                else
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "testcasename":
                            query = query.OrderByDescending(q => q.TestCaseName);
                            break;
                        case "createddate":
                            query = query.OrderByDescending(q => q.CreatedDate);
                            break;
                        case "testsuiteid":
                            query = query.OrderByDescending(q => q.TestSuiteId);
                            break;
                        case "testcasetypeid":
                            query = query.OrderByDescending(q => q.TestCaseTypeId);
                            break;
                        case "pipelinestageid":
                            query = query.OrderByDescending(q => q.PipelineStageId);
                            break;
                        case "testcaseid":
                            query = query.OrderByDescending(q => q.TestCaseId);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderBy(q => q.TestCaseId);
            }
            var total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }
            return this.Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult kendoGettestcases()
        {

            ValidationMapper<TestCase, Models.TestCase> mapperObj = new ValidationMapper<TestCase, Models.TestCase>();
            var dalObj = new DALRepository();
            var testSuiteList = dalObj.Gettestsuitechilds();
            var records = new List<Models.TestCase>();
            if (testSuiteList.Any())
            {
                foreach (var ts in testSuiteList)
                {
                    records.Add(mapperObj.Translate(ts));
                }
            }
            var data = records.ToList();
           
            var pageSize = data.Count();
            return Json(new { data, pageSize }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult kendoUpdateGettestcases(Models.TestCase testcase)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCase, TestCase> mapperObj = new ValidationMapper<Models.TestCase, TestCase>();
                var dalObj = new DALRepository();
                result = dalObj.UpdateTestcase(mapperObj.Translate(testcase));
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
        public JsonResult kendosaveGettestcases(Models.TestCase testcase)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCase, TestCase> mapperObj = new ValidationMapper<Models.TestCase, TestCase>();
                var dalObj = new DALRepository();
                result = dalObj.SaveTestCase(mapperObj.Translate(testcase));
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
        public ActionResult GetTestSuiteById(int id)
        {
            ValidationMapper<TestSuite, Models.TestSuites> mapperObj = new ValidationMapper<TestSuite, Models.TestSuites>();
            var dalObj = new DALRepository();
            var tstSuite = dalObj.GetTestSuiteById(id);
            var testSuite = new Models.TestSuites();
            if (tstSuite != null)
            {
                testSuite = mapperObj.Translate(tstSuite);
                return Json(testSuite, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        [HttpPost]
        public JsonResult SaveTestSuite(Models.TestSuites testSuite)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestSuites, TestSuite> mapperObj = new ValidationMapper<Models.TestSuites, TestSuite>();
                var dalObj = new DALRepository();
                result = dalObj.SaveTestSuite(mapperObj.Translate(testSuite));
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
        [HttpPost]
        public JsonResult UpdateTestSuite(Models.TestSuites testSuite)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestSuites, TestSuite> mapperObj = new ValidationMapper<Models.TestSuites, TestSuite>();
                var dalObj = new DALRepository();
                result = dalObj.UpdatedTestSuite(mapperObj.Translate(testSuite));
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

        [HttpPost]
        public JsonResult DeleteTestSuite(Models.TestSuites testSuite)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<TestSuite, Models.TestSuites> mapperObj = new ValidationMapper<TestSuite, Models.TestSuites>();
                var dalObj = new DALRepository();
                result = dalObj.DeleteTestsuite(testSuite.TestSuiteId);
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

        public JsonResult GetTestCaseDetails()
        {
            ValidationMapper<TestCase, Models.TestCase> mapperObj = new ValidationMapper<TestCase, Models.TestCase>();
            var dalObj = new DALRepository();
            var testCaseList = dalObj.GetTestCases();
            var testCases = new List<Models.TestCase>();
            if (testCaseList.Any())
            {
                foreach (var ts in testCaseList)
                {
                    testCases.Add(mapperObj.Translate(ts));
                }
                return Json(testCases.ToList(), JsonRequestBehavior.AllowGet);
            }
            return null;


        }

        public ActionResult GetTestCaseById(int id)
        {
            ValidationMapper<TestCase, Models.TestCase> mapperObj = new ValidationMapper<TestCase, Models.TestCase>();
            var dalObj = new DALRepository();
            var tstCase = dalObj.GetTestCasesById(id);
            var tstCasem = new Models.TestCase();
            if (tstCase != null)
            {
                tstCasem = mapperObj.Translate(tstCase);
                return Json(tstCasem, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        [HttpPost]
        public JsonResult SaveTestCase(Models.TestCase test)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCase, TestCase> mapperObj = new ValidationMapper<Models.TestCase, TestCase>();
                var dalObj = new DALRepository();
                result = dalObj.SaveTestCase(mapperObj.Translate(test));
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
        [HttpPost]
        public JsonResult UpdateTestCase(Models.TestCase testcase)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCase, TestCase> mapperObj = new ValidationMapper<Models.TestCase, TestCase>();
                var dalObj = new DALRepository();
                result = dalObj.UpdatedTestcase(mapperObj.Translate(testcase));
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

        [HttpPost]
        public JsonResult DeleteTestCase(Models.TestCase testCase)
        {
            int result = 0; bool status = false;
            try
            {
                var dalObj = new DALRepository();
                result = dalObj.DeleteTestcase(testCase.TestCaseId);
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

        public JsonResult GetTestCaseTypeDetails()
        {
            ValidationMapper<TestCaseType, Models.TestCaseType> mapperObj = new ValidationMapper<TestCaseType, Models.TestCaseType>();
            var dalObj = new DALRepository();
            var lst = dalObj.GetTestCaseTypes();
            var typelst = new List<Models.TestCaseType>();
            if (lst.Any())
            {
                foreach (var ts in lst)
                {
                    typelst.Add(mapperObj.Translate(ts));
                }
                var data = typelst.ToList();
                ViewBag.testCasetypelist = data;
                var pageSize = data.Count();
                return Json(new { data, pageSize }, JsonRequestBehavior.AllowGet);
            }
            return null;


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
       
        public virtual ActionResult SaveEvent()
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

        public virtual ActionResult UpdateEvent()
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
        public virtual ActionResult DeleteEvent()
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



        public JsonResult GetNotificationContacts()
        {
            var list = 0;
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            //list= NC.GetEvents(notificationRegisterTime);
            //update session here for get only new added contacts (notification)
            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveRowCountParameters(Models.TestCaseParameterRowCount row)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCaseParameterRowCount, TestCaseParameterRowCount> mapperObj = new ValidationMapper<Models.TestCaseParameterRowCount, TestCaseParameterRowCount>();
                var dalObj = new DALRepository();
                result = dalObj.SaveRowCountParams(mapperObj.Translate(row));
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
        public ActionResult SavePrimaryKeyParameters(Models.TestCaseParameterPrimaryKey ppk)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCaseParameterPrimaryKey, TestCaseParameterPrimaryKey> mapperObj = new ValidationMapper<Models.TestCaseParameterPrimaryKey, TestCaseParameterPrimaryKey>();
                var dalObj = new DALRepository();
                result = dalObj.SavePrimarykeyParams(mapperObj.Translate(ppk));
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
        public ActionResult SaveFKParameters(Models.TestCaseParameterForeignKey ppk)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCaseParameterForeignKey, TestCaseParameterForeignKey> mapperObj = new ValidationMapper<Models.TestCaseParameterForeignKey, TestCaseParameterForeignKey>();
                var dalObj = new DALRepository();
                result = dalObj.SaveFKParams(mapperObj.Translate(ppk));
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
        public ActionResult SaveValueCheckParams(Models.TestCaseParameterValueCheck vc)
        {
            int result = 0; bool status = false;
            try
            {
                ValidationMapper<Models.TestCaseParameterValueCheck, TestCaseParameterValueCheck> mapperObj = new ValidationMapper<Models.TestCaseParameterValueCheck, TestCaseParameterValueCheck>();
                var dalObj = new DALRepository();
                result = dalObj.SaveValueCheckParams(mapperObj.Translate(vc));
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
        


        // GET: Validation/Details/5
        public ActionResult Details(int id)
        {
            return View();
           
        }

        // GET: Validation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Validation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Validation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Validation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Validation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Validation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
