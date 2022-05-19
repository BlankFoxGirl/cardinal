# Cardinal
Cardinal is a scalable distributed event driven MMORPG Server framework used to provide development simplicity through event definitions and provides Terraform for all major CSPs to deploy Kubernetes pods and supports AWS ECS.


## Basic Setup.
1. `$ cd Core`.
1. `$ dotnet build`.
1. `$ cd ../Worker`.
1. `$ dotnet run`.

## Expected Features:
- Unit Tests.
- Switching to TDD.
- Dockerfile for each Node.
- Compose.yaml file to instantiate development environment.
- Build scripts to control creating own images and publishing to docker.
- Terraform to create and maintain your own deployments.
- Cloudformation (An extension of the above.)
- Kubernetes configuration to deploy your own K8 pods.