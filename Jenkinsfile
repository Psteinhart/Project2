node('master'){
    stage('Import'){
        try{
            git url: 'https://github.com/Psteinhart/Project2.git'
           slackSend message:"started ${env.JOB_NAME} ${env.BUILD_NUMBER} (<${env.BUILD_URL}|Open>)", color:'danger'

        } catch (error){
            //SlackSend message: color:'danger'
        }
    }
    stage('Build'){
        try{
            dir('Chatbot'){
                bat 'dotnet restore'
                //bat 'msbuild /t:clean,build JenkinsMVC.csproj'
                bat 'dotnet build'
            }

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage ('Analyze'){
        try{
             dir('Chatbot'){
                bat 'C:\\Tools\\SonarQube\\SonarQube.Scanner.MSBuild.exe begin /k:jkinsmvc'
                //bat 'msbuild /t:build JenkinsMVC.csproj'
                bat 'dotnet build'
                bat 'C:\\Tools\\SonarQube\\SonarQube.Scanner.MSBuild.exe end'
            }


        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage('Test'){
        try{
            dir('JenkinsMVC.Test')
            {
                bat 'dotnet restore'
                bat 'msbuild /t:build JenkinsMVC.Test.csproj'
                bat 'dotnet test'
            }

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage('Package'){
        try{
            dir('JenkinsMVC')
            {
                bat 'dotnet publish JenkinsMVC.csproj --output ../Package'
                //bat 'msbuild /t:pack JenkinsMVC.csproj'
            }

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage('Deploy'){
        try{
           // bat 'dotnet build ./JenkinsMVC/JenkinsMVC.csproj /p:DeployOnBuild=true /p:PublishProfile=publish'
            bat '"C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:iisApp="C:\\Program Files (x86)\\Jenkins\\workspace\\jenkinops\\JenkinsMVC\\" -dest:iisApp="Default Web Site/jenkinops",computername=ec2-34-207-249-238.compute-1.amazonaws.com:8172/msdeploy.axd,username=Administrator,password=Pizza123 -allowuntrusted -enableRule:AppOffline'

        } catch(error){
            //SlackSend message: color:'danger'
        }
    }
}