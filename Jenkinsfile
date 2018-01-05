node('master'){
    stage('Import'){
        try{
            git url: 'https://github.com/Psteinhart/Project2.git'

        } catch (error){
            //SlackSend message: color:'danger'
        }
    }
    stage('Build'){
        try{
            dir('chatbot'){
                bat 'nuget restore'
                //bat 'msbuild /t:clean,build JenkinsMVC.csproj'
               bat 'dotnet build'
            }

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage ('Analyze'){
        try{
             dir('chatbot'){
                bat 'C:\\Tools\\SonarQube\\SonarQube.Scanner.MSBuild.exe begin /k:jkinsmvc'
                //bat 'msbuild /t:build JenkinsMVC.csproj'
                bat 'nuget build'
                bat 'C:\\Tools\\SonarQube\\SonarQube.Scanner.MSBuild.exe end'
            }


        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage('Test'){
        try{

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage('Package'){
        try{

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage('Deploy'){
        try{

        } catch(error){
            //SlackSend message: color:'danger'
        }
    }
}