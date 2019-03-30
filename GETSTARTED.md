# Getting started with ITONK-11 TSEIS

## Installation

A few requirements are neccessary to develop on TSEIS.

- Docker
  - docker-compose
- gcloud
- Kubectl
- dotnet

### Docker

Docker is somewhat easy to get started with, and there is extensive documentation on the interwebs for it. Do take note that docker-compose is sometimes services as an external binary, the docker documentation explains how to add it. (This might only be relevant for Mac/Linux users)

### gcloud

gcloud comes bundled with google-cloud-sdk, see this site for how to download and install it.

- [google-cloud-sdk WINDOWS](https://cloud.google.com/sdk/docs/quickstart-windows)
- [google-cloud-sdk DEBIAN/UBUNTU](https://cloud.google.com/sdk/docs/quickstart-debian-ubuntu)
- [google-cloud-sdk MAC](https://cloud.google.com/sdk/docs/quickstart-macos)

Follow the installation and login with your google account. Hermansen-project, should be available as a project for you (if you've got the invitation)

Through gcloud it's possible to download kubectl, this is handy as it can generate kubeconfig for you.

### kubectl

> gcloud components install kubectl

This will add kubectl to your system. Make sure that google-cloud-sdk path is added to your path. Else kubectl might not be visible from the commandline

> kubectl get pods --all-namespaces

## Running the application

Go to the root of the git repository / clone it

> git clone https://github.com/kjuulh/ITONK-11

enter the project

> cd ITONK-11

Now we will launch the application for the testing environment

> docker-compose up --build (optional -d for detach)

The applications should now boot up, and if -d was not added the console window should be filled with text (logs)

Now go to [http://localhost:5000/api/health](http://localhost:5000/api/health)

This should output some json (simple right?)

Now it's possible to begin developing code.

## Developing

First create a new branch from master, about the feature that you want to build

> git checkout -b feature/<feature-of-your-choice>

> git push --set-upstream <branch>

### Adding a service

If a new service is to be added. Add a folder to the services folder, and copy a template over, this might be the users template etc. or a database
Change the naming of the service, it's useful to test this locally without docker, as it's important to get the naming right.

Add the service to the docker-compose file in the root of the directory.

### Running the application (again)

Using docker compose again it's possible to have the entire application in memory.

### Pushing it to production

First and foremost we need to tag a build of the specific service

> docker build -t kasperhermansen/itonk11-users:<version of the latest image + 1> .

> docker login

> docker push kasperhermansen/itonk11-users:<version>

If you receive an error with insufficient permissions, please write an email to me with your Docker ID (mine is kasperhermansen)

I will invite you, and you can now push to the repository (this might take a bit)

Now the docker image should be in the docker hub repository.

Then update the <service>-deployment.yaml file with the new image. Look for other kube.yaml files for inspiration. Each time a new iteration of the production image is produced, it's necessary to bump the image version (sadly).

To validate the changes

> kubectl get pod -n itonk-11

see your pod, and test it with

> kubectl get svc -n itonk-11

an endpoint should be generated after some time

Now push your changes to git, and create a pull-request whenever you're done. Another team-member will review the code and approve the changes.

When it's been approved, squash and merge (it's probably in the dropdown menu) the changes into the master branch and delete your feature branch (there should be a button in git on the pull-request to do this).
