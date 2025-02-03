angular.module('login').controller("LoginController", function ($scope, $location) {
  $scope.usuario = { nome: "", senha: "" };

  $scope.login = function () {
    if ($scope.usuario.nome === "SISTEMA" && $scope.usuario.senha === "canditado123") {
      $location.path("/cadastro");
    } else {
      alert("Usuário ou senha incorretos!");
    }
  };
});
