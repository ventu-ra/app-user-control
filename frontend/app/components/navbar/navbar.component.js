angular.module("navbarModule", [])
  .component("navbar", {
    templateUrl: "components/navbar/navbar.template.html",
    controller: function ($window, authService, $location) {

      this.isAuthenticated = authService.isAuthenticated();

      this.logout = function () {
        localStorage.removeItem("token");
        $location.path("/login");
      };
    }
  });
