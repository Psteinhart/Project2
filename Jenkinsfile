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
            dir('JenkinsMVC'){
                bat 'dotnet restore'
                bat 'msbuild build JenkinsMVC.csproj , /t:clean, '
            }

        } catch(error){
             //SlackSend message: color:'danger'
        }
    }

    stage ('Analyze'){
        try{

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