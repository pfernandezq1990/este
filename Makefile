#Project variables

PROJECT_NAME ?= testreserva

.PHONY: migrations db hello

migrations:
		cd ./Reserva.Data && dotnet ef --startup-project ../Reserva.Web/ migrations add $(mname) && cd ..
db:
		cd ./Reserva.Data && dotnet ef --startup-project ../Reserva.Web/ database update && cd ..
hello:
		echo 'hello'