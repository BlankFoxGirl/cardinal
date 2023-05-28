# Project Status:  🚨 Unmaintained 🚨

This project is no longer maintained. We will not be accepting pull requests, addressing issues, nor making future releases.

Please refer to the new project: [Cardinal C++ Rewrite](https://github.com/sarahjabado/cardinal-cpp)

### Cardinal
[Cardinal Core Website (Hosted via Github Pages)](https://cardinalcore.io)
  
Cardinal is a scalable distributed event driven MMORPG Server framework used to provide development simplicity through event definitions and provides Terraform for all major CSPs to deploy Kubernetes pods and supports AWS ECS.
  
![GitHub last commit](https://img.shields.io/github/last-commit/sarahjabado/cardinal) ![Master Branch Build](https://img.shields.io/github/checks-status/sarahjabado/cardinal/master) ![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/sarahjabado/cardinal/CodeQL/master?label=SCA)
  
![GitHub](https://img.shields.io/github/license/sarahjabado/cardinal) ![GitHub top language](https://img.shields.io/github/languages/top/sarahjabado/cardinal) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/sarahjabado/cardinal)

Live Development Stream: [![Twitch Status](https://img.shields.io/twitch/status/BlankFoxGirl)](https://twitch.tv/BlankFoxGirl)  
*Feel free to pop on, ask questions, and chat.*

#### Basic Setup.
1. `$ cd Core`.
1. `$ dotnet build`.
1. `$ cd ../Worker`.
1. `$ dotnet run`.

#### Running Unit Tests.
1. `$ cd CoreTest && dotnet test && cd ../`.

#### Generate Coverage.
1. `$ cd CoreTest && dotnet test --collect:"XPlat Code Coverage" && cd ../`.

#### Expected Features:
- Dockerfile for each Node.
- Compose.yaml file to instantiate development environment.
- Build scripts to control creating own images and publishing to docker.
- Terraform to create and maintain your own deployments.
- Cloudformation (An extension of the above.)
- Kubernetes configuration to deploy your own K8 pods.
- How to use guide.
- Tutorials.
- Verbose Development Documentation.
