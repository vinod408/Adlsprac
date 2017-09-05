using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationDAL
{
    public class DALRepository
    {
        private ValidationDBContext context { get; set; }
        public DALRepository()
        {
            context = new ValidationDBContext();
        }
        public int RegisterAdmin(Admin admin)
        {
            try
            {
                var  user = new Admin();
                user = (from c in context.Admins where c.UserEmail == admin.UserEmail select c).FirstOrDefault<Admin>();
                
                if (user == null)
                {
                    var retval = context.usp_AdminAdd(admin.FullName, admin.FullName, admin.UserEmail, admin.Password);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        public Admin GetAdminCredentials(Admin userObj)
        {
            try
            {
                var user = new Admin();
                user = (from c in context.Admins where c.UserEmail == userObj.UserEmail & c.Password == userObj.Password select c).FirstOrDefault<Admin>();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public List<PipelineStage> GetPipeLineStage()
        {

            try
            {
                var lst = new List<PipelineStage>();
                lst = (from c in context.PipelineStages select c).ToList<PipelineStage>();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public PipelineStage GetPipelineById(int id)
        {

            try
            {
                var pipe = new PipelineStage();
                pipe = (from c in context.PipelineStages where c.PipelineStageId == id select c).FirstOrDefault<PipelineStage>();
                return pipe;
            }
            catch
            {
                return null;
            }
        }
        public int SavePipeline(PipelineStage pipe)
        {
            try
            {
                var lastid = (from c in context.PipelineStages select c.PipelineStageId).Max();
                var pipeline = new PipelineStage();
                pipeline.PipelineStageId = lastid + 1;
                pipeline.PipelineStage1 = pipe.PipelineStage1;
                pipeline.PipelineStageDescription = pipe.PipelineStageDescription;
                pipeline.CreatedDate = DateTime.Now;
                pipeline.CreatedBy = pipe.CreatedBy;
                context.PipelineStages.Add(pipeline);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int UpdatedPipeline(PipelineStage pipeline)
        {
            try
            {
                var pipe = (from c in context.PipelineStages where c.PipelineStageId == pipeline.PipelineStageId select c).FirstOrDefault<PipelineStage>();
                pipe.PipelineStage1 = pipeline.PipelineStage1;
                pipe.PipelineStageDescription = pipeline.PipelineStageDescription;
                pipe.CreatedBy = pipeline.CreatedBy;
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }
        public int DeletePipeline(int id)
        {
            try
            {
                var deletepipe = (from c in context.PipelineStages where c.PipelineStageId == id select c).FirstOrDefault<PipelineStage>();
                context.PipelineStages.Remove(deletepipe);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }
        public List<TestSuite> GetTestSuites()
        {

            try
            {
                var lst = new List<TestSuite>();
                lst = (from c in context.TestSuites select c).ToList<TestSuite>();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public List<TestSuite> GetTestSuites(int pageNumber, int pageSize)
        {

            try
            {
                var lst = new List<TestSuite>();
                lst = context.TestSuites.OrderBy(t => t.TestSuiteId).Skip(pageNumber).Take(pageSize).ToList<TestSuite>();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public List<TestSuite> jqueryGetTestSuites(int? page, int? limit, string sortBy, string direction, string TestSuite1)
        {
            var records = new List<TestSuite>();
            try
            {
                var query = context.TestSuites.Select(p => p);

                if (!string.IsNullOrWhiteSpace(TestSuite1))
                {
                    query = query.Where(q => q.TestSuite1.Contains(TestSuite1));
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
                            case "createdby":
                                query = query.OrderBy(q => q.CreatedBy);
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
                            case "createdby":
                                query = query.OrderByDescending(q => q.CreatedBy);
                                break;

                        }
                    }
                }

                var total = query.Count();
                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = query.OrderBy(p => p).Skip(start).Take(limit.Value).ToList();
                }
                else
                {
                    records = query.ToList();
                }

                return records;
            }
            catch
            {
                return null;
            }
        }
        public List<TestCase> Gettestsuitechilds(int TestsuiteId, int? page, int? limit)
        {
            List<TestCase> records;
            int total;

            var query = context.TestCases.Where(tc => tc.TestSuiteId == TestsuiteId).Select(tc => tc);

            total = query.Count();
            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = query.OrderBy(pt => pt.TestCaseId).Skip(start).Take(limit.Value).ToList();
            }
            else
            {
                records = query.ToList();
            }
            return records;
        }
        public List<TestCase> Gettestsuitechilds()
        {
            List<TestCase> records;

            var query = context.TestCases.Select(tc => tc);

            return records = query.ToList<TestCase>();
        }
        public int UpdatedTestcase(TestCase testcase)
        {
            try
            {
                var tcase = (from c in context.TestCases where c.TestCaseId == testcase.TestCaseId select c).FirstOrDefault<TestCase>();
                tcase.TestCaseName = testcase.TestCaseName;
                tcase.TestSuiteId = testcase.TestSuiteId;
                tcase.TestCaseTypeId = testcase.TestCaseTypeId;
                tcase.PipelineStageId = testcase.PipelineStageId;
                testcase.IsActive = testcase.IsActive;
                tcase.IsObsolete = testcase.IsObsolete;
                tcase.ObsoleteReason = testcase.ObsoleteReason;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int UpdateTestcase(TestCase TestcaseObj)
        {
            try
            {
                var testcasetemp = (from c in context.TestCases where c.TestCaseId == TestcaseObj.TestCaseId select c).FirstOrDefault<TestCase>();
                testcasetemp.TestCaseName = TestcaseObj.TestCaseName;
                testcasetemp.IsActive = TestcaseObj.IsActive;
                testcasetemp.TestCaseTypeId = TestcaseObj.TestCaseTypeId;
                testcasetemp.PipelineStageId = TestcaseObj.PipelineStageId;
                testcasetemp.TestSuiteId = TestcaseObj.TestSuiteId;
                testcasetemp.IsObsolete = TestcaseObj.IsObsolete;
                testcasetemp.ObsoleteReason = TestcaseObj.ObsoleteReason;
                testcasetemp.CreatedBy = TestcaseObj.CreatedBy;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }

        public TestSuite GetTestSuiteById(int id)
        {

            try
            {
                var testsuite = new TestSuite();
                testsuite = (from c in context.TestSuites where c.TestSuiteId == id select c).FirstOrDefault<TestSuite>();
                return testsuite;
            }
            catch
            {
                return null;
            }
        }
        public int SaveTestSuite(TestSuite test)
        {
            try
            {
                var lastid = (from c in context.TestSuites select c.TestSuiteId).Max();
                var testsuite = new TestSuite();
                testsuite.TestSuiteId = lastid + 1;
                testsuite.TestSuite1 = test.TestSuite1;
                testsuite.TestSuiteOwner = test.TestSuiteOwner;
                testsuite.CreatedDate = DateTime.Now;
                testsuite.CreatedBy = test.CreatedBy;
                testsuite.IsActive = test.IsActive;
                testsuite.TestSuiteDescription = test.TestSuiteDescription;
                testsuite.ModifiedDate = DateTime.Now;
                context.TestSuites.Add(testsuite);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }
        public int UpdatedTestSuite(TestSuite test)
        {
           
            try
            {
                var testSuite = (from c in context.TestSuites where c.TestSuiteId == test.TestSuiteId select c).FirstOrDefault<TestSuite>();
                testSuite.TestSuite1 = test.TestSuite1;
                testSuite.TestSuiteOwner = test.TestSuiteOwner;
                testSuite.CreatedBy = test.CreatedBy;
                testSuite.IsActive = test.IsActive;
                testSuite.TestSuiteDescription = test.TestSuiteDescription;
                testSuite.ModifiedDate = System.DateTime.Now;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }

        public int DeleteTestsuite(int id)
        {
            try
            {
                var deltestsuite = (from c in context.TestSuites where c.TestSuiteId == id select c).FirstOrDefault<TestSuite>();
                context.TestSuites.Remove(deltestsuite);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }

        public List<TestCase> GetTestCases()
        {

            try
            {
                var lst = new List<TestCase>();
                lst = (from c in context.TestCases select c).ToList<TestCase>();
                return lst;
            }
            catch
            {
                return null;
            }
        }

        public TestCase GetTestCasesById(int id)
        {

            try
            {
                var testcase = new TestCase();
                testcase = (from c in context.TestCases where c.TestCaseId == id select c).FirstOrDefault<TestCase>();
                return testcase;
            }
            catch
            {
                return null;
            }
        }
        public int SaveTestCase(TestCase test)
        {
            try
            {
                var lastid = (from c in context.TestCases select c.TestCaseId).Max();
                var testcase = new TestCase();
                testcase.TestCaseId = lastid + 1;
                testcase.TestSuiteId = test.TestSuiteId;
                testcase.TestCaseName = test.TestCaseName;
                testcase.TestCaseTypeId = test.TestCaseTypeId;
                testcase.TestSuite = test.TestSuite;
                testcase.PipelineStage = test.PipelineStage;
                testcase.PipelineStageId = test.PipelineStageId;
                testcase.ObsoleteReason = test.ObsoleteReason;
                testcase.IsActive = test.IsActive;
                testcase.IsObsolete = test.IsObsolete;
                testcase.Description = test.Description;
                testcase.CreatedDate = DateTime.Now;
                testcase.CreatedBy = test.CreatedBy;
                context.TestCases.Add(testcase);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }

        public int DeleteTestcase(int id)
        {
            try
            {
                var deltestcase = (from c in context.TestCases where c.TestCaseId == id select c).FirstOrDefault<TestCase>();
                context.TestCases.Remove(deltestcase);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }

        public List<TestCaseType> GetTestCaseTypes()
        {

            try
            {
                var lst = new List<TestCaseType>();
                lst = (from c in context.TestCaseTypes select c).ToList<TestCaseType>();
                return lst;
            }
            catch
            {
                return null;
            }
        }

        public List<Event> GetEvents()
        {

            try
            {
                var lst = new List<Event>();
                lst = context.Events.ToList<Event>();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public List<Scheduler> GetSchedulerEvents()
        {

            try
            {
                var lst = new List<Scheduler>();
                lst = context.Schedulers.ToList<Scheduler>();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public int SaveSchedularEvent(Scheduler schedu)
        {
            try
            {
                var eventfetched = new Scheduler();

                var query = (from c in context.Schedulers select c.TaskID).ToList(); 
                if(query.Count()!=0)
                {
                    var lastid = query.Max();
                    eventfetched.TaskID = lastid + 1;
                }
                else
                {
                    eventfetched.TaskID = 1;
                }                
                eventfetched.Title = schedu.Title;
                eventfetched.Start = schedu.Start;
                eventfetched.End = schedu.End;
                eventfetched.StartTimezone = schedu.StartTimezone;
                eventfetched.EndTimezone = schedu.EndTimezone;
                eventfetched.Description = schedu.Description;
                eventfetched.RecurrenceID = schedu.RecurrenceID;
                eventfetched.RecurrenceRule = schedu.RecurrenceRule;
                eventfetched.RecurrenceException = schedu.RecurrenceException;
                eventfetched.IsAllDay = schedu.IsAllDay;
                context.Schedulers.Add(eventfetched);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int UpdateSchedularEvent(Scheduler schedu)
        {
            var testsuite = new Event();
            try
            {
                var eventfetched = (from c in context.Schedulers where c.TaskID == schedu.TaskID select c).FirstOrDefault<Scheduler>();
                eventfetched.Title = schedu.Title;
                eventfetched.Start = schedu.Start;
                eventfetched.End = schedu.End;
                eventfetched.StartTimezone = schedu.StartTimezone;
                eventfetched.EndTimezone = schedu.EndTimezone;
                eventfetched.Description = schedu.Description;
                eventfetched.RecurrenceID = schedu.RecurrenceID;
                eventfetched.RecurrenceRule = schedu.RecurrenceRule;
                eventfetched.RecurrenceException = schedu.RecurrenceException;
                eventfetched.IsAllDay = schedu.IsAllDay;

                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        
        public int UpdatedEvent(Event eventc)
        {
            var testsuite = new Event();
            try
            {
                var eventfetched = (from c in context.Events where c.TaskID == eventc.TaskID select c).FirstOrDefault<Event>();
                eventfetched.Title = eventc.Title;
                eventfetched.Start = eventc.Start;
                eventfetched.End = eventc.End;
                eventfetched.StartTimezone = eventc.StartTimezone;
                eventfetched.EndTimezone = eventc.EndTimezone;
                eventfetched.Description = eventc.Description;
                eventfetched.RecurrenceID = eventc.RecurrenceID;
                eventfetched.RecurrenceRule = eventc.RecurrenceRule;
                eventfetched.RecurrenceException = eventc.RecurrenceException;
                eventfetched.IsAllDay = eventc.IsAllDay;

                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int DeleteSchedularEvent(int id)
        {
            try
            {
                var delevent = (from c in context.Schedulers where c.TaskID == id select c).FirstOrDefault<Scheduler>();
                context.Schedulers.Remove(delevent);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }
        
        public int SaveEvent(Event eventc)
        {
            try
            {
                var lastid = (from c in context.Events select c.TaskID).Max();
                var eventfetched = new Event();
                eventfetched.TaskID = lastid + 1;
                eventfetched.Title = eventc.Title;
                eventfetched.Start = eventc.Start;
                eventfetched.End = eventc.End;
                eventfetched.StartTimezone = eventc.StartTimezone;
                eventfetched.EndTimezone = eventc.EndTimezone;
                eventfetched.Description = eventc.Description;
                eventfetched.RecurrenceID = eventc.RecurrenceID;
                eventfetched.RecurrenceRule = eventc.RecurrenceRule;
                eventfetched.RecurrenceException = eventc.RecurrenceException;
                eventfetched.IsAllDay = eventc.IsAllDay;
                context.Events.Add(eventfetched);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int DeleteEvent(int id)
        {
            try
            {
                var delevent = (from c in context.Events where c.TaskID == id select c).FirstOrDefault<Event>();
                context.Events.Remove(delevent);
                context.SaveChanges();
                return 1;
            }
            catch
            { return 0; }
        }
        public List<string> GetTestSuitesNames()
        {

            try
            {
                var lst = new List<string>();
                lst = (from c in context.TestSuites select c.TestSuite1).ToList<string>();
                return lst;
            }
            catch
            {
                return null;
            }
        }

        public List<TestCaseLog> GetTestCaseLogentries()
        {

            try
            {
                var lst = new List<TestCaseLog>();
                lst = (from c in context.TestCaseLogs orderby c.CreatedDate select c).ToList<TestCaseLog>();
                //lst = (from c in context.TestCaseLogs where EntityFunctions.TruncateTime(c.CreatedDate) >= EntityFunctions.TruncateTime(startDate) & EntityFunctions.TruncateTime(c.CreatedDate) <= EntityFunctions.TruncateTime(endDate) orderby c.CreatedDate select c).ToList<TestCaseLog>();
                var dates = context.TestCaseLogs.Select(d => d.CreatedDate).Distinct();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public List<DateTime?> GetTestCaseLogentriesdates()
        {

            try
            {
                var dates = context.TestCaseLogs.Select(d => (d.CreatedDate)).Distinct().ToList();
                return dates;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<TestCaseLog> GetTestCaseLogentries(DateTime startDate, DateTime endDate)
        {

            try
            {
                var lst = new List<TestCaseLog>();
                //lst = (from c in context.TestCaseLogs orderby c.CreatedDate select c).ToList<TestCaseLog>();
                lst = (from c in context.TestCaseLogs where EntityFunctions.TruncateTime(c.CreatedDate) >= EntityFunctions.TruncateTime(startDate) & EntityFunctions.TruncateTime(c.CreatedDate) <= EntityFunctions.TruncateTime(endDate) orderby c.CreatedDate select c).ToList<TestCaseLog>();
                var dates = context.TestCaseLogs.Select(d => d.CreatedDate).Distinct();
                return lst;
            }
            catch
            {
                return null;
            }
        }
        public List<TestCaseLog> GetTestCaseLogentries_Today()
        {

            try
            {
                var lst = new List<TestCaseLog>();
                lst = (from c in context.TestCaseLogs where DateTime.Today == EntityFunctions.TruncateTime(c.CreatedDate) select c).ToList<TestCaseLog>();



                //from c in entities.vwReportDate
                //let eventDate = EntityFunctions.TruncateTime(c.EventCreateDate)
                //where eventDate >= dtStart && eventDate <= dtEndDate
                //select c;

                return lst;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int GetNumberOfTestCases()
        {
            int NumberOfTestCases;
            try
            {
                NumberOfTestCases = context.TestCases.Count(p => p.IsActive == true);
            }
            catch
            {
                return 0;
            }
            return NumberOfTestCases;

        }
        public List<int> GetYearsAvailable()
        {

            try
            {
                var yrs = context.TestCaseLogs.Select(d => (d.CreatedDate.Value.Year)).Distinct().ToList();
                return yrs;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<TestCase> GetTestCaseDetails()
        {

            try
            {
                var lst = new List<TestCase>();
                lst = (from c in context.TestCases select c).ToList<TestCase>();
                return lst;
            }
            catch
            {
                return null;
            }
        }


        public int SaveRowCountParams(TestCaseParameterRowCount row)
        {
            try
            {
                var lastid = (from c in context.TestCaseParameterRowCounts select c.TestCaseParameterRowCountId).Max();
                var rowparamfetched = new TestCaseParameterRowCount();
                rowparamfetched.TestCaseParameterRowCountId = lastid + 1;
                rowparamfetched.TableName = row.TableName;
                rowparamfetched.TestCaseId = row.TestCaseId;
                rowparamfetched.SourceFilePath = row.SourceFilePath;
                rowparamfetched.DestinationFilePath = row.DestinationFilePath;
                rowparamfetched.FileColumnsCount = row.FileColumnsCount;
                rowparamfetched.CreatedBy = row.CreatedBy;
                rowparamfetched.CreatedDate = DateTime.Now;  
                context.TestCaseParameterRowCounts.Add(rowparamfetched);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int SavePrimarykeyParams(TestCaseParameterPrimaryKey ppk)
        {
            try
            {
                var lastid = (from c in context.TestCaseParameterPrimaryKeys select c.TestCaseParameterPrimaryKeyId).Max();
                var paramfetched = new TestCaseParameterPrimaryKey();
                paramfetched.TestCaseParameterPrimaryKeyId = lastid + 1;
                paramfetched.TestCaseId = ppk.TestCaseId;
                paramfetched.TableName = ppk.TableName;
                paramfetched.PrimaryKeyColumn = ppk.PrimaryKeyColumn;
                paramfetched.CreatedBy = ppk.CreatedBy;
                paramfetched.CreatedDate = DateTime.Now;
                context.TestCaseParameterPrimaryKeys.Add(paramfetched);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int SaveFKParams(TestCaseParameterForeignKey ppk)
        {
            try
            {
                var lastid = (from c in context.TestCaseParameterForeignKeys select c.TestCaseParameterForeignKeyId).Max();
                var paramfetched = new TestCaseParameterForeignKey();
                paramfetched.TestCaseParameterForeignKeyId = lastid + 1;
                paramfetched.TestCaseId = ppk.TestCaseId;
                paramfetched.PrimaryKeyTableName = ppk.PrimaryKeyTableName;
                paramfetched.PrimaryKeyColumn = ppk.PrimaryKeyColumn;
                paramfetched.ForeignKeyTableName = ppk.ForeignKeyTableName;
                paramfetched.ForeignKeyColumn = ppk.ForeignKeyColumn;
                paramfetched.CreatedBy = ppk.CreatedBy;
                paramfetched.CreatedDate = DateTime.Now;
                context.TestCaseParameterForeignKeys.Add(paramfetched);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }
        public int SaveValueCheckParams(TestCaseParameterValueCheck vc)
        {
            try
            {
                var lastid = (from c in context.TestCaseParameterValueChecks select c.TestCaseParameterValueCheckId).Max();
                var paramfetched = new TestCaseParameterValueCheck();
                paramfetched.TestCaseParameterValueCheckId = lastid + 1;
                paramfetched.TestCaseId = vc.TestCaseId;
                paramfetched.TableName = vc.TableName;
                paramfetched.PrimaryKeyColumnName = vc.PrimaryKeyColumnName;
                paramfetched.ValueCheckColumnName = vc.ValueCheckColumnName;
                paramfetched.DestinationFilePath = vc.DestinationFilePath;
                paramfetched.FileColumnsCount = vc.FileColumnsCount;
                paramfetched.FileColumnsName = vc.FileColumnsName;
                paramfetched.SourceWhereClauseFilter = vc.SourceWhereClauseFilter;
                paramfetched.DestinationWhereClauseFilter = vc.DestinationWhereClauseFilter;
                paramfetched.CreatedBy = vc.CreatedBy;
                paramfetched.CreatedDate = DateTime.Now;
                context.TestCaseParameterValueChecks.Add(paramfetched);
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }

    }
    
}
