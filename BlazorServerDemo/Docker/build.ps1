<#
Build and run docker image.

You have to clear the publish folder before.
Then build and publish the Blazor App.
After all you can run this script to create an docker image.

S. Weihrauch
09.07.2022
#>

# Current directory of build.ps
Set-Location -Path $PSScriptRoot

# my format is: 1.22.7.10
$version = Get-Date -Format "1.y.M.d"
docker build -t blazorserverdemo:$version .
docker save blazorserverdemo:$version -o .\image\blazorserverdemo.$version.tar

# Start Container Variant 1
# 8088 is our local port
# 80 is the port in the container app
#docker run -p 8088:80 blazorserverdemo:$version

# Start Container Variant 2
# use random local port
# container port is from dockerfile (EXPOSE 80)
#docker run -p :80 blazorserverdemo:$version
