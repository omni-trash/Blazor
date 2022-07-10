# Docker

> This is not a docker documentation

> Sticky notes for me to remember some steps

> This folder is not a part of the VS2022 project

> Use Visual Studio Code separately

Kurzübersicht

- Docker urprünglich Linux, später für Windows verfügbar

- Basis hier ist Windows, also unser Entwicklungs-Rechner

- DockerDesktop for Windows (https://docs.docker.com/desktop/windows/install/)

- CPU Virtualization Extensions needed

- default tagging is ``latest``

- u can use any text as tagging (``release``, ``1.0``, ``test`` ... ``whatever``)

# Host
- Im BIOS VT-x aktivieren (Intel Virtualization Technology)
- Im BIOS SVM aktivieren (AMD Secure Virtual Machine)

# Nested Virtual Machine
Wenn VM in VM, dann muss auf dem Host für die verschachtelte VM das CPU-Feature zur Virtualisierung verfügbar gemacht werden.

Trifft jetzt nur zu, wenn man mit einer VM arbeiten möchte (zB Entwicklungs-VM), und dort Docker eingesetzt werden soll.

Hier wäre die zweite verschachtelte VM die DockerDesktopVM, also wir benötigen somit Hyper-V in der ersten VM.


```powershell
# Beispiel
# Host: Test -> VM: Demo -> VM2: DockerDesktopVM
# On Host Test (VM Demo must be shutdown)
Get-VMProcessor -VMName Demo | fl
Set-VMProcessor -VMName Demo -ExposeVirtualizationExtensions $true

# Start VM Demo and install Hyper-V
```

# Container

Virtual isolated space to run apps.

> Es geht um die Container, ob da Linux oder Windows drauf laufen soll.

Target Linux
- Hyper-V (DockerDesktopVM)
- WSL 2 (Windows Subsystem for Linux)

Target Windows
- Containers (über Windows-Features aktivieren)

Die Docker Daten liegen unter ``C:\ProgramData\Docker``.

# Build Context
- Das Verzeichnis mit den Quellen. 

- Relative Pfade in der Dockerfile beziehen sich darauf. 

- Am Besten in das Verzeichnis wechseln.

- Beim Erstellen vom Image kann dann der Punkt für das aktuelle Verzeichnis gesetzt werden.

- Es kann aber auch ein Pfad für die relativen Angaben gesetzt werden.

```powershell
cd C:\Users\Steffen\source\repos\BlazorServerDemo
docker build -t name:tagging dockerfile context

# current directory
docker build -t firstimage .

# different dockerfile and context for relativ paths in dockerfile
docker build -t firstimage .\path\to\dockerfile .\bin\Release
```

# Docker Image erstellen
```powershell
docker build -t blazorserverdemo -f .\Blazor.Server\Dockerfile .

# mit tagging
docker build -t blazorserverdemo:release -f .\Blazor.Server\Dockerfile .
```

# Docker Image starten
```powershell
docker run -p 8080:80 blazorserverdemo

# mit tagging
docker run -p 8080:80 blazorserverdemo:1.0
```

# Docker Image exportieren

- export from docker and save as local image

> local image is a tar archive (use 7zip to inspect)

```powershell
docker save blazorserverdemo -o c:\temp\bob_latest.tar

# mit tagging
docker save blazorserverdemor:release -o c:\temp\bob_release.tar
```
# Docker Image importieren
- import local image to docker

```powershell
# name and tag are in the tar archive, no option to override them
docker image load --input c:\temp\bob_latest.tar

#371936e18b8b: Loading layer [==================================================>]  54.27kB/54.27kB
#94a6e0991435: Loading layer [==================================================>]  52.74kB/52.74kB
#8c59e90d70dd: Loading layer [==================================================>]   36.7MB/36.7MB
#ea00d3503dfb: Loading layer [==================================================>]  52.74kB/52.74kB
#Loaded image: blazorserverdemo:1.22.7.10
```

# Export/Import Docker Container

- Image + Environment Configuration? (Ports etc)
- Snapshot of Container's file system
- Did not work with Windows Containers

```powershell
# take the correct Container ID or Name
docker container list

# Error response from daemon: the daemon on this operating system does not support exporting Windows containers
# Pech gehabt
docker export --output C:\temp\demo_container_latest.tar "97c3d988edf2"
docker export --output C:\temp\demo_container_latest.tar jolly_panini

# omitted, export did not work ... for completness only
docker import C:\temp\demo_container_latest.tar
```


# Beispiel mit Build

- Das Dockerfile von VS2022 kopiert die Projekte (``*.csproj``) in den Container, und erstellt dieses dort dann als Release (restore + build).

- Danach wird es veröffentlicht (publish), und das Ergebnis für das Ziel-Image bereitgestellt.

>Eine Veröffentlichung ist erforderlich, da nach einem Build nicht alle Dateien im Release-Ordner liegen.

>Restore, Build und Publish findet im Docker-Container statt.

```powershell
cd C:\Users\Steffen\source\repos\BlazorServerDemo
docker build -t blazorserverdemo:1.0 -f .\Blazor.Server\Dockerfile .
docker run -p 8088:80 blazorserverdemo:1.0
```

# Beispiel mit Dateien (publish)

> Der publish-Ordner sollte zuerst manuell geleert werden!

- Die Anwendung als Release erstellen und dann Blazor.Server veröffentlichen.

- Das Profile wurde schon eingestellt ``<PublishUrl>..\Docker\publish</PublishUrl>``.

```powershell
cd C:\Users\Steffen\source\repos\BlazorServerDemo

$version = Get-Date -Format "1.yy.M.d"
docker build -t blazorserverdemo:$version -f .\Docker\Dockerfile .\Docker
docker run -p 8088:80 blazorserverdemo:$version
```

# Anonymous Volumes

- hat jetzt für mich keine Relevanz

```powershell
docker volume create

# automatically created name
# 4b32f5a39f673e4f094b51ac5a45ced94d30b963c7d97822d1b812110907c39f

# Note: that is not our local folder from host, it is the path in the container
docker run -p 8080:80 -v "c:\data" blazorserverdemo:1.22.7.10

docker run -p 8080:80 -v "4b32f5a39f673e4f094b51ac5a45ced94d30b963c7d97822d1b812110907c39f:c:\data" blazorserverdemo:1.22.7.10

# see VOLUME [ "/data" ] in Dockerfile
# ist wohl mehr für die compose sachen, create and forgot
```

# Named Volumes
- virtual volume created in docker

- anonymous volumes but with name

- ``C:\ProgramData\Docker\volumes``

```powershell
# list volumes
docker volume list

# create volume
docker volume create data

# show config
docker volume inspect data

# mout volume (starts the image with container)
docker run --mount "source=data,destination=c:\data" blazorserverdemo:latest

# run image with volume
docker run -p 8080:80 -v "data:c:\data" blazorserverdemo:latest
```

# Host Volumes

- shared folder from host (for example ``c:\temp``)

```powershell
docker run -p 8080:80 -v "c:\temp:c:\data" blazorserverdemo:latest

# use another app directory, nice for testing without build images again and again
# C:\app in container will refers to our host folder, not to that previously created in the image.
# Note: ":ro" for readonly
docker run -p 8080:80 -v "C:\Users\Steffen\source\repos\BlazorServerDemo\Docker\publish:c:\app:ro" blazorserverdemo:latest
```

# Remove Volumes

- anonymous volumes
- named volumes

```powershell
# removes all unused volumes
docker volume prune

# dont prompt for confirmation
docker volume prune --force

# remove volume by name
docker volume rm data
```