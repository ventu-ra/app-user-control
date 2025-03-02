app.controller("BemVindoController", function ($scope, $window) {
  $scope.token = localStorage.getItem("jwtToken");

  if (!$scope.token) {
    $window.location.href = "#!/";
  }

  $scope.logout = function () {
    localStorage.removeItem("jwtToken");
    $window.location.href = "#!/";
  };
});
