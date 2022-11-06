$env:DOCKER_BUILDKIT = 1 

docker build -t tagit-server:dev -f .\.docker\host.Dockerfile .
docker build -t tagit-worker:dev -f .\.docker\worker.Dockerfile .

docker tag tagit-server:dev skycontainers.azurecr.io/tagit-server:latest
docker push skycontainers.azurecr.io/tagit-server:latest

docker tag tagit-worker:dev skycontainers.azurecr.io/tagit-worker:latest
docker push skycontainers.azurecr.io/tagit-worker:latest