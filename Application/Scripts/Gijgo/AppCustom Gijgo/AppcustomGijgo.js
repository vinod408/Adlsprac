    //@*gijgoscript*@

var grid, dialog, dialogtest,typeid;
    function Popup(e) {
        var dt = new Date(parseInt(e.data.record.CreatedDate.substr(6)));
        $('#TestSuiteId').val(e.data.id);
        $('#TestSuite1').val(e.data.record.TestSuite1);
        $('#TestSuiteDescription').val(e.data.record.TestSuiteDescription);
        $('#IsActive').prop('checked', e.data.record.IsActive);
        $('#TestSuiteOwner').val(e.data.record.TestSuiteOwner);
        $('#CreatedDate').val(dt);
        $('#CreatedBy').val(e.data.record.CreatedBy);
        $('#btnUpdate').show();
        $('#btnDelete').show();
        $('#btnSave').hide();
        dialog.open('TestSuite');
    }
    function Popupparams(e) {
        debugger
         typeid = e.data.record.TestCaseTypeId;
         var id= e.data.record.TestCaseId;
         if (typeid === 1)
        { 
            //assigning typecaseid
            $('#RowTestCaseId').val(typeid);

            //clearing previous val
            $('#Rtablename').val(''),
            $('#RSourceFilePath').val(''),
            $('#RDestinationFilePath').val(''),
            $('#RFilesColumnsCount').val(0),
            dialogparamsR.open('Add Parameters For Row Count');
        }
         else if (typeid === 2)
        {
            //assigning typecaseid
            $('#PrimarykeyTestCaseId').val(typeid);

            //clearing previous val
        
             $('#PKtablename').val(''),
            $('#PKColumnname').val(''),
            $('#FKtablename').val(''),
            $('#FKColumnname').val(''),

            dialogparamsPrimarykey.open('Add Parameters For Primary Key');
        }
         else if (typeid === 3) {
            //assigning typecaseid
            $('#FKTestCaseId').val(typeid);

            //clearing previous val
            $('#PrimaryKeytablename').val(''),
            $('#PrimaryKeyColumn').val(''),
            dialogparamsFK.open('Add Parameters For Foreign Key');
         }
         else if (typeid === 4) {
             //assigning typecaseid
             $('#VCTestCaseTypeId').val(typeid);

             //clearing previous val
             $('#VCtablename').val(''),
             $('#VCPKColumnname').val(''),
             $('#VCColumnname').val(''),
             $('#VCDestinationFilePath').val(''),
             $('#VCFilesColumnsCount').val(''),
             $('#VCFilesColumnsName').val(''),
             $('#VCSourcewhereClauseFilter').val(''),
             $('#VCDestinationwhereClauseFilter').val(''),
             dialogparamsValuecheck.open('Add Parameters For Value Check');
         }
    }
    function Popuptest(e) {
        var dt = new Date(parseInt(e.data.record.CreatedDate.substr(6)));
        debugger
        $('#TestCaseId').val(e.data.id);
        $('#PipelineStageId').val(e.data.record.PipelineStageId);
        $('#TestCaseTypeId').val(e.data.record.TestCaseTypeId);
        $('#TestCaseName').val(e.data.record.TestCaseName);
        $('#Description').val(e.data.record.Description);
        $('#IsActive1').prop('checked', e.data.record.IsActive);
        $('#IsObsolete').prop('checked', e.data.record.IsObsolete);
        $('#ObsoleteReason').val(e.data.record.ObsoleteReason);
        $('#CreatedDate').val(dt);
        $('#CreatedBy1').val(e.data.record.CreatedBy);
        $('#tbtnUpdate').show();
        $('#tbtnDelete').show();
        $('#tbtnSave').hide();
        dialogtest.open('TestCase');
    }
    function Save() {
        var record = {
            TestSuiteId: $('#TestSuiteId').val(),
            TestSuite1: $('#TestSuite1').val(),
            TestSuiteDescription: $('#TestSuiteDescription').val(),
            IsActive: $('#IsActive').prop('checked'),
            TestSuiteOwner: $('#TestSuiteOwner').val(),
            CreatedDate: $('#CreatedDate').val(),
            CreatedBy: $('#CreatedBy').val(),
        };
        if (record.TestSuiteId != 0) {

            $.ajax({ url: '/Validation/UpdateTestSuite', data: record, method: 'POST' })
            .done(function (e) {
                debugger
                var $log = $("#lalert"),
                str = "<p class='alert alert-dismissable'>TestSuite Updated Successfully<a href='#' class='close' data-dismiss='alert' aria-label='close'>×</A></p>"
                html = $.parseHTML(str),
                nodeNames = [];
                // Append the parsed HTML
                $log.append(html);
              
                dialogupdate.open("");
                dialog.close();

                grid.reload();
            })
            .fail(function () {
                $('#lerror').text('We cant delete the this item which is referencing in other tables')
                dialog.close();
            });
        }
        else {

            $.ajax({ url: '/Validation/SaveTestSuite', data: record, method: 'POST' })
            .done(function (e) {
                debugger
                if (e.status == 'success')
                {
                    dialogsuccess.open("");
                    dialog.close();
                    grid.reload();
                }
                else {
                    alert("Please Enter Required Fields(*)")
                }
            })
            .fail(function () {
                alert('Failed to save.');
                dialog.close();
            });
        }

    }
    function Delete() {
        var record = {
            TestSuiteId: $('#TestSuiteId').val(),
            TestSuite1: $('#TestSuite1').val(),
            TestSuiteDescription: $('#TestSuiteDescription').val(),
            TestSuiteOwner: $('#TestSuiteOwner').val(),
            CreatedDate: $('#CreatedDate').val(),
            CreateBy: $('#CreateBy').val(),
        };

        $.ajax({ url: '/Validation/DeleteTestSuite', data: record, method: 'POST' })
                        .done(function () {
                            dialogdelete.open("");
                            dialog.close();
                            grid.reload();

                        })
                        .fail(function () {
                            alert('Failed to delete.');
                        });

    }
    function Savetestcase() {
        debugger
        var obsr = '';
        if ($('#IsObsolete').prop('checked'))
        {
            obsr = $('#ObsoleteReason').val();
        }
        var record = {
            TestCaseId: $('#TestCaseId').val(),
            TestCaseName: $('#TestCaseName').val(),
            TestSuiteId: $('#TestSuiteId1').val(),
            PipelineStageId: $('#PipelineStageId').val(),
            TestCaseTypeId: $('#TestCaseTypeId').val(),
            Description: $('#Description').val(),
            IsActive: $('#IsActive1').prop('checked'),
            IsObsolete: $('#IsObsolete').prop('checked'),
            ObsoleteReason: obsr,
            CreatedDate: $('#CreatedDate').val(),
            CreatedBy: $('#CreatedBy1').val(),
        };

        if (record.TestCaseId != 0) {
            $.ajax({ url: '/Validation/kendoUpdateGettestcases', data: record, method: 'POST' })
            .done(function () {
                var $log = $("#lalert"),
                str = "<p class='alert alert-dismissable'>TestSuite Updated Successfully<a href='#' class='close' data-dismiss='alert' aria-label='close'>×</A></p>"
                html = $.parseHTML(str),
                nodeNames = [];
                // Append the parsed HTML
                $log.append(html);
                dialogupdate.open("");
                dialogtest.close();
                grid.reload();
            })
            .fail(function () {
                $('#lerror').text('We cant delete the this item which is referencing in other tables')
                dialogtest.close();
            });
        }
        else {
            $.ajax({ url: '/Validation/kendosaveGettestcases', data: record, method: 'POST' })
            .done(function (e) {
                //alert(JSON.stringify(response.success));
                //if (response.success == false) {
                //    alert("Problem in saving Testsuite")
                //};
                if (e.status == 'success') {
                    dialogsuccess.open("");
                    dialogtest.close();
                    grid.reload();
                }
                else {
                    alert("Please Enter Required Fields(*)")
                }
              
            })
            .fail(function () {
                alert('Failed to save.');
                dialogtest.close();
            });
        }

    }
    function DeleteTestcase() {
        var record = {
            TestCaseId: $('#TestCaseId').val(),
        };
        $.ajax({ url: '/Validation/DeleteTestCase', data: record, method: 'POST' })
                        .done(function () {
                            dialogdelete.open("");
                            dialogtest.close();
                            grid.reload();

                        })
                        .fail(function () {
                            alert('Failed to delete.');
                        });

    }
    function SaveParamsRow() {
        debugger
        if (typeid === 1)
        {
            var record = {
                TestCaseId: $('#RowTestCaseId').val(),
                TableName: $('#Rtablename').val(),
                SourceFilePath: $('#RSourceFilePath').val(),
                DestinationFilePath: $('#RDestinationFilePath').val(),
                FileColumnsCount: parseInt($('#RFilesColumnsCount').val()),
                CreatedBy: 'ucmdwsuper'
            };
            $.ajax({ url: '/Validation/SaveRowCountParameters', data: record, method: 'POST' })
          .done(function (e) {
              if (e.status == 'success') {
                  dialogparamsR.close();
                  dialogsuccess.open("");
                  grid.reload();
              }
              else {
                  alert("Please Enter Required Fields(*)")
              }
             
          })
          .fail(function () {
              $('#lerror').text('Failed to save RowCount Parameters')
              dialogparamsR.close();
          });
        }
        else if(typeid===2)
        {
            var record = {
                TestCaseId: $('#PrimarykeyTestCaseId').val(),
                TableName: $('#PrimaryKeytablename').val(),
                PrimaryKeyColumn: $('#PrimaryKeyColumn').val(),
                CreatedBy: 'ucmdwsuper'
            };
            $.ajax({ url: '/Validation/SavePrimaryKeyParameters', data: record, method: 'POST' })
          .done(function (e) {
              if (e.status == 'success') {
                  dialogsuccess.open("");
                  dialogparamsPrimarykey.close();
                  grid.reload();
              }
              else {
                  alert("Please Enter Required Fields(*)")
              }
            
          })
          .fail(function () {
              $('#lerror').text('Failed to save RowCount Parameters')
              dialogparamsPrimarykey.close();
          });
        }
        else if (typeid === 3) {
            var record = {
                TestCaseId: $('#FKTestCaseId').val(),
                PrimaryKeyTableName:  $('#PKtablename').val(),
                PrimaryKeyColumn:    $('#PKColumnname').val(),
                ForeignKeyTableName: $('#FKtablename').val(),
                ForeignKeyColumn:    $('#FKColumnname').val(),
                CreatedBy: 'ucmdwsuper'
            };
            $.ajax({ url: '/Validation/SaveFKParameters', data: record, method: 'POST' })
          .done(function (e) {
              if (e.status == 'success') {
                  dialogsuccess.open("");
                  dialogparamsFK.close();
                  grid.reload();
              }
              else {
                  alert("Please Enter Required Fields(*)")
              }
              
          })
          .fail(function () {
              $('#lerror').text('Failed to save RowCount Parameters')
              dialogparamsFK.close();
          });
        }
        else if (typeid === 4) {
            var record = {
                TestCaseId: $('#VCTestCaseTypeId').val(),
                TableName: $('#VCtablename').val(),
                PrimaryKeyColumnName: $('#VCPKColumnname').val(),
                ValueCheckColumnName: $('#VCColumnname').val(),
                DestinationFilePath: $('#VCDestinationFilePath').val(),
                FileColumnsCount: parseInt($('#VCFilesColumnsCount').val()),
                FileColumnsName: $('#VCFilesColumnsName').val(),
                SourcewhereClauseFilter: $('#VCSourcewhereClauseFilter').val(),
                DestinationwhereClauseFilter: $('#VCDestinationwhereClauseFilter').val(),
                CreatedBy: 'ucmdwsuper'
            };
            $.ajax({ url: '/Validation/SaveValueCheckParams', data: record, method: 'POST' })
          .done(function (e) {
              if (e.status == 'success') {
                  dialogsuccess.open("");
                  dialogparamsValuecheck.close();
                  grid.reload();
              }
              else {
                  alert("Please Enter Required Fields(*)")
              }
              
          })
          .fail(function () {
              $('#lerror').text('Failed to save RowCount Parameters')
              dialogparamsValuecheck.close();
          });
        }
        

    }

    function showMe(it, box) {
        var vis = (box.checked) ? "block" : "none";
        document.getElementById(it).style.display = vis;
    }
    function schedul() {
        dialogsched.open("Schedule the Event");
        $.ajax({
            type: "GET",
            url: "/Vaidation/_schedulerpartial",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                debugger;
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    };

    $('#btnAdd').on('click', function () {
        $('#TestSuiteId').val(0);
        $('#TestSuite1').val('');
        $('#TestSuiteDescription').val('');
        $('#IsActive').prop("checked", false);
        $('#TestSuiteOwner').val('');
        $('#CreatedDate').val('');
        $('#CreatedBy').val('');
        $('#btnUpdate').hide();
        $('#btnDelete').hide();
        $('#btnSave').show();
        dialog.open('Create TestSuite');
    });
    $('#btnSave').on('click', Save);
    $('#btnUpdate').on('click', Save);
    $('#btnDelete').on('click', Delete);
    $('#btnCancel').on('click', function () {
        dialog.close();
    });

    $('#tbtnSave').on('click', Savetestcase);
    $('#tbtnUpdate').on('click', Savetestcase);
    $('#tbtnDelete').on('click', DeleteTestcase);
    $('#tbtnCancel').on('click', function () {
        dialogtest.close();
    });

    $('.paramssave').on('click', SaveParamsRow);
    $('.paramsclose').on('click', function () {
        dialogparamsR.close();
        dialogparamsPrimarykey.close();
        dialogparamsFK.close();
        dialogparamsValuecheck.close();
    });

    $('#btnSearch').on('click', function () {
        grid.reload({ page: 1, TestSuite1: $('#txtTestsuite').val(), IsActive: $('#txtIsactive').prop('checked') });
    });
    $('#btnClear').on('click', function () {
        $('#txtTestsuite').val('');
        $('#txtIsactive').prop('checked', false);
        grid.reload({ page: 1, TestSuite1: '', IsActive: '' });
    });

    $(document).ready(function () {
        grid = $('#grid').grid({
            primaryKey: 'TestSuiteId',
            responsive: true,
            uiLibrary: 'bootstrap4',
            iconsLibrary: 'fontawesome',
            dataSource: '/Validation/jqueryGettestsuites',

            columns: [
                { field: 'TestSuiteId', sortable: true, width: 64,title:'Id' },
                { field: 'TestSuite1', sortable: true, editor: false, title: 'TestSuiteName', minWidth: 120, priority: 1 },
                { field: 'TestSuiteDescription', editor: false, priority: 2, minWidth: 160 },
                { field: 'IsActive', editor: false, minWidth: 80, priority: 1 },
                { field: 'TestSuiteOwner', editor: false, priority: 2, minWidth: 160 },
                { field: 'CreatedDate', title: 'Created Date', type: 'date', format: 'mm/dd/yyyy', sortable: true, minWidth: 120, priority: 2 },
                { field: 'CreatedBy', title: 'Created By', minWidth: 120, priority: 2 },
                { width: 60, tmpl: '<span class="material-icons gj-cursor-pointer">edit</span>',title:'Edit', align: 'center', events: { 'click': Popup } },
                //{ width: 64, tmpl: '<span class="material-icons gj-cursor-pointer">delete</span>', align: 'center', events: { 'click': Popup } }
            ],
            pager: { limit: 8 },

            detailTemplate: ' <div style="padding-left:3em" class="col-lg-12 col-md-12"> '+
                '<div><button id="tbtnAdd" type="button" class="pull-left gj-button-md" style="color:#e45200;"><span class="material-icons gj-cursor-pointer">add</span><b>Add New TestCase</b></button> '+
                '<form class="pull-right display-inline"><input id="txtTestcase" type="text" placeholder="Testcasename.." class="gj-textbox-md gj-display-inline-block gj-width-200" />IsActive &nbsp;<input id="xtIsactive" type="checkbox"class="gj-display-inline-block"/>&nbsp;&nbsp;&nbsp;&nbsp;IsObsolete&nbsp;<input id="xtIsObsolete" type="checkbox"class="gj-display-inline-block"/><button id="tbtnSearch" type="button" class="gj-button-md" style="color:#e45200;">Search</button><button id="tbtnClear" type="button" class="gj-button-md">Clear</button></form></div>'+
                '<table/></div>'

        });
        grid.on('detailExpand', function (e, $detailWrapper, id) {
            $('#TestSuiteId1').val(id);
            var subgrid = $detailWrapper.find('table').grid({
                params: { Id: id },
                uiLibrary: 'bootstrap4',
                responsive: true,
                iconsLibrary: 'fontawesome',
                primaryKey: 'TestCaseId',
                //inlineEditing: { mode: 'command' },
                dataSource: '/Validation/jqueryGettestcases',
                columns: [
                { field: 'TestCaseId', sortable: true, title: 'Id', width: 50 },
                    { field: 'TestCaseName', sortable: true, minWidth: 200 },
                { field: 'Description', sortable: true, minWidth: 200, priority: 2 },
                { field: 'IsActive', title: 'Active', sortable: true, width: 70 },
                { field: 'IsObsolete', title: 'Obsolete', sortable: true, width : 80 },
                { field: 'ObsoleteReason', Width: 80, priority: 2 },
                //{ field: 'PipelineStageId', hidden: true },
                //{ field: 'TestSuiteId', hidden: true },
                //{ field: 'TestCaseTypeId', hidden: true },
                { width: 50, tmpl: '<span class="material-icons gj-cursor-pointer">edit</span>',title:'Edit', align: 'center', events: { 'click': Popuptest }},
                { width: 56, tmpl: '<span class="material-icons gj-cursor-pointer">event</span>', title: 'Event', align: 'center', events: { 'click': windowschedulerpopup } },
                { width: 70, tmpl: '<span class="material-icons gj-cursor-pointer">add</span>', title: 'Params', align: 'center', events: { 'click': Popupparams} }

                ],
                pager: { limit: 5 }


            });
            subgrid.on('rowDataChanged', function (e, id, record) {
                alert(JSON.stringify(record)),
                $.ajax({ url: '/Validation/kendoUpdateGettestcases', data: record, method: 'POST' })
                    .done(function () {
                        grid.reload();
                    })
                    .fail(function () {
                        alert('Failed to save.');
                    });
            });
            $('#tbtnAdd').on('click', function () {
                debugger
                $('#TestCaseName').val('');
                $('#Description').val('');
                $('#IsActive1').prop("checked", false);
                $('#IsObsolete').prop("checked", false);
                $('#ObsoleteReason').val('');
                $('#CreatedDate').val('');
                $('#CreatedBy1').val('');
                $('#tbtnUpdate').hide();
                $('#tbtnDelete').hide();
                $('#tbtnSave').show();
                dialogtest.open('TestCase');
            });

            $('#tbtnSearch').on('click', function () {
                subgrid.reload({ page: 1, TestCaseName: $('#txtTestcase').val(), IsActive: $('#xtIsactive').prop('checked'), IsObsolete: $('#xtIsObsolete').prop('checked') });
            });
            $('#tbtnClear').on('click', function () {
                debugger
                $('#txtTestcase').val('');
                $('#xtIsactive').prop('checked', false);
                $('#xtIsObsolete').prop('checked', false);
                subgrid.reload({ page: 1, TestCaseName: '', IsActive: '',IsObsolete:''});
            });
        });
        grid.on('detailCollapse', function (e, $detailWrapper, id) {
            $detailWrapper.find('table').grid('destroy', true, true);
        });

        grid.on('rowDataChanged', function (e, id, record) {
            //alert(JSON.stringify(record)),
            $.ajax({ url: '/Validation/UpdateTestSuite', data: record, method: 'POST' })
                .done(function () {
                    grid.reload();
                })
                .fail(function () {
                    alert('Failed to save.');
                });
        });
        grid.on('rowRemoving', function (e, $row, id, record) {
            //alert(JSON.stringify(id));
            if (confirm('Are you sure?')) {
                $.ajax({ url: '/Validation/DeleteTestSuite', data: record, method: 'POST' })
                    .done(function () {
                        grid.reload();
                    })
                    .fail(function () {
                        alert('Failed to delete.');
                    });
            }
        });


        //dialog assigning
        dialog = $('#dialog').dialog({
            uiLibrary: 'bootstrap4',
            autoOpen: false,
            resizable: false,
            modal: true,
            width: 360
        });
        dialogtest = $('#dialogTestCase').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: false,
            modal: true,
            width: 430
        });
        dialogparamsR = $('#dialogparamsR').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: false,
            width: 450
        });
        dialogparamsPrimarykey = $('#dialogparamsPrimarykey').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: false,
            width: 450
        });
        dialogparamsFK = $('#dialogparamsFK').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: false,
            width: 450
        });
        dialogparamsValuecheck= $('#dialogparamsValuecheck').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: false,
            width:550
        });
        dialogsuccess = $('#dialogsuccess').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: false,
            width: 350
        });
        dialogupdate = $('#dialogupdate').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: false,
            width: 350
        });
        dialogdelete= $('#dialogdelete').dialog({
            uiLibrary: 'bootstrap',
            autoOpen: false,
            resizable: true,
            modal: true,
            title:'Message',
            width: 350
        });
    });
