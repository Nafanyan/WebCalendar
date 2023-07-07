pipeline {
    agent any
    stages {
        stage('Tooling version') {
            steps {
                sh '''
                    docker --version
                    docker compose version
                '''
            }
        }
        stage('Build') {
            steps {
                sh 'docker context use default'
                sh 'docker compose build'
                sh 'docker compose push'
            }
        }
        stage('Deploy') {
            steps {
                sh 'docker compose up'
                sh 'docker compose ps --format json'
            }
        }
    }
}