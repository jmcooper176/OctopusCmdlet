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
    <img src="images/logo.png" alt="Logo" width="80" height="80">
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
2. Clone the repo
   ```sh
   git clone https://github.com/jmcooper176/OctopusCmdlet.git
   ```
3. Install .NET 9.0 SDK
   ```sh
   choco install dotnet-9.0-sdk
   ```
4. Change git remote url to avoid accidental pushes to base project
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

- [x] Add Changelog
- [x] Add back to top links
- [ ] Add Additional Templates w/ Examples
- [ ] Add "components" document to easily copy & paste sections of the readme
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