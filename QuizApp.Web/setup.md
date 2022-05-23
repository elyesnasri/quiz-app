# Setup a new Project with .NET 6 and Vue.js using Vite

## Create the projects

- Create empty .NET Web Project
- Create a new ClientApp Folder and then run `npm create vite@latest`
  - `Vite` will ask you some question and then creates a new Vue.js App

## Update the project file `.csproj` to setup the Microsoft.AspNetCore.SpaProxy Package

Add the following lines to the `PropertyGroup` Section:

````csharp
<SpaRoot>ClientApp\</SpaRoot>
<SpaProxyServerUrl>https://localhost:44498</SpaProxyServerUrl>
<SpaProxyLaunchCommand>npm run dev</SpaProxyLaunchCommand>
````
> Note: the port 44498 must be the same in vite.config.ts

Install the `Microsoft.AspNetCore.SpaProxy` Nuget package

````csharp
<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.SpaProxy" Version="6.0.5" />
</ItemGroup>
````

Add this lines to define the steps to run vue project and how it will be published

````csharp
<ItemGroup>
    <!-- Don't publish the SPA source files, but do show them in the project files list -->
    <Content Remove="$(SpaRoot)**" />
    <None Remove="$(SpaRoot)**" />
    <None Include="$(SpaRoot)**" Exclude="$(SpaRoot)node_modules\**" />
    <None Remove="Controllers\" />
</ItemGroup>
<Target Name="DebugEnsureNodeEnv" BeforeTargets="Build" Condition=" '$(Configuration)' == 'Debug' And !Exists('$(SpaRoot)node_modules') ">
    <!-- Ensure Node.js is installed -->
    <Exec Command="node --version" ContinueOnError="true">
        <Output TaskParameter="ExitCode" PropertyName="ErrorCode" />
    </Exec>
    <Error Condition="'$(ErrorCode)' != '0'" Text="Node.js is required to build and run this project. To continue, please install Node.js from https://nodejs.org/, and then restart your command prompt or IDE." />
    <Message Importance="high" Text="Restoring dependencies using 'npm'. This may take several minutes..." />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm install" />
    <!--	Generate a new SSL certificate. command prestart ist defined in package.json	-->
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run prestart" />
</Target>
<Target Name="PublishRunWebpack" AfterTargets="ComputeFilesToPublish">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm install" />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build -- --prod" />
    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
        <DistFiles Include="$(SpaRoot)dist\**; $(SpaRoot)dist-server\**" />
        <ResolvedFileToPublish Include="@(DistFiles->'%(FullPath)')" Exclude="@(ResolvedFileToPublish)">
            <RelativePath>wwwroot\%(RecursiveDir)%(FileName)%(Extension)</RelativePath>
            <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
            <ExcludeFromSingleFile>true</ExcludeFromSingleFile>
        </ResolvedFileToPublish>
    </ItemGroup>
</Target>
````

## Create a new file in the ClientApp folder to generate the SSL certificate for the Vue.js project

````js
// This script sets up HTTPS for the application using the ASP.NET Core HTTPS certificate
const fs = require('fs');
const spawn = require('child_process').spawn;
const path = require('path');

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : process.env.npm_package_name;

if (!certificateName) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    spawn('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', })
        .on('exit', (code) => process.exit(code));
}
````

## Edit the `vite.config.ts` file

````typescript
import { fileURLToPath, URL } from 'url'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

const fs = require('fs');
const path = require('path');
const { env } = require('process');

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
// @ts-ignore
const certificateName = certificateArg ? certificateArg.groups.value : process.env.npm_package_name;

if (!certificateName) {
  console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
  process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:15190'; // the same port deinfed in launchSettings.json under iisExpress:applicationUrl

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 44498, // the same port defined in the .csproj file
    host: 'localhost',
    https: {
      cert: fs.readFileSync(certFilePath),
      key: fs.readFileSync(keyFilePath)
    },
    proxy: {
      '/weather': {
        target: target,
        secure: false
      },
    }
  }
})
````

in `package.json` add the following command under `scripts` to generate the SSL Certificates

````
"prestart": "node aspnetcore-https",
````

