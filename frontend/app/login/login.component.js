loginModule.controller("LoginController", function ($scope, $http, $window) {
  $scope.usuario = {};
  $scope.mensagem = "";

  $scope.fazerLogin = function () {
    var loginData = {
      Usuario: $scope.usuario.username,
      Senha: $scope.usuario.password
    };

    $http.post("http://localhost:5085/api/Usuario/Login", loginData)
      .then(function (response) {
        if (response.data.token) {
          localStorage.setItem("jwtToken", response.data.token);
          console.log("Token salvo:", response.data.token);
          $window.location.href = "#!/bem-vindo"; // Redireciona para a página de bem-vindo
        } else {
          $scope.mensagem = "Erro ao autenticar!";
        }
      })
      .catch(function () {
        $scope.mensagem = "Usuário ou senha inválidos!";
      });
  };
});
