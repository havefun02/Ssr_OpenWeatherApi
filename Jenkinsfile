pipeline {
    agent any
    
    environment {
        IMAGE_NAME="weather_app"
        IMAGE_TAG = "latest"
        DOCKER_REPO = "lapphan/demo_github_jenkins_docker"
        DOCKER_CREDENTIALS_ID = "docker-auth" // Jenkins credentials ID for Docker login
    }


    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

         stage('Build Docker Image') {
            steps {
                script {
                    echo "Start build image"

                    if (isUnix()) {
                       sh "docker build -t ${IMAGE_NAME}:${IMAGE_TAG} ."
                    } else {
                        bat "docker build -t ${IMAGE_NAME}:${IMAGE_TAG} ."
                    }
                }
            }
        }

        /*stage('Run Tests in Docker') {
            steps {
                script {
                    echo "Run test in docker"
                    if (isUnix()) {
                        sh "docker run --rm ${IMAGE_NAME}:${IMAGE_TAG} dotnet test --no-build --verbosity normal"
                    } else{
                        bat "docker run --rm ${IMAGE_NAME}:${IMAGE_TAG} dotnet test --no-build --verbosity normal"
                    }
                }
            }
        }*/
        stage('Push Docker Image to Registry') {
            steps {
                script {

                    echo "Push to docker hub"
                    withCredentials([usernamePassword(credentialsId: ${DOCKER_CREDENTIALS_ID}, 
                                              usernameVariable: 'DOCKER_USERNAME', 
                                              passwordVariable: 'DOCKER_PASSWORD')]) {
                        if (isUnix()) {
                            sh "echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin"
                            sh "docker tag ${IMAGE_NAME}:${IMAGE_TAG} ${DOCKER_REPO}/${IMAGE_NAME}:${IMAGE_TAG}"
                            sh "docker push ${DOCKER_REPO}/${IMAGE_NAME}:${IMAGE_TAG}"
                        } else {
                            bat "echo %DOCKER_PASSWORD% | docker login -u %DOCKER_USERNAME% --password-stdin"
                            bat "docker tag ${IMAGE_NAME}:${IMAGE_TAG} ${DOCKER_REPO}/${IMAGE_NAME}:${IMAGE_TAG}"
                            bat "docker push ${DOCKER_REPO}/${IMAGE_NAME}:${IMAGE_TAG}"
                        }
                    }
                }
            }
        }

        /*stage('Deploy') {
            steps {
                // Deploy the application (e.g., using SSH, Kubernetes, etc.)
                script {
                    // Example SSH deployment
                    sh ''
                    ssh user@yourserver "docker pull $DOCKER_IMAGE && docker stop yourcontainername || true && docker rm yourcontainername || true && docker run -d -p 80:80 --name yourcontainername $DOCKER_IMAGE"
                    ''
                }
            }
        }*/
    }

    post {
        always {
            // Always remove the Docker image locally after the pipeline finishes
            sh "docker rmi ${IMAGE_NAME}:${IMAGE_TAG} || true"
        }
        success {
            echo "Pipeline completed successfully!"
        }
        failure {
            echo "Pipeline failed!"
        }
    }
}
