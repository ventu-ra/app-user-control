loginModule.controller("LoginController", function ($scope, $http, $window) {
  $scope.usuario = {};
  $scope.mensagem = "";

  $scope.fazerLogin = function () {
    var loginData = {
      name: $scope.usuario.username,  // Corrigido: username ao invés de Usuario
      password: $scope.usuario.password   // Corrigido: password ao invés de Senha
    };

    $http.post("http://localhost:5130/api/Login", loginData)
      .then(function (response) {
        if (response.data.accessToken) {
          localStorage.setItem("jwtToken", response.data.accessToken);
          localStorage.setItem("User", response.data.name)
          $window.location.href = "#!/bem-vindo";
        } else {
          $scope.mensagem = "Erro ao autenticar!";
        }
      })
      .catch(function () {
        $scope.mensagem = "Usuário ou senha inválidos!";
      });
  };
});
