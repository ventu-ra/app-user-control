userModule.component("usuarioListComponent", {
  template: `
    <div class="container mt-5">
      <h2>Lista de Usuários</h2>
      <div class="table-responsive">
        <table class="table table-bordered table-striped">
          <thead>
            <tr>
              <th>#</th>
              <th>Nome</th>
              <th>CPF</th>
              <th>Endereço</th>
              <th>Telefone</th>
            </tr>
          </thead>
          <tbody>
            <tr ng-repeat="usuario in $ctrl.usuarios">
              <td>{{$index + 1}}</td>
              <td>{{usuario.nome}}</td>
              <td>{{usuario.cpf}}</td>
              <td>{{usuario.endereco}}</td>
              <td>{{usuario.telefone}}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  `,
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

        })
        .catch(function (error) {
          console.error("Erro ao carregar usuários:", error);
        });
    };
  }
});
