angular.module("authModule", [])
  .factory("authService", function ($window) {
    return {
      login: function (token) {
        $window.localStorage.setItem("jwtToken", token);
        $window.localStorage.setItem("User", User);
      },
      logout: function () {
        $window.localStorage.removeItem("jwtToken");
      },
      isAuthenticated: function () {
        return !!$window.localStorage.getItem("jwtToken");
      }
    };
  });
