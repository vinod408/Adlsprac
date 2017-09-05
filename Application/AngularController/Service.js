app.service('AppService', function ($http) {

    this.getAllstages = function () {
        return $http.get("/Validation/GetPipeLinestageDetails");

        //return $http.get("/Validation/GetTestSuiteDetails");

    }

    this.getStageById = function (pipelineData) {

        return $http({
            method: 'GET',
            url: '/Validation/GetPipelineStageById',
            params: { id: pipelineData.PipelineStageId }

        })
    }

    this.savePipeline = function (pipelineData) {
        return $http({
            method: 'POST',
            url: '/Validation/SavePipeline',
            data: pipelineData,
            contentType: 'application/json',
            dataType: 'Json'
        });
    }

    this.updatePipeline = function (pipelineData) {
        return $http({
            method: 'POST',
            url: '/Validation/UpdatePipeline',
            data: pipelineData,
            contentType: 'application/json',
            dataType: 'Json'
        });
    }

    this.deletePipeline = function (pipelineData) {
        var IsConf = confirm('You are about to delete ' + pipelineData.PipelineStageId + '. Are you sure?');

        if (IsConf) {
            return $http({

                method: 'POST',
                url: '/Validation/DeletePipeline',
                data: pipelineData,
                contentType: 'application/json',
                dataType: 'Json'

            });
        }

    }


});


app.service('TestSuiteservice', function ($http) {

    this.getAllTestsuites = function () {
        return $http.get("/Validation/GetTestSuiteDetails");

        //return $http.get("/Validation/GetTestSuiteDetails");

    }

    this.getTestsuiteById = function (testsuiteData) {

        return $http({
            method: 'GET',
            url: '/Validation/GetTestSuiteById',
            params: { id: testsuiteData.TestSuiteId }

        });
    }

    this.saveTestSuite = function (testsuiteData) {
        return $http({
            method: 'POST',
            url: '/Validation/SaveTestSuite',
            data: testsuiteData,
            contentType: 'application/json',
            dataType: 'Json'
        });
    }

    this.updateTestSuite = function (testsuiteData) {
        return $http({
            method: 'POST',
            url: '/Validation/UpdateTestSuite',
            data: testsuiteData,
            contentType: 'application/json',
            dataType: 'Json'
        });
    }

    this.deleteTestSuite = function (testsuiteData) {
        var IsConf = confirm('You are about to delete ' + testsuiteData.TestSuite1 + '. Are you sure?');
        
        if (IsConf) {

            return $http({

                method: 'POST',
                url: '/Validation/DeleteTestSuite',
                data: testsuiteData,
                contentType: 'application/json',
                dataType: 'Json'

            });
        }
        
    }

});


app.service("LoginService", function ($http) {
    //initilize factory object.
    var fact = {};
    fact.getUserDetails = function (userdata) {
        debugger;
        if (userdata.Role === 'Admin') {
            var urllink = '/Validation/LoginAdmin';
        }
        if (userdata.Role === 'Operator') {
            var urllink = '/Validation/LoginEditor';
        }
        if (userdata.Role === 'Manager') {
            var urllink = '/Validation/LoginExecutor';
        }
        else
        {
            '/Validation/GetLoginData'
        }          
        return $http({
            url: urllink,
            method: 'POST',
            data: userdata,
            contentType: 'application/json',
            dataType: 'Json'
        }
        );
    };
    return fact;
});

app.service('RegistrationService', function ($http) {

    var fac = {};   
        fac.RegisetrformData = function (userdata) {
        debugger;
        if (userdata.Role === 'Admin')
        {
            var urllink = '/Validation/RegisterAdmin';
        }
        if (userdata.Role === 'Operator') {
            var urllink = '/Validation/RegistorOperator';
        }
        if (userdata.Role === 'Manager') {
            var urllink = '/Validation/RegisterManager';
        }
        debugger;
        return $http({
            url: urllink,
            method: 'POST',
            data: userdata,
            contentType: 'application/json',
            dataType: 'Json'
        })

    }

        return fac;
});
app.service('Chartservice', function ($http) {
    var chartdata = function (chardata) {
        
        return $http({
            url: '/DatapointsController/Index',
            method: 'POST',
            data: chardata,
            contentType: 'application/json',
            dataType: 'Json'
        })

    }

    return chartdata;
});