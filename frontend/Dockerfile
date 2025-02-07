# Usa uma imagem do Node.js para rodar o frontend
FROM node:18-alpine

# Define o diretório de trabalho no container
WORKDIR /app

# Copia os arquivos do projeto para dentro do container
COPY . .

# Instala as dependências
RUN npm install -g http-server

# Expõe a porta 3000
EXPOSE 3000

# Comando para iniciar o frontend
CMD ["http-server", "./app", "-a", "0.0.0.0", "-p", "3000", "-c-1"]
