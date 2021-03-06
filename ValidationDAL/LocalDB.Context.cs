﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ValidationDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ValidationDBContext : DbContext
    {
        public ValidationDBContext()
            : base("name=ValidationDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<PipelineStage> PipelineStages { get; set; }
        public virtual DbSet<Scheduler> Schedulers { get; set; }
        public virtual DbSet<TestCase> TestCases { get; set; }
        public virtual DbSet<TestCaseParameterAccountCheck> TestCaseParameterAccountChecks { get; set; }
        public virtual DbSet<TestCaseParameterCubeRevenueAllUp> TestCaseParameterCubeRevenueAllUps { get; set; }
        public virtual DbSet<TestCaseParameterCubeRevenueAllUpsByDimension> TestCaseParameterCubeRevenueAllUpsByDimensions { get; set; }
        public virtual DbSet<TestCaseParameterForAccrualDailyDateCheck> TestCaseParameterForAccrualDailyDateChecks { get; set; }
        public virtual DbSet<TestCaseParameterForCubeScenario> TestCaseParameterForCubeScenarios { get; set; }
        public virtual DbSet<TestCaseParameterForeignKey> TestCaseParameterForeignKeys { get; set; }
        public virtual DbSet<TestCaseParameterForFreshnessCheck> TestCaseParameterForFreshnessChecks { get; set; }
        public virtual DbSet<TestCaseParameterForWwic> TestCaseParameterForWwics { get; set; }
        public virtual DbSet<TestCaseParameterForWwicExceptional> TestCaseParameterForWwicExceptionals { get; set; }
        public virtual DbSet<TestCaseParameterPrimaryKey> TestCaseParameterPrimaryKeys { get; set; }
        public virtual DbSet<TestCaseParameterQvtValidation> TestCaseParameterQvtValidations { get; set; }
        public virtual DbSet<TestCaseParameterQvtValidationException> TestCaseParameterQvtValidationExceptions { get; set; }
        public virtual DbSet<TestCaseParameterRowCount> TestCaseParameterRowCounts { get; set; }
        public virtual DbSet<TestCaseParameterRowCountExceptional> TestCaseParameterRowCountExceptionals { get; set; }
        public virtual DbSet<TestCaseParameterValueCheck> TestCaseParameterValueChecks { get; set; }
        public virtual DbSet<TestCaseType> TestCaseTypes { get; set; }
        public virtual DbSet<TestSuite> TestSuites { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Editor> Editors { get; set; }
        public virtual DbSet<Executor> Executors { get; set; }
        public virtual DbSet<TestCaseLog> TestCaseLogs { get; set; }
        public virtual DbSet<TestCaseParameterCube> TestCaseParameterCubes { get; set; }
        public virtual DbSet<TestCaseParametersForCubeScenariosAssociatedTable> TestCaseParametersForCubeScenariosAssociatedTables { get; set; }
        public virtual DbSet<TestCaseParametersForQvtValidationAssociatedTable> TestCaseParametersForQvtValidationAssociatedTables { get; set; }
        public virtual DbSet<TestCaseParametersForWwicExceptionalAssociatedTable> TestCaseParametersForWwicExceptionalAssociatedTables { get; set; }
        public virtual DbSet<TestCaseResultType> TestCaseResultTypes { get; set; }
        public virtual DbSet<TestInstance> TestInstances { get; set; }
    
        public virtual int usp_AddTestCase(Nullable<int> testSuiteId, Nullable<int> pipeLineStageId, Nullable<int> testCaseTypeId, string testCaseName, string description, Nullable<bool> isActive, Nullable<bool> isObsolete, string obsoleteReason, Nullable<System.DateTime> createdDate, string createdBy)
        {
            var testSuiteIdParameter = testSuiteId.HasValue ?
                new ObjectParameter("TestSuiteId", testSuiteId) :
                new ObjectParameter("TestSuiteId", typeof(int));
    
            var pipeLineStageIdParameter = pipeLineStageId.HasValue ?
                new ObjectParameter("PipeLineStageId", pipeLineStageId) :
                new ObjectParameter("PipeLineStageId", typeof(int));
    
            var testCaseTypeIdParameter = testCaseTypeId.HasValue ?
                new ObjectParameter("TestCaseTypeId", testCaseTypeId) :
                new ObjectParameter("TestCaseTypeId", typeof(int));
    
            var testCaseNameParameter = testCaseName != null ?
                new ObjectParameter("TestCaseName", testCaseName) :
                new ObjectParameter("TestCaseName", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var isObsoleteParameter = isObsolete.HasValue ?
                new ObjectParameter("IsObsolete", isObsolete) :
                new ObjectParameter("IsObsolete", typeof(bool));
    
            var obsoleteReasonParameter = obsoleteReason != null ?
                new ObjectParameter("ObsoleteReason", obsoleteReason) :
                new ObjectParameter("ObsoleteReason", typeof(string));
    
            var createdDateParameter = createdDate.HasValue ?
                new ObjectParameter("CreatedDate", createdDate) :
                new ObjectParameter("CreatedDate", typeof(System.DateTime));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_AddTestCase", testSuiteIdParameter, pipeLineStageIdParameter, testCaseTypeIdParameter, testCaseNameParameter, descriptionParameter, isActiveParameter, isObsoleteParameter, obsoleteReasonParameter, createdDateParameter, createdByParameter);
        }
    
        public virtual int usp_AdminAdd(string fullName, string userName, string userEmail, string userPassword)
        {
            var fullNameParameter = fullName != null ?
                new ObjectParameter("fullName", fullName) :
                new ObjectParameter("fullName", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var userEmailParameter = userEmail != null ?
                new ObjectParameter("userEmail", userEmail) :
                new ObjectParameter("userEmail", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("userPassword", userPassword) :
                new ObjectParameter("userPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_AdminAdd", fullNameParameter, userNameParameter, userEmailParameter, userPasswordParameter);
        }
    
        public virtual int usp_EditornAdd(string fullName, string userName, string userEmail, string userPassword)
        {
            var fullNameParameter = fullName != null ?
                new ObjectParameter("fullName", fullName) :
                new ObjectParameter("fullName", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var userEmailParameter = userEmail != null ?
                new ObjectParameter("userEmail", userEmail) :
                new ObjectParameter("userEmail", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("userPassword", userPassword) :
                new ObjectParameter("userPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_EditornAdd", fullNameParameter, userNameParameter, userEmailParameter, userPasswordParameter);
        }
    
        public virtual int usp_ExecutornAdd(string fullName, string userName, string userEmail, string userPassword)
        {
            var fullNameParameter = fullName != null ?
                new ObjectParameter("fullName", fullName) :
                new ObjectParameter("fullName", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var userEmailParameter = userEmail != null ?
                new ObjectParameter("userEmail", userEmail) :
                new ObjectParameter("userEmail", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("userPassword", userPassword) :
                new ObjectParameter("userPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_ExecutornAdd", fullNameParameter, userNameParameter, userEmailParameter, userPasswordParameter);
        }
    
        public virtual int usp_ManagerAdd(string fullName, string userName, string userEmail, string userPassword)
        {
            var fullNameParameter = fullName != null ?
                new ObjectParameter("fullName", fullName) :
                new ObjectParameter("fullName", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var userEmailParameter = userEmail != null ?
                new ObjectParameter("userEmail", userEmail) :
                new ObjectParameter("userEmail", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("userPassword", userPassword) :
                new ObjectParameter("userPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_ManagerAdd", fullNameParameter, userNameParameter, userEmailParameter, userPasswordParameter);
        }
    
        public virtual int usp_OperatorAdd(string fullName, string userName, string userEmail, string userPassword)
        {
            var fullNameParameter = fullName != null ?
                new ObjectParameter("fullName", fullName) :
                new ObjectParameter("fullName", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var userEmailParameter = userEmail != null ?
                new ObjectParameter("userEmail", userEmail) :
                new ObjectParameter("userEmail", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("userPassword", userPassword) :
                new ObjectParameter("userPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_OperatorAdd", fullNameParameter, userNameParameter, userEmailParameter, userPasswordParameter);
        }
    
        public virtual int usp_RegisterUser(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_RegisterUser", usernameParameter, passwordParameter);
        }
    }
}
