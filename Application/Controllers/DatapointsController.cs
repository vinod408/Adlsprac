using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using ValidationDAL;
using Application.ValAutoMapper;
using System.Text;
using System.IO;

namespace Application.Controllers
{
    public class DatapointsController : Controller
    {
       
        public ActionResult Error()
        {
            return PartialView("_Error");
        }

        public ActionResult Charts()
        {

            string newFileName = "D:\\application till register\\Application\\Application\\Content\\Chart.TSV";


            if (System.IO.File.Exists(newFileName))
            {
                System.IO.File.Delete(newFileName);
            }

            int thisMonth = DateTime.Today.Month;
            //int thisMonth = 5;
            int thisYear = DateTime.Today.Year;

            DateTime startDate = new DateTime(2016, 01, 01, 0, 0, 0);
            DateTime endDate = new DateTime(2016, 01, 01, 0, 0, 0);
            int[] MonthArrayInt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] MonthArraydays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((thisYear % 4 == 0 && thisYear % 100 != 0) || (thisYear % 400 == 0))
            {
                MonthArraydays[1] = 29;
            }
            double EfficiencyForPassedTestCases = 0.0;

            for (int i = 0; i < 12; i++)
            {
                if (thisMonth == MonthArrayInt[i])
                {
                    startDate = new DateTime(thisYear, MonthArrayInt[i], 01, 0, 0, 0);
                    endDate = new DateTime(thisYear, MonthArrayInt[i], MonthArraydays[i], 0, 0, 0);
                }
            }
            var dalObj = new DALRepository();
            var TestCaselogList = dalObj.GetTestCaseLogentries(startDate, endDate);

