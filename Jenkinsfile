node('master'){
    stage('Import'){
        try{
            git url: 'https://github.com/Psteinhart/Project2.git'
           slackSend "started ${env.JOB_NAME} ${env.BUILD_NUMBER} (<${env.BUILD_URL}|Open>)"

        } catch (error){
            //SlackSend message: color:'danger'
        }
    }
    stage('Build'){
        try{
            dir('JenkinsMVC'){
                bat 'dotnet restore'
                bat 'msbuild /t:clean,build JenkinsMVC.csproj'
               // bat 'dotnet build'
            }

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage ('Analyze'){
        try{
             dir('JenkinsMVC'){
                bat 'C:\\Tools\\SonarQube\\SonarQube.Scanner.MSBuild.exe begin /k:jkinsmvc'
                bat 'msbuild /t:build JenkinsMVC.csproj'
               // bat 'dotnet build'
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
            //bat 'msdeploy --verb:sync --sourcepath: --destpath:'

        } catch(error){
            //SlackSend message: color:'danger'
        }
    }
}