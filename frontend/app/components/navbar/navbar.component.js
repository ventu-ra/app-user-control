angular.module("navbarModule", [])
  .component("navbar", {
    templateUrl: "components/navbar/navbar.template.html",
    controller: function ($scope, $window, authService, $location) {
      this.user = $window.localStorage.User
      this.isAuthenticated = authService.isAuthenticated();
      console.log($window.localStorage.User);

      // this.logout = function () {
      //   localStorage.removeItem("token");
      //   $location.path("/login");
      // };

      $scope.logout = function () {
        localStorage.removeItem("jwtToken");
        $window.location.href = "#!/";
      };
    }
  });