            var datesList = TestCaselogList.Select(d => (Convert.ToDateTime(d.CreatedDate)).Date).Distinct();
            try
            {
                if (datesList.Any())
                {
                    foreach (var ts in datesList)
                    {
                        int PassedTestCasescount = 0;
                        int TestCaseCount = 0;

                        if (ts >= startDate.Date & ts <= endDate.Date)
                        {
                            int day = ts.Day;
                            foreach (var tsl in TestCaselogList)
                            {
                                if (ts == (Convert.ToDateTime(tsl.CreatedDate)).Date)
                                {
                                    if (tsl.ResultType == 1)
                                    {
                                        PassedTestCasescount += 1;
                                    }
                                    if (tsl.ResultType != 3 & tsl.ResultType != null)
                                    {
                                        TestCaseCount += 1;
                                    }
                                }

                            }
                            EfficiencyForPassedTestCases = ((PassedTestCasescount * 100.00) / TestCaseCount);
                            EfficiencyForPassedTestCases = Math.Round(EfficiencyForPassedTestCases, 2);

                            /****************Insering data into csv file*************************/

                            string clientDetails = day + "\t" + EfficiencyForPassedTestCases + Environment.NewLine;
                            if (!System.IO.File.Exists(newFileName))
                            {
                                string clientHeader = "day" + "\t" + "efficiency" + Environment.NewLine;
                                System.IO.File.WriteAllText(newFileName, clientHeader);
                            }
                            System.IO.File.AppendAllText(newFileName, clientDetails);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");
            }
           
            return View();
        }

        public ActionResult Chartspartail()
        {
            return PartialView("_Charts");
        }

        //testcase pipeline stages report for Google charts
        public JsonResult PipelineStageTestcaseDetails()
        {
           ValidationMapper<TestCase, Models.TestCase> mapperObj =new ValidationMapper<TestCase, Models.TestCase>();
           ValidationMapper<PipelineStage, Models.PipeLineStage> mapperObjForPipelineStage = new ValidationMapper<PipelineStage, Models.PipeLineStage>();

            var dalObj = new DALRepository();
            var TestCaseListList = dalObj.GetTestCaseDetails();
            var PipelineList = dalObj.GetPipeLineStage();

            var testcaseTableRows = new List<Models.TestCase>();
            if (TestCaseListList.Any())
            {
                foreach (var ts in TestCaseListList)
                {
                    testcaseTableRows.Add(mapperObj.Translate(ts));
                }

            }
            List<Models.TwoArgumentStringInt> dataPoint = new List<Models.TwoArgumentStringInt>();

            foreach (var onepipe in PipelineList)
            {
                var count = 0;
                foreach (var onetest in TestCaseListList)
                {
                    if (onepipe.PipelineStageId == onetest.PipelineStageId)
                    {
                        count += 1;
                    }

                }
                dataPoint.Add(new Models.TwoArgumentStringInt(onepipe.PipelineStage1, count));
            }
            return Json(dataPoint, JsonRequestBehavior.AllowGet);
            
        }

        //Google charts month efficiency
        public ActionResult GetThisMonthEfficiency()
        {
            int thisMonth = DateTime.Today.Month;
            //int thisMonth = 5;
            int thisYear = DateTime.Today.Year;
            
            DateTime startDate = new DateTime(2016, 01, 01, 0, 0, 0);
            DateTime endDate = new DateTime(2016, 01, 01, 0, 0, 0);
            int[] MonthArrayInt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] MonthArraydays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((thisYear % 4 == 0 && thisYear % 100 != 0) || (thisYear % 400 == 0))
            {
                MonthArraydays[1] = 29;
            }
           
            double EfficiencyForPassedTestCases = 0.0;
            // int NumberOfTestCasesFired = 0;

            for (int i = 0; i < 12; i++)
            {
                if (thisMonth == MonthArrayInt[i])
                {
                    startDate = new DateTime(thisYear, MonthArrayInt[i], 01, 0, 0, 0);
                    endDate = new DateTime(thisYear, MonthArrayInt[i], MonthArraydays[i], 0, 0, 0);
                }
            }

           ValidationMapper<TestCaseLog, Models.TestCaseLog> mapperObj = new ValidationMapper<TestCaseLog, Models.TestCaseLog>();

            var dalObj = new DALRepository();
            var TestCaselogList = dalObj.GetTestCaseLogentries(startDate, endDate);

            //var testCaseList = new List<Models.TestCaseLog>();
            List<Models.TwoArgumentDataPoint> dataPointsToSend = new List<Models.TwoArgumentDataPoint>();

            var datesList = TestCaselogList.Select(d => (Convert.ToDateTime(d.CreatedDate)).Date).Distinct();
            try
            {

                if (datesList.Any())
                {
                    foreach (var ts in datesList)
                    {
                        int PassedTestCasescount = 0;
                        int TestCaseCount = 0;

                        if (ts >= startDate.Date & ts <= endDate.Date)
                        {
                            int day = ts.Day;
                            foreach (var tsl in TestCaselogList)
                            {
                                if (ts == (Convert.ToDateTime(tsl.CreatedDate)).Date)
                                {
                                    if (tsl.ResultType == 1)
                                    {
                                        PassedTestCasescount += 1;
                                    }
                                    if (tsl.ResultType != 3 & tsl.ResultType != null)
                                    {
                                        TestCaseCount += 1;
                                    }
                                }

                            }
                            EfficiencyForPassedTestCases = ((PassedTestCasescount * 100.00) / TestCaseCount);
                            EfficiencyForPassedTestCases = Math.Round(EfficiencyForPassedTestCases, 2);
                            dataPointsToSend.Add(new Models.TwoArgumentDataPoint(day, EfficiencyForPassedTestCases));
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPointsToSend);
            return Json(dataPointsToSend , JsonRequestBehavior.AllowGet);           
        }

        //used in googlecharts dashboard
        public ActionResult GetTestCasesForToday()
        {
            int today = DateTime.Today.Day;
           ValidationMapper<TestCaseLog, Models.TestCaseLog> mapperObj = new ValidationMapper<TestCaseLog, Models.TestCaseLog>();
            var dalObj = new DALRepository();
            var TestCaselogList_today = dalObj.GetTestCaseLogentries_Today();

            List<Models.TwoArgumentDataPoint> dataPointsForSkipped = new List<Models.TwoArgumentDataPoint>();
            List<Models.TwoArgumentDataPoint> dataPointsForPassed = new List<Models.TwoArgumentDataPoint>();
            List<Models.TwoArgumentDataPoint> dataPointsForFailed = new List<Models.TwoArgumentDataPoint>();

            int Skippedcount = 0;
            int passedCount = 0;
            int failedCount = 0;
            try
            {


                if (TestCaselogList_today.Any())
                {
                    foreach (var item in TestCaselogList_today)
                    {
                        if (item.ResultType == 1)
                        {
                            passedCount += 1;
                        }
                        if (item.ResultType == 2)
                        {
                            failedCount += 1;
                        }
                        if (item.ResultType == 3)
                        {
                            Skippedcount += 1;
                        }
                    }
                    dataPointsForSkipped.Add(new Models.TwoArgumentDataPoint(today, Skippedcount));
                    dataPointsForPassed.Add(new Models.TwoArgumentDataPoint(today, passedCount));
                    dataPointsForFailed.Add(new Models.TwoArgumentDataPoint(today, failedCount));
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
            ViewBag.DataPointsSkipped = JsonConvert.SerializeObject(dataPointsForSkipped);
            ViewBag.DataPointsPassed = JsonConvert.SerializeObject(dataPointsForPassed);
            ViewBag.DataPointsFailed = JsonConvert.SerializeObject(dataPointsForFailed);
            return Json(new { dataPointsForSkipped, dataPointsForPassed, dataPointsForFailed }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLastMonthEfficiency()
        {
            int LastMonth = 0;
            int thisYear = 0;
            //int thisMonth = 5;
            if (DateTime.Today.Month==1)
            {
                LastMonth = 12;
                thisYear = DateTime.Today.Year - 1;
            }
            else
            {
                LastMonth = DateTime.Today.Month - 1;
                thisYear = DateTime.Today.Year;
            }
            
            DateTime startDate = new DateTime(2016, 01, 01, 0, 0, 0);
            DateTime endDate = new DateTime(2016, 01, 01, 0, 0, 0);
            int[] MonthArrayInt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] MonthArraydays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((thisYear % 4 == 0 && thisYear % 100 != 0) || (thisYear % 400 == 0))
            {
                MonthArraydays[1] = 29;
            }

            double EfficiencyForPassedTestCases = 0.0;
            // int NumberOfTestCasesFired = 0;

            for (int i = 0; i < 12; i++)
            {
                if (LastMonth == MonthArrayInt[i])
                {
                    startDate = new DateTime(thisYear, MonthArrayInt[i], 01, 0, 0, 0);
                    endDate = new DateTime(thisYear, MonthArrayInt[i], MonthArraydays[i], 0, 0, 0);
                }
            }

           ValidationMapper<TestCaseLog, Models.TestCaseLog> mapperObj = new ValidationMapper<TestCaseLog, Models.TestCaseLog>();

            var dalObj = new DALRepository();
            var TestCaselogList = dalObj.GetTestCaseLogentries(startDate, endDate);

            //var testCaseList = new List<Models.TestCaseLog>();
            List<Models.TwoArgumentDataPoint> dataPointsToSend = new List<Models.TwoArgumentDataPoint>();

            var datesList = TestCaselogList.Select(d => (Convert.ToDateTime(d.CreatedDate)).Date).Distinct();
            try
            {

                if (datesList.Any())
                {
                    foreach (var ts in datesList)
                    {
                        int PassedTestCasescount = 0;
                        int TestCaseCount = 0;

                        if (ts >= startDate.Date & ts <= endDate.Date)
                        {
                            int day = ts.Day;
                            foreach (var tsl in TestCaselogList)
                            {
                                if (ts == (Convert.ToDateTime(tsl.CreatedDate)).Date)
                                {
                                    if (tsl.ResultType == 1)
                                    {
                                        PassedTestCasescount += 1;
                                    }
                                    if (tsl.ResultType != 3 & tsl.ResultType != null)
                                    {
                                        TestCaseCount += 1;
                                    }
                                }

                            }
                            EfficiencyForPassedTestCases = ((PassedTestCasescount * 100.00) / TestCaseCount);
                            EfficiencyForPassedTestCases = Math.Round(EfficiencyForPassedTestCases, 2);
                            dataPointsToSend.Add(new Models.TwoArgumentDataPoint(day, EfficiencyForPassedTestCases));
                        }
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPointsToSend);
            return Json(dataPointsToSend, JsonRequestBehavior.AllowGet);
        }
    }
}
   