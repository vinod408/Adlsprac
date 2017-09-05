app.controller('PipeLineController', function ($rootScope, $scope, AppService) {
    $scope.message = '';
    $scope.result = "color-default";
    $scope.PipeLinemodel = {};
    $scope.enable = false;

    GetAllRecords();
    function GetAllRecords() {
        var st = AppService.getAllstages();
        st.then(function (pl) { $rootScope.stages = pl.data },
              function () {
                  //$log.error('Some Error in Getting Records.', errorPl);
                  alert('Error fetching data')
              });

    }
    $scope.ctrlenable = function () {
        $rootScope.pipelineenable = true;
        $rootScope.testsuiteenable = false;
    }
    $scope.EnableButton = function () {
        $scope.enable = !$scope.enable;
        $scope.PipeLinemodel = {};
        $scope.showbutton = true;
    };

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }


    //******=========Get Pipeline byid=========******
    $scope.getPipeline = function (pipeline) {


        AppService.getStageById(pipeline)

       .then(function (pl) {

           $scope.PipeLinemodel = pl;

           $scope.enable = true;
           GetAllRecords();
           $window.scrollTo(0, angular.element(document.getElementById('createid')).offsetTop);
       }, function (data) {
           $scope.message = 'Unexpected Error while fetching the row!!';
           $scope.result = "color-red";
       });
    };

    //******=========Save Pipeline=========******
    $scope.SavePipeline = function () {

        AppService.savePipeline($scope.PipeLinemodel)
        .then(function (data) {
            if (data.success = true) {
                $scope.message = 'Form data Saved!';
                $scope.result = "color-green";
                GetAllRecords();
                $scope.enable = false;
                $scope.PipeLinemodel = {};
            }
            else {
                $scope.message = 'Form data not Saved!';
                $scope.result = "color-red";
            }
        }, function (data) {
            $scope.message = 'Unexpected Error while saving data!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
        GetAllRecords();
    };
    //******=========Edit Customer=========******
    $scope.UpdatePipeline = function () {
        //debugger;

        var updatepl = AppService.updatePipeline();

        updatepl.then(function (data) {
            if (data.success = true) {

                $scope.message = 'Form data Updated!';
                $scope.result = "color-green";
                GetAllRecords();

                $scope.enable = false;
                $scope.PipeLinemodel = {};
            }
            else {
                $scope.message = 'Form data not Updated!';
                $scope.result = "color-red";
            }
        }, function (data) {
            $scope.message = 'Unexpected Error while update!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };

    //******=========Delete Pipeline=========******
    $scope.DeletePipeline = function (pipeline) {

        AppService.deletePipeline(pipeline)
       .then(function (data) {

           if (data.success = true) {
               $scope.message = 'PipeLineStage with id' + pipeline.PipelineStageId + ' deleted from record!!';
               $scope.result = "color-green";
               GetAllRecords();
               $window.scrollTo(0, angular.element(document.getElementById('createid')).offsetTop);
           }
           else {
               $scope.message = 'Error on deleting Record!';
               $scope.result = "color-red";
           }
       },
       function (data) {
           $scope.message = 'Unexpected Error while deleting data!!';
           $scope.result = "color-red";
       });

    };

});


app.controller('TestSuiteController', function ($rootScope, $scope, TestSuiteservice) {
    $scope.testsuite = {};
    $scope.message = '';
    $scope.result = "color-default";
    $scope.isViewLoading = false;
    $scope.enable = false;
    GetAllTestsuites();
    function GetAllTestsuites() {
        var st = TestSuiteservice.getAllTestsuites();
        st.then(function (ts) { $rootScope.suites = ts.data },
              function () {
                  alert('Error fetching data')
              });

    }
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

    $scope.EnableButton = function () {
        $scope.enable = !$scope.enable;
        $scope.testsuite = {};
        $scope.showbutton = true;
    };

    $scope.Cleartestsuite = function () {
        $scope.testsuite = {};
    };

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }


    $scope.getTestsuite = function (testsuiteData) {
        $scope.showbutton = false;
        TestSuiteservice.getTestsuiteById(testsuiteData)

        .then(function (ts) {
            //debugger;

            $scope.testsuite = ts.data;
            //alert(JSON.stringify($scope.testsuite))
            $scope.enable = true;
            GetAllTestsuites();
            $window.scrollTo(0, angular.element(document.getElementById('createid')).offsetTop);

        }, function (data) {
            $scope.message = 'Unexpected Error while GetcustbyId!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };


    $scope.SaveTestsuite = function () {

        TestSuiteservice.saveTestSuite($scope.testsuite)
       .then(function (data) {

           if (data.success = true) {
               $scope.message = 'Form data Saved!';
               $scope.result = "color-green";
               GetAllTestsuites();
               $scope.enable = false;
               $scope.testsuite = {};
               $scope.dis = false;
           }
           else {
               $scope.message = 'Form data not Saved!';
               $scope.result = "color-red";
           }
       }, function (data) {
           $scope.message = 'Unexpected Error while saving data!!' + data.errors;
           $scope.result = "color-red";
           console.log($scope.message);
       });
        GetAllTestsuites();
    };


    $scope.UpdateinlineTestsuite = function (testm) {
        //debugger;


        TestSuiteservice.updateTestSuite(testm)

        .then(function (data) {
            if (data.success = true) {
                $scope.message = 'Form data Updated!';
                $scope.result = "color-green";
                GetAllTestsuites();
                $scope.enable = false;
                $scope.testsuite = {};
                $scope.dis = false;
            }
            else {
                $scope.message = 'Form data not Updated!';
                $scope.result = "color-red";
            }
        }, function (data) {
            $scope.message = 'Unexpected Error while update!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };

    $scope.UpdateTestsuite = function (testsuite) {
        //debugger;
        TestSuiteservice.updateTestSuite(testsuite)

        .then(function (data) {
            if (data.success = true) {
                $scope.message = 'Form df data Updated!';
                $scope.result = "color-green";
                GetAllTestsuites();
                $scope.enable = false;
                $scope.testsuite = {};
                $scope.dis = false;
            }
            else {
                $scope.message = 'Form data not Updated!';
                $scope.result = "color-red";
            }
        }, function (data) {
            $scope.message = 'Unexpected Error while update!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };

    $scope.DeleteTestsuite = function (testsuite) {
        TestSuiteservice.deleteTestSuite(testsuite)
       .then(function (data) {

           if (data.success = true) {
               $scope.message = testsuite.TestSuite1 + ' deleted from record!!';
               $scope.result = "color-green";
               GetAllTestsuites();
               $window.scrollTo(0, angular.element(document.getElementById('createid')).offsetTop);
           }
           else {
               $scope.message = 'Error on deleting Record!';
               $scope.result = "color-red";
           }
       },
       function (data) {
           $scope.message = 'Unexpected Error while deleting data!!';
           $scope.result = "color-red";
       });

    };

})

app.controller('TestCaseController', function ($rootScope, $scope, $http) {
    $scope.message = '';
    $scope.result = "color-default";
    $scope.isViewLoading = false;
    $scope.enable = false;
    $scope.testcase = {};
    GetAllTestcasetypes();
    GetAllTestCases();
    function GetAllTestCases() {
        //debugger;
        $http.get('/Validation/GetTestCaseDetails')
            .success(function (data, status, headers, config) {
                $scope.cases = data;
                $scope.isViewLoading = false;
            })
            .error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
    }

    $scope.EnableButton = function () {
        $scope.enable = !$scope.enable;
        $scope.PipeLinemodel = {};
        $scope.showbutton = true;
    };
    $scope.SaveTestCase = function (testcase) {
        $http({
            method: 'POST',
            url: '/Validation/SaveTestCase',
            data: testcase,
            contentType: 'application/json',
            dataType: 'Json'
        })
           .success(function (data, status, headers, config) {
               if (data.success = true) {
                   $scope.cases = {};
                   GetAllTestCases();
                   $scope.message = 'Test Case saved successfully! !';
                   $scope.result = "color-green";
                   $scope.isViewLoading = false;
               }
               else {
                   $scope.message = 'Test Case data not saved!';
                   $scope.result = "color-red";
               }

           })
           .error(function (data, status, headers, config) {
               $scope.message = 'Unexpected Error while saving data!!';
               $scope.result = "color-red";
               console.log($scope.message);
           });
    };


    function GetAllTestcasetypes() {
        $http.get('/Validation/GetTestCaseTypeDetails')
           .success(function (data, status, headers, config) {
               $scope.testcasetypes = data;
               $scope.isViewLoading = false;
           })
           .error(function (data, status, headers, config) {
               $scope.message = 'Unexpected Error while loading data!!';
               $scope.result = "color-red";
               console.log($scope.message);
           });

    }

});

app.controller('LoginController', function ($rootScope, $scope, LoginService) {
    debugger;
    $rootScope.message = "";
    $scope.Submited = false;
    $rootScope.signupenable =false;
    $scope.IsLoggedIn = false;
    $rootScope.loadingimage = false;
    $scope.IsFormValid = false;
    $scope.UserModel = {};
    $scope.UserModel.Role = $rootScope.role;

    $scope.LoginForm = function () {
        $scope.errormessage = '';
        $rootScope.loadingimage = true;
        $scope.Submited = true;
        if (true) {
            debugger
            LoginService.getUserDetails($scope.UserModel).then(function (d) {
                debugger;
                if (d.data==1) {
                    location.href = "/Validation/Dashboard2";   
                    $rootScope.loadingimage = false;
                }
                else if(d.data==0) {
                    $scope.errormessage = "Invalid credentials! try again";
                    $scope.UserModel = {};
                    $rootScope.loadingimage = false;
                }
            });
        }

        else {
            $rootScope.loadingimage = false;
            $scope.errormessage = 'Please enter ValidData';
        }
    }

    $rootScope.loadingimage = false;
})


app.controller('RegistrationController', function ($rootScope, $scope, RegistrationService) {
    debugger
   
    $rootScope.loadingimage = false;

    $scope.Registerform = function () {
        $rootScope.loadingimage = false;
        $scope.submitted = true;
        $rootScope.message = '';
        $scope.errormessage = '';

        if (true) {
            RegistrationService.RegisetrformData($scope.UserModel).then(function (d) {
                debugger;
                if (d.data==1) {
                    $rootScope.message = 'You have registered successfully, Please login';
                    $rootScope.role = $scope.UserModel.Role;
                    $scope.UserModel = {};
                    $scope.clearform = false;
                    $rootScope.signupenable = false;
                    $rootScope.loadingimage = false;
                    location.href = "/Validation/Dashboard2";
                }
                else if(d.data==0){

                    $scope.errormessage = 'Eamil is already available. Try Sign in';
                    $scope.UserModel = {};
                    $rootScope.loadingimage = false;
             
                    }
                 else
                  {
                    $scope.errormessage = 'Some error occur!! please try aftersometime';
                    $rootScope.loadingimage = false;
                     }

            });
        }
        else {
            $scope.errormessage = 'Please enter ValidData';
            $rootScope.loadingimage = false;
        }
        
    }
});

app.controller('ChartsController',function ($rootScope, $scope, Chartservice){
    $scope.yeardropdown = Chartservice.chardata
    $scope.chardata = {};
});

