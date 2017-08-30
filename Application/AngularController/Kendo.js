angular.module("KendoDemos", ["kendo.directives"])
     .controller("MyCtrl", function ($scope, $rootScope) {
         //$scope.loading = false;
         $scope.mainGridOptions = {
             toolbar: [
            { template: kendo.template($("#template").html()) }
             ],

             dataSource:new kendo.data.DataSource( {
                 type: "json",
                 transport: {
                     read:
                     {
                         url: "/Validation/GetTestSuiteDetails",
                         dataType: "json",
                         complete: function (e) {                          
                             $scope.loading = false;
                         }
                     },
                     destroy:
                       {
                           url: "/Validation/DeleteTestSuite",
                           type: "POST",

                       },
                     create:
                     {
                         url: "/Validation/SaveTestSuite",
                         type: "POST",
                         //complete function to refresh the data after the create operation //takes 1 whole day to find this option
                         complete: function (e) {
                             $scope.mainGridOptions.dataSource.read();
                             $scope.loading = true;

                         }
                     },
                     update:
                     {
                         url: "/Validation/UpdateTestSuite",
                         type: "POST",

                         parameterMap: function (options, operation) {
                             if (operation !== "read" && options.models) {
                                 return {
                                     models: kendo.stringify(options.models)
                                 };
                             }
                             else {
                                 $scope.loading = false;
                             }
                         }
                     },
                 },
                 schema: {
                     data: "data", total: "pageSize", errors: "Errors",
                     model:
                     {
                         id: "TestSuiteId",
                         fields: {
                             TestSuiteId: { editable: false, nullable: true, type: "number" },
                             TestSuite1: { editable: true, nullable: true, type: "string", validation: { required: true } },
                             TestSuiteDescription: { editable: true, nullable: true, type: "string" },
                             IsActive: { editable: true, nullable: true, type: "boolean", filterable: { multi: true, dataSource: [{ IsActive: true }, { IsActive: false }] } },
                             TestSuiteOwner: { editable: true, nullable: true, type: "string", validation: { required: true } },
                             CreatedBy: { editable: true, nullable: true, type: "string", validation: { required: true } },
                         }
                     }
                 },
                 error: function (e) {
                     if (e.errors) {
                         alert(e.errors);
                     }
                 },
                 pageSize: 10,
                 serverPaging: false,
                 serverSorting: false
             }),
           
             toolbar: [{ name: "create", text: "Create TestSuite" }],
             filterable: true,
             sortable: true,
             //pageable: true,
             pageable: {
                 refresh: true,
                 pageSizes: true,
                 buttonCount: 5
             },
             resizeable: true,
             refresh: true,
             columns: [
                  { field: "TestSuiteId", title: "TestSuite Id", width: "120px" },
                  { field: "TestSuite1", title: "TestSuite", width: "120px" },
                  { field: "TestSuiteDescription", title: "TestSuite Desc", width: "40px" },
                  { field: "IsActive", title: "IsActive", width: "120px" },
                  { field: "TestSuiteOwner", title: "TestSuite Owner", width: "120px" },
                  { field: "CreatedBy", title: "Created By", width: "120px", editable: false },
                  { command: ["edit"], title: "&nbsp;", width: "120px" }],
             editable: "popup",
         };
         
         $scope.detailGridOptions = function (dataItem) {
             $scope.e = dataItem;
             return {
                 dataSource: new kendo.data.DataSource({
                     type: "json",
                     transport: {
                         read:
                         {
                             url: "/Validation/kendoGettestcases",
                             dataType: "json",
                             complete: function (e) {
                                 $scope.loading = false;
                             }
                         },
                         destroy:
                           {
                               url: "/Validation/DeleteTestCase",
                               type: "POST",

                           },
                         create:
                         {
                             url: "/Validation/kendosaveGettestcases",
                             type: "POST",
                             //complete function to refresh the data after the create operation //takes 1 whole day to find this option
                             complete: function (e) {
                                 $scope.detailGridOptions(e).dataSource.read();

                             }
                         },
                         update:
                         {
                             url: "/Validation/kendoUpdateGettestcases",
                             type: "POST",

                             parameterMap: function (options, operation) {
                                 if (operation !== "read" && options.models) {
                                     return {
                                         models: kendo.stringify(options.models)
                                     };
                                 }
                             }
                         },
                     },
                     schema: {
                         data: "data", total: "pageSize",
                         model:
                         {
                             id: "TestCaseId",
                             fields: {
                                 TestCaseId: { editable: false, nullable: true, type: "number" },
                                 TestCaseName: { editable: true, nullable: true, type: "string" },
                                 //TestSuiteId: { editable: true, nullable: true, type: "number" },
                                 //TestCaseTypeId: { editable: true, nullable: true, type: "number" },
                                 //PipelineStageId: { editable: true, nullable: true, type: "number" },
                                 IsActive: { editable: true, nullable: true, type: "boolean" },
                                 IsObsolete: { editable: true, nullable: true, type: "boolean" },
                                 ObsoleteReason: { editable: true, nullable: true, type: "string" },
                                 CreatedBy: { editable: true, nullable: true, type: "string" },
                             }
                         }
                     },
                     pageSize: 5,
                     filter: { field: "TestSuiteId", operator: "eq", value: dataItem.TestSuiteId },
                     serverPaging: false,
                     serverSorting: false
                 }),
                 filterable: true,
                 sortable: { mode: "single", allowUnsort: false },
                 pageable: true,
                 columns: [
                 { field: "TestCaseId", title: "TestCase Id", width: "18px" },
                 { field: "TestCaseName", title: "TestCase Name", width: "60px" },
                 //{ field: "TestSuiteId", title: "TestSuiteId", width: "10px" },
                 //{ field: "PipelineStageId", title: "PipelineStageId", width: "10px" },
                 //{ field: "TestCaseTypeId", title: "TestCaseType Id ", width: "10px" },
                 { field: "IsActive", title: "IsActive", width: "15px", filterable: true },
                 { field: "IsObsolete", title: "IsObsolete", width: "15px" },
                 { field: "ObsoleteReason", title: "ObsoleteReason", width: "60px" },
                 { field: "CreatedBy", title: "Created By", width: "30px" },
                 { command: ["edit", "destroy"], title: "&nbsp;", width: "40px" }],
                 editable: "popup",


             };
         };

         //testcase
         $scope.testGridOptions = {

                 dataSource: new kendo.data.DataSource({
                     type: "json",
                     transport: {
                         read:
                         {
                             url: "/Validation/kendoGettestcases",
                             dataType: "json",
                             complete: function (e) {
                                 $scope.loading = false;
                             }
                         },
                         destroy:
                           {
                               url: "/Validation/DeleteTestCase",
                               type: "POST",

                           },
                         create:
                         {
                             url: "/Validation/kendosaveGettestcases",
                             type: "POST",
                             //complete function to refresh the data after the create operation //takes 1 whole day to find this option
                             complete: function (e) {
                                 $scope.testGridOptions.dataSource.read();

                             }
                         },
                         update:
                         {
                             url: "/Validation/kendoUpdateGettestcases",
                             type: "POST",

                             parameterMap: function (options, operation) {
                                 if (operation !== "read" && options.models) {
                                     return {
                                         models: kendo.stringify(options.models)
                                     };
                                 }
                             }
                         },
                     },
                     schema: {
                         data: "data", total: "pageSize",
                         model:
                         {
                             id: "TestCaseId",
                             fields: {
                                 TestCaseId: { editable: false, nullable: true, type: "number" },
                                 TestCaseName: { editable: true, nullable: true, type: "string", validation: { required: true } },
                                 TestSuiteId: { editable: true, validation: { required: true } },//remove nullable if we want value from dropdown if it is there it misbehaves
                                 TestCaseTypeId: { editable: true, validation: { required: true } },
                                 PipelineStageId: { editable: true, validation: { required: true } },
                             IsActive: { editable: true, type: "boolean" },
                                 IsObsolete: { editable: true, nullable: true, type: "boolean" },
                             ObsoleteReason: { editable: $scope.validation, nullable: true, type: "string" },
                                 CreatedBy: { editable: true, nullable: true, type: "string", validation: { required: true } },
                             }
                         }
                     },
                     pageSize: 10,
                     serverPaging: false,
                     serverSorting: false
                 }),
                 filterable: true,
                 resizable: true,
                 toolbar: [{ name: "create", text: "Create TestCase" }],
                 sortable: { mode: "single", allowUnsort: false },
                 pageable: true,
                 columns: [
                 { field: "TestCaseId", title: "TestCase Id", width: "18px" },
                 { field: "TestCaseName", title: "TestCase Name", width: "60px", validation: { required: true } },
                 { field: "TestSuiteId", title: "TestSuiteId", width: "20px", editor: testsuiteDropDownEditor, validation: { required: true } },
                 { field: "PipelineStageId", title: "PipelineStageId", width: "20px", editor: pipelineDropDownEditor, validation: { required: true } },
                 { field: "TestCaseTypeId", title: "TestCaseType Id ", width: "20px", editor: testcasetypeDropDownEditor, validation: { required: true } },
                 { field: "IsActive", title: "IsActive", width: "20px", filterable: true },
             { field: "IsObsolete", title: "IsObsolete", width: "20px" },
                 { field: "ObsoleteReason", title: "ObsoleteReason", width: "40px" },
                 { field: "CreatedBy", title: "Created By", width: "30px" },
                 { command: ["edit", "destroy"], title: "&nbsp;", width: "40px" }],
                 editable: "popup", 
         };
        
         function testsuiteDropDownEditor(container, options) {
             $('<input data-bind="value:' + options.field + '"/>')
               .appendTo(container)
               .kendoDropDownList({
                   dataTextField: "TestSuite1",
                   dataValueField: "TestSuiteId",
                   dataSource: {
                       type: "json",
                       transport: {
                           read: "/Validation/GetTestSuiteDetails"
                       },
                       schema: {
                           data: "records", total: "total",
                       }
                   },
               });
         }
         function pipelineDropDownEditor(container, options) {
             $('<input data-bind="value:' + options.field + '"/>')
               .appendTo(container)
               .kendoDropDownList({
                   dataTextField: "PipelineStage1",
                   dataValueField: "PipelineStageId",
                   dataSource: {
                       type: "json",
                       transport: {
                           read: "/Validation/GetPipeLinestageDetails"
                       },
                       schema: {
                           data: "data", total: "pageSize",
                       }
                   },
               });
         }
         function testcasetypeDropDownEditor(container, options) {
             $('<input data-bind="value:' + options.field + '"/>')
               .appendTo(container)
               .kendoDropDownList({
                   dataTextField: "TestCaseType1",
                   dataValueField: "TestCaseTypeId",
                   dataSource: {
                       type: "json",
                       transport: {
                           read: "/Validation/GetTestCaseTypeDetails"
                       },
                       schema: {
                           data: "data", total: "pageSize",
                       }
                   },
               });
         }

         //pipeline
         $scope.pipelineGridOptions = {
             toolbar: [
           { template: kendo.template($("#template").html()) }
             ],
             dataSource: new kendo.data.DataSource({
                 type: "json",
                 transport: {
                     read:
                     {
                         url: "/Validation/GetPipeLinestageDetails",
                         dataType: "json",
                         complete: function (e) {
                             $scope.loading = false;
                         }
                     },
                     destroy:
                       {
                           url: "/Validation/DeletePipeline",
                           type: "POST",

                       },
                     create:
                     {
                         url: "/Validation/SavePipeline",
                         type: "POST",
                         //complete function to refresh the data after the create operation //takes 1 whole day to find this option
                         complete: function (e) {
                             $scope.pipelineGridOptions.dataSource.read();

                         }
                     },
                     update:
                     {
                         url: "/Validation/UpdatePipeline",
                         type: "POST",

                         parameterMap: function (options, operation) {
                             if (operation !== "read" && options.models) {
                                 return {
                                     models: kendo.stringify(options.models)
                                 };
                             }
                         }
                     },
                 },
                 schema: {
                     data: "data", total: "pageSize",
                     model:
                     {
                         id: "PipelineStageId",
                         fields: {
                             PipelineStageId: { editable: false, nullable: true, type: "number" },
                             PipelineStage1: { editable: true, nullable: true, type: "string", validation: { required: true } },
                             PipelineStageDescription: { editable: true },
                             CreatedBy: { editable: true, nullable: true, type: "string", validation: { required: true } },
                             CreatedDate: { editable: true, nullable: true, type: "date" }
                         }
                     }
                 },
                 pageSize: 10,
                 serverPaging: false,
                 serverSorting: false
             }),

             filterable: true,
             resizable: true,
             toolbar: [{ name: "create", text: "Create Pipeline" }],
             sortable: { mode: "single", allowUnsort: false },
             pageable: true,
             columns: [
             { field: "PipelineStageId", title: "PipelineStage Id", width: "18px" },
             { field: "PipelineStage1", title: "PipelineStage Name", width: "60px", validation: { required: true } },
             { field: "PipelineStageDescription", title: "PipelineStageDescription", width: "40px" },
             { field: "CreatedBy", title: "Created By", width: "30px" },
               { field: "CreatedDate", title: "CreatedDate", width: "30px" },
             { command: ["edit", "destroy"], title: "&nbsp;", width: "40px" }],
             editable: "popup",

         };
         //Searching the Grid by the value of the textbox
         $("#btnSearch").click(function () {
             var searchValue = $('#searchBox').val();

             //Setting the filter of the Grid
             $("#mainGridOptions").data("kendoGrid").dataSource.filter({
                 logic: "or",
                 filters: [
                   {
                       field: "TestSuite1",
                       operator: "contains",
                       value: searchValue
                   },
                   {
                       field: "TestSuiteOwner",
                       operator: "contains",
                       value: searchValue
                   },
                   {
                       field: "CreatedBy",
                       operator: "contains",
                       value: searchValue
                   }
                 ]
             });
         });

         //Clearing the filter
         $("#btnReset").click(function () {
             $("#mainGridOptions").data("kendoGrid").dataSource.filter({});
         });

         $scope.ctrltestenable = function () {
           
             $rootScope.testsuiteenable = true;
             $rootScope.pipelineenable = false;
             $rootScope.testcaseenable = false;
         }
         $scope.ctrlpipeenable = function () {
             $rootScope.testsuiteenable = false;
             $rootScope.pipelineenable = true;
             $rootScope.testcaseenable = false;
         }
         $scope.ctrltestcaseenable = function () {
             $rootScope.testsuiteenable = false;
             $rootScope.pipelineenable = false;
             $rootScope.testcaseenable = true;
         }

         $scope.customersDataSource = {

             transport: {
                 dataTextField: 'TestCaseName',
                 read: {
                    
                     url: "/Validation/GetTestCaseDetails",
                     dataType: 'json'
                    
                   
                 }
             }
         };
     })


