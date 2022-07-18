# Git and Github Stuff

## before u start
- check your git config
- login https://github.com/

```powershell
# show user name and email
git config --global user.name
git config --global user.email

# set user name and email
# same as your github profile
git config --global user.name  "John Wayne"
git config --global user.email "john.wayne@example.com"
```

# steps to create new repository

- we start on the local computer
- go to the project in your source directory (``c:\dev\blazor``)
- open powershell or Visual Studio Code (``code .``)

## exclude files
- create ``.gitingore`` to exclude files we dont want to upload
- it can be temp or build files etc.

## create local repository
``git init``

## add all files of current folder to repository
``git add .``

## check files was excluded
``git status``

## commit files
``git commit -m "first commit"``

## create a new repository on github.com
- u cannot do that from command line, use the web interface
- u have to select the license, public/private and many other options

## link the local repo to the remote repository

- the name must not be ``origin``, it can be ``blazor``, ``santaclause``, ``whatever``
- it is a convenient shortcut for the url

``git remote add origin https://github.com/omni-trash/Blazor.git``

## set main branch
- i think before 2022 it was master but it was changed to main

``git branch -M main``

## upload files
``git push -u origin main``

# steps to clone a remote repository

- we start on the local computer
- go to the source root directory (``c:\dev``)

## clone

```powershell
# powershell
cd c:\dev

# download repository
git clone https://github.com/omni-trash/Blazor.git

# start Visual Studio Code
cd c:\dev\Blazor
code .
```

> that is all, u done

> happy coding
