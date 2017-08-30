var app;
(function () {
    app = angular.module("myApp", [])
})();

app.config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});
