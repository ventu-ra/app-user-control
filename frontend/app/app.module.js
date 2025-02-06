var app = angular.module("app", ["ngRoute", "loginModule"]);

app.config(function ($routeProvider) {
  $routeProvider
    .when("/", {
      templateUrl: "login/login.template.html",
      controller: "LoginController"
    })
    .when("/bem-vindo", {
      templateUrl: "bem-vindo.html",
      controller: "BemVindoController"
    })
    .otherwise({
      redirectTo: "/"
    });
});
