loginModule.controller("LoginController", function ($scope, $http, $window) {
  $scope.usuario = {};
  $scope.mensagem = "";

  $scope.fazerLogin = function () {
    var loginData = {
      username: $scope.usuario.username,  // Corrigido: username ao invés de Usuario
      password: $scope.usuario.password   // Corrigido: password ao invés de Senha
    };

    $http.post("http://localhost:5198/api/auth/login", loginData)
      .then(function (response) {
        if (response.data.token) {
          localStorage.setItem("jwtToken", response.data.token);
          console.log("Token salvo:", response.data.token);
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
