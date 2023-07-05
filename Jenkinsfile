// Global Variable goes here
// Pipeline block
pipeline {
// Agent block
agent { node { label 'Manage_Contact_Demo'}}
options {
buildDiscarder(logRotator(numToKeepStr: '30'))
timestamps()
}
....
....
// Stage Block
stages {// stage blocks
stage("Build docker images") {
steps {
script {
echo "Bulding docker images"
def buildArgs = """\
--build-arg HTTP_PROXY=${params.HTTP_PROXY} \
--build-arg HTTPS_PROXY=${params.HTTPS_PROXY} \
-f Dockerfile \
.
"""
docker.build(
"${params.Image_Name}:${params.Image_Tag}"
,
buildArgs)
}
}
}
}
}