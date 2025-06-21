# README for OctopusCmdlet

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![BSD-3-Clause License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/jmcooper176/OctopusCmdlet">
    <img src="images/logo.png" alt="Logo" width="160" height="90">
  </a>

  <h3 align="center">README for OctopusCmdlet</h3>

  <p align="center">
    A project to re-imagine, re-design, and re-engineer 'Octopus-Cmdlets'
    <br />
    <a href="https://github.com/jmcooper176/OctopusCmdlet"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/jmcooper176/OctopusCmdlet/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    &middot;
    <a href="https://github.com/jmcooper176/OctopusCmdlet/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About OctopusCmdlet

[![OctopusCmdlet][product-screenshot]](https://github.com/jmcooper176/OctopusCmdlet/images/screenshot.png)

Since 2007, I have done plenty of work with Octopus Deploy.  Octopus-Cmdlets were interesting, but had some issues:

* The Octopus Client has not been kept up to date, which has resulted in some significant drift;
* Many of the Cmdlets tried to do too much;
* The Cmdlets did not really leverage the PowerShell pipeline well; AND
* While at least it had unit tests, I think they could be better, and segregate some tests as integration tests.

So, why not?

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

This section should list any major frameworks/libraries used to bootstrap your project. Leave any add-ons/plugins for the acknowledgements section. Here are a few examples.

* [Octopus Client][ODClient-NuGet-url]
* [PowerShell SDK][PowerShellSDK-NuGet-url]
* [PowerShell][PowerShell-Installers-url]
* [.NET 9.0 SDK][DotNet-SDK-Installers-url]
* [Octopus.Local][Octopus-Local-Docker-Compose-url]
* [Pester PowerShell Module][Pester-PowerShell-Gallery-url]
* [Installing Chocolatey][Chocolatey-Home-Install-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

How to install OctopusCmdlet in PowerShell.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* PowerShell (minimum version 7.2)

### Installation

_Below is an example of how you can instruct your audience on installing and setting up this PowerShell module. These instructions do not rely on any external dependencies or services._

1. Install PowerShell Core of at least version 7.2 or later.
   ```sh
   choco install powershell-core
   ```

2. Clone the repo
   ```sh
   git clone https://github.com/jmcooper176/OctopusCmdlet.git
   ```
3. Install .NET 9.0 SDK
   ```sh
   choco install dotnet-9.0-sdk
   ```

4. Setup a Local Octopus Deploy (follow the README.md at https://github.com/OctopusDeployLabs/octopus.local/blob/main/README.md)
   ```sh
   openssl req -x509 -out octopus.crt -keyout octopus.key -newkey rsa:2048 -nodes -sha256 -subj '/CN=octopus.local' -extensions EXT -config <(printf "[dn]\nCN=octopus.local\n[req]\ndistinguished_name = dn\n[EXT]\nsubjectAltName=DNS:octopus.local\nkeyUsage=digitalSignature\nextendedKeyUsage=serverAuth")

   predeploy.sh

   deploy.sh
   ```

5. Install latest stable Pester
   ```sh
   Install-Module -Name Pester -RequiredVersion 5.7.1
   ```

6. Change git remote url to avoid accidental pushes to base project
   ```sh
   git remote set-url origin github_username/repo_name
   git remote -v # confirm the changes
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Useful examples of how OctopusCmdlet can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [x] Add CHANGELOG.md
- [x] Add CODEOWNERS
- [x] Add CODE_OF_CONDUCT.md
- [x] Add LICENSE.txt and Licensing headers to all source code
- [x] Add PowerShell Linter and Pester Unit Test Workflow
- [x] Add README.md
- [x] Add RELEASENOTES.txt
- [x] Add SECURITY.md
- [x] Add stand-in logo.png
- [x] Add Issue-support code
- [ ] Implement all Infrastructure Cmdlets with Unit Tests (both VSTest and Pester)
- [ ] Implement New-Endpoint, New-Client, New-Repository, New-Client, Find-Space, and Get-Repository with Unit Tests (both VSTest and Pester)
  * [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
  * [ ] Implement New-Endpoint Cmdlet
  * [ ] Implement New-Repository Cmdlet
  * [ ] Implement New-Client Cmdlet
  * [ ] Implement Find-Space
  * [ ] Implement Get-Repository

- [ ] Implement Remaining Cmdlets (there will be subregions here)

    **ACTION**
  * [ ] ActionTemplate Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ActionUpdate Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **ARTIFACTS**
  * [ ] Artifact Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] BuildInformation Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **AUTHORIZATION/AUTHENTICATION**
  * [ ] Account Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Authentication Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Certificate Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DirectoryServices Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ExternalSecurityGroup Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Guest Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] IDConfiguration Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Identity Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] LDAP Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Oicd Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Okta Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] OpenID Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Scoped Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] SSH Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Token Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] UsernamePassword Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **BASIC**
  * [ ] ApiKey Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Client Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Endpoint Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Repository Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] SystemRepository Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Space Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **BUILTIN**
  * [ ] BuiltInFeed Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] BuiltInPackageRepository Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **CLOUD**
  * [ ] AWS Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Azure Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] GCP Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Kubernetes Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **CONFIGURATION**
  * [ ] Archive Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Configuration Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Database Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Features Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] License Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] OfflineDrop Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Performance Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Persistence Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Proxy Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Retention Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] SMTP Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Subscription Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] SystemInfo Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **DEPLOY**
  * [ ] AutoDeploy Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Deploy Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **DEPLOYMENT**
  * [ ] Deployment Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentAction Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentFreeze Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentPreview Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentProcess Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentSettings Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentStep Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] DeploymentTemplate Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **FAILURE ARTIFACTS**
  * [ ] Defect Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Interruption Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **FEED**
  * [ ] Docker Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Feed Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Helm Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Maven Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] NuGet Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] OciRegistry Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Package Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **MACHINE/TARGET**
  * [ ] Machine/Target Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Machine/Target Policy Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Machine/Target Role Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Tentacle/Target Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Upgrade Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **MAINTENANCE**
  * [ ] Backup Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Maintenance Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **MISC**
  * [ ] DocumentType Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Execution Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Migration Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Named Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Process Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] PropertyValue Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Server Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ServiceFabric Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ServiceNow Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] VersioningStragegy Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] XOption Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **PROJECT**
  * [ ] ProjectCmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ProjectFeed Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ProjectGroup Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ProjectTrigger Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **RELEASE**
  * [ ] Release Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] ReleaseTemplate Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **REPORT**
  * [ ] Report Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Summary Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **RUNBOOK**
  * [ ] Runbook Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] RunbookProcess Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] RunbookRun Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] RunbookSnapshot Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] RunbookTrigger Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Snapshot Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **SCHEDULE(R)/TASK**
  * [ ] ScheduleTask Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Scheduler Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Task Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Trigger Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **SECURITY**
  * [ ] Audited Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Event Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] HostProtection Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Telemetry Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **SOURCE CONTROL**
  * [ ] Git Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] GitHub Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **TAG**
  * [ ] Tag Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] TagSet Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **USER**
  * [ ] Invitation Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Team Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] User Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] UserInvite Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] UserPermission Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] UserRole Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] UserTeam Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **USER INTERFACE**
  * [ ] Dashboard Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Icon Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] WebPortal Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **VARIABLE**
  * [ ] LibraryVariableSet Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Variable Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] VariableSet Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **WORKER**
  * [ ] Worker Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] WorkerPool Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **WORK ITEM**
  * [ ] Jira Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

    **WORKFLOW**
  * [ ] Channel Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Commit Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] CommunityActionTemplate Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Environment Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] LifeCycle Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Phase Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Progression Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] StepPackage Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

  * [ ] Tenant Cmdlets
    + [ ] Implement VSTest and Pester Unit Tests and Integration Tests for all Cmdlets
    + [ ] Implement New- Cmdlet(s)
    + [ ] Implement Get-, Remove-, and Update- Cmdlets
    + [ ] Implement any other Cmdlets
    + [ ] Author MAML Help for each Cmdlet

- [ ] Circle back and refine code, unit tests, etc.

- [ ] Multi-language Support
    - [ ] German

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make OctopusCmdlet better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Do not forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Top contributors:

<a href="https://github.com/jmcooper176/OctopusCmdlet/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=jmcooper176/OctopusCmdlet" alt="contrib.rocks image" />
</a>

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the BSD-3-Clause License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

John Merryweather Cooper
- BlueSky [@jmerryweathercooper.bsky.social](https://bsky.app/profile/jmerryweathercooper.bsky.social)
- LinkedIn [johnmcooper8654](https://linkedin.com/in/johnmcooper8654)
- email [jmcooper8654@gmail.com](mailto:jmcooper8654@gmail.com)

### Project Link:

[https://github.com/jmcooper176/OctopusCmdlet](https://github.com/jmcooper176/OctopusCmdlet)

### Clone Link:

[https://github.com/jmcooper176/OctopusCmdlet.git](https://github.com/jmcooper176/OctopusCmdlet.git)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

Useful resources I find helpful.

* [Choose an Open Source License](https://choosealicense.com)
* [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
* [Img Shields](https://shields.io)
* [GitHub Pages](https://pages.github.com)
* [Required Development Guildlines](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/required-development-guidelines?view=powershell-7.5)
* [Compile-time logging source generation](https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator)
* [Octopus Deploy API/Octopus.Client/C#](https://github.com/OctopusDeploy/OctopusDeploy-Api/tree/master/Octopus.Client/Csharp)
* [Octopus.Client Documentation](https://octopus.com/docs/octopus-rest-api/octopus.client)
* [Octopus.Local](https://github.com/OctopusDeployLabs/octopus.local)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/jmcooper176/OctopusCmdlet.svg?style=for-the-badge
[contributors-url]: https://github.com/jmcooper176/OctopusCmdlet/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/jmcooper176/OctopusCmdlet.svg?style=for-the-badge
[forks-url]: https://github.com/jmcooper176/OctopusCmdlet/network/members
[stars-shield]: https://img.shields.io/github/stars/jmcooper176/OctopusCmdlet.svg?style=for-the-badge
[stars-url]: https://github.com/jmcooper176/OctopusCmdlet/stargazers
[issues-shield]: https://img.shields.io/github/issues/jmcooper176/OctopusCmdlet.svg?style=for-the-badge
[issues-url]: https://github.com/jmcooper176/OctopusCmdlet/issues
[license-shield]: https://img.shields.io/github/license/jmcooper176/OctopusCmdlet.svg?style=for-the-badge&name=BSD-3-Clause
[license-url]: https://github.com/jmcooper176/OctopusCmdlet/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/johnmcooper8654
[product-screenshot]: images/screenshot.png
[ODClient-NuGet-url]: https://www.nuget.org/packages/Octopus.Client
[PowerShellSDK-NuGet-url]: https://www.nuget.org/packages/Microsoft.PowerShell.SDK
[PowerShell-Installers-url]: https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows?view=powershell-7.5
[DotNet-SDK-Installers-url]: https://dotnet.microsoft.com/en-us/download/dotnet/9.0
[Octopus-Local-Docker-Compose-url]: https://github.com/OctopusDeployLabs/octopus.local/blob/main/docker-compose.yml
[Pester-PowerShell-Gallery-url]: https://www.powershellgallery.com/packages/Pester/5.7.1
[Chocolatey-Home-Install-url]: https://chocolatey.org/install