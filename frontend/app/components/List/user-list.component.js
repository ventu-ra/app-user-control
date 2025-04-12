userModule.component("usuarioListComponent", {
  templateUrl: "/components/List/user-list.component.html",
  controller: function ($http) {
    var vm = this;
    vm.usuarios = [];
    vm.cont = 0;

    // Função para carregar os usuários
    vm.$onInit = function () {
      var token = localStorage.getItem("jwtToken"); // Obtém o token JWT
      $http.get("http://localhost:5130/api/User", {
        headers: {
          "Authorization": "Bearer " + token
        }
      })
        .then(function (response) {
          vm.usuarios = response.data.users;
          vm.usuarios = vm.usuarios.map(function (usuario) {
            const cpf = usuario.cpf.replace(/\D/g, '');
            const ultimos = cpf.slice(-3);
            usuario.cpf = '***.***.**-' + ultimos;
            return usuario;
          })

        })
        .catch(function (error) {
          console.error("Erro ao carregar usuários:", error);
        });
    };
  }
});
