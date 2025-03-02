var app = angular.module("app", ["ngRoute", "userModule", "loginModule", "navbarModule", "authModule"]);

app.config(function ($routeProvider) {
  $routeProvider
    .when("/", {
      templateUrl: "login/login.template.html",
      controller: "LoginController"
    })
    .when("/bem-vindo", {
      templateUrl: "bem-vindo.html",
      controller: "BemVindoController",
      resolve: {
        auth: function ($location, authService) {
          if (!authService.isAuthenticated()) {
            $location.path("/login");
          }
        }
      }
    })
    .when("/app", {
      templateUrl: "app.template.html",
      resolve: {
        auth: function ($location, authService) {
          if (!authService.isAuthenticated()) {
            $location.path("/login");
          }
        }
      }
    })
    .when("/cadastro", {
      templateUrl: "user/user.template.html",
      resolve: {
        auth: function ($location, authService) {
          if (!authService.isAuthenticated()) {
            $location.path("/login");
          }
        }
      }
    })
    .otherwise({
      redirectTo: "/"
    });
});
