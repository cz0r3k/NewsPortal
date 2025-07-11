all: run
    
build:
	dotnet build --configuration Release
    
test:
	dotnet test --configuration Release

run:
	dotnet run --project NewsPortal.Api --configuration Release
    
docker-build:
	docker compose build

docker-run:
	docker compose up
