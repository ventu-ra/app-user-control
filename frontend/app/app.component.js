app.controller("BemVindoController", function ($scope, $window) {
  $scope.token = localStorage.getItem("jwtToken");
  $scope.User = localStorage.getItem("User");

  console.log($scope.User)
  if (!$scope.token) {
    $window.location.href = "#!/";
  }

  $scope.logout = function () {
    localStorage.removeItem("jwtToken");
    $window.location.href = "#!/";
  };
});
