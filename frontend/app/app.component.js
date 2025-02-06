app.controller("BemVindoController", function ($scope, $window) {
  $scope.token = localStorage.getItem("jwtToken");

  if (!$scope.token) {
    console.log("Nenhum token encontrado. Redirecionando para login...");
    $window.location.href = "#!/";
  }

  $scope.logout = function () {
    localStorage.removeItem("jwtToken");
    console.log("Token removido! Redirecionando para login...");
    $window.location.href = "#!/";
  };
});
