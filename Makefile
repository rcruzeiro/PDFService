filter= ""

.PHONY: network
network:
	docker network create shared

.PHONY: restore
restore:
	dotnet restore PDFService.sln

.PHONY: build
build: stop restore
	docker-compose build

.PHONY: up
up:
	docker-compose up -d

.PHONY: stop
stop:
	docker-compose stop

.PHONY: down
down:
	docker-compose down

.PHONY: attach
attach: up
	docker-compose exec pdfservice.api bash

.PHONY: mysql
mysql: up
	docker-compose exec database mysql -u root -h database -psecret --database pdfservice
    
.PHONY: migrate
migrate: up
	cd pdfservice.repository.mysql && dotnet ef database update

.PHONY: migrate-script
migrate-script: up
	cd pdfservice.repository.mysql && dotnet ef migrations script

.PHONY: prune
prune: stop
	docker system prune

.PHONY: release
release:
	git tag -a $(version) -m "$(comment)"
