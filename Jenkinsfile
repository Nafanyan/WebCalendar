// Global Variable goes here
// Pipeline block
pipeline {
// Agent block
agent {docker {
    image "nafanyan/backend:latest"
}}
stages {
    stage("test_build")
    {
        steps{
            echo "hello world"
        }
    }
}
}