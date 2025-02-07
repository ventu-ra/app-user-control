userModule.controller("UsuarioController", function ($scope, $http) {
  $scope.usuario = {};
  $scope.mensagem = "";

  // Função para cadastrar o usuário
  $scope.cadastrar = function () {
    var token = localStorage.getItem("jwtToken"); // Obtém o token do localStorage

    console.log("TOKEN: ", token); // Verifique se o token está correto

    $http.post("http://localhost:5198/api/usuario/cadastrar", $scope.usuario, {
      headers: {
        "Authorization": "Bearer " + token // Adiciona o token no cabeçalho
      }
    })
      .then(function (response) {
        $scope.mensagem = response.data;
        $scope.usuario = {}; // Limpa o formulário após o cadastro
        console.log("Erro no cadastro:", response);
      })
      .catch(function (error) {
        console.error("Erro no cadastro:", error);
        $scope.mensagem = "Erro ao cadastrar usuário!";
      });
  };

  // Função para listar os usuários
  $scope.listarUsuarios = function () {
    var token = localStorage.getItem("jwtToken");

    $http.get("http://localhost:5198/api/Usuario/listar", {
      headers: {
        "Authorization": "Bearer " + token
      }
    })
      .then(function (response) {
        $scope.usuarios = response.data;
      })
      .catch(function (error) {
        console.error("Erro ao listar usuários:", error);
      });
  };
});

